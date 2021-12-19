namespace ErikWe.SharpMeasures.Quantities.Definitions
{
    public interface IQuantity3
    {
        public abstract Scalar XMagnitude { get; }
        public abstract Scalar YMagnitude { get; }
        public abstract Scalar ZMagnitude { get; }
    }

    public interface IQuantity3<TComponent> : IQuantity3 where TComponent : IQuantity
    {
        public abstract TComponent X { get; }
        public abstract TComponent Y { get; }
        public abstract TComponent Z { get; }
    }
}
