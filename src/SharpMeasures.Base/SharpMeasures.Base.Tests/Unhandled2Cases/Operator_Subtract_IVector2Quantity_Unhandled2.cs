namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class Operator_Subtract_IVector2Quantity_Unhandled2
{
    private static Unhandled2 Target(IVector2Quantity a, Unhandled2 b) => a - b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetNullVector2Quantity();
        var b = Datasets.GetValidUnhandled2();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void Valid_MatchMethod(Unhandled2 a, Unhandled2 b)
    {
        var expected = Unhandled2.Subtract(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IVector2Quantity a, Unhandled2 b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
