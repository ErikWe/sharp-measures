namespace SharpMeasures.Tests.Vector2Cases;

using System;

using Xunit;

public class AsIEquatable_Equals_Vector2
{
    private static bool Target(Vector2 vector, Vector2 other)
    {
        return execute(vector);

        bool execute(IEquatable<Vector2> equatable) => equatable.Equals(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void Valid_MatchVector2Equals(Vector2 vector, Vector2 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
