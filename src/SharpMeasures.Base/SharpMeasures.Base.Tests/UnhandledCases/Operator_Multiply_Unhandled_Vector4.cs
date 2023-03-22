namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Multiply_Unhandled_Vector4
{
    private static Unhandled4 Target(Unhandled x, Vector4 y) => x * y;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector4))]
    public void MatchMethod(Unhandled x, Vector4 y)
    {
        var expected = Unhandled.Multiply(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
