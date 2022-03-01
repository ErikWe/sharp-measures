#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Displacement3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MagnitudeTests
{
    [Theory]
    [ClassData(typeof(Displacement3Dataset))]
    public void Components_MagnitudesShouldBeEqual(Displacement3 quantity)
    {
        Vector3 components = quantity.Components;

        Utility.AssertExtra.AssertEqualComponents(quantity, components.ToValueTuple());
    }

    [Theory]
    [ClassData(typeof(Displacement3Dataset))]
    public void Magnitude_ShouldBeSquareRootOfSumOfSquares(Displacement3 quantity)
    {
        Length magnitude = quantity.Magnitude();

        Assert.Equal(Math.Sqrt(quantity.X * quantity.X + quantity.Y * quantity.Y + quantity.Z * quantity.Z), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Displacement3Dataset))]
    public void SquaredMagnitude_ShouldBeSumOfSquares(Displacement3 quantity)
    {
        Area squaredMagnitude = quantity.SquaredMagnitude();

        Assert.Equal(quantity.X * quantity.X + quantity.Y * quantity.Y + quantity.Z * quantity.Z, squaredMagnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Displacement3Dataset))]
    public void Magnitude_Explicit_ShouldBeSquareRootOfSumOfSquares(Displacement3 quantity)
    {
        Scalar magnitude = ((IVector3Quantity)quantity).Magnitude();

        Assert.Equal(Math.Sqrt(quantity.X * quantity.X + quantity.Y * quantity.Y + quantity.Z * quantity.Z), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Displacement3Dataset))]
    public void SquaredMagnitude_Explicit_ShouldBeSumOfSquares(Displacement3 quantity)
    {
        Scalar squaredMagnitude = ((IVector3Quantity)quantity).SquaredMagnitude();

        Assert.Equal(quantity.X * quantity.X + quantity.Y * quantity.Y + quantity.Z * quantity.Z, squaredMagnitude.Magnitude, 2);
    }
}
