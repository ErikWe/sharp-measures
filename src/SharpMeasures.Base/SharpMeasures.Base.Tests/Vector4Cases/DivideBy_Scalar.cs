﻿namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class DivideBy_Scalar
{
    private static Vector4 Target(Vector4 vector, Scalar divisor) => vector.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchXDivideBy(Vector4 vector, Scalar divisor)
    {
        var expected = vector.X.DivideBy(divisor);

        var actual = Target(vector, divisor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchYDivideBy(Vector4 vector, Scalar divisor)
    {
        var expected = vector.Y.DivideBy(divisor);

        var actual = Target(vector, divisor).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchZDivideBy(Vector4 vector, Scalar divisor)
    {
        var expected = vector.Z.DivideBy(divisor);

        var actual = Target(vector, divisor).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void MatchWDivideBy(Vector4 vector, Scalar divisor)
    {
        var expected = vector.W.DivideBy(divisor);

        var actual = Target(vector, divisor).W;

        Assert.Equal(expected, actual);
    }
}
