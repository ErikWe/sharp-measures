namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class NormalizeTests
{
    public static void ShouldBeLengthOne<TQuantity>(TQuantity a)
        where TQuantity : INormalizableVector3Quantity<TQuantity>
    {
        TQuantity result = a.Normalize();

        if (!a.Magnitude().IsNaN && !a.Magnitude().IsInfinite)
        {
            Assert.Equal(1, result.Magnitude(), 2);
        }
    }

    public static void ComponentRatioShouldBePreserved<TQuantity>(TQuantity a)
        where TQuantity : INormalizableVector3Quantity<TQuantity>
    {
        TQuantity normalized = a.Normalize();

        Assert.Equal(a.MagnitudeX / a.Magnitude().Magnitude, normalized.MagnitudeX, 2);
        Assert.Equal(a.MagnitudeY / a.Magnitude().Magnitude, normalized.MagnitudeY, 2);
        Assert.Equal(a.MagnitudeZ / a.Magnitude().Magnitude, normalized.MagnitudeZ, 2);
    }
}