namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply2_Unhandled_TVector
{
    private static Unhandled2 Target<TVector>(Unhandled a, TVector b) where TVector : IVector2Quantity<TVector> => Unhandled.Multiply2(a, b);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled();

        var b = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector2Quantity>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector2))]
    public void MatchInstanceMethod(Unhandled a, Vector2 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled a, TVector b) where TException : Exception where TVector : IVector2Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
