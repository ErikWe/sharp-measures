namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class AsIEquatable_Equals_Unhandled3
{
    private static bool Target(Unhandled3 vector, Unhandled3 other)
    {
        return execute(vector);

        bool execute(IEquatable<Unhandled3> equatable) => equatable.Equals(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void Valid_MatchUnhandled2Equals(Unhandled3 vector, Unhandled3 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
