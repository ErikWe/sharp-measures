namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Dot_Unhandled3_TVector
{
    private static Unhandled Target<TVector>(Unhandled3 a, TVector b) where TVector : IVector3Quantity => Unhandled3.Dot(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled3();
        var b = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void MatchInstanceMethod(Unhandled3 a, Unhandled3 b)
    {
        var expected = a.Dot(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled3 vector, TVector factor) where TException : Exception where TVector : IVector3Quantity
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
