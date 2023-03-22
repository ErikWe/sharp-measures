namespace SharpMeasures.Tests.Unhandled3Cases;

using System.Globalization;

using Xunit;

public class ToStringInvariant
{
    private static string Target(Unhandled3 vector) => vector.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchToStringWithInvariantCulture(Unhandled3 vector)
    {
        var expected = vector.ToString(CultureInfo.InvariantCulture);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
