#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Position3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MagnitudeTests
{
    [Theory]
    [ClassData(typeof(Position3Dataset))]
    public void Individual_MagnitudesShouldBeSameAsOriginal(Position3 quantity)
    {
        Assert.Equal(quantity.MagnitudeX, quantity.X.Magnitude, 2);
        Assert.Equal(quantity.MagnitudeY, quantity.Y.Magnitude, 2);
        Assert.Equal(quantity.MagnitudeZ, quantity.Z.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Position3Dataset))]
    public void Components_MagnitudesShouldBeEqual(Position3 quantity)
    {
        Vector3 components = quantity.Components;

        Utility.AssertExtra.AssertEqualComponents(quantity, components.ToValueTuple());
    }

    [Theory]
    [ClassData(typeof(Position3Dataset))]
    public void Magnitude_ShouldBeSquareRootOfSumOfSquares(Position3 quantity)
    {
        Distance magnitude = quantity.Magnitude();

        Assert.Equal(Math.Sqrt(quantity.MagnitudeX * quantity.MagnitudeX + quantity.MagnitudeY * quantity.MagnitudeY + quantity.MagnitudeZ * quantity.MagnitudeZ), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Position3Dataset))]
    public void SquaredMagnitude_ShouldBeSumOfSquares(Position3 quantity)
    {
        Area squaredMagnitude = quantity.SquaredMagnitude();

        Assert.Equal(quantity.MagnitudeX * quantity.MagnitudeX + quantity.MagnitudeY * quantity.MagnitudeY + quantity.MagnitudeZ * quantity.MagnitudeZ, squaredMagnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Position3Dataset))]
    public void Magnitude_Explicit_ShouldBeSquareRootOfSumOfSquares(Position3 quantity)
    {
        Scalar magnitude = ((IVector3Quantity)quantity).Magnitude();

        Assert.Equal(Math.Sqrt(quantity.MagnitudeX * quantity.MagnitudeX + quantity.MagnitudeY * quantity.MagnitudeY + quantity.MagnitudeZ * quantity.MagnitudeZ), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Position3Dataset))]
    public void SquaredMagnitude_Explicit_ShouldBeSumOfSquares(Position3 quantity)
    {
        Scalar squaredMagnitude = ((IVector3Quantity)quantity).SquaredMagnitude();

        Assert.Equal(quantity.MagnitudeX * quantity.MagnitudeX + quantity.MagnitudeY * quantity.MagnitudeY + quantity.MagnitudeZ * quantity.MagnitudeZ, squaredMagnitude.Magnitude, 2);
    }
}
