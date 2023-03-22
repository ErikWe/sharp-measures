namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply_Unhandled_IVector2Quantity
{
    private static Unhandled2 Target<TVector>(Unhandled a, IVector2Quantity<TVector> b) where TVector : IVector2Quantity<TVector> => Unhandled.Multiply(a, b);

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

    private static void AnyError_TException<TException, TVector>(Unhandled a, IVector2Quantity<TVector> b) where TException : Exception where TVector : IVector2Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
