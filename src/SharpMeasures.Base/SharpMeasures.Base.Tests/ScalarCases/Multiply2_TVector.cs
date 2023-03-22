namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Multiply2_TVector
{
    private static TVector Target<TVector>(Scalar scalar, TVector factor) where TVector : IVector2Quantity<TVector> => scalar.Multiply2(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var scalar = Datasets.GetValidScalar();

        var factor = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector2Quantity>(scalar, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector2))]
    public void MatchComponents(Scalar scalar, Vector2 factor)
    {
        Vector2 expected = (scalar * factor.X, scalar * factor.Y);

        var actual = Target(scalar, factor);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Scalar scalar, TVector factor) where TException : Exception where TVector : IVector2Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(scalar, factor));

        Assert.IsType<TException>(exception);
    }
}
