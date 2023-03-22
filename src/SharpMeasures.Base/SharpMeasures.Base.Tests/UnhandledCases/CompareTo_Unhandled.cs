namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class CompareTo_Unhandled
{
    private static int Target(Unhandled unhangled, Unhandled other) => unhangled.CompareTo(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void Valid_MatchSignOfMagnitudeCompareTo(Unhandled unhandled, Unhandled other)
    {
        var expected = Math.Sign(unhandled.Magnitude.CompareTo(other.Magnitude));

        var actual = Math.Sign(Target(unhandled, other));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void Valid_OneOfAllowedValues(Unhandled unhandled, Unhandled other)
    {
        var allowed = new[] { 1, 0, -1 };

        var actual = Target(unhandled, other);

        Assert.Contains(actual, allowed);
    }
}
