namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Multiply_IVector4Quantity
{
    private static TVector Target<TVector>(Scalar scalar, IVector4Quantity<TVector> factor) where TVector : IVector4Quantity<TVector> => scalar.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var scalar = Datasets.GetValidScalar();

        var factor = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector4Quantity>(scalar, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector4))]
    public void MatchVector4Multiply(Scalar scalar, Vector4 factor)
    {
        var expected = scalar.Multiply(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Scalar scalar, IVector4Quantity<TVector> factor) where TException : Exception where TVector : IVector4Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(scalar, factor));

        Assert.IsType<TException>(exception);
    }
}
