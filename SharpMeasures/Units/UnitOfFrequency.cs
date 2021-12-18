using ErikWe.SharpMeasures.Quantities;

namespace ErikWe.SharpMeasures.Units
{
    public struct UnitOfFrequency
    {
        public static readonly UnitOfFrequency Hertz = new(1);

        public Scalar Scale { get; }

        private UnitOfFrequency(Scalar scale)
        {
            Scale = scale;
        }
    }
}
