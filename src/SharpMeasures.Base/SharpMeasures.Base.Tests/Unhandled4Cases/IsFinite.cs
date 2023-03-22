namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class IsFinite
{
    private static bool Target(Unhandled4 vector) => vector.IsFinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchComponentsIsFinite(Unhandled4 vector)
    {
        var expected = vector.Components.IsFinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
