namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply3_TVector
{
    private static Unhandled3 Target<TVector>(Unhandled unhandled, TVector factor) where TVector : IVector3Quantity<TVector> => unhandled.Multiply3(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();

        var factor = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(unhandled, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector3))]
    public void MatchMagnitudeMultiply(Unhandled unhandled, Vector3 factor)
    {
        var expected = unhandled.Magnitude.Multiply(factor);

        var actual = Target(unhandled, factor).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled unhandled, TVector factor) where TException : Exception where TVector : IVector3Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(unhandled, factor));

        Assert.IsType<TException>(exception);
    }
}
