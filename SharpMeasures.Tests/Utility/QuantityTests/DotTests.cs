namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class DotTests
{
    public static void Vector3_ShouldBeSumOfSquares<TQuantity, TProductQuantity, TFactorQuantity>(TQuantity a, TFactorQuantity b)
        where TQuantity : IDotableVector3Quantity<TProductQuantity, TFactorQuantity>
        where TProductQuantity : IScalarQuantity
        where TFactorQuantity : IVector3Quantity
    {
        TProductQuantity result = a.Dot(b);

        Assert.Equal(a.X * b.X + a.Y * b.Y + a.Z * b.Z, result.Magnitude, 2);
    }
}