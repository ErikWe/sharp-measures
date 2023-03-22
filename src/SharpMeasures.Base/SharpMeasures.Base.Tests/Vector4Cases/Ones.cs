namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Ones
{
    private static Vector4 Target() => Vector4.Ones;

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

    [Fact]
    public void ZIsOne()
    {
        var actual = Target().Z;

        Assert.Equal(1, actual);
    }

    [Fact]
    public void WIsOne()
    {
        var actual = Target().W;

        Assert.Equal(1, actual);
    }
}
