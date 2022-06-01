namespace SharpMeasures.Generators.Scalars.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

internal static class GeneratedScalarDiagnostics
{
    public static Diagnostic TypeNotUnit(GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public static Diagnostic UnitNotSupportingBiasedQuantities(GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.UnitNotSupportingBias(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public static Diagnostic TypeNotScalar(MinimalLocation? location, NamedType type)
    {
        return DiagnosticConstruction.TypeNotScalar(location?.AsRoslynLocation(), type.Name);
    }

    public static Diagnostic? TypeNotVector(GeneratedScalarDefinition definition)
    {
        if (definition.Vector is null)
        {
            return null;
        }

        return DiagnosticConstruction.TypeNotVector(definition.Locations.Vector?.AsRoslynLocation(), definition.Vector.Value.Name);
    }

    public static Diagnostic UnrecognizedUnit(GeneratedScalarDefinition definition, UnitInterface unit)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, unit.UnitType.Name);
    }
}
