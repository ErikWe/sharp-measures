namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class GetHashCode
{
    private static int Target(Unhandled unhandled) => unhandled.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void Equal_Match(Unhandled unhandled)
    {
        Unhandled other = new(unhandled.Magnitude);

        var expected = Target(other);

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
