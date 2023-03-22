namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class IsNaN
{
    private static bool Target(Unhandled4 vector) => vector.IsNaN;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchComponentsIsNaN(Unhandled4 vector)
    {
        var expected = vector.Components.IsNaN;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
