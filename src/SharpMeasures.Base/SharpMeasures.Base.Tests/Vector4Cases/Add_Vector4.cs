namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Add_Vector4
{
    private static Vector4 Target(Vector4 vector, Vector4 addend) => vector.Add(addend);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchXAdd(Vector4 vector, Vector4 addend)
    {
        var expected = vector.X.Add(addend.X);

        var actual = Target(vector, addend).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchYAdd(Vector4 vector, Vector4 addend)
    {
        var expected = vector.Y.Add(addend.Y);

        var actual = Target(vector, addend).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchZAdd(Vector4 vector, Vector4 addend)
    {
        var expected = vector.Z.Add(addend.Z);

        var actual = Target(vector, addend).Z;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void MatchWAdd(Vector4 vector, Vector4 addend)
    {
        var expected = vector.W.Add(addend.W);

        var actual = Target(vector, addend).W;

        Assert.Equal(expected, actual);
    }
}
