namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.Processing;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, DocumentationFile Documentation, EquatableCollection<ProcessedDerivableUnit> Derivations)
{
    public DataModel(DefinedType unit, NamedType quantity, DocumentationFile documentation, IReadOnlyCollection<ProcessedDerivableUnit> derivations)
        : this(unit, quantity, documentation, new(derivations)) { }
}
