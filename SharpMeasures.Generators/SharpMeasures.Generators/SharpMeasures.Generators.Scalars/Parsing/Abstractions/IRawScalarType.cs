namespace SharpMeasures.Generators.Scalars.Parsing.Abstractions;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;

internal interface IRawScalarType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IEnumerable<RawDerivedQuantityDefinition> Derivations { get; }

    public IEnumerable<RawScalarConstantDefinition> Constants { get; }

    public IEnumerable<RawIncludeBasesDefinition> IncludeBases { get; }
    public IEnumerable<RawExcludeBasesDefinition> ExcludeBases { get; }

    public IEnumerable<RawIncludeUnitsDefinition> IncludeUnits { get; }
    public IEnumerable<RawExcludeUnitsDefinition> ExcludeUnits { get; }

    public IEnumerable<RawConvertibleQuantityDefinition> ConvertibleQuantities { get; }
}
