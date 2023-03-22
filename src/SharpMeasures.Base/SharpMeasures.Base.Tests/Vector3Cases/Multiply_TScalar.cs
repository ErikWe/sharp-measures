namespace SharpMeasures.Tests.Vector3Cases;

using System;

using Xunit;

public class Multiply_TScalar
{
    private static (TScalar X, TScalar Y, TScalar Z) Target<TScalar>(Vector3 vector, TScalar factor) where TScalar : IScalarQuantity<TScalar> => vector.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidVector3();
        var factor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(vector, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void Valid_MatchXMultiply(Vector3 vector, Scalar factor)
    {
        var expected = vector.X.Multiply(factor);

        var actual = Target(vector, factor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void Valid_MatchYMultiply(Vector3 vector, Scalar factor)
    {
        var expected = vector.Y.Multiply(factor);

        var actual = Target(vector, factor).Y;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Vector3 vector, TScalar factor) where TException : Exception where TScalar : IScalarQuantity<TScalar>
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
