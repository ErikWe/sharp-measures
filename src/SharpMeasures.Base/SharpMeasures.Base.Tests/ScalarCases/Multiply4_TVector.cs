namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Multiply4_TVector
{
    private static TVector Target<TVector>(Scalar scalar, TVector factor) where TVector : IVector4Quantity<TVector> => scalar.Multiply4(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var scalar = Datasets.GetValidScalar();

        var factor = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector4Quantity>(scalar, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector4))]
    public void MatchComponents(Scalar scalar, Vector4 factor)
    {
        Vector4 expected = (scalar * factor.X, scalar * factor.Y, scalar * factor.Z, scalar * factor.W);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Scalar scalar, TVector factor) where TException : Exception where TVector : IVector4Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(scalar, factor));

        Assert.IsType<TException>(exception);
    }
}
