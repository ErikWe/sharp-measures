namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Operator_Multiply_IScalarQuantity_Unhandled4
{
    private static Unhandled4 Target(IScalarQuantity a, Unhandled4 b) => a * b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetNullScalarQuantity();
        var b = Datasets.GetValidUnhandled4();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled))]
    public void Valid_MatchMethod(Unhandled4 b, Unhandled a)
    {
        var expected = Unhandled4.Multiply(b, a);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IScalarQuantity a, Unhandled4 b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
