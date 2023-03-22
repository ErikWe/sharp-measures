namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Multiply_Unhandled_Vector3
{
    private static Unhandled3 Target(Unhandled x, Vector3 y) => x * y;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector3))]
    public void MatchMethod(Unhandled x, Vector3 y)
    {
        var expected = Unhandled.Multiply(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
