namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class NormalizeTests
{
    public static void ShouldBeLengthOne<TQuantity>(TQuantity a)
        where TQuantity : INormalizableVector3Quantity<TQuantity>
    {
        IVector3Quantity result = a.Normalize();

        Assert.Equal(1, result.Magnitude(), 2);
    }
}