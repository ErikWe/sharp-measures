namespace SharpMeasures.Tests.Vector4Cases;

using System;

using Xunit;

public class AsIEquatable_Equals_Vector4
{
    private static bool Target(Vector4 vector, Vector4 other)
    {
        return execute(vector);

        bool execute(IEquatable<Vector4> equatable) => equatable.Equals(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void Valid_MatchVector4Equals(Vector4 vector, Vector4 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
