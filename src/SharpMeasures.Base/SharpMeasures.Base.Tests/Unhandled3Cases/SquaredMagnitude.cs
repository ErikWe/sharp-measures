namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class SquaredMagnitude
{
    private static Unhandled Target(Unhandled3 vector) => vector.SquaredMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchComponentsSquaredMagnitude(Unhandled3 vector)
    {
        var expected = vector.Components.SquaredMagnitude();

        var actual = Target(vector).Magnitude;

        Assert.Equal(expected, actual);
    }
}
