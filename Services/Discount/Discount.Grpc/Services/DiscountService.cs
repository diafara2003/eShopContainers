using Discount.Grpc.Data;
using Discount.Grpc.Moldes;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName : {productName}", coupon.ProductName);

        var couponModel= coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken);

        if (coupon is  null)
            throw new RpcException(new Status(StatusCode.NotFound, "Doscount not found"));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount id delete for ProductName : {productName}", request.ProductName);

        return new DeleteDiscountResponse() { Success = true };
    }
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken);

        if (coupon is null)
            coupon = new Moldes.Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount", Id = 0 };

        var couponModel = coupon.Adapt<CouponModel>();

        logger.LogInformation("Discount id retrived for ProductName : {productName}", request.ProductName);

        return couponModel;
    }
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated. ProductName : {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }
}
