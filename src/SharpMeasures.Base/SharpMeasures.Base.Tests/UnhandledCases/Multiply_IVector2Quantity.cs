namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply_IVector2Quantity
{
    private static Unhandled2 Target<TVector>(Unhandled unhandled, IVector2Quantity<TVector> factor) where TVector : IVector2Quantity<TVector> => unhandled.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();

        var factor = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector2Quantity>(unhandled, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector2))]
    public void MatchVector2Multiply(Unhandled unhandled, Vector2 factor)
    {
        var expected = unhandled.Multiply(factor);

        var actual = Target(unhandled, factor);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled unhandled, IVector2Quantity<TVector> factor) where TException : Exception where TVector : IVector2Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(unhandled, factor));

        Assert.IsType<TException>(exception);
    }
}
