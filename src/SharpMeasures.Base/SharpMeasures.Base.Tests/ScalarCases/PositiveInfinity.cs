namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class PositiveInfinity
{
    public static Scalar Target() => Scalar.PositiveInfinity;

    [Fact]
    public void ToDoubleIsPositiveInfinity()
    {
        var actual = Target().ToDouble();

        Assert.Equal(double.PositiveInfinity, actual);
    }
}
