namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Operator_Multiply_Unhandled_IVector2Quantity
{
    private static Unhandled2 Target(Unhandled x, IVector2Quantity y) => x * y;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var x = Datasets.GetValidUnhandled();
        var y = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException>(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector2))]
    public void MatchMethod(Unhandled x, Vector2 y)
    {
        var expected = Unhandled.Multiply(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(Unhandled x, IVector2Quantity y) where TException : Exception
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
