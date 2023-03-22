namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Magnitude
{
    private static Unhandled Target(Unhandled4 vector) => vector.Magnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchComponentsMagnitude(Unhandled4 vector)
    {
        var expected = vector.Components.Magnitude();

        var actual = Target(vector).Magnitude;

        Assert.Equal(expected, actual);
    }
}
