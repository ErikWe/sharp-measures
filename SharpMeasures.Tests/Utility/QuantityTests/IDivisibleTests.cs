
using Xunit;

namespace ErikWe.SharpMeasures.Tests.Utility.QuantityTests
{
    public static class IDivisibleTests
    {
        public static void Method_ShouldEqualScalar<TResultQuantity, TOtherQuantity, TQuantity>(TQuantity quantity1, TOtherQuantity quantity2)
            where TResultQuantity : IQuantity
            where TOtherQuantity : IQuantity
            where TQuantity : IQuantity<TQuantity>, IDivisibleQuantity<TResultQuantity, TOtherQuantity>
        {
            TResultQuantity result = quantity1.Divide(quantity2);

            Assert.Equal(quantity1.Magnitude / quantity2.Magnitude, result.Magnitude, 2);
        }

        public static void Method_IQuantity2_ShouldEqualScalar<TResultQuantity, TOtherComponent, TOtherQuantity, TComponent, TQuantity>(TQuantity quantity1, TOtherQuantity quantity2)
            where TResultQuantity : IQuantity2
            where TOtherComponent : IQuantity
            where TOtherQuantity : IQuantity2
            where TComponent : IQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>, IDivisibleQuantity2<TResultQuantity, TOtherComponent, TOtherQuantity>
        {
            TResultQuantity result = quantity1.DivideElementwise(quantity2);

            Assert.Equal(quantity1.MagnitudeX / quantity2.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(quantity1.MagnitudeY / quantity2.MagnitudeY, result.MagnitudeY, 2);
        }

        public static void Method_IQuantity2_ShouldEqualScalar<TResultQuantity, TOtherComponent, TOtherQuantity, TComponent, TQuantity>(TQuantity quantity1, TOtherComponent quantity2)
            where TResultQuantity : IQuantity2
            where TOtherComponent : IQuantity
            where TOtherQuantity : IQuantity2
            where TComponent : IQuantity
            where TQuantity : IQuantity2<TComponent, TQuantity>, IDivisibleQuantity2<TResultQuantity, TOtherComponent, TOtherQuantity>
        {
            TResultQuantity result = quantity1.Divide(quantity2);

            Assert.Equal(quantity1.MagnitudeX / quantity2.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(quantity1.MagnitudeY / quantity2.Magnitude, result.MagnitudeY, 2);
        }

        public static void Method_IQuantity3_ShouldEqualScalar<TResultQuantity, TOtherComponent, TOtherQuantity, TComponent, TQuantity>(TQuantity quantity1, TOtherQuantity quantity2)
            where TResultQuantity : IQuantity3
            where TOtherComponent : IQuantity
            where TOtherQuantity : IQuantity3
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>, IDivisibleQuantity3<TResultQuantity, TOtherComponent, TOtherQuantity>
        {
            TResultQuantity result = quantity1.DivideElementwise(quantity2);

            Assert.Equal(quantity1.MagnitudeX / quantity2.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(quantity1.MagnitudeY / quantity2.MagnitudeY, result.MagnitudeY, 2);
            Assert.Equal(quantity1.MagnitudeZ / quantity2.MagnitudeZ, result.MagnitudeZ, 2);
        }

        public static void Method_IQuantity3_ShouldEqualScalar<TResultQuantity, TOtherComponent, TOtherQuantity, TComponent, TQuantity>(TQuantity quantity1, TOtherComponent quantity2)
            where TResultQuantity : IQuantity3
            where TOtherComponent : IQuantity
            where TOtherQuantity : IQuantity3
            where TComponent : IQuantity
            where TQuantity : IQuantity3<TComponent, TQuantity>, IDivisibleQuantity3<TResultQuantity, TOtherComponent, TOtherQuantity>
        {
            TResultQuantity result = quantity1.Divide(quantity2);

            Assert.Equal(quantity1.MagnitudeX / quantity2.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(quantity1.MagnitudeY / quantity2.Magnitude, result.MagnitudeY, 2);
            Assert.Equal(quantity1.MagnitudeZ / quantity2.Magnitude, result.MagnitudeZ, 2);
        }

        public static void Operator_ShouldEqualMethod(IQuantity quantity1, IQuantity quantity2, IQuantity result)
        {
            Assert.Equal(quantity1.Magnitude / quantity2.Magnitude, result.Magnitude, 2);
        }

        public static void Operator_ShouldEqualMethod(IQuantity2 quantity1, IQuantity2 quantity2, IQuantity2 result)
        {
            Assert.Equal(quantity1.MagnitudeX / quantity2.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(quantity1.MagnitudeY / quantity2.MagnitudeY, result.MagnitudeY, 2);
        }

        public static void Operator_ShouldEqualMethod(IQuantity2 quantity1, IQuantity quantity2, IQuantity2 result)
        {
            Assert.Equal(quantity1.MagnitudeX / quantity2.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(quantity1.MagnitudeY / quantity2.Magnitude, result.MagnitudeY, 2);
        }

        public static void Operator_ShouldEqualMethod(IQuantity3 quantity1, IQuantity3 quantity2, IQuantity3 result)
        {
            Assert.Equal(quantity1.MagnitudeX / quantity2.MagnitudeX, result.MagnitudeX, 2);
            Assert.Equal(quantity1.MagnitudeY / quantity2.MagnitudeY, result.MagnitudeY, 2);
            Assert.Equal(quantity1.MagnitudeZ / quantity1.MagnitudeZ, result.MagnitudeZ, 2);
        }

        public static void Operator_ShouldEqualMethod(IQuantity3 quantity1, IQuantity quantity2, IQuantity3 result)
        {
            Assert.Equal(quantity1.MagnitudeX / quantity2.Magnitude, result.MagnitudeX, 2);
            Assert.Equal(quantity1.MagnitudeY / quantity2.Magnitude, result.MagnitudeY, 2);
            Assert.Equal(quantity1.MagnitudeZ / quantity2.Magnitude, result.MagnitudeZ, 2);
        }
    }
}
