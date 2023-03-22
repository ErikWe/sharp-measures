namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class DivideBy_TScalar
{
    private static Unhandled Target<TScalar>(Unhandled unhandled, TScalar divisor) where TScalar : IScalarQuantity => unhandled.DivideBy(divisor);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var unhandled = Datasets.GetValidUnhandled();
        var divisor = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException, ReferenceScalarQuantity>(unhandled, divisor);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMagnitudeDivideBy(Unhandled unhandled, Scalar divisor)
    {
        var expected = unhandled.Magnitude.DivideBy(divisor);

        var actual = Target(unhandled, divisor).Magnitude;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TScalar>(Unhandled unhandled, TScalar divisor) where TException : Exception where TScalar : IScalarQuantity
    {
        var exception = Record.Exception(() => Target(unhandled, divisor));

        Assert.IsType<TException>(exception);
    }
}
