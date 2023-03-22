namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Operator_Subtract_IVector4Quantity_Unhandled4
{
    private static Unhandled4 Target(IVector4Quantity a, Unhandled4 b) => a - b;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var a = Datasets.GetNullVector4Quantity();
        var b = Datasets.GetValidUnhandled4();

        AnyError_TException<ArgumentNullException>(a, b);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void Valid_MatchMethod(Unhandled4 a, Unhandled4 b)
    {
        var expected = Unhandled4.Subtract(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IVector4Quantity a, Unhandled4 b) where TException : Exception
    {
        var exception = Record.Exception(() => Target(a, b));

        Assert.IsType<TException>(exception);
    }
}
