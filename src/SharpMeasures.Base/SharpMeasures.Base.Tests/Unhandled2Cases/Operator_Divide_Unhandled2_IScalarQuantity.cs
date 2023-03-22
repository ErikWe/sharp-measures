namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class Operator_Divide_Unhandled2_IScalarQuantity
{
    private static Unhandled2 Target(Unhandled2 a, IScalarQuantity b) => a / b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled2();
        var b = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled))]
    public void Valid_MatchMethod(Unhandled2 a, Unhandled b)
    {
        var expected = Unhandled2.Divide(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(Unhandled2 a, IScalarQuantity b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
