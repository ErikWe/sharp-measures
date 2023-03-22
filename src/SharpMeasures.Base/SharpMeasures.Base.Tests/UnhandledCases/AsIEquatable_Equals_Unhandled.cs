namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class AsIEquatable_Equals_Unhandled
{
    private static bool Target(Unhandled unhandled, Unhandled other)
    {
        return execute(unhandled);

        bool execute(IEquatable<Unhandled> equatable) => equatable.Equals(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void Valid_MatchUnhandledEquals(Unhandled unhandled, Unhandled other)
    {
        var expected = unhandled.Equals(other);

        var actual = Target(unhandled, other);

        Assert.Equal(expected, actual);
    }
}
