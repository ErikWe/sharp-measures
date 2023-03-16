namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateQuantityOperation
{
    [Fact]
    public Task VerifyDuplicateQuantityOperation_Method() => AssertQuantityOperationName_Scalar(NameWithName).VerifyDiagnostics();

    [Fact]
    public Task VerifyDuplicateQuantityOperation_Operator() => AssertQuantityOperationOperator_Scalar(NameWithName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void QuantityOperationName_Scalar(Names names) => AssertQuantityOperationName_Scalar(names);

    [Fact]
    public void QuantityOperationOperator_Scalar() => AssertQuantityOperationOperator_Scalar(NameWithName);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void QuantityOperationName_SpecializedScalar(Names names) => AssertQuantityOperationName_SpecializedScalar(names);

    [Fact]
    public void QuantityOperationOperator_SpecializedScalar() => AssertQuantityOperationName_SpecializedScalar(NameWithName);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void VectorOperation_Vector(Names names) => AssertVectorOperation_Vector(names);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SharedOperation_Vector(Names names) => AssertSharedOperation_Vector(names);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void VectorOperation_SpecializedVector(Names names) => AssertVectorOperation_SpecializedVector(names);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SharedOperation_SpecializedVector(Names names) => AssertSharedOperation_SpecializedVector(names);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void VectorOperation_VectorGroup(Names names) => AssertVectorOperation_VectorGroup(names);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SharedOperation_VectorGroup(Names names) => AssertSharedOperation_VectorGroup(names);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void VectorOperation_SpecializedVectorGroup(Names operationName) => AssertVectorOperation_SpecializedVectorGroup(operationName);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SharedOperation_SpecializedVectorGroup(Names operationName) => AssertSharedOperation_SpecializedVectorGroup(operationName);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void VectorOperation_VectorGroupMember(Names operationName) => AssertVectorOperation_VectorGroupMember(operationName);

    [Theory]
    [MemberData(nameof(DuplicateNames))]
    public void SharedOperation_VectorGroupMember(Names operationName) => AssertSharedOperation_VectorGroupMember(operationName);

    public static IEnumerable<object[]> DuplicateNames() => new object[][]
    {
        new object[] { NameWithName },
        new object[] { MirroredNameWithName },
        new object[] { NameWithMirroredName },
        new object[] { MirroredNameWithMirroredName },
        new object[] { Both }
    };

    private static Names NameWithName { get; } = new("A", "B", "A", "C", DiagnosticsTarget.Name);
    private static Names MirroredNameWithName { get; } = new("A", "B", "C", "A", DiagnosticsTarget.MirroredName);
    private static Names NameWithMirroredName { get; } = new("A", "B", "B", "C", DiagnosticsTarget.Name);
    private static Names MirroredNameWithMirroredName { get; } = new("A", "B", "C", "B", DiagnosticsTarget.MirroredName);
    private static Names Both { get; } = new("A", "B", "A", "B", DiagnosticsTarget.Name);

    public readonly record struct Names(string FirstName, string FirstMirroredName, string SecondName, string SecondMirroredName, DiagnosticsTarget Target);
    public enum DiagnosticsTarget { Name, MirroredName }

    private static TextSpan GetExpectedQuantityOperationLocation(string source, Names names)
    {
        if (names.Target is DiagnosticsTarget.Name)
        {
            return ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{names.SecondName}\"", postfix: $", MirroredMethodName = \"{names.SecondMirroredName}\")] // <-");
        }

        return ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{names.SecondMirroredName}\"", postfix: ")] // <-");
    }

    private static TextSpan GetExpectedVectorOperationLocation(string source, Names names)
    {
        if (names.Target is DiagnosticsTarget.Name)
        {
            return ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{names.SecondName}\"", postfix: $", MirroredName = \"{names.SecondMirroredName}\")] // <-");
        }

        return ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{names.SecondMirroredName}\"", postfix: ")] // <-");
    }

    private static GeneratorVerifier AssertDuplicateQuantityOperationDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateQuantityOperationDiagnostics);
    private static IReadOnlyCollection<string> DuplicateQuantityOperationDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateQuantityOperation };

    private static string QuantityOperationNameText_Scalar(Names names) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Division, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Subtraction, MethodName = "{{names.SecondName}}", MirroredMethodName = "{{names.SecondMirroredName}}")] // <-
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Other { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationName_Scalar(Names names)
    {
        var source = QuantityOperationNameText_Scalar(names);
        var expectedLocation = GetExpectedQuantityOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(names));
    }

    private static string QuantityOperationOperatorText_Scalar(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Division, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Division)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Other { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationOperator_Scalar(Names names)
    {
        var source = QuantityOperationOperatorText_Scalar(names);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "QuantityOperation", postfix: "(typeof(Other), typeof(Other), OperatorType.Division)]");

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical(names));
    }

    private static string QuantityOperationNameText_SpecializedScalar(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Division, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Subtraction, MethodName = "{{names.SecondName}}", MirroredMethodName = "{{names.SecondMirroredName}}")] // <-
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Other { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationName_SpecializedScalar(Names names)
    {
        var source = QuantityOperationNameText_SpecializedScalar(names);
        var expectedLocation = GetExpectedQuantityOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(names));
    }

    private static string QuantityOperationOperatorText_SpecializedScalar(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Division, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Division)]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Other { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertQuantityOperationOperator_SpecializedScalar(Names names)
    {
        var source = QuantityOperationOperatorText_SpecializedScalar(names);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "QuantityOperation", postfix: "(typeof(Other), typeof(Other), OperatorType.Division)]");

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical(names));
    }

    private static string VectorOperationText_Vector(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_Vector(Names names)
    {
        var source = VectorOperationText_Vector(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical_VectorOperation(names));
    }

    private static string SharedOperationText_Vector(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other3), typeof(Other3), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSharedOperation_Vector(Names names)
    {
        var source = SharedOperationText_Vector(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical_QuantityOperation(names));
    }

    private static string VectorOperationText_SpecializedVector(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_SpecializedVector(Names names)
    {
        var source = VectorOperationText_SpecializedVector(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical_VectorOperation(names));
    }

    private static string SharedOperationText_SpecializedVector(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other3), typeof(Other3), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSharedOperation_SpecializedVector(Names names)
    {
        var source = SharedOperationText_SpecializedVector(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical_QuantityOperation(names));
    }

    private static string VectorOperationText_VectorGroup(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Other), typeof(Other), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other), typeof(Other), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Other { }

        [VectorGroupMember(typeof(Other))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_VectorGroup(Names names)
    {
        var source = VectorOperationText_VectorGroup(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical_VectorOperation(names));
    }

    private static string SharedOperationText_VectorGroup(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other), typeof(Other), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Other { }
        
        [VectorGroupMember(typeof(Other))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSharedOperation_VectorGroup(Names names)
    {
        var source = SharedOperationText_VectorGroup(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical_QuantityOperation(names));
    }

    private static string VectorOperationText_SpecializedVectorGroup(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Other), typeof(Other), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other), typeof(Other), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Other { }
        
        [VectorGroupMember(typeof(Other))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_SpecializedVectorGroup(Names names)
    {
        var source = VectorOperationText_SpecializedVectorGroup(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical_VectorOperator(names));
    }

    private static string SharedOperationText_SpecializedVectorGroup(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other), typeof(Other), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Other { }
        
        [VectorGroupMember(typeof(Other))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSharedOperation_SpecializedVectorGroup(Names names)
    {
        var source = SharedOperationText_SpecializedVectorGroup(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical_QuantityOperator(names));
    }

    private static string VectorOperationText_VectorGroupMember(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorOperation_VectorGroupMember(Names names)
    {
        var source = VectorOperationText_VectorGroupMember(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical_VectorOperator(names));
    }

    private static string SharedOperationText_VectorGroupMember(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other3), typeof(Other3), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.SecondName}}", MirroredName = "{{names.SecondMirroredName}}")] // <-
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSharedOperation_VectorGroupMember(Names names)
    {
        var source = SharedOperationText_VectorGroupMember(names);
        var expectedLocation = GetExpectedVectorOperationLocation(source, names);

        return AssertDuplicateQuantityOperationDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical_QuantityOperator(names));
    }

    private static GeneratorVerifier ScalarIdentical(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText(names));
    private static GeneratorVerifier SpecializedScalarIdentical(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText(names));
    private static GeneratorVerifier VectorIdentical_QuantityOperation(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText_QuantityOperation(names));
    private static GeneratorVerifier VectorIdentical_VectorOperation(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText_VectorOperation(names));
    private static GeneratorVerifier SpecializedVectorIdentical_QuantityOperation(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText_QuantityOperation(names));
    private static GeneratorVerifier SpecializedVectorIdentical_VectorOperation(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText_VectorOperation(names));
    private static GeneratorVerifier VectorGroupIdentical_QuantityOperation(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText_QuantityOperation(names));
    private static GeneratorVerifier VectorGroupIdentical_VectorOperation(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText_VectorOperation(names));
    private static GeneratorVerifier SpecializedVectorGroupIdentical_QuantityOperator(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText_QuantityOperator(names));
    private static GeneratorVerifier SpecializedVectorGroupIdentical_VectorOperator(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText_VectorOperator(names));
    private static GeneratorVerifier VectorGroupMemberIdentical_QuantityOperator(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText_QuantityOperator(names));
    private static GeneratorVerifier VectorGroupMemberIdentical_VectorOperator(Names names) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText_VectorOperator(names));

    private static string ScalarIdenticalText(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Division, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Other { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Division, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Other { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText_QuantityOperation(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other3), typeof(Other3), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText_VectorOperation(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText_QuantityOperation(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other3), typeof(Other3), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText_VectorOperation(Names names) => $$"""
        using SharpMeasures.Generators;

        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText_QuantityOperation(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Other { }
        
        [VectorGroupMember(typeof(Other))]
        public partial class Other3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText_VectorOperation(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Other), typeof(Other), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Other { }
        
        [VectorGroupMember(typeof(Other))]
        public partial class Other3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText_QuantityOperator(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other), typeof(Other), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Other { }
        
        [VectorGroupMember(typeof(Other))]
        public partial class Other3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText_VectorOperator(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Other), typeof(Other), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Other { }
        
        [VectorGroupMember(typeof(Other))]
        public partial class Other3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText_QuantityOperator(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Other3), typeof(Other3), OperatorType.Subtraction, MethodName = "{{names.FirstName}}", MirroredMethodName = "{{names.FirstMirroredName}}")]
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText_VectorOperator(Names names) => $$"""
        using SharpMeasures.Generators;
        
        [VectorOperation(typeof(Other3), typeof(Other3), VectorOperatorType.Cross, Name = "{{names.FirstName}}", MirroredName = "{{names.FirstMirroredName}}")]
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Other3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
