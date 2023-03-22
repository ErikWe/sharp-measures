namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class IsFinite
{
    private static bool Target(Unhandled3 vector) => vector.IsFinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchComponentsIsFinite(Unhandled3 vector)
    {
        var expected = vector.Components.IsFinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
