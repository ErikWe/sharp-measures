namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Operator_Add_IScalarQuantity_Unhandled
{
    private static Unhandled Target(IScalarQuantity x, Unhandled y) => x + y;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var x = Datasets.GetNullScalarQuantity();
        var y = Datasets.GetValidUnhandled();

        AnyError_TException<ArgumentNullException>(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMethod(Unhandled y, Scalar x)
    {
        var expected = Unhandled.Add(y, x);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IScalarQuantity x, Unhandled y) where TException : Exception
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
