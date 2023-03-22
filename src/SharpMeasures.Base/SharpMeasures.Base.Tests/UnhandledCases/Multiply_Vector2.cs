namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Vector2
{
    private static Unhandled2 Target(Unhandled unhandled, Vector2 factor) => unhandled.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector2))]
    public void MatchMultiply2(Unhandled unhandled, Vector2 factor)
    {
        var expected = unhandled.Multiply2(factor);

        var actual = Target(unhandled, factor);

        Assert.Equal(expected, actual);
    }
}
