namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class SquaredMagnitude
{
    private static Unhandled Target(Unhandled4 vector) => vector.SquaredMagnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchComponentsSquaredMagnitude(Unhandled4 vector)
    {
        var expected = vector.Components.SquaredMagnitude();

        var actual = Target(vector).Magnitude;

        Assert.Equal(expected, actual);
    }
}
