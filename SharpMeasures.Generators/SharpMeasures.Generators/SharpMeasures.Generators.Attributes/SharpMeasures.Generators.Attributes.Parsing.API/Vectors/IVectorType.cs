namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorType
{
    public abstract DefinedType Type { get; }
    public abstract IVector VectorDefinition { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }

    public abstract IReadOnlyList<IVectorConstant> Constants { get; }

    public abstract IReadOnlyList<IIncludeUnits> IncludeUnits { get; }
    public abstract IReadOnlyList<IExcludeUnits> ExcludeUnits { get; }

    public abstract IReadOnlyList<IConvertibleQuantity> ConvertibleQuantities { get; }
}
