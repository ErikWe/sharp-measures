namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply4_TVector
{
    private static Unhandled4 Target<TVector>(Unhandled unhandled, TVector factor) where TVector : IVector4Quantity<TVector> => unhandled.Multiply4(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();

        var factor = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector4Quantity>(unhandled, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector4))]
    public void MatchMagnitudeMultiply(Unhandled unhandled, Vector4 factor)
    {
        var expected = unhandled.Magnitude.Multiply(factor);

        var actual = Target(unhandled, factor).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled unhandled, TVector factor) where TException : Exception where TVector : IVector4Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(unhandled, factor));

        Assert.IsType<TException>(exception);
    }
}
