namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Operator_Multiply_IVector4Quantity_Unhandled
{
    private static Unhandled4 Target(IVector4Quantity x, Unhandled y) => x * y;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var x = Datasets.GetNullVector4Quantity();
        var y = Datasets.GetValidUnhandled();

        AnyError_TException<ArgumentNullException>(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector4))]
    public void MatchMethod(Unhandled y, Vector4 x)
    {
        var expected = Unhandled.Multiply(y, x);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IVector4Quantity x, Unhandled y) where TException : Exception
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
