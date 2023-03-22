namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Zero
{
    public static Scalar Target() => Scalar.Zero;

    [Fact]
    public void ToDoubleIsZero()
    {
        var actual = Target().ToDouble();

        Assert.Equal(0, actual);
    }
}
