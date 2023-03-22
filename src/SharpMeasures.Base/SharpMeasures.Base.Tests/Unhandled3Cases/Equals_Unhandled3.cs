namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Equals_Unhandled3
{
    private static bool Target(Unhandled3 vector, Unhandled3 other) => vector.Equals(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void Valid_MatchComponentsEquals(Unhandled3 vector, Unhandled3 other)
    {
        var expected = vector.Components.Equals(other.Components);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
