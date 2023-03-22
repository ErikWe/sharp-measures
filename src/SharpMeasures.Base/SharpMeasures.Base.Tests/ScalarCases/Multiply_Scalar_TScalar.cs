namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Multiply_Scalar_TScalar
{
    private static TScalar Target<TScalar>(Scalar x, TScalar y) where TScalar : IScalarQuantity<TScalar> => Scalar.Multiply(x, y);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var scalar = Datasets.GetValidScalar();

        var factor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(scalar, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchInstanceMethod(Scalar x, Scalar y)
    {
        var expected = x.Multiply<Scalar>(y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Scalar x, TScalar y) where TException : Exception where TScalar : IScalarQuantity<TScalar>
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
