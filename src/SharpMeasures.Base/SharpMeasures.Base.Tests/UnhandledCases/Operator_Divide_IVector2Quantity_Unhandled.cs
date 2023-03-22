namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Operator_Divide_IVector2Quantity_Unhandled
{
    private static Unhandled2 Target(IVector2Quantity x, Unhandled y) => x / y;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var x = Datasets.GetNullVector2Quantity();
        var y = Datasets.GetValidUnhandled();

        AnyError_TException<ArgumentNullException>(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector2))]
    public void MatchMethod(Unhandled y, Vector2 x)
    {
        var expected = Vector2.Divide(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IVector2Quantity x, Unhandled y) where TException : Exception
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
