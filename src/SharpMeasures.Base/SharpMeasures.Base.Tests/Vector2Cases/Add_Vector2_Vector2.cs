namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Add_Vector2_Vector2
{
    private static Vector2 Target(Vector2 a, Vector2 b) => Vector2.Add(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void MatchInstanceMethod(Vector2 a, Vector2 b)
    {
        var expected = a.Add(b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
