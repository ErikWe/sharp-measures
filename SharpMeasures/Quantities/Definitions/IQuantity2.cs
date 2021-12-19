namespace ErikWe.SharpMeasures.Quantities.Definitions
{
    public interface IQuantity2
    {
        public abstract Scalar XMagnitude { get; }
        public abstract Scalar YMagnitude { get; }
    }

    public interface IQuantity2<TComponent> : IQuantity2 where TComponent : IQuantity
    {
        public abstract TComponent X { get; }
        public abstract TComponent Y { get; }
    }
}
