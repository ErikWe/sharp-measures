namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class SquaredMagnitude
{
    private static Unhandled Target(Unhandled2 vector) => vector.SquaredMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchComponentsSquaredMagnitude(Unhandled2 vector)
    {
        var expected = vector.Components.SquaredMagnitude();

        var actual = Target(vector).Magnitude;

        Assert.Equal(expected, actual);
    }
}
