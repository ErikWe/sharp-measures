namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class IsZero
{
    private static bool Target(Unhandled3 vector) => vector.IsZero;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchComponentsIsZero(Unhandled3 vector)
    {
        var expected = vector.Components.IsZero;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
