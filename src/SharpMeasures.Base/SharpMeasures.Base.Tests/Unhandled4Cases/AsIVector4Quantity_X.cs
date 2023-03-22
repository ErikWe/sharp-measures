namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class AsIVector4Quantity_X
{
    private static Scalar Target(Unhandled4 vector)
    {
        return execute(vector);

        static Scalar execute(IVector4Quantity vector) => vector.X;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchXMagnitude(Unhandled4 vector)
    {
        var expected = vector.X.Magnitude;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
