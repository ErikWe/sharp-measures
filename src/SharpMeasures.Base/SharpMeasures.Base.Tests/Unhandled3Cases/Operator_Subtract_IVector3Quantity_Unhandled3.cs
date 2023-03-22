namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Operator_Subtract_IVector3Quantity_Unhandled3
{
    private static Unhandled3 Target(IVector3Quantity a, Unhandled3 b) => a - b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetNullVector3Quantity();
        var b = Datasets.GetValidUnhandled3();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void Valid_MatchMethod(Unhandled3 a, Unhandled3 b)
    {
        var expected = Unhandled3.Subtract(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IVector3Quantity a, Unhandled3 b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
