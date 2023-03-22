namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Equals_Unhandled4
{
    private static bool Target(Unhandled4 vector, Unhandled4 other) => vector.Equals(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void Valid_MatchComponentsEquals(Unhandled4 vector, Unhandled4 other)
    {
        var expected = vector.Components.Equals(other.Components);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
