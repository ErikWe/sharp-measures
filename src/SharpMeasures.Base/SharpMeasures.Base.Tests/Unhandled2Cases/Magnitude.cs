namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Magnitude
{
    private static Unhandled Target(Unhandled2 vector) => vector.Magnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchComponentsMagnitude(Unhandled2 vector)
    {
        var expected = vector.Components.Magnitude();

        var actual = Target(vector).Magnitude;

        Assert.Equal(expected, actual);
    }
}
