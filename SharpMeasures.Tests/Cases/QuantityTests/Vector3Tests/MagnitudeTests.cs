namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Vector3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MagnitudeTests
{
    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Magnitude_ShouldBeSquareRootOfSumOfSquares(Vector3 vector)
    {
        Scalar magnitude = vector.Magnitude();

        Assert.Equal(Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z), magnitude.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void SquaredMagnitude_ShouldBeSumOfSquares(Vector3 vector)
    {
        Scalar squaredMagnitude = vector.SquaredMagnitude();

        Assert.Equal(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z, squaredMagnitude.Magnitude, 2);
    }
}
