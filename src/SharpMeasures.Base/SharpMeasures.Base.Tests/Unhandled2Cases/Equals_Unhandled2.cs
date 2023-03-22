namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Equals_Unhandled2
{
    private static bool Target(Unhandled2 vector, Unhandled2 other) => vector.Equals(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void Valid_MatchComponentsEquals(Unhandled2 vector, Unhandled2 other)
    {
        var expected = vector.Components.Equals(other.Components);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
