namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Add_Vector3
{
    private static Vector3 Target(Vector3 vector, Vector3 addend) => vector.Add(addend);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchXAdd(Vector3 vector, Vector3 addend)
    {
        var expected = vector.X.Add(addend.X);

        var actual = Target(vector, addend).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchYAdd(Vector3 vector, Vector3 addend)
    {
        var expected = vector.Y.Add(addend.Y);

        var actual = Target(vector, addend).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchZAdd(Vector3 vector, Vector3 addend)
    {
        var expected = vector.Z.Add(addend.Z);

        var actual = Target(vector, addend).Z;

        Assert.Equal(expected, actual);
    }
}
