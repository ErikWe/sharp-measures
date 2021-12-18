using ErikWe.SharpMeasures.Quantities;

using System;

namespace ErikWe.SharpMeasures.Units
{
    public struct UnitOfArea
    {
        public static readonly UnitOfArea Are = new(FromUnitOfLength(UnitOfLength.Metre) * Math.Pow(10, 2));
        public static readonly UnitOfArea Hectare = new(Are.Scale * Math.Pow(10, 2));

        public static readonly UnitOfArea Acre = new(FromUnitOfLength(UnitOfLength.Mile) / 640);

        public Scalar Scale { get; }

        private UnitOfArea(Scalar scale)
        {
            Scale = scale;
        }

        private static Scalar FromUnitOfLength(UnitOfLength lengthUnit) => Math.Pow(lengthUnit.Scale, 2);
    }
}
