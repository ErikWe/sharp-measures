﻿namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Operator_Negate
{
    private static Unhandled3 Target(Unhandled3 vector) => -vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void MatchMethod(Unhandled3 vector)
    {
        var expected = vector.Negate();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
