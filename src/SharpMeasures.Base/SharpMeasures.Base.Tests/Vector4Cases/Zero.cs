namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Zero
{
    private static Vector4 Target() => Vector4.Zero;

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

    [Fact]
    public void ZIsZero()
    {
        var actual = Target().Z;

        Assert.Equal(0, actual);
    }

    [Fact]
    public void WIsZero()
    {
        var actual = Target().W;

        Assert.Equal(0, actual);
    }
}
