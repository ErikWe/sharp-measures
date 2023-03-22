namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Operator_Cast_ToValueTuple
{
    private static (Scalar, Scalar, Scalar, Scalar) Target(Vector4 vector) => ((Scalar, Scalar, Scalar, Scalar))vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchToValueTuple(Vector4 vector)
    {
        var expected = vector.ToValueTuple();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
