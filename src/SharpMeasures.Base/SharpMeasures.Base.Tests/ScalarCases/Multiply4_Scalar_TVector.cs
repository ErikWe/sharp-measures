namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Multiply4_Scalar_TVector
{
    private static TVector Target<TVector>(Scalar a, TVector b) where TVector : IVector4Quantity<TVector> => Scalar.Multiply4(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidScalar();

        var b = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector4Quantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidVector4))]
    public void MatchInstanceMethod(Scalar a, Vector4 b)
    {
        var expected = a.Multiply4(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Scalar a, TVector b) where TException : Exception where TVector : IVector4Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
