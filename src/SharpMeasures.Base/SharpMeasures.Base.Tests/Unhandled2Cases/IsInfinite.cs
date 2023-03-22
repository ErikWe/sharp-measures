namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class IsInfinite
{
    private static bool Target(Unhandled2 vector) => vector.IsInfinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchComponentsIsInfinite(Unhandled2 vector)
    {
        var expected = vector.Components.IsInfinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
