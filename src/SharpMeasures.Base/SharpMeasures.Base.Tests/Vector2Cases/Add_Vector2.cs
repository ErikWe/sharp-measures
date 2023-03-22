namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Add_Vector2
{
    private static Vector2 Target(Vector2 vector, Vector2 addend) => vector.Add(addend);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void MatchXAdd(Vector2 vector, Vector2 addend)
    {
        var expected = vector.X.Add(addend.X);

        var actual = Target(vector, addend).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void MatchYAdd(Vector2 vector, Vector2 addend)
    {
        var expected = vector.Y.Add(addend.Y);

        var actual = Target(vector, addend).Y;

        Assert.Equal(expected, actual);
    }
}
