namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Negate
{
    private static Unhandled4 Target(Unhandled4 vector) => vector.Negate();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchNegatedComponents(Unhandled4 vector)
    {
        var expected = vector.Components.Negate();

        var actual = Target(vector).Components;

        Assert.Equal(expected, actual);
    }
}
