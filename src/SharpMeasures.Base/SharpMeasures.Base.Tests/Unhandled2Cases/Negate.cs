namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Negate
{
    private static Unhandled2 Target(Unhandled2 vector) => vector.Negate();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchNegatedComponents(Unhandled2 vector)
    {
        var expected = vector.Components.Negate();

        var actual = Target(vector).Components;

        Assert.Equal(expected, actual);
    }
}
