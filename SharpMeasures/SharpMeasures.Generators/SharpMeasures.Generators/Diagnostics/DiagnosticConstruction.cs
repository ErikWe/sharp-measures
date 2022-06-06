namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;

internal static partial class DiagnosticConstruction
{
    public static Diagnostic TypeNotPartial<TAttribute>(Location? location, string typeName)
    {
        return TypeNotPartial(location, typeof(TAttribute), typeName);
    }

    public static Diagnostic TypeNotPartial(Location? location, Type attributeType, string typeName)
    {
        return TypeNotPartial(location, attributeType.Name, typeName);
    }

    public static Diagnostic TypeNotPartial(Location? location, string attributeName, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotPartial, location, attributeName, typeName);
    }

    public static Diagnostic TypeNotScalar(Location? location, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotScalar, location, typeName);
    }

    public static Diagnostic TypeNotScalar_Null(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotScalar_Null, location);
    }

    public static Diagnostic TypeNotVector(Location? location, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotVector, location, typeName);
    }

    public static Diagnostic TypeNotVector_Null(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotVector_Null, location);
    }

    public static Diagnostic TypeNotUnit(Location? location, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotUnit, location, typeName);
    }

    public static Diagnostic TypeNotUnit_Null(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotUnit_Null, location);
    }

    public static Diagnostic TypeAlreadyDefined(Location? location, string typeName, string attemptedDefinition, string existingDefinition)
    {
        return Diagnostic.Create(DiagnosticRules.TypeAlreadyDefined, location, typeName, attemptedDefinition, existingDefinition);
    }

    public static Diagnostic TypeAlreadyDefined_AsUnit_Unit(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "unit", "unit");
    public static Diagnostic TypeAlreadyDefined_AsUnit_Scalar(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "scalar", "unit");
    public static Diagnostic TypeAlreadyDefined_AsUnit_Vector(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "vector", "unit");
    public static Diagnostic TypeAlreadyDefined_AsScalar_Unit(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "unit", "scalar");
    public static Diagnostic TypeAlreadyDefined_AsScalar_Scalar(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "scalar", "scalar");
    public static Diagnostic TypeAlreadyDefined_AsScalar_Vector(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "vector", "scalar");
    public static Diagnostic TypeAlreadyDefined_AsVector_Unit(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "unit", "vector");
    public static Diagnostic TypeAlreadyDefined_AsVector_Scalar(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "scalar", "vector");
    public static Diagnostic TypeAlreadyDefined_AsVector_Vector(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "vector", "vector");

    public static Diagnostic ScalarNotUnbiased_UnitDefinition(Location? location, string scalarName)
    {
        return Diagnostic.Create(DiagnosticRules.ScalarNotUnbiased_UnitDefinition, location, scalarName);
    }

    public static Diagnostic ScalarNotUnbiased(Location? location, string scalarName)
    {
        return Diagnostic.Create(DiagnosticRules.ScalarNotUnbiased, location, scalarName);
    }

    public static Diagnostic ScalarNotBiased(Location? location, string scalarName)
    {
        return Diagnostic.Create(DiagnosticRules.ScalarNotBiased, location, scalarName);
    }

    public static Diagnostic UnitNotSupportingBias(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitNotSupportingBias, location, unitName);
    }

    public static Diagnostic EmptyList(Location? location, string objectType)
    {
        return Diagnostic.Create(DiagnosticRules.EmptyList, location, objectType);
    }

    public static Diagnostic EmptyList_Unit(Location? location) => EmptyList(location, "unit");
    public static Diagnostic EmptyList_Quantity(Location? location) => EmptyList(location, "quantity");

    public static Diagnostic DuplicateListing(Location? location, string objectType, string objectName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateListing, location, objectType, objectName);
    }

    public static Diagnostic DuplicateListing_Unit(Location? location, string unitName) => DuplicateListing(location, "unit", unitName);
    public static Diagnostic DuplicateListing_Quantity(Location? location, string quantityName) => DuplicateListing(location, "quantity", quantityName);
}
