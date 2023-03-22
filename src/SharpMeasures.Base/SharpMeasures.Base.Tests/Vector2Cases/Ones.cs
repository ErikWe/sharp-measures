namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Ones
{
    private static Vector2 Target() => Vector2.Ones;

    [Fact]
    public void XIsOne()
    {
        var actual = Target().X;

        Assert.Equal(1, actual);
    }

    [Fact]
    public void YIsOne()
    {
        var actual = Target().Y;

        Assert.Equal(1, actual);
    }
}
