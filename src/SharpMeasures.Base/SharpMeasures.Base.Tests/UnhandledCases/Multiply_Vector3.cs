namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Vector3
{
    private static Unhandled3 Target(Unhandled unhandled, Vector3 factor) => unhandled.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector3))]
    public void MatchMultiply2(Unhandled unhandled, Vector3 factor)
    {
        var expected = unhandled.Multiply3(factor);

        var actual = Target(unhandled, factor);

        Assert.Equal(expected, actual);
    }
}
