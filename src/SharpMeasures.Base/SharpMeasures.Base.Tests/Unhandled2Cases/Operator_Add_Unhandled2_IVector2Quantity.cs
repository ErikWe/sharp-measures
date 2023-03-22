namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class Operator_Add_Unhandled2_IVector2Quantity
{
    private static Unhandled2 Target(Unhandled2 a, IVector2Quantity b) => a + b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled2();
        var b = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void Valid_MatchMethod(Unhandled2 a, Unhandled2 b)
    {
        var expected = Unhandled2.Add(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(Unhandled2 a, IVector2Quantity b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
