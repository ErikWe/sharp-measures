namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Cast_ToValueTuple
{
    private static (Scalar, Scalar, Scalar) Target(Vector3 vector) => ((Scalar, Scalar, Scalar))vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchToValueTuple(Vector3 vector)
    {
        var expected = vector.ToValueTuple();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
