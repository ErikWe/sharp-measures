namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Multiply_IVector3Quantity
{
    private static TVector Target<TVector>(Scalar scalar, IVector3Quantity<TVector> factor) where TVector : IVector3Quantity<TVector> => scalar.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var scalar = Datasets.GetValidScalar();

        var factor = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(scalar, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector3))]
    public void MatchVector3Multiply(Scalar scalar, Vector3 factor)
    {
        var expected = scalar.Multiply(factor);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Scalar scalar, IVector3Quantity<TVector> factor) where TException : Exception where TVector : IVector3Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(scalar, factor));

        Assert.IsType<TException>(exception);
    }
}
