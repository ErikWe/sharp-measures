namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class IsInfinite
{
    private static bool Target(Unhandled4 vector) => vector.IsInfinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchComponentsIsInfinite(Unhandled4 vector)
    {
        var expected = vector.Components.IsInfinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
