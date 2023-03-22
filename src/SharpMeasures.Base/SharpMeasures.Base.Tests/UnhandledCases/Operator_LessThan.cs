﻿namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_LessThan
{
    private static bool Target(Unhandled lhs, Unhandled rhs) => lhs < rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void Valid_MatchMagnitudeLessThan(Unhandled lhs, Unhandled rhs)
    {
        var expected = lhs.Magnitude < rhs.Magnitude;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
