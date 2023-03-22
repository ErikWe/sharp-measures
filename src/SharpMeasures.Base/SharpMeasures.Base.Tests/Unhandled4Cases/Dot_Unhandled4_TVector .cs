namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Dot_Unhandled4_TVector
{
    private static Unhandled Target<TVector>(Unhandled4 a, TVector b) where TVector : IVector4Quantity => Unhandled4.Dot(a, b);

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
        var expected = a.Dot(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled4 vector, TVector factor) where TException : Exception where TVector : IVector4Quantity
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
