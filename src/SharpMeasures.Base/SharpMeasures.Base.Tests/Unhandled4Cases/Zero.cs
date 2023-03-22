namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Zero
{
    private static Unhandled4 Target() => Unhandled4.Zero;

    [Fact]
    public void XIsZero()
    {
        var actual = Target().X;

        Assert.Equal(Unhandled.Zero, actual);
    }

    [Fact]
    public void YIsZero()
    {
        var actual = Target().Y;

        Assert.Equal(Unhandled.Zero, actual);
    }

    [Fact]
    public void ZIsZero()
    {
        var actual = Target().Z;

        Assert.Equal(Unhandled.Zero, actual);
    }

    [Fact]
    public void WIsZero()
    {
        var actual = Target().W;

        Assert.Equal(Unhandled.Zero, actual);
    }
}
