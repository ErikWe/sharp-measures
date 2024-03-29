﻿namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class DerivableUnitProperty<TPropertyType> : AttributeProperty<SymbolicDerivableUnitDefinition, DerivableUnitLocations, TPropertyType>
{
    public DerivableUnitProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public DerivableUnitProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}
