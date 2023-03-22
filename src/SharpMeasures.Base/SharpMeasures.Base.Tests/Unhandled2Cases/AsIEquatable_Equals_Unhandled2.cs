namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class AsIEquatable_Equals_Unhandled2
{
    private static bool Target(Unhandled2 vector, Unhandled2 other)
    {
        return execute(vector);

        bool execute(IEquatable<Unhandled2> equatable) => equatable.Equals(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void Valid_MatchUnhandled2Equals(Unhandled2 vector, Unhandled2 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
