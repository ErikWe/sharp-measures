﻿namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;

public static partial class DiagnosticConstruction
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

    public static Diagnostic NullTypeNotScalar(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullTypeNotScalar, location);
    }

    public static Diagnostic TypeNotVector(Location? location, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotVector, location, typeName);
    }

    public static Diagnostic NullTypeNotVector(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullTypeNotVector, location);
    }

    public static Diagnostic TypeNotUnit(Location? location, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotUnit, location, typeName);
    }

    public static Diagnostic NullTypeNotUnit(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullTypeNotUnit, location);
    }

    public static Diagnostic TypeAlreadyDefined(Location? location, string typeName, string attemptedDefinition, string existingDefinition)
    {
        return Diagnostic.Create(DiagnosticRules.TypeAlreadyDefined, location, typeName, attemptedDefinition, existingDefinition);
    }

    public static Diagnostic UnitTypeAlreadyDefinedAsUnit(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "unit", "unit");
    public static Diagnostic ScalarTypeAlreadyDefinedAsUnit(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "scalar", "unit");
    public static Diagnostic VectorTypeAlreadyDefinedAsUnit(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "vector", "unit");
    public static Diagnostic UnitTypeAlreadyDefinedAsScalar(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "unit", "scalar");
    public static Diagnostic ScalarTypeAlreadyDefinedAsScalar(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "scalar", "scalar");
    public static Diagnostic VectorTypeAlreadyDefinedAsScalar(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "vector", "scalar");
    public static Diagnostic UnitTypeAlreadyDefinedAsVector(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "unit", "vector");
    public static Diagnostic ScalarTypeAlreadyDefinedAsVector(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "scalar", "vector");
    public static Diagnostic VectorTypeAlreadyDefinedAsVector(Location? location, string typeName) => TypeAlreadyDefined(location, typeName, "vector", "vector");

    public static Diagnostic TypeNotUnbiasedScalar(Location? location, string scalarName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotUnbiasedScalar, location, scalarName);
    }

    public static Diagnostic TypeNotBiasedScalar(Location? location, string scalarName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotBiasedScalar, location, scalarName);
    }

    public static Diagnostic UnitNotIncludingBiasTerm(Location? location, string unitName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitNotIncludingBiasTerm, location, unitName);
    }

    public static Diagnostic InvalidDerivationExpression(Location? location, string expression)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidDerivationExpression, location, expression);
    }

    public static Diagnostic NullDerivationExpression(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullDerivationExpression, location);
    }

    public static Diagnostic NullDerivationSignature(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullDerivationSignature, location);
    }

    public static Diagnostic EmptyDerivationSignature(Location? location, string objectType)
    {
        return Diagnostic.Create(DiagnosticRules.EmptyDerivationSignature, location, objectType);
    }

    public static Diagnostic EmptyUnitDerivationSignature(Location? location) => EmptyDerivationSignature(location, "unit");
    public static Diagnostic EmptyQuantityDerivationSignature(Location? location) => EmptyDerivationSignature(location, "quantity");

    public static Diagnostic EmptyList(Location? location, string objectType)
    {
        return Diagnostic.Create(DiagnosticRules.EmptyList, location, objectType);
    }

    public static Diagnostic EmptyUnitList(Location? location) => EmptyList(location, "unit");
    public static Diagnostic EmptyQuantityList(Location? location) => EmptyList(location, "quantity");

    public static Diagnostic DuplicateListing(Location? location, string objectType, string objectName)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateListing, location, objectType, objectName);
    }

    public static Diagnostic DuplicateUnitListing(Location? location, string unitName) => DuplicateListing(location, "unit", unitName);
    public static Diagnostic DuplicateQuantityListing(Location? location, string quantityName) => DuplicateListing(location, "quantity", quantityName);
}
