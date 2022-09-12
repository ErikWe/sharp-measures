namespace SharpMeasures.Generators;

public interface ISharpMeasuresObjectType
{
    public abstract DefinedType Type { get; }

    public abstract ISharpMeasuresObject Definition { get; }
}
