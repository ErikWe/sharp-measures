namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Multiply_Unhandled_Vector4
{
    private static Unhandled4 Target(Unhandled a, Vector4 b) => Unhandled.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector4))]
    public void MatchInstanceMethod(Unhandled a, Vector4 b)
    {
        var expected = a.Multiply(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
