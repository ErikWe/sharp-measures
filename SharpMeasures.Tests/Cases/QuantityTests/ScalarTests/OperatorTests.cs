using ErikWe.SharpMeasures.Quantities;

using Xunit;

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests
{
    public class OperatorTests
    {
        [Fact]
        public void Equality_Null_ShouldBeTrueIfBothNull()
        {
            Scalar? scalarNull1 = null;
            Scalar? scalarNull2 = null;
            object? objectNull = null;
            Scalar scalarNonNull = new(0);

            Assert.True(scalarNull1 == scalarNull2);
            Assert.False(scalarNonNull == scalarNull1);
            Assert.False(scalarNull1 == scalarNonNull);
            Assert.False(scalarNonNull.Equals(scalarNull1));
            Assert.False(scalarNonNull.Equals(objectNull));
        }

        [Fact]
        public void Inequality_Null_ShouldBeFalseIfBothNull()
        {
            Scalar? scalarNull1 = null;
            Scalar? scalarNull2 = null;
            Scalar scalarNonNull = new(0);

            Assert.False(scalarNull1 != scalarNull2);
            Assert.True(scalarNonNull != scalarNull1);
            Assert.True(scalarNull1 != scalarNonNull);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Equality_ShouldBeEqualAfterAdditionIfNaN(Scalar scalar)
        {
            Scalar scalar2 = new(scalar.Magnitude + 1);
            Scalar scalar3 = new(scalar.Magnitude);

            Assert.True(scalar == scalar3);

            if (scalar.Magnitude.Equals(scalar2.Magnitude))
            {
                Assert.True(scalar == scalar2);
                Assert.True(scalar2 == scalar);
            }
            else
            {
                Assert.False(scalar == scalar2);
                Assert.False(scalar2 == scalar);
            }
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Inequality_ShouldBeEqualAfterAdditionIfNaN(Scalar scalar)
        {
            Scalar scalar2 = new(scalar.Magnitude + 1);
            Scalar scalar3 = new(scalar.Magnitude);

            Assert.False(scalar != scalar3);

            if (scalar.Magnitude.Equals(scalar2.Magnitude))
            {
                Assert.False(scalar != scalar2);
                Assert.False(scalar2 != scalar);
            }
            else
            {
                Assert.True(scalar != scalar2);
                Assert.True(scalar2 != scalar);
            }
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Plus_ShouldBeEqual(Scalar scalar)
        {
            Assert.Equal(scalar, +scalar);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void Negate_ShouldMatchDouble(Scalar scalar)
        {
            Assert.Equal(-(scalar.Magnitude), (-scalar).Magnitude);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void Add_ShouldMatchDouble(Scalar scalar1, Scalar scalar2)
        {
            Assert.Equal(scalar1.Magnitude + scalar2.Magnitude, (scalar1 + scalar2).Magnitude);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void Subtract_ShouldMatchDouble(Scalar scalar1, Scalar scalar2)
        {
            Assert.Equal(scalar1.Magnitude - scalar2.Magnitude, (scalar1 - scalar2).Magnitude);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void Remainder_ShouldMatchDouble(Scalar scalar1, Scalar scalar2)
        {
            Assert.Equal(scalar1.Magnitude % scalar2.Magnitude, (scalar1 % scalar2).Magnitude);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void Multiply_ShouldMatchDouble(Scalar scalar1, Scalar scalar2)
        {
            Assert.Equal(scalar1.Magnitude * scalar2.Magnitude, (scalar1 * scalar2).Magnitude);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void Divide_ShouldMatchDouble(Scalar scalar1, Scalar scalar2)
        {
            Assert.Equal(scalar1.Magnitude / scalar2.Magnitude, (scalar1 / scalar2).Magnitude);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void Comparison_ShouldBePositiveIfLarger(Scalar scalar1, Scalar scalar2)
        {
            if (scalar1.Magnitude.CompareTo(scalar2.Magnitude) > 0)
            {
                Assert.True(scalar1.CompareTo(scalar2) > 0);
            }
            else if (scalar1.Magnitude.CompareTo(scalar2.Magnitude) < 0)
            {
                Assert.True(scalar1.CompareTo(scalar2) < 0);
            }
            else
            {
                Assert.Equal(0, scalar1.CompareTo(scalar2));
            }
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void LessThan_ShouldMatchDouble(Scalar scalar1, Scalar scalar2)
        {
            Assert.Equal(scalar1.Magnitude < scalar2.Magnitude, scalar1 < scalar2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void GreaterThan_ShouldMatchDouble(Scalar scalar1, Scalar scalar2)
        {
            Assert.Equal(scalar1.Magnitude > scalar2.Magnitude, scalar1 > scalar2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void LessThanOrEqual_ShouldMatchDouble(Scalar scalar1, Scalar scalar2)
        {
            Assert.Equal(scalar1.Magnitude <= scalar2.Magnitude, scalar1 <= scalar2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.TwoScalarDataset))]
        public void GreaterThanOrEqual_ShouldMatchDouble(Scalar scalar1, Scalar scalar2)
        {
            Assert.Equal(scalar1.Magnitude >= scalar2.Magnitude, scalar1 >= scalar2);
        }

        [Theory]
        [ClassData(typeof(Datasets.Scalar.ScalarDataset))]
        public void ImplicitCast_ShouldMatch(Scalar scalar)
        {
            double downCasted = scalar;
            Scalar upCasted = downCasted;

            Assert.Equal(downCasted, scalar.Magnitude);
            Assert.Equal(scalar, upCasted);
        }
    }
}
