#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MagnitudeTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularVelocity3Dataset))]
    public void Components_MagnitudesShouldBeEqual(OrbitalAngularVelocity3 quantity)
    {
        Vector3 components = quantity.Components;

        Utility.AssertExtra.AssertEqualComponents(quantity, components.ToValueTuple());
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularVelocity3Dataset))]
    public void Magnitude_ShouldBeSquareRootOfSumOfSquares(OrbitalAngularVelocity3 quantity)
    {
        OrbitalAngularSpeed magnitude = quantity.Magnitude();

        Assert.Equal(Math.Sqrt(quantity.X * quantity.X + quantity.Y * quantity.Y + quantity.Z * quantity.Z), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularVelocity3Dataset))]
    public void Magnitude_Explicit_ShouldBeSquareRootOfSumOfSquares(OrbitalAngularVelocity3 quantity)
    {
        Scalar magnitude = ((IVector3Quantity)quantity).Magnitude();

        Assert.Equal(Math.Sqrt(quantity.X * quantity.X + quantity.Y * quantity.Y + quantity.Z * quantity.Z), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularVelocity3Dataset))]
    public void SquaredMagnitude_Explicit_ShouldBeSumOfSquares(OrbitalAngularVelocity3 quantity)
    {
        Scalar squaredMagnitude = ((IVector3Quantity)quantity).SquaredMagnitude();

        Assert.Equal(quantity.X * quantity.X + quantity.Y * quantity.Y + quantity.Z * quantity.Z, squaredMagnitude.Magnitude, 2);
    }
}
