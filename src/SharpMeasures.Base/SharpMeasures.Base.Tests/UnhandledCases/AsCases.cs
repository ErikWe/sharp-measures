namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class AsCases
{
    private static TScalar Target<TScalar>(Unhandled unhandled) where TScalar : IScalarQuantity<TScalar> => unhandled.As<TScalar>();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void Scalar_MatchMagnitude(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude;

        var actual = Target<Scalar>(unhandled);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void ReferenceScalarQuantity_MatchMagnitude(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude;

        var actual = Target<ReferenceScalarQuantity>(unhandled).Magnitude;

        Assert.Equal(expected, actual);
    }
}
