namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Zero
{
    private static Unhandled3 Target() => Unhandled3.Zero;

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
}
