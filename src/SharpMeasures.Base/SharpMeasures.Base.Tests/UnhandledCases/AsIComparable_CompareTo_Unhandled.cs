namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class AsIComparable_CompareTo_Unhandled
{
    private static int Target(Unhandled unhangled, Unhandled other)
    {
        return execute(unhangled);

        int execute(IComparable<Unhandled> comparable) => comparable.CompareTo(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void Valid_MatchUnhandledCompareTo(Unhandled unhandled, Unhandled other)
    {
        var expected = unhandled.CompareTo(other);

        var actual = Target(unhandled, other);

        Assert.Equal(expected, actual);
    }
}
