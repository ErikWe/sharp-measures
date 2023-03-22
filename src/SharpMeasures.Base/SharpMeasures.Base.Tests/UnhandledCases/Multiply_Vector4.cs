namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Vector4
{
    private static Unhandled4 Target(Unhandled unhandled, Vector4 factor) => unhandled.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector4))]
    public void MatchMultiply4(Unhandled unhandled, Vector4 factor)
    {
        var expected = unhandled.Multiply4(factor);

        var actual = Target(unhandled, factor);

        Assert.Equal(expected, actual);
    }
}
