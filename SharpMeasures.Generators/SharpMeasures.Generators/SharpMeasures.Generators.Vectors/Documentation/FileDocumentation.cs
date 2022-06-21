namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

using System;

internal class FileDocumentation : IDocumentationStrategy, IEquatable<FileDocumentation>
{
    private DocumentationTags DocumentationTags { get; }

    private DocumentationFile DocumentationFile { get; }
    private IDocumentationStrategy DefaultDocumentationStrategy { get; }

    public FileDocumentation(int dimension, DocumentationFile documentationFile, IDocumentationStrategy defaultDocumentationStrategy)
    {
        DocumentationTags = new(dimension);

        DocumentationFile = documentationFile;
        DefaultDocumentationStrategy = defaultDocumentationStrategy;
    }

    public string Header() => FromFileOrDefault(static (strategy) => strategy.Header());

    public string Zero() => FromFileOrDefault(static (strategy) => strategy.Zero());
    public string Constant(RefinedVectorConstantDefinition definition) => FromFileOrDefault((strategy) => strategy.Constant(definition));

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
    public string InConstantMultiples(RefinedVectorConstantDefinition definition) => FromFileOrDefault((strategy) => strategy.InConstantMultiples(definition));
    public string InSpecifiedUnit(UnitInstance unitInstance) => FromFileOrDefault((strategy) => strategy.InSpecifiedUnit(unitInstance));

    public string AsDimensionallyEquivalent(IVectorInterface vector) => FromFileOrDefault((strategy) => strategy.AsDimensionallyEquivalent(vector));
    public string CastToDimensionallyEquivalent(IVectorInterface vector) => FromFileOrDefault((strategy) => strategy.CastToDimensionallyEquivalent(vector));

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

    public string MultiplyScalarMethod() => FromFileOrDefault(static (strategy) => strategy.MultiplyScalarMethod());
    public string DivideScalarMethod() => FromFileOrDefault(static (strategy) => strategy.DivideScalarMethod());

    public string UnaryPlusOperator() => FromFileOrDefault(static (strategy) => strategy.UnaryPlusOperator());
    public string NegateOperator() => FromFileOrDefault(static (strategy) => strategy.NegateOperator());

    public string MultiplyScalarOperatorLHS() => FromFileOrDefault(static (strategy) => strategy.MultiplyScalarOperatorLHS());
    public string MultiplyScalarOperatorRHS() => FromFileOrDefault(static (strategy) => strategy.MultiplyScalarOperatorRHS());
    public string DivideScalarOperatorLHS() => FromFileOrDefault(static (strategy) => strategy.DivideScalarOperatorLHS());

    private string FromFileOrDefault(Func<IDocumentationStrategy, string> defaultDelegate)
    {
        string tag = defaultDelegate(DocumentationTags);

        if (DocumentationFile.OptionallyResolveTag(tag) is not string { Length: > 0 } tagContent)
        {
            tagContent = defaultDelegate(DefaultDocumentationStrategy);
        }

        return tagContent;
    }

    public virtual bool Equals(FileDocumentation? other) => other is not null && DocumentationTags == other.DocumentationTags
        && DocumentationFile == other.DocumentationFile && DefaultDocumentationStrategy.Equals(other.DefaultDocumentationStrategy);

    public override bool Equals(object? obj) => obj is FileDocumentation other && Equals(other);

    public static bool operator ==(FileDocumentation? lhs, FileDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(FileDocumentation? lhs, FileDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (DocumentationTags, DocumentationFile, DefaultDocumentationStrategy).GetHashCode();
}
