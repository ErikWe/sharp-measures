namespace SharpMeasures.Tests.UnhandledCases;

using System.Globalization;

using Xunit;

public class ToStringInvariant
{
    private static string Target(Unhandled unhandled) => unhandled.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void MatchToStringWithInvariantCulture(Unhandled unhandled)
    {
        var expected = unhandled.ToString(CultureInfo.InvariantCulture);

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
