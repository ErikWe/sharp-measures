namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply_TScalar
{
    private static Unhandled Target<TScalar>(Unhandled unhandled, TScalar factor) where TScalar : IScalarQuantity => unhandled.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();
        var factor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(unhandled, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMagnitudeMultiply(Unhandled unhandled, Scalar factor)
    {
        var expected = unhandled.Magnitude.Multiply(factor);

        var actual = Target(unhandled, factor).Magnitude;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled unhandled, TScalar factor) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(unhandled, factor));

        Assert.IsType<TException>(exception);
    }
}
