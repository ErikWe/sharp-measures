﻿namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Divide_Unhandled_Unhandled
{
    private static Unhandled Target(Unhandled x, Unhandled y) => x / y;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void MatchMethod(Unhandled x, Unhandled y)
    {
        var expected = Unhandled.Divide(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
