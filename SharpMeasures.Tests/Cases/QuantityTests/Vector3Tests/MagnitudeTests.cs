namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Vector3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MagnitudeTests
{
    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Individual_MagnitudesShouldBeSameAsOriginal(Vector3 vector)
    {
        Assert.Equal(vector.MagnitudeX, vector.X.Magnitude, 2);
        Assert.Equal(vector.MagnitudeY, vector.Y.Magnitude, 2);
        Assert.Equal(vector.MagnitudeZ, vector.Z.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Magnitude_ShouldBeSquareRootOfSumOfSquares(Vector3 vector)
    {
        Scalar magnitude = vector.Magnitude();

        Assert.Equal(Math.Sqrt(vector.MagnitudeX * vector.MagnitudeX + vector.MagnitudeY * vector.MagnitudeY + vector.MagnitudeZ * vector.MagnitudeZ), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void SquaredMagnitude_ShouldBeSumOfSquares(Vector3 vector)
    {
        Scalar squaredMagnitude = vector.SquaredMagnitude();

        Assert.Equal(vector.MagnitudeX * vector.MagnitudeX + vector.MagnitudeY * vector.MagnitudeY + vector.MagnitudeZ * vector.MagnitudeZ, squaredMagnitude.Magnitude, 2);
    }
}
