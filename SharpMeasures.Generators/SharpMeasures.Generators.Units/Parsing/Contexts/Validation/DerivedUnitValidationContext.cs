namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using System.Collections.Generic;

internal record class DerivedUnitValidationContext : SimpleProcessingContext, IDerivedUnitValidationContext
{
    public IDerivableUnit? UnnamedDerivation { get; }
    public IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID { get; }

    public IUnitPopulation UnitPopulation { get; }

    public DerivedUnitValidationContext(DefinedType type, IDerivableUnit? unnamedDerivation, IReadOnlyDictionary<string, IDerivableUnit> derivationsByID, IUnitPopulation unitPopulation) : base(type)
    {
        UnnamedDerivation = unnamedDerivation;
        DerivationsByID = derivationsByID;

        UnitPopulation = unitPopulation;
    }
}
