namespace SharpMeasures.Tests.Vector4Cases;

using System;

using Xunit;

public class Multiply_TScalar
{
    private static (TScalar X, TScalar Y, TScalar Z, TScalar W) Target<TScalar>(Vector4 vector, TScalar factor) where TScalar : IScalarQuantity<TScalar> => vector.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidVector4();
        var factor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(vector, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void Valid_MatchXMultiply(Vector4 vector, Scalar factor)
    {
        var expected = vector.X.Multiply(factor);

        var actual = Target(vector, factor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void Valid_MatchYMultiply(Vector4 vector, Scalar factor)
    {
        var expected = vector.Y.Multiply(factor);

        var actual = Target(vector, factor).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void Valid_MatchZMultiply(Vector4 vector, Scalar factor)
    {
        var expected = vector.Z.Multiply(factor);

        var actual = Target(vector, factor).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidScalar))]
    public void Valid_MatchWMultiply(Vector4 vector, Scalar factor)
    {
        var expected = vector.W.Multiply(factor);

        var actual = Target(vector, factor).W;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Vector4 vector, TScalar factor) where TException : Exception where TScalar : IScalarQuantity<TScalar>
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
