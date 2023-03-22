namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Floor
{
    private static Scalar Target(Scalar scalar) => scalar.Floor();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchSystemFloor(Scalar scalar)
    {
        var expected = Math.Floor(scalar);

        var actual = Target(scalar).ToDouble();

        Assert.Equal(expected, actual);
    }
}
