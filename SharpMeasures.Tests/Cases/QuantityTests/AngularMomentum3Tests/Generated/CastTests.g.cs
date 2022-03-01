#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(AngularMomentum3Dataset))]
    public void ToTuple_ComponentsShouldMatch(AngularMomentum3 quantity)
    {
        (double x, double y, double z) result = quantity.ToValueTuple();

        Utility.AssertExtra.AssertEqualComponents(quantity, result);
    }

    [Theory]
    [ClassData(typeof(AngularMomentum3Dataset))]
    public void CastToTuple_ComponentsShouldMatch(AngularMomentum3 quantity)
    {
        (double x, double y, double z) result = ((double, double, double))quantity;

        Utility.AssertExtra.AssertEqualComponents(quantity, result);
    }

    [Theory]
    [ClassData(typeof(AngularMomentum3Dataset))]
    public void ToVector3_ComponentsShouldMatch(AngularMomentum3 quantity)
    {
        Vector3 result = quantity.ToVector3();

        Utility.AssertExtra.AssertEqualComponents(quantity, result.ToValueTuple());
    }

    [Theory]
    [ClassData(typeof(AngularMomentum3Dataset))]
    public void CastToVector3_ComponentsShouldMatch(AngularMomentum3 quantity)
    {
        Vector3 result = (Vector3)quantity;

        Utility.AssertExtra.AssertEqualComponents(quantity, result.ToValueTuple());
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void FromTuple_ComponentsShouldMatch(double x, double y, double z)
    {
        AngularMomentum3 result = AngularMomentum3.FromValueTuple((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), result);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void CastFromTuple_ShouldMatchMagnitude(double x, double y, double z)
    {
        AngularMomentum3 result = (AngularMomentum3)(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), result);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void FromVector3_ShouldMatchMagnitude(Vector3 vector)
    {
        AngularMomentum3 result = AngularMomentum3.FromVector3(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), result);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void CastFromVector3_ShouldMatchMagnitude(Vector3 vector)
    {
        AngularMomentum3 result = (AngularMomentum3)vector;

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), result);
    }
}
