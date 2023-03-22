namespace SharpMeasures.Tests.Unhandled2Cases;

using System.Globalization;

using Xunit;

public class ToStringInvariant
{
    private static string Target(Unhandled2 vector) => vector.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchToStringWithInvariantCulture(Unhandled2 vector)
    {
        var expected = vector.ToString(CultureInfo.InvariantCulture);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
