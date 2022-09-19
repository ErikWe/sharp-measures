namespace SharpMeasures.Generators.Units.Pipelines.UnitInstances;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Unit { get; }
    public NamedType Quantity { get; }

    public bool BiasTerm { get; }

    public FixedUnitInstanceDefinition? FixedUnitInstance { get; }

    public IEnumerable<UnitInstanceAliasDefinition> UnitInstanceAliases { get; }
    public IEnumerable<DerivedUnitInstanceDefinition> DerivedUnitInstances { get; }
    public IEnumerable<BiasedUnitInstanceDefinition> BiasedUnitInstances { get; }
    public IEnumerable<PrefixedUnitInstanceDefinition> PrefixedUnitInstances { get; }
    public IEnumerable<ScaledUnitInstanceDefinition> ScaledUnitInstances { get; }

    public IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType unit, NamedType quantity, bool biasTerm, FixedUnitInstanceDefinition? fixedUnitInstance, IEnumerable<UnitInstanceAliasDefinition> unitInstanceAliases, IEnumerable<DerivedUnitInstanceDefinition> derivedUnitInstances,
        IEnumerable<BiasedUnitInstanceDefinition> biasedUnitInstances, IEnumerable<PrefixedUnitInstanceDefinition> prefixedUnitInstances, IEnumerable<ScaledUnitInstanceDefinition> scaledUnitInstances, IReadOnlyDictionary<string, IDerivableUnit> derivationsByID,
        IDocumentationStrategy documentation)
    {
        Unit = unit;
        Quantity = quantity;

        BiasTerm = biasTerm;

        FixedUnitInstance = fixedUnitInstance;

        UnitInstanceAliases = unitInstanceAliases.AsEquatable();
        DerivedUnitInstances = derivedUnitInstances.AsEquatable();
        BiasedUnitInstances = biasedUnitInstances.AsEquatable();
        PrefixedUnitInstances = prefixedUnitInstances.AsEquatable();
        ScaledUnitInstances = scaledUnitInstances.AsEquatable();

        DerivationsByID = derivationsByID.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
