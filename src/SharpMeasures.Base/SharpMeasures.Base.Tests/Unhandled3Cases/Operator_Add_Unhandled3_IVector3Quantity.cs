namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Operator_Add_Unhandled3_IVector3Quantity
{
    private static Unhandled3 Target(Unhandled3 a, IVector3Quantity b) => a + b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled3();
        var b = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void Valid_MatchMethod(Unhandled3 a, Unhandled3 b)
    {
        var expected = Unhandled3.Add(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(Unhandled3 a, IVector3Quantity b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
