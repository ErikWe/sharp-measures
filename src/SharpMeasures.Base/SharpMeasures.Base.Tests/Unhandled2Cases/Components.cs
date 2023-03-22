namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Components
{
    private static Vector2 Target(Unhandled2 vector) => vector.Components;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchXMagnitude(Unhandled2 vector)
    {
        var expected = vector.X.Magnitude;

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchYMagnitude(Unhandled2 vector)
    {
        var expected = vector.Y.Magnitude;

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }
}
