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

    public IEnumerable<UnitInstanceAliasDefinition> UnitInstanceAliases => unitInstanceAliases;
    public IEnumerable<DerivedUnitInstanceDefinition> DerivedUnitInstances => derivedUnitInstances;
    public IEnumerable<BiasedUnitInstanceDefinition> BiasedUnitInstances => biasedUnitInstances;
    public IEnumerable<PrefixedUnitInstanceDefinition> PrefixedUnitInstances => prefixedUnitInstances;
    public IEnumerable<ScaledUnitInstanceDefinition> ScaledUnitInstances => scaledUnitInstances;

    public IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID => derivationsByID;

    public IDocumentationStrategy Documentation { get; }

    private EquatableEnumerable<UnitInstanceAliasDefinition> unitInstanceAliases { get; }
    private EquatableEnumerable<DerivedUnitInstanceDefinition> derivedUnitInstances { get; }
    private EquatableEnumerable<BiasedUnitInstanceDefinition> biasedUnitInstances { get; }
    private EquatableEnumerable<PrefixedUnitInstanceDefinition> prefixedUnitInstances { get; }
    private EquatableEnumerable<ScaledUnitInstanceDefinition> scaledUnitInstances { get; }

    private ReadOnlyEquatableDictionary<string, IDerivableUnit> derivationsByID { get; }

    public DataModel(DefinedType unit, NamedType quantity, bool biasTerm, FixedUnitInstanceDefinition? fixedUnitInstance, IEnumerable<UnitInstanceAliasDefinition> unitInstanceAliases, IEnumerable<DerivedUnitInstanceDefinition> derivedUnitInstances,
        IEnumerable<BiasedUnitInstanceDefinition> biasedUnitInstances, IEnumerable<PrefixedUnitInstanceDefinition> prefixedUnitInstances, IEnumerable<ScaledUnitInstanceDefinition> scaledUnitInstances, IReadOnlyDictionary<string, IDerivableUnit> derivationsByID,
        IDocumentationStrategy documentation)
    {
        Unit = unit;
        Quantity = quantity;

        BiasTerm = biasTerm;

        FixedUnitInstance = fixedUnitInstance;

        this.unitInstanceAliases = unitInstanceAliases.AsEquatable();
        this.derivedUnitInstances = derivedUnitInstances.AsEquatable();
        this.biasedUnitInstances = biasedUnitInstances.AsEquatable();
        this.prefixedUnitInstances = prefixedUnitInstances.AsEquatable();
        this.scaledUnitInstances = scaledUnitInstances.AsEquatable();

        this.derivationsByID = derivationsByID.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
