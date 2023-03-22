namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Operator_Divide_Unhandled4_IScalarQuantity
{
    private static Unhandled4 Target(Unhandled4 a, IScalarQuantity b) => a / b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetValidUnhandled4();
        var b = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled))]
    public void Valid_MatchMethod(Unhandled4 a, Unhandled b)
    {
        var expected = Unhandled4.Divide(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(Unhandled4 a, IScalarQuantity b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
