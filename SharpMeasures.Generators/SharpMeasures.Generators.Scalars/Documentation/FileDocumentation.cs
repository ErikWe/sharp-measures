namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units;

using System;

internal class FileDocumentation : IDocumentationStrategy, IEquatable<FileDocumentation>
{
    private DocumentationFile DocumentationFile { get; }
    private IDocumentationStrategy DefaultDocumentationStrategy { get; }

    public FileDocumentation(DocumentationFile documentationFile, IDocumentationStrategy defaultDocumentationStrategy)
    {
        DocumentationFile = documentationFile;
        DefaultDocumentationStrategy = defaultDocumentationStrategy;
    }

    public string Header() => FromFileOrDefault(static (strategy) => strategy.Header());

    public string Zero() => FromFileOrDefault(static (strategy) => strategy.Zero());
    public string Constant(IScalarConstant constant) => FromFileOrDefault((strategy) => strategy.Constant(constant));
    public string UnitBase(IUnitInstance unitInstance) => FromFileOrDefault((strategy) => strategy.UnitBase(unitInstance));

    public string WithMagnitude() => FromFileOrDefault(static (strategy) => strategy.WithMagnitude());

    public string FromReciprocal() => FromFileOrDefault(static (strategy) => strategy.FromReciprocal());
    public string FromSquare() => FromFileOrDefault(static (strategy) => strategy.FromSquare());
    public string FromCube() => FromFileOrDefault(static (strategy) => strategy.FromCube());
    public string FromSquareRoot() => FromFileOrDefault(static (strategy) => strategy.FromSquareRoot());
    public string FromCubeRoot() => FromFileOrDefault(static (strategy) => strategy.FromCubeRoot());

    public string Magnitude() => FromFileOrDefault(static (strategy) => strategy.Magnitude());

    public string ScalarConstructor() => FromFileOrDefault(static (strategy) => strategy.ScalarConstructor());
    public string ScalarAndUnitConstructor() => FromFileOrDefault(static (strategy) => strategy.ScalarAndUnitConstructor());

    public string InUnit() => FromFileOrDefault(static (strategy) => strategy.InUnit());
    public string InConstantMultiples(IScalarConstant constant) => FromFileOrDefault((strategy) => strategy.InConstantMultiples(constant));
    public string InSpecifiedUnit(IUnitInstance unitInstance) => FromFileOrDefault((strategy) => strategy.InSpecifiedUnit(unitInstance));

    public string Conversion(NamedType scalar) => FromFileOrDefault((strategy) => strategy.Conversion(scalar));
    public string AntidirectionalConversion(NamedType scalar) => FromFileOrDefault((strategy) => strategy.AntidirectionalConversion(scalar));
    public string CastConversion(NamedType scalar) => FromFileOrDefault((strategy) => strategy.CastConversion(scalar));
    public string AntidirectionalCastConversion(NamedType scalar) => FromFileOrDefault((strategy) => strategy.AntidirectionalCastConversion(scalar));

    public string IsNaN() => FromFileOrDefault(static (strategy) => strategy.IsNaN());
    public string IsZero() => FromFileOrDefault(static (strategy) => strategy.IsZero());
    public string IsPositive() => FromFileOrDefault(static (strategy) => strategy.IsPositive());
    public string IsNegative() => FromFileOrDefault(static (strategy) => strategy.IsNegative());
    public string IsFinite() => FromFileOrDefault(static (strategy) => strategy.IsFinite());
    public string IsInfinite() => FromFileOrDefault(static (strategy) => strategy.IsInfinite());
    public string IsPositiveInfinity() => FromFileOrDefault(static (strategy) => strategy.IsPositiveInfinity());
    public string IsNegativeInfinity() => FromFileOrDefault(static (strategy) => strategy.IsNegativeInfinity());

    public string Absolute() => FromFileOrDefault(static (strategy) => strategy.Absolute());
    public string Sign() => FromFileOrDefault(static (strategy) => strategy.Sign());

    public string Reciprocal() => FromFileOrDefault(static (strategy) => strategy.Reciprocal());
    public string Square() => FromFileOrDefault(static (strategy) => strategy.Square());
    public string Cube() => FromFileOrDefault(static (strategy) => strategy.Cube());
    public string SquareRoot() => FromFileOrDefault(static (strategy) => strategy.SquareRoot());
    public string CubeRoot() => FromFileOrDefault(static (strategy) => strategy.CubeRoot());

    public string ToStringDocumentation() => FromFileOrDefault(static (strategy) => strategy.ToStringDocumentation());

    public string EqualsSameTypeMethod() => FromFileOrDefault(static (strategy) => strategy.EqualsSameTypeMethod());
    public string EqualsObjectMethod() => FromFileOrDefault(static (strategy) => strategy.EqualsObjectMethod());

    public string EqualitySameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.EqualitySameTypeOperator());
    public string InequalitySameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.InequalitySameTypeOperator());

    public string GetHashCodeDocumentation() => FromFileOrDefault(static (strategy) => strategy.GetHashCodeDocumentation());

    public string UnaryPlusMethod() => FromFileOrDefault(static (strategy) => strategy.UnaryPlusMethod());
    public string NegateMethod() => FromFileOrDefault(static (strategy) => strategy.NegateMethod());

    public string AddSameTypeMethod() => FromFileOrDefault(static (strategy) => strategy.AddSameTypeMethod());
    public string SubtractSameTypeMethod() => FromFileOrDefault(static (strategy) => strategy.SubtractSameTypeMethod());

    public string AddDifferenceMethod() => FromFileOrDefault(static (strategy) => strategy.AddDifferenceMethod());
    public string SubtractDifferenceMethod() => FromFileOrDefault(static (strategy) => strategy.SubtractDifferenceMethod());

    public string MultiplyScalarMethod() => FromFileOrDefault(static (strategy) => strategy.MultiplyScalarMethod());
    public string DivideScalarMethod() => FromFileOrDefault(static (strategy) => strategy.DivideScalarMethod());

    public string MultiplySameTypeMethod() => FromFileOrDefault(static (strategy) => strategy.MultiplySameTypeMethod());
    public string DivideSameTypeMethod() => FromFileOrDefault(static (strategy) => strategy.DivideSameTypeMethod());

    public string MultiplyVectorMethod(int dimension) => FromFileOrDefault((strategy) => strategy.MultiplyVectorMethod(dimension));

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
    public string DivideScalarOperatorRHS() => FromFileOrDefault(static (strategy) => strategy.DivideScalarOperatorRHS());

    public string MultiplySameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.MultiplySameTypeOperator());
    public string DivideSameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.DivideSameTypeOperator());

    public string MultiplyVectorOperatorLHS(int dimension) => FromFileOrDefault((strategy) => strategy.MultiplyVectorOperatorLHS(dimension));
    public string MultiplyVectorOperatorRHS(int dimension) => FromFileOrDefault((strategy) => strategy.MultiplyVectorOperatorRHS(dimension));

    public string CompareToSameType() => FromFileOrDefault(static (strategy) => strategy.CompareToSameType());

    public string LessThanSameType() => FromFileOrDefault(static (strategy) => strategy.LessThanSameType());
    public string GreaterThanSameType() => FromFileOrDefault(static (strategy) => strategy.GreaterThanSameType());
    public string LessThanOrEqualSameType() => FromFileOrDefault(static (strategy) => strategy.LessThanOrEqualSameType());
    public string GreaterThanOrEqualSameType() => FromFileOrDefault(static (strategy) => strategy.GreaterThanOrEqualSameType());

    private string FromFileOrDefault(Func<IDocumentationStrategy, string> defaultDelegate)
    {
        string tag = defaultDelegate(DocumentationTags.Instance);

        if (DocumentationFile.OptionallyResolveTag(tag) is not string { Length: > 0 } tagContent)
        {
            tagContent = defaultDelegate(DefaultDocumentationStrategy);
        }

        return tagContent;
    }

    public virtual bool Equals(FileDocumentation? other) => other is not null && DocumentationFile == other.DocumentationFile
        && DefaultDocumentationStrategy.Equals(other.DefaultDocumentationStrategy);

    public override bool Equals(object? obj) => obj is FileDocumentation other && Equals(other);

    public static bool operator ==(FileDocumentation? lhs, FileDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(FileDocumentation? lhs, FileDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (DocumentationFile, DefaultDocumentationStrategy).GetHashCode();
}
