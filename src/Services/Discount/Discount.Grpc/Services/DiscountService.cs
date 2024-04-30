namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);
        coupon ??= new Coupon { ProductName = "No Discount", Amount=0, Description="No discount description" };
        string msgLog = $"Discount is retrieved for {request.ProductName}, Amount: {coupon.Amount}";
        logger.LogInformation(msgLog);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if(coupon is null) throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid request object."));
         dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        string msgLog = $"Discount is successfully created. ProductName :  {coupon.ProductName}";
        logger.LogInformation(msgLog);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel ?? new CouponModel { ProductName = "No Discount", Amount = 0, Description = "No discount description" }; 
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.Coupon.ProductName);
        if (coupon is null) throw new RpcException(new Status(StatusCode.NotFound, "Discount not found."));
        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();
        string msgLog = $"Discount is successfully deleted. ProductName :  {coupon.ProductName}";
        logger.LogInformation(msgLog);
        return new DeleteDiscountResponse {  Success = true };
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        string msgLog = $"Discount is successfully updated. ProductName :  {coupon.ProductName}";
        logger.LogInformation(msgLog);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel ?? new CouponModel { ProductName = "No Discount", Amount = 0, Description = "No discount description" };
    }
}
