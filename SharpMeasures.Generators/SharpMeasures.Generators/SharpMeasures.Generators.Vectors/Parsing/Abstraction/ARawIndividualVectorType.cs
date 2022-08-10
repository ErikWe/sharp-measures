namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal abstract record class ARawIndividualVectorType : ARawVectorGroupType
{
    public IEnumerable<RawVectorConstantDefinition> Constants => constants;

    private EquatableEnumerable<RawVectorConstantDefinition> constants { get; }

    protected ARawIndividualVectorType(DefinedType type, MinimalLocation typeLocation, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawVectorConstantDefinition> constants,
        IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions)
        : base(type, typeLocation, derivations, conversions, unitInclusions, unitExclusions)
    {
        this.constants = constants.AsEquatable();
    }
}
