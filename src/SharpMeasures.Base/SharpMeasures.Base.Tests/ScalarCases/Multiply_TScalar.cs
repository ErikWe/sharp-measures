namespace SharpMeasures.Tests.ScalarCases;

using SharpMeasures.Tests;

using System;

using Xunit;

public class Multiply_TScalar
{
    private static TScalar Target<TScalar>(Scalar scalar, TScalar factor) where TScalar : IScalarQuantity<TScalar> => scalar.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var scalar = Datasets.GetValidScalar();

        var factor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(scalar, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchScalarMultiply(Scalar scalar, Scalar factor)
    {
        var expected = scalar.Multiply(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Scalar scalar, TScalar factor) where TException : Exception where TScalar : IScalarQuantity<TScalar>
    {
        var exception = Record.Exception(() => Target(scalar, factor));

        Assert.IsType<TException>(exception);
    }
}
