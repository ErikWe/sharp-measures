﻿namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

internal class VectorFileDocumentation : IVectorDocumentationStrategy, IEquatable<VectorFileDocumentation>
{
    private VectorDocumentationTags DocumentationTags { get; }

    private DocumentationFile DocumentationFile { get; }
    private IVectorDocumentationStrategy DefaultDocumentationStrategy { get; }

    public VectorFileDocumentation(VectorDataModel model, DocumentationFile documentationFile, IVectorDocumentationStrategy defaultDocumentationStrategy)
    {
        DocumentationTags = new(model.Vector.Dimension);

        DocumentationFile = documentationFile;
        DefaultDocumentationStrategy = defaultDocumentationStrategy;
    }

    public string Header() => FromFileOrDefault(static (strategy) => strategy.Header());

    public string Zero() => FromFileOrDefault(static (strategy) => strategy.Zero());
    public string Constant(IVectorConstant constant) => FromFileOrDefault((strategy) => strategy.Constant(constant));

    public string WithScalarComponents() => FromFileOrDefault(static (strategy) => strategy.WithScalarComponents());
    public string WithVectorComponents() => FromFileOrDefault(static (strategy) => strategy.WithVectorComponents());

    public string ComponentsConstructor() => FromFileOrDefault(static (strategy) => strategy.ComponentsConstructor());
    public string ScalarsConstructor() => FromFileOrDefault(static (strategy) => strategy.ScalarsConstructor());
    public string VectorConstructor() => FromFileOrDefault(static (strategy) => strategy.VectorConstructor());

    public string ScalarsAndUnitConstructor() => FromFileOrDefault(static (strategy) => strategy.ScalarsAndUnitConstructor());
    public string VectorAndUnitConstructor() => FromFileOrDefault(static (strategy) => strategy.VectorAndUnitConstructor());

    public string CastFromComponents() => FromFileOrDefault(static (strategy) => strategy.CastFromComponents());

    public string Component(int componentIndex) => FromFileOrDefault((strategy) => strategy.Component(componentIndex));
    public string ComponentMagnitude(int componentIndex) => FromFileOrDefault((strategy) => strategy.ComponentMagnitude(componentIndex));
    public string Components() => FromFileOrDefault(static (strategy) => strategy.Components());

    public string InUnit() => FromFileOrDefault(static (strategy) => strategy.InUnit());
    public string InConstantMultiples(IVectorConstant constant) => FromFileOrDefault((strategy) => strategy.InConstantMultiples(constant));
    public string InSpecifiedUnit(IUnitInstance unitInstance) => FromFileOrDefault((strategy) => strategy.InSpecifiedUnit(unitInstance));

    public string Conversion(NamedType vector) => FromFileOrDefault((strategy) => strategy.Conversion(vector));
    public string AntidirectionalConversion(NamedType vector) => FromFileOrDefault((strategy) => strategy.AntidirectionalConversion(vector));
    public string CastConversion(NamedType vector) => FromFileOrDefault((strategy) => strategy.CastConversion(vector));
    public string AntidirectionalCastConversion(NamedType vector) => FromFileOrDefault((strategy) => strategy.AntidirectionalCastConversion(vector));

    public string Derivation(DerivedQuantitySignature signature, IReadOnlyList<string> parameterNames) => FromFileOrDefault((strategy) => strategy.Derivation(signature, parameterNames));
    public string OperatorDerivation(OperatorDerivation derivation) => FromFileOrDefault((strategy) => strategy.OperatorDerivation(derivation));

    public string IsNaN() => FromFileOrDefault(static (strategy) => strategy.IsNaN());
    public string IsZero() => FromFileOrDefault(static (strategy) => strategy.IsZero());
    public string IsFinite() => FromFileOrDefault(static (strategy) => strategy.IsFinite());
    public string IsInfinite() => FromFileOrDefault(static (strategy) => strategy.IsInfinite());

    public string Magnitude() => FromFileOrDefault(static (strategy) => strategy.Magnitude());
    public string SquaredMagnitude() => FromFileOrDefault(static (strategy) => strategy.SquaredMagnitude());

    public string ScalarMagnitude() => FromFileOrDefault(static (strategy) => strategy.ScalarMagnitude());
    public string ScalarSquaredMagnitude() => FromFileOrDefault(static (strategy) => strategy.ScalarSquaredMagnitude());

    public string Normalize() => FromFileOrDefault(static (strategy) => strategy.Normalize());
    public string Transform() => FromFileOrDefault(static (strategy) => strategy.Transform());

    public string ToStringDocumentation() => FromFileOrDefault(static (strategy) => strategy.ToStringDocumentation());

    public string EqualsSameTypeMethod() => FromFileOrDefault(static (strategy) => strategy.EqualsSameTypeMethod());
    public string EqualsObjectMethod() => FromFileOrDefault(static (strategy) => strategy.EqualsObjectMethod());

    public string EqualitySameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.EqualitySameTypeOperator());
    public string InequalitySameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.InequalitySameTypeOperator());

    public string GetHashCodeDocumentation() => FromFileOrDefault(static (strategy) => strategy.GetHashCodeDocumentation());

    public string Deconstruct() => FromFileOrDefault(static (strategy) => strategy.Deconstruct());

    public string UnaryPlusMethod() => FromFileOrDefault(static (strategy) => strategy.UnaryPlusMethod());
    public string NegateMethod() => FromFileOrDefault(static (strategy) => strategy.NegateMethod());

    public string AddSameTypeMethod() => FromFileOrDefault(static (strategy) => strategy.AddSameTypeMethod());
    public string SubtractSameTypeMethod() => FromFileOrDefault(static (strategy) => strategy.SubtractSameTypeMethod());

    public string AddDifferenceMethod() => FromFileOrDefault(static (strategy) => strategy.AddDifferenceMethod());
    public string SubtractDifferenceMethod() => FromFileOrDefault(static (strategy) => strategy.SubtractDifferenceMethod());

    public string MultiplyScalarMethod() => FromFileOrDefault(static (strategy) => strategy.MultiplyScalarMethod());
    public string DivideScalarMethod() => FromFileOrDefault(static (strategy) => strategy.DivideScalarMethod());

    public string UnaryPlusOperator() => FromFileOrDefault(static (strategy) => strategy.UnaryPlusOperator());
    public string NegateOperator() => FromFileOrDefault(static (strategy) => strategy.NegateOperator());

    public string AddSameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.AddSameTypeOperator());
    public string SubtractSameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.SubtractSameTypeOperator());

    public string AddDifferenceOperatorLHS() => FromFileOrDefault(static (strategy) => strategy.AddDifferenceOperatorLHS());
    public string AddDifferenceOperatorRHS() => FromFileOrDefault(static (strategy) => strategy.AddDifferenceOperatorRHS());
    public string SubtractDifferenceOperatorLHS() => FromFileOrDefault(static (strategy) => strategy.SubtractDifferenceOperatorLHS());

    public string MultiplyScalarOperatorLHS() => FromFileOrDefault(static (strategy) => strategy.MultiplyScalarOperatorLHS());
    public string MultiplyScalarOperatorRHS() => FromFileOrDefault(static (strategy) => strategy.MultiplyScalarOperatorRHS());
    public string DivideScalarOperatorLHS() => FromFileOrDefault(static (strategy) => strategy.DivideScalarOperatorLHS());

    private string FromFileOrDefault(Func<IVectorDocumentationStrategy, string> defaultDelegate)
    {
        string tag = defaultDelegate(DocumentationTags);

        if (DocumentationFile.OptionallyResolveTag(tag) is not string { Length: > 0 } tagContent)
        {
            tagContent = defaultDelegate(DefaultDocumentationStrategy);
        }

        return tagContent;
    }

    public virtual bool Equals(VectorFileDocumentation? other) => other is not null && DocumentationTags == other.DocumentationTags
        && DocumentationFile == other.DocumentationFile && DefaultDocumentationStrategy.Equals(other.DefaultDocumentationStrategy);

    public override bool Equals(object? obj) => obj is VectorFileDocumentation other && Equals(other);

    public static bool operator ==(VectorFileDocumentation? lhs, VectorFileDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(VectorFileDocumentation? lhs, VectorFileDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (DocumentationTags, DocumentationFile, DefaultDocumentationStrategy).GetHashCode();
}
