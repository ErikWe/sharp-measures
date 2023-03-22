namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply4_Unhandled_TVector
{
    private static Unhandled4 Target<TVector>(Unhandled a, TVector b) where TVector : IVector4Quantity<TVector> => Unhandled.Multiply4(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled();

        var b = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector4Quantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector4))]
    public void MatchInstanceMethod(Unhandled a, Vector4 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled a, TVector b) where TException : Exception where TVector : IVector4Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
