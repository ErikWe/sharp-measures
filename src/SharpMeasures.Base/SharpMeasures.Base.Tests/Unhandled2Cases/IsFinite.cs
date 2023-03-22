namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class IsFinite
{
    private static bool Target(Unhandled2 vector) => vector.IsFinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchComponentsIsFinite(Unhandled2 vector)
    {
        var expected = vector.Components.IsFinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
