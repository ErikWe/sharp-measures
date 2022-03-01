namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class SquaredMagnitudeTests
{
    public static void Scalar_ShouldBeSumOfSquares(IVector3Quantity a)
    {
        Scalar result = a.SquaredMagnitude();

        Assert.Equal(a.MagnitudeX * a.MagnitudeX + a.MagnitudeY * a.MagnitudeY + a.MagnitudeZ * a.MagnitudeZ, result.Magnitude, 2);
    }

    public static void SquaredComponent_ShouldBeSumOfSquares(IVector3Quantity a, IScalarQuantity result)
    {
        Assert.Equal(a.MagnitudeX * a.MagnitudeX + a.MagnitudeY * a.MagnitudeY + a.MagnitudeZ * a.MagnitudeZ, result.Magnitude, 2);
    }
}