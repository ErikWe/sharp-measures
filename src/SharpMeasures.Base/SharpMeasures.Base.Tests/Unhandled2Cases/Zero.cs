namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Zero
{
    private static Unhandled2 Target() => Unhandled2.Zero;

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
}
