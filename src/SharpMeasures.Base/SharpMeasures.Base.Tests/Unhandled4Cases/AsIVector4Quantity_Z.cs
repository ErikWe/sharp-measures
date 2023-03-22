namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class AsIVector4Quantity_Z
{
    private static Scalar Target(Unhandled4 vector)
    {
        return execute(vector);

        static Scalar execute(IVector4Quantity vector) => vector.Z;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void MatchZMagnitude(Unhandled4 vector)
    {
        var expected = vector.Z.Magnitude;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
