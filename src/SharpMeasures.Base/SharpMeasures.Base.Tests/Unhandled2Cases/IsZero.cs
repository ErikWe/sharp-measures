namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class IsZero
{
    private static bool Target(Unhandled2 vector) => vector.IsZero;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchComponentsIsZero(Unhandled2 vector)
    {
        var expected = vector.Components.IsZero;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
