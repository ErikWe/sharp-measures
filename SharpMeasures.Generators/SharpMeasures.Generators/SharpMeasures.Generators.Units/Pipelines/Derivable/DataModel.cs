namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Refinement.DerivableUnit;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, IDocumentationStrategy Documentation,
    ReadOnlyEquatableCollection<RefinedDerivableUnitDefinition> Derivations)
{
    public DataModel(DefinedType unit, NamedType quantity, IDocumentationStrategy documentation, IReadOnlyCollection<RefinedDerivableUnitDefinition> derivations)
        : this(unit, quantity, documentation, derivations.AsReadOnlyEquatable()) { }
}
