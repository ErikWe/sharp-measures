namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class IsNaN
{
    private static bool Target(Unhandled3 vector) => vector.IsNaN;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchComponentsIsNaN(Unhandled3 vector)
    {
        var expected = vector.Components.IsNaN;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
