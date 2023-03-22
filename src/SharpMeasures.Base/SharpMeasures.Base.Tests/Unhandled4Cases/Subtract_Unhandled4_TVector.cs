namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Subtract_Unhandled4_TVector
{
    private static Unhandled4 Target<TVector>(Unhandled4 a, TVector b) where TVector : IVector4Quantity => Unhandled4.Subtract(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled4();
        var b = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector4Quantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void MatchInstanceMethod(Unhandled4 a, Unhandled4 b)
    {
        var expected = a.Subtract(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled4 a, TVector b) where TException : Exception where TVector : IVector4Quantity
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
