namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Multiply_IVector3Quantity
{
    private static Unhandled3 Target<TVector>(Unhandled unhandled, IVector3Quantity<TVector> factor) where TVector : IVector3Quantity<TVector> => unhandled.Multiply(factor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();

        var factor = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(unhandled, factor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector3))]
    public void MatchVector3Multiply(Unhandled unhandled, Vector3 factor)
    {
        var expected = unhandled.Multiply(factor);

        var actual = Target(unhandled, factor);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled unhandled, IVector3Quantity<TVector> factor) where TException : Exception where TVector : IVector3Quantity<TVector>
    {
        var exception = Record.Exception(() => Target(unhandled, factor));

        Assert.IsType<TException>(exception);
    }
}
