namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Sign
{
    private static int Target(Unhandled unhandled) => unhandled.Sign();

    [Theory]
    [ClassData(typeof(Datasets.ValidExceptNaNUnhandled))]
    public void NotNaN_MatchMagnitudeSign(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.Sign();

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NaN_ArithmeticException()
    {
        var unhandled = Datasets.GetNaNUnhandled();

        AnyError_TException<ArithmeticException>(unhandled);
    }

    private static void AnyError_TException<TException>(Unhandled unhandled) where TException : Exception
    {
        var exception = Record.Exception(() => Target(unhandled));

        Assert.IsType<TException>(exception);
    }
}
