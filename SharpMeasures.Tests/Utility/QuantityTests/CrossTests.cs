namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class CrossTests
{
    public static void ShouldMatchDefinition<TQuantity, TProductQuantity, TFactorQuantity>(TQuantity a, TFactorQuantity b)
        where TQuantity : ICrossableVector3Quantity<TProductQuantity, TFactorQuantity>
        where TProductQuantity : IVector3Quantity
        where TFactorQuantity : IVector3Quantity
    {
        TProductQuantity result = a.Cross(b);

        Assert.Equal(a.Y * b.Z - a.Z * b.Y, result.X, 2);
        Assert.Equal(a.Z * b.X - a.X * b.Z, result.Y, 2);
        Assert.Equal(a.X * b.Y - a.Y * b.X, result.Z, 2);
    }
}