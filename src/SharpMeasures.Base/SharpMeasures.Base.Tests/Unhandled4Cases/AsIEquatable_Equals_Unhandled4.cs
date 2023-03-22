namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class AsIEquatable_Equals_Unhandled4
{
    private static bool Target(Unhandled4 vector, Unhandled4 other)
    {
        return execute(vector);

        bool execute(IEquatable<Unhandled4> equatable) => equatable.Equals(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void Valid_MatchUnhandled2Equals(Unhandled4 vector, Unhandled4 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
