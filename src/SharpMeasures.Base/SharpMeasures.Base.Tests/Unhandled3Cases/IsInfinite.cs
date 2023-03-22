namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class IsInfinite
{
    private static bool Target(Unhandled3 vector) => vector.IsInfinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchComponentsIsInfinite(Unhandled3 vector)
    {
        var expected = vector.Components.IsInfinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
