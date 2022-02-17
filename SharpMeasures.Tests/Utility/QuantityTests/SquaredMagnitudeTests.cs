namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using Xunit;

public static class SquaredMagnitudeTests
{
    public static void Scalar_ShouldBeSumOfSquares(IVector3Quantity a)
    {
        Scalar result = a.SquaredMagnitude();

        Assert.Equal(a.X * a.X + a.Y * a.Y + a.Z * a.Z, result.Magnitude, 2);
    }

    public static void SquaredComponent_ShouldBeSumOfSquares(IVector3Quantity a, IScalarQuantity result)
    {
        Assert.Equal(a.X * a.X + a.Y * a.Y + a.Z * a.Z, result.Magnitude, 2);
    }
}