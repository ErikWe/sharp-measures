namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class Operator_Multiply_IScalarQuantity_Unhandled2
{
    private static Unhandled2 Target(IScalarQuantity a, Unhandled2 b) => a * b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetNullScalarQuantity();
        var b = Datasets.GetValidUnhandled2();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled))]
    public void Valid_MatchMethod(Unhandled2 b, Unhandled a)
    {
        var expected = Unhandled2.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IScalarQuantity a, Unhandled2 b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
