#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Jerk3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MagnitudeTests
{
    [Theory]
    [ClassData(typeof(Jerk3Dataset))]
    public void Individual_MagnitudesShouldBeSameAsOriginal(Jerk3 quantity)
    {
        Assert.Equal(quantity.MagnitudeX, quantity.X.Magnitude, 2);
        Assert.Equal(quantity.MagnitudeY, quantity.Y.Magnitude, 2);
        Assert.Equal(quantity.MagnitudeZ, quantity.Z.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Jerk3Dataset))]
    public void Components_MagnitudesShouldBeEqual(Jerk3 quantity)
    {
        Vector3 components = quantity.Components;

        Utility.AssertExtra.AssertEqualComponents(quantity, components.ToValueTuple());
    }

    [Theory]
    [ClassData(typeof(Jerk3Dataset))]
    public void Magnitude_ShouldBeSquareRootOfSumOfSquares(Jerk3 quantity)
    {
        Jerk magnitude = quantity.Magnitude();

        Assert.Equal(Math.Sqrt(quantity.MagnitudeX * quantity.MagnitudeX + quantity.MagnitudeY * quantity.MagnitudeY + quantity.MagnitudeZ * quantity.MagnitudeZ), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Jerk3Dataset))]
    public void Magnitude_Explicit_ShouldBeSquareRootOfSumOfSquares(Jerk3 quantity)
    {
        Scalar magnitude = ((IVector3Quantity)quantity).Magnitude();

        Assert.Equal(Math.Sqrt(quantity.MagnitudeX * quantity.MagnitudeX + quantity.MagnitudeY * quantity.MagnitudeY + quantity.MagnitudeZ * quantity.MagnitudeZ), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Jerk3Dataset))]
    public void SquaredMagnitude_Explicit_ShouldBeSumOfSquares(Jerk3 quantity)
    {
        Scalar squaredMagnitude = ((IVector3Quantity)quantity).SquaredMagnitude();

        Assert.Equal(quantity.MagnitudeX * quantity.MagnitudeX + quantity.MagnitudeY * quantity.MagnitudeY + quantity.MagnitudeZ * quantity.MagnitudeZ, squaredMagnitude.Magnitude, 2);
    }
}
