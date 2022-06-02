namespace SharpMeasures.Generators.Vectors.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

internal static class GeneratedVectorDiagnostics
{
    public static Diagnostic TypeAlreadyUnit(MinimalLocation? location, NamedType type)
    {
        return DiagnosticConstruction.TypeAlreadyDefined_AsUnit_Scalar(location?.AsRoslynLocation(), type.Name);
    }

    public static Diagnostic TypeAlreadyScalar(MinimalLocation? location, NamedType type)
    {
        return DiagnosticConstruction.TypeAlreadyDefined_AsScalar_Vector(location?.AsRoslynLocation(), type.Name);
    }

    public static Diagnostic TypeNotUnit(GeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public static Diagnostic TypeNotScalar(GeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public static Diagnostic UnrecognizedUnit(GeneratedVectorDefinition definition, UnitInterface unit)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, unit.UnitType.Name);
    }
}
