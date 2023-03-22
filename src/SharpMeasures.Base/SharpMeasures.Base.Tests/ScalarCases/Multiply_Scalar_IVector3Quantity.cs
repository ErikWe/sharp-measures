namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Multiply_Scalar_IVector3Quantity
{
    private static TVector Target<TVector>(Scalar a, IVector3Quantity<TVector> b) where TVector : IVector3Quantity<TVector> => Scalar.Multiply(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidScalar();

        var b = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector3))]
    public void MatchInstanceMethod(Scalar a, Vector3 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Scalar a, IVector3Quantity<TVector> b) where TException : Exception where TVector : IVector3Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
