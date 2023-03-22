namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Multiply3_TVector
{
    private static TVector Target<TVector>(Scalar scalar, TVector factor) where TVector : IVector3Quantity<TVector> => scalar.Multiply3(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var scalar = Datasets.GetValidScalar();

        var factor = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(scalar, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector3))]
    public void MatchComponents(Scalar scalar, Vector3 factor)
    {
        Vector3 expected = (scalar * factor.X, scalar * factor.Y, scalar * factor.Z);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Scalar scalar, TVector factor) where TException : Exception where TVector : IVector3Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(scalar, factor));

        Assert.IsType<TException>(exception);
    }
}
