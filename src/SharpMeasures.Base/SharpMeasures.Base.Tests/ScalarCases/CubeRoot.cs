namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class CubeRoot
{
    private static Scalar Target(Scalar scalar) => scalar.CubeRoot();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchSystemCbrt(Scalar scalar)
    {
        var expected = Math.Cbrt(scalar);

        var actual = Target(scalar).ToDouble();

        Assert.Equal(expected, actual);
    }
}
