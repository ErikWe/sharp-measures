using ErikWe.SharpMeasures.Quantities;

using System;

namespace ErikWe.SharpMeasures.Units
{
    public struct UnitOfAngle
    {
        public static readonly UnitOfAngle Radian = new(1);
        public static readonly UnitOfAngle Degree = new(Radian.Scale * Math.PI / 180);
        public static readonly UnitOfAngle ArcMinute = new(Degree.Scale / 60);
        public static readonly UnitOfAngle ArcSecond = new(ArcMinute.Scale / 60);
        public static readonly UnitOfAngle Turn = new(Radian.Scale * Math.Tau);

        public Scalar Scale { get; }

        private UnitOfAngle(Scalar scale)
        {
            Scale = scale;
        }
    }
}
