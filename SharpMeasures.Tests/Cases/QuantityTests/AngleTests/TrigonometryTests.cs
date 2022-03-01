namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngleTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;
using System.Collections.Generic;

using Xunit;

public class TrigonometryTests
{
    [Theory]
    [ClassData(typeof(AngleDataset))]
    public void Sin_ShouldMatchSystem(Angle angle)
    {
        Assert.Equal(Math.Sin(angle.Radians), angle.Sin(), 2);
    }

    [Theory]
    [ClassData(typeof(AngleDataset))]
    public void Cos_ShouldMatchSystem(Angle angle)
    {
        Assert.Equal(Math.Cos(angle.Radians), angle.Cos(), 2);
    }

    [Theory]
    [ClassData(typeof(AngleDataset))]
    public void Tan_ShouldMatchSystem(Angle angle)
    {
        Assert.Equal(Math.Tan(angle.Radians), angle.Tan(), 2);
    }

    [Theory]
    [ClassData(typeof(AngleDataset))]
    public void Sinh_ShouldMatchSystem(Angle angle)
    {
        Assert.Equal(Math.Sinh(angle.Radians), angle.Sinh(), 2);
    }

    [Theory]
    [ClassData(typeof(AngleDataset))]
    public void Cosh_ShouldMatchSystem(Angle angle)
    {
        Assert.Equal(Math.Cosh(angle.Radians), angle.Cosh(), 2);
    }

    [Theory]
    [ClassData(typeof(AngleDataset))]
    public void Tanh_ShouldMatchSystem(Angle angle)
    {
        Assert.Equal(Math.Tanh(angle.Radians), angle.Tanh(), 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Asin_ShouldMatchSystem(Scalar sine)
    {
        double expected = Math.Asin(sine);

        Angle angle1 = Angle.Asin(sine);
        Angle angle2 = Angle.Asin(sine.Magnitude);

        Assert.Equal(expected, angle1.Radians, 2);
        Assert.Equal(expected, angle2.Radians, 2);
    }

    [Fact]
    public void Asin_ShouldBeInRange()
    {
        IEnumerable<Angle> angles = new Angle[]
        {
            Angle.Asin(-0.99),
            Angle.Asin(0.99),
            Angle.Asin(0),
            Angle.Asin(-1),
            Angle.Asin(1)
        };

        foreach (Angle angle in angles)
        {
            Assert.InRange(angle.Radians, -Math.PI / 2 - double.Epsilon, Math.PI / 2 + double.Epsilon);
        }
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Acos_ShouldMatchSystem(Scalar cosine)
    {
        double expected = Math.Acos(cosine);

        Angle angle1 = Angle.Acos(cosine);
        Angle angle2 = Angle.Acos(cosine.Magnitude);

        Assert.Equal(expected, angle1.Radians, 2);
        Assert.Equal(expected, angle2.Radians, 2);
    }

    [Fact]
    public void Acos_ShouldBeInRange()
    {
        IEnumerable<Angle> angles = new Angle[]
        {
            Angle.Acos(-0.99),
            Angle.Acos(0.99),
            Angle.Acos(0),
            Angle.Acos(-1),
            Angle.Acos(1)
        };

        foreach (Angle angle in angles)
        {
            Assert.InRange(angle.Radians, -double.Epsilon, Math.PI + double.Epsilon);
        }
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Atan_ShouldMatchSystem(Scalar tangent)
    {
        double expected = Math.Atan(tangent);

        Angle angle1 = Angle.Atan(tangent);
        Angle angle2 = Angle.Atan(tangent.Magnitude);

        Assert.Equal(expected, angle1.Radians, 2);
        Assert.Equal(expected, angle2.Radians, 2);
    }

    [Fact]
    public void Atan_ShouldBeInRange()
    {
        IEnumerable<Angle> angles = new Angle[]
        {
            Angle.Atan(double.PositiveInfinity),
            Angle.Atan(double.NegativeInfinity),
            Angle.Atan(0),
            Angle.Atan(-1),
            Angle.Atan(1)
        };

        foreach (Angle angle in angles)
        {
            Assert.InRange(angle.Radians, -Math.PI / 2 - double.Epsilon, Math.PI / 2 + double.Epsilon);
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void Atan2_ShouldMatchSystem(Scalar y, Scalar x)
    {
        double expected = Math.Atan2(y, x);

        Angle angle1 = Angle.Atan2(y, x);
        Angle angle2 = Angle.Atan2(y.Magnitude, x.Magnitude);

        Assert.Equal(expected, angle1.Radians, 2);
        Assert.Equal(expected, angle2.Radians, 2);
    }

    [Fact]
    public void Atan2_ShouldBeInRange()
    {
        IEnumerable<Angle> angles = new Angle[]
        {
            Angle.Atan2(-0.99, -0.99),
            Angle.Atan2(319, -78),
            Angle.Atan2(0, 0),
            Angle.Atan2(-1, -1)
        };

        foreach (Angle angle in angles)
        {
            Assert.InRange(angle.Radians, -Math.PI - double.Epsilon, Math.PI + double.Epsilon);
        }
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Asinh_ShouldMatchSystem(Scalar hyperbolicSine)
    {
        double expected = Math.Asinh(hyperbolicSine);

        Angle angle1 = Angle.Asinh(hyperbolicSine);
        Angle angle2 = Angle.Asinh(hyperbolicSine.Magnitude);

        Assert.Equal(expected, angle1.Radians, 2);
        Assert.Equal(expected, angle2.Radians, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Acosh_ShouldMatchSystem(Scalar hyperbolicCosine)
    {
        double expected = Math.Acosh(hyperbolicCosine);

        Angle angle1 = Angle.Acosh(hyperbolicCosine);
        Angle angle2 = Angle.Acosh(hyperbolicCosine.Magnitude);

        Assert.Equal(expected, angle1.Radians, 2);
        Assert.Equal(expected, angle2.Radians, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Atanh_ShouldMatchSystem(Scalar hyperbolicTangent)
    {
        double expected = Math.Atanh(hyperbolicTangent);

        Angle angle1 = Angle.Atanh(hyperbolicTangent);
        Angle angle2 = Angle.Atanh(hyperbolicTangent.Magnitude);

        Assert.Equal(expected, angle1.Radians, 2);
        Assert.Equal(expected, angle2.Radians, 2);
    }
}
