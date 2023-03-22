namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Magnitude
{
    private static Unhandled Target(Unhandled3 vector) => vector.Magnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchComponentsMagnitude(Unhandled3 vector)
    {
        var expected = vector.Components.Magnitude();

        var actual = Target(vector).Magnitude;

        Assert.Equal(expected, actual);
    }
}
