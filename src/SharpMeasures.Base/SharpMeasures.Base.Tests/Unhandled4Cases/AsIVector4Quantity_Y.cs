namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class AsIVector4Quantity_Y
{
    private static Scalar Target(Unhandled4 vector)
    {
        return execute(vector);

        static Scalar execute(IVector4Quantity vector) => vector.Y;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchYMagnitude(Unhandled4 vector)
    {
        var expected = vector.Y.Magnitude;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
