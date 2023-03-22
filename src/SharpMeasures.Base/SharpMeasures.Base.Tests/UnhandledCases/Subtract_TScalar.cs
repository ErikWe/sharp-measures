namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Subtract_TScalar
{
    private static Unhandled Target<TScalar>(Unhandled unhandled, TScalar subtrahend) where TScalar : IScalarQuantity => unhandled.Subtract(subtrahend);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();
        var subtrahend = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(unhandled, subtrahend);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMagnitudeSubtract(Unhandled unhandled, Unhandled subtrahend)
    {
        var expected = unhandled.Magnitude.Subtract(subtrahend.Magnitude);

        var actual = Target(unhandled, subtrahend).Magnitude;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled unhandled, TScalar subtrahend) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(unhandled, subtrahend));

        Assert.IsType<TException>(exception);
    }
}
