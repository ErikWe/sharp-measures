namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Operator_Multiply_IScalarQuantity_Unhandled3
{
    private static Unhandled3 Target(IScalarQuantity a, Unhandled3 b) => a * b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetNullScalarQuantity();
        var b = Datasets.GetValidUnhandled3();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled))]
    public void Valid_MatchMethod(Unhandled3 b, Unhandled a)
    {
        var expected = Unhandled3.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IScalarQuantity a, Unhandled3 b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
