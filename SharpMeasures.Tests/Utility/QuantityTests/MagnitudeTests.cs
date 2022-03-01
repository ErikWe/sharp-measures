namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests;

using ErikWe.SharpMeasures.Quantities;

using System;

using Xunit;

public static class MagnitudeTests
{
    public static void Scalar_ShouldBeSquareRootOfSumOfSquares(IVector3Quantity a)
    {
        Scalar result = a.Magnitude();

        Assert.Equal(Math.Sqrt(a.MagnitudeX * a.MagnitudeX + a.MagnitudeY * a.MagnitudeY + a.MagnitudeZ * a.MagnitudeZ), result.Magnitude, 2);
    }

    public static void Component_ShouldBeSquareRootOfSumOfSquares(IVector3Quantity a, IScalarQuantity result)
    {
        Assert.Equal(Math.Sqrt(a.MagnitudeX * a.MagnitudeX + a.MagnitudeY * a.MagnitudeY + a.MagnitudeZ * a.MagnitudeZ), result.Magnitude, 2);
    }
}