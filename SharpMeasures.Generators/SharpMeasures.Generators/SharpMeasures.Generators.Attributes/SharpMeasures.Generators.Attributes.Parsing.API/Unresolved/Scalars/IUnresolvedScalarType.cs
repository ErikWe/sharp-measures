namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

public interface IUnresolvedScalarType
{
    public abstract IScalarType UnresolvedTarget { get; }

    public abstract DefinedType Type { get; }
    public abstract IUnresolvedScalar ScalarDefinition { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IUnresolvedScalarConstant> Constants { get; }
    public abstract IReadOnlyList<IConvertibleQuantity> ConvertibleQuantities { get; }

    public abstract IReadOnlyList<IUnresolvedIncludeBases> IncludeBases { get; }
    public abstract IReadOnlyList<IUnresolvedExcludeBases> ExcludeBases { get; }

    public abstract IReadOnlyList<IIncludeUnits> IncludeUnits { get; }
    public abstract IReadOnlyList<IExcludeUnits> ExcludeUnits { get; }

    public IReadOnlyDictionary<string, IUnresolvedScalarConstant> ConstantsByName { get; }
}
