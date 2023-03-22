namespace SharpMeasures.Tests.Unhandled4Cases;

using System.Globalization;

using Xunit;

public class ToStringInvariant
{
    private static string Target(Unhandled4 vector) => vector.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchToStringWithInvariantCulture(Unhandled4 vector)
    {
        var expected = vector.ToString(CultureInfo.InvariantCulture);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
