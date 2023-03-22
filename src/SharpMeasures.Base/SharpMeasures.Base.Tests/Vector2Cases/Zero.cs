namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Zero
{
    private static Vector2 Target() => Vector2.Zero;

    [Fact]
    public void XIsZero()
    {
        var actual = Target().X;

        Assert.Equal(0, actual);
    }

    [Fact]
    public void YIsZero()
    {
        var actual = Target().Y;

        Assert.Equal(0, actual);
    }
}
