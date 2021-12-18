using ErikWe.SharpMeasures.Quantities;

namespace ErikWe.SharpMeasures.Units
{
    public struct UnitOfTime
    {
        public static readonly UnitOfTime Second = new(1);

        public static readonly UnitOfTime Femtosecond = new(Second.Scale * MetricPrefix.Femto);
        public static readonly UnitOfTime Picosecond = new(Second.Scale * MetricPrefix.Pico);
        public static readonly UnitOfTime Nanosecond = new(Second.Scale * MetricPrefix.Nano);
        public static readonly UnitOfTime Microsecond = new(Second.Scale * MetricPrefix.Micro);
        public static readonly UnitOfTime Millisecond = new(Second.Scale * MetricPrefix.Milli);

        public static readonly UnitOfTime Minute = new(Second.Scale * 60);
        public static readonly UnitOfTime Hour = new(Minute.Scale * 60);
        public static readonly UnitOfTime Day = new(Hour.Scale * 24);
        public static readonly UnitOfTime Week = new(Day.Scale * 7);
        public static readonly UnitOfTime CommonYear = new(Day.Scale * 365);

        public Scalar Scale { get; }

        private UnitOfTime(Scalar scale)
        {
            Scale = scale;
        }
    }
}
