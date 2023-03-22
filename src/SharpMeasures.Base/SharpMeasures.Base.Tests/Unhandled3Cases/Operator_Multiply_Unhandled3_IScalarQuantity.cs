namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Operator_Multiply_Unhandled3_IScalarQuantity
{
    private static Unhandled3 Target(Unhandled3 a, IScalarQuantity b) => a * b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled3();
        var b = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled))]
    public void Valid_MatchMethod(Unhandled3 a, Unhandled b)
    {
        var expected = Unhandled3.Multiply(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(Unhandled3 a, IScalarQuantity b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
