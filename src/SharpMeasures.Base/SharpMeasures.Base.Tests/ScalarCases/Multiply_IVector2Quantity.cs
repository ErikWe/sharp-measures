namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Multiply_IVector2Quantity
{
    private static TVector Target<TVector>(Scalar scalar, IVector2Quantity<TVector> factor) where TVector : IVector2Quantity<TVector> => scalar.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var scalar = Datasets.GetValidScalar();

        var factor = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector2Quantity>(scalar, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector2))]
    public void MatchVector2Multiply(Scalar scalar, Vector2 factor)
    {
        var expected = scalar.Multiply(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Scalar scalar, IVector2Quantity<TVector> factor) where TException : Exception where TVector : IVector2Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(scalar, factor));

        Assert.IsType<TException>(exception);
    }
}
