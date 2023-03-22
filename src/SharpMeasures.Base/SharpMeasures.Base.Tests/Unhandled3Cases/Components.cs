namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Components
{
    private static Vector3 Target(Unhandled3 vector) => vector.Components;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchXMagnitude(Unhandled3 vector)
    {
        var expected = vector.X.Magnitude;

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchYMagnitude(Unhandled3 vector)
    {
        var expected = vector.Y.Magnitude;

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchZMagnitude(Unhandled3 vector)
    {
        var expected = vector.Z.Magnitude;

        var actual = Target(vector).Z;

        Assert.Equal(expected, actual);
    }
}
