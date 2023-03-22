namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Cast_ToValueTuple
{
    private static (Scalar, Scalar) Target(Vector2 vector) => ((Scalar, Scalar))vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchToValueTuple(Vector2 vector)
    {
        var expected = vector.ToValueTuple();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
