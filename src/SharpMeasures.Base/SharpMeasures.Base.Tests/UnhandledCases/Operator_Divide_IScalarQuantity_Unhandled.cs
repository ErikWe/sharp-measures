namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Operator_Divide_IScalarQuantity_Unhandled
{
    private static Unhandled Target(IScalarQuantity x, Unhandled y) => x / y;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var x = Datasets.GetNullScalarQuantity();
        var y = Datasets.GetValidUnhandled();

        AnyError_TException<ArgumentNullException>(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchArithmetic(Unhandled y, Scalar x)
    {
        var expected = x / y.Magnitude;

        var actual = Target(x, y).Magnitude;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IScalarQuantity x, Unhandled y) where TException : Exception
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
