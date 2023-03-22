namespace SharpMeasures.Tests.Vector3Cases;

using System;

using Xunit;

public class AsIEquatable_Equals_Vector3
{
    private static bool Target(Vector3 vector, Vector3 other)
    {
        return execute(vector);

        bool execute(IEquatable<Vector3> equatable) => equatable.Equals(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void Valid_MatchVector3Equals(Vector3 vector, Vector3 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
