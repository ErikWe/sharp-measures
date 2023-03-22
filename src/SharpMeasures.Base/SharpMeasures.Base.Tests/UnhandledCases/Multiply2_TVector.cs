namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply2_TVector
{
    private static Unhandled2 Target<TVector>(Unhandled unhandled, TVector factor) where TVector : IVector2Quantity<TVector> => unhandled.Multiply2(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();

        var factor = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector2Quantity>(unhandled, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector2))]
    public void MatchMagnitudeMultiply(Unhandled unhandled, Vector2 factor)
    {
        var expected = unhandled.Magnitude.Multiply(factor);

        var actual = Target(unhandled, factor).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled unhandled, TVector factor) where TException : Exception where TVector : IVector2Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(unhandled, factor));

        Assert.IsType<TException>(exception);
    }
}
