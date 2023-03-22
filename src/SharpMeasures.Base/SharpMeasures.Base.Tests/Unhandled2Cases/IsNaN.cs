namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class IsNaN
{
    private static bool Target(Unhandled2 vector) => vector.IsNaN;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchComponentsIsNaN(Unhandled2 vector)
    {
        var expected = vector.Components.IsNaN;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
