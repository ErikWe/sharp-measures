namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class IsZero
{
    private static bool Target(Unhandled4 vector) => vector.IsZero;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchComponentsIsZero(Unhandled4 vector)
    {
        var expected = vector.Components.IsZero;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
