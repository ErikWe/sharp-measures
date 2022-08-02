namespace SharpMeasures.Generators.Diagnostics;

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
        return Diagnostic.Create(DiagnosticRules.TypeNotPartial, location, Utility.AttributeName(attributeName), typeName);
    }

    public static Diagnostic TypeStatic<TAttribute>(Location? location, string typeName)
    {
        return TypeStatic(location, typeof(TAttribute), typeName);
    }

    public static Diagnostic TypeStatic(Location? location, Type attributeType, string typeName)
    {
        return TypeStatic(location, attributeType.Name, typeName);
    }

    public static Diagnostic TypeStatic(Location? location, string attributeName, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeStatic, location, Utility.AttributeName(attributeName), typeName);
    }

    public static Diagnostic TypeNotStatic<TAttribute>(Location? location, string typeName)
    {
        return TypeNotStatic(location, typeof(TAttribute), typeName);
    }

    public static Diagnostic TypeNotStatic(Location? location, Type attributeType, string typeName)
    {
        return TypeNotStatic(location, attributeType.Name, typeName);
    }

    public static Diagnostic TypeNotStatic(Location? location, string attributeName, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotStatic, location, Utility.AttributeName(attributeName), typeName);
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

    public static Diagnostic TypeNotVectorGroup(Location? location, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotVectorGroup, location, typeName);
    }

    public static Diagnostic NullTypeNotVectorGroup(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullTypeNotVectorGroup, location);
    }

    public static Diagnostic TypeNotVectorGroupMember(Location? location, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotVectorGroupMember, location, typeName);
    }

    public static Diagnostic NullTypeNotVectorGroupMember(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullTypeNotVectorGroupMember, location);
    }

    public static Diagnostic TypeNotVectorGroupMemberSpecificGroup(Location? location, string typeName, string groupTypeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotVectorGroupMemberSpecificGroup, location, groupTypeName, typeName);
    }

    public static Diagnostic TypeNotQuantity(Location? location, string typeName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotQuantity, location, typeName);
    }

    public static Diagnostic NullTypeNotQuantity(Location? location)
    {
        return Diagnostic.Create(DiagnosticRules.NullTypeNotQuantity, location);
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

    public static Diagnostic TypeAlreadyDefinedAsUnit(Location? location, string typeName, string attemptedDefinition)
        => TypeAlreadyDefined(location, typeName, attemptedDefinition, "unit");
    public static Diagnostic TypeAlreadyDefinedAsScalar(Location? location, string typeName, string attemptedDefinition)
        => TypeAlreadyDefined(location, typeName, attemptedDefinition, "scalar");
    public static Diagnostic TypeAlreadyDefinedAsVector(Location? location, string typeName, string attemptedDefinition)
        => TypeAlreadyDefined(location, typeName, attemptedDefinition, "vector");
    public static Diagnostic TypeAlreadyDefinedAsVectorGroup(Location? location, string typeName, string attemptedDefinition)
        => TypeAlreadyDefined(location, typeName, attemptedDefinition, "vector group");
    public static Diagnostic TypeAlreadyDefinedAsVectorGroupMember(Location? location, string typeName, string attemptedDefinition)
        => TypeAlreadyDefined(location, typeName, attemptedDefinition, "vector group member");

    public static Diagnostic UnitTypeAlreadyDefinedAsUnit(Location? location, string typeName) => TypeAlreadyDefinedAsUnit(location, typeName, "unit");
    public static Diagnostic ScalarTypeAlreadyDefinedAsUnit(Location? location, string typeName) => TypeAlreadyDefinedAsUnit(location, typeName, "scalar");
    public static Diagnostic VectorTypeAlreadyDefinedAsUnit(Location? location, string typeName) => TypeAlreadyDefinedAsUnit(location, typeName, "vector");
    public static Diagnostic VectorGroupTypeAlreadyDefinedAsUnit(Location? location, string typeName) => TypeAlreadyDefinedAsUnit(location, typeName, "vector group");
    public static Diagnostic VectorGroupMemberTypeAlreadyDefinedAsUnit(Location? location, string typeName) => TypeAlreadyDefinedAsUnit(location, typeName, "vector group member");
    public static Diagnostic UnitTypeAlreadyDefinedAsScalar(Location? location, string typeName) => TypeAlreadyDefinedAsScalar(location, typeName, "unit");
    public static Diagnostic ScalarTypeAlreadyDefinedAsScalar(Location? location, string typeName) => TypeAlreadyDefinedAsScalar(location, typeName, "scalar");
    public static Diagnostic VectorTypeAlreadyDefinedAsScalar(Location? location, string typeName) => TypeAlreadyDefinedAsScalar(location, typeName, "vector");
    public static Diagnostic VectorGroupTypeAlreadyDefinedAsScalar(Location? location, string typeName) => TypeAlreadyDefinedAsScalar(location, typeName, "vector group");
    public static Diagnostic VectorGroupMemberTypeAlreadyDefinedAsScalar(Location? location, string typeName) => TypeAlreadyDefinedAsScalar(location, typeName, "vector group member");
    public static Diagnostic UnitTypeAlreadyDefinedAsVector(Location? location, string typeName) => TypeAlreadyDefinedAsVector(location, typeName, "unit");
    public static Diagnostic ScalarTypeAlreadyDefinedAsVector(Location? location, string typeName) => TypeAlreadyDefinedAsVector(location, typeName, "scalar");
    public static Diagnostic VectorTypeAlreadyDefinedAsVector(Location? location, string typeName) => TypeAlreadyDefinedAsVector(location, typeName, "vector");
    public static Diagnostic VectorGroupTypeAlreadyDefinedAsVector(Location? location, string typeName) => TypeAlreadyDefinedAsVector(location, typeName, "vector group");
    public static Diagnostic VectorGroupMemberTypeAlreadyDefinedAsVector(Location? location, string typeName) => TypeAlreadyDefinedAsVector(location, typeName, "vector group member");
    public static Diagnostic UnitTypeAlreadyDefinedAsVectorGroup(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroup(location, typeName, "unit");
    public static Diagnostic ScalarTypeAlreadyDefinedAsVectorGroup(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroup(location, typeName, "scalar");
    public static Diagnostic VectorTypeAlreadyDefinedAsVectorGroup(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroup(location, typeName, "vector");
    public static Diagnostic VectorGroupTypeAlreadyDefinedAsVectorGroup(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroup(location, typeName, "vector group");
    public static Diagnostic VectorGroupMemberTypeAlreadyDefinedAsVectorGroup(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroup(location, typeName, "vector group member");
    public static Diagnostic UnitTypeAlreadyDefinedAsVectorGroupMember(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroupMember(location, typeName, "unit");
    public static Diagnostic ScalarTypeAlreadyDefinedAsVectorGroupMember(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroupMember(location, typeName, "scalar");
    public static Diagnostic VectorTypeAlreadyDefinedAsVectorGroupMember(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroupMember(location, typeName, "vector");
    public static Diagnostic VectorGroupTypeAlreadyDefinedAsVectorGroupMember(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroupMember(location, typeName, "vector group");
    public static Diagnostic VectorGroupMemberTypeAlreadyDefinedAsVectorGroupMember(Location? location, string typeName) => TypeAlreadyDefinedAsVectorGroupMember(location, typeName, "vector group member");

    public static Diagnostic TypeNotUnbiasedScalar(Location? location, string scalarName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotUnbiasedScalar, location, scalarName);
    }

    public static Diagnostic TypeNotBiasedScalar(Location? location, string scalarName)
    {
        return Diagnostic.Create(DiagnosticRules.TypeNotBiasedScalar, location, scalarName);
    }

    public static Diagnostic UnitNotIncludingBiasTerm(Location? location, string unitName, string quantityName)
    {
        return Diagnostic.Create(DiagnosticRules.UnitNotIncludingBiasTerm, location, unitName, quantityName);
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
