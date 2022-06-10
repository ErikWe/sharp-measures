namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.Refinement.DerivableUnit;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, DocumentationFile Documentation,
    ReadOnlyEquatableCollection<RefinedDerivableUnitDefinition> Derivations)
{
    public DataModel(DefinedType unit, NamedType quantity, DocumentationFile documentation, IReadOnlyCollection<RefinedDerivableUnitDefinition> derivations)
        : this(unit, quantity, documentation, new(derivations)) { }
}
