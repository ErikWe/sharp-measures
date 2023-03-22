namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Negate
{
    private static Unhandled3 Target(Unhandled3 vector) => vector.Negate();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchNegatedComponents(Unhandled3 vector)
    {
        var expected = vector.Components.Negate();

        var actual = Target(vector).Components;

        Assert.Equal(expected, actual);
    }
}
