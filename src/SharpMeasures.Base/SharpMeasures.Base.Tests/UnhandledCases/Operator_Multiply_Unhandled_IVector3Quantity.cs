namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Operator_Multiply_Unhandled_IVector3Quantity
{
    private static Unhandled3 Target(Unhandled x, IVector3Quantity y) => x * y;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var x = Datasets.GetValidUnhandled();
        var y = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException>(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector3))]
    public void MatchMethod(Unhandled x, Vector3 y)
    {
        var expected = Unhandled.Multiply(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(Unhandled x, IVector3Quantity y) where TException : Exception
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
