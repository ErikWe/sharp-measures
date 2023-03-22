namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class One
{
    public static Scalar Target() => Scalar.One;

    [Fact]
    public void ToDoubleIsOne()
    {
        var actual = Target().ToDouble();

        Assert.Equal(1, actual);
    }
}
