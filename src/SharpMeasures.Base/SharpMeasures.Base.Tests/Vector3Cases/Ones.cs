namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Ones
{
    private static Vector3 Target() => Vector3.Ones;

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
}
