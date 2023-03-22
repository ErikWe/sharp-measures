namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class Dot_Unhandled2_TVector
{
    private static Unhandled Target<TVector>(Unhandled2 a, TVector b) where TVector : IVector2Quantity => Unhandled2.Dot(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled2();
        var b = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector2Quantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void MatchInstanceMethod(Unhandled2 a, Unhandled2 b)
    {
        var expected = a.Dot(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled2 vector, TVector factor) where TException : Exception where TVector : IVector2Quantity
    {
        var exception = Record.Exception(() => Target(vector, factor));

        Assert.IsType<TException>(exception);
    }
}
