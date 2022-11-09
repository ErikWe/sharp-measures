namespace SharpMeasures.Generators.Scalars.SourceBuilding.Common;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Optional<DataModel> data)
    {
        if (context.CancellationToken.IsCancellationRequested || data.HasValue is false)
        {
            return;
        }

        string source = Composer.Compose(data.Value);

        context.AddSource($"{data.Value.Scalar.QualifiedName}.Common.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private sealed class Composer
    {
        public static string Compose(DataModel data)
        {
            Composer composer = new(data);
            composer.Compose();
            return composer.Retrieve();
        }

        private StringBuilder Builder { get; } = new();
        private NewlineSeparationHandler SeparationHandler { get; }

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContent);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            AppendDocumentation(new Indentation(0), Data.SourceBuildingContext.Documentation.Header());
            Builder.Append(Data.Scalar.ComposeDeclaration());

            AppendInterfaces();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve() => Builder.ToString();

        private void AppendInterfaces()
        {
            List<string> interfaceNames = new()
            {
                $"global::SharpMeasures.IScalarQuantity<{Data.Scalar.FullyQualifiedName}>",
                $"global::System.IComparable<{Data.Scalar.FullyQualifiedName}>"
            };

            if (Data.Scalar.IsRecord is false)
            {
                interfaceNames.Add($"global::System.IEquatable<{Data.Scalar.FullyQualifiedName}>");
            }

            InterfaceBuilding.AppendInterfaceImplementationOnNewLines(Builder, new Indentation(1), interfaceNames);
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            AppendZero(indentation);
            AppendMagnitude(indentation);
            AppendScalarConstructor(indentation);
            AppendScalarAndUnitConstructor(indentation);
            AppendInUnit(indentation);
            AppendToString(indentation);

            if (Data.Scalar.IsRecord is false)
            {
                AppendEquality(indentation);
                AppendGetHashCode(indentation);
            }

            AppendComparisons(indentation);
            AppendWithMagnitude(indentation);

            if (Data.Unit.IsReferenceType)
            {
                AppendComputationUtilityMethod(indentation);
            }
        }

        private void AppendZero(Indentation indentation)
        {
            if (Data.UseUnitBias is false)
            {
                SeparationHandler.AddIfNecessary();
                
                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Zero());
                Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} Zero {{ get; }} = new(0);");
            }
        }

        private void AppendMagnitude(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Magnitude());
            Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar Magnitude {{ get; }}");
        }

        private void AppendScalarConstructor(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScalarConstructor());
            Builder.AppendLine($$"""
                {{indentation}}public {{Data.Scalar.Name}}(global::SharpMeasures.Scalar magnitude)
                {{indentation}}{
                {{indentation.Increased}}Magnitude = magnitude;
                {{indentation}}}
                """);
        }

        private void AppendScalarAndUnitConstructor(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScalarAndUnitConstructor());

            if (Data.Unit.IsReferenceType)
            {
                DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            }

            Builder.Append($"{indentation}public {Data.Scalar.Name}(global::SharpMeasures.Scalar magnitude, {Data.Unit.FullyQualifiedName} {Data.UnitParameterName}) ");

            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($": this(ComputeRepresentedMagnitude(magnitude, {Data.UnitParameterName})) {{ }}");
            }
            else
            {
                Builder.AppendLine($": this({ConstructorComputation(Data)}) {{ }}");
            }
        }

        private void AppendInUnit(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InUnit());

            if (Data.Unit.IsReferenceType)
            {
                DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            }

            Builder.Append($"{indentation}public global::SharpMeasures.Scalar InUnit({Data.Unit.FullyQualifiedName} {Data.UnitParameterName})");
            
            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine();

                Builder.AppendLine($$"""
                    {{indentation}}{
                    {{indentation.Increased}}{{StaticBuilding.NullArgumentGuard(Data.UnitParameterName)}}
                    
                    {{indentation.Increased}}return new({{InUnitComputation(Data)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($" => new({InUnitComputation(Data)});");
            }
        }

        private void AppendToString(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ToStringMethod());
            Builder.AppendLine($"{indentation}public override string ToString() => ToString(global::System.Globalization.CultureInfo.CurrentCulture);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ToStringFormat());
            Builder.AppendLine($"{indentation}public string ToString(string? format) => ToString(format, global::System.Globalization.CultureInfo.CurrentCulture);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ToStringProvider());
            Builder.AppendLine($"""{indentation}public string ToString(global::System.IFormatProvider? formatProvider) => ToString("G", formatProvider);""");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ToStringFormatAndProvider());
            Builder.Append($"{indentation}public string ToString(string? format, global::System.IFormatProvider? formatProvider) => ");

            if (Data.DefaultUnitInstanceSymbol is not null)
            {
                Builder.AppendLine($$"""$"{InUnit({{Data.Unit.FullyQualifiedName}}.{{Data.DefaultUnitInstanceName}}).ToString(format, formatProvider)} [{{Data.DefaultUnitInstanceSymbol}}]";""");

                return;
            }

            if (Data.DefaultUnitInstanceName is not null)
            {
                Builder.AppendLine($"InUnit({Data.Unit.FullyQualifiedName}.{Data.DefaultUnitInstanceName}).ToString(format, formatProvider);");

                return;
            }

            Builder.AppendLine("Magnitude.ToString(format, formatProvider);");
        }

        private void AppendEquality(Indentation indentation)
        {
            if (Data.Scalar.IsReferenceType)
            {
                AppendReferenceTypeEquality(indentation);
            }
            else
            {
                AppendValueTypeEquality(indentation);
            }
        }

        private void AppendReferenceTypeEquality(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            string virtualText = Data.Scalar.IsSealed ? string.Empty : " virtual";

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"{indentation}public{virtualText} bool Equals({Data.Scalar.FullyQualifiedName}? other) => other is not null && Magnitude.Value == other.Magnitude.Value;");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Scalar.FullyQualifiedName);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Scalar.FullyQualifiedName}? lhs, {Data.Scalar.FullyQualifiedName}? rhs) => lhs?.Equals(rhs) ?? rhs is null;");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Scalar.FullyQualifiedName}? lhs, {Data.Scalar.FullyQualifiedName}? rhs) => (lhs == rhs) is false;");
        }

        private void AppendValueTypeEquality(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($$"""{{indentation}}public bool Equals({{Data.Scalar.FullyQualifiedName}} other) => Magnitude.Value == other.Magnitude.Value;""");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Scalar.FullyQualifiedName);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Scalar.FullyQualifiedName} lhs, {Data.Scalar.FullyQualifiedName} rhs) => lhs.Equals(rhs);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Scalar.FullyQualifiedName} lhs, {Data.Scalar.FullyQualifiedName} rhs) => (lhs == rhs) is false;");
        }

        private void AppendGetHashCode(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GetHashCodeDocumentation());
            Builder.AppendLine($"{indentation}public override int GetHashCode() => Magnitude.GetHashCode();");
        }

        private void AppendComparisons(Indentation indentation)
        {
            if (Data.Scalar.IsReferenceType)
            {
                AppendReferenceTypeComparisons(indentation);
            }
            else
            {
                AppendValueTypeComparisons(indentation);
            }
        }

        private void AppendReferenceTypeComparisons(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.CompareToSameType());
            Builder.AppendLine($"{indentation}public int CompareTo({Data.Scalar.FullyQualifiedName}? other) => Magnitude.Value.CompareTo(other?.Magnitude.Value);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.LessThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator <({Data.Scalar.FullyQualifiedName}? x, {Data.Scalar.FullyQualifiedName}? y) " +
                $"=> x?.Magnitude.Value < y?.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GreaterThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator >({Data.Scalar.FullyQualifiedName}? x, {Data.Scalar.FullyQualifiedName}? y) " +
                $"=> x?.Magnitude.Value > y?.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.LessThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator <=({Data.Scalar.FullyQualifiedName}? x, {Data.Scalar.FullyQualifiedName}? y) " +
                $"=> x?.Magnitude.Value <= y?.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GreaterThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator >=({Data.Scalar.FullyQualifiedName}? x, {Data.Scalar.FullyQualifiedName}? y) " +
                $"=> x?.Magnitude.Value >= y?.Magnitude.Value;");
        }

        private void AppendValueTypeComparisons(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.CompareToSameType());
            Builder.AppendLine($"{indentation}public int CompareTo({Data.Scalar.FullyQualifiedName} other) => Magnitude.Value.CompareTo(other.Magnitude.Value);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.LessThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator <({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                $"=> x.Magnitude.Value < y.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GreaterThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator >({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                $"=> x.Magnitude.Value > y.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.LessThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator <=({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                $"=> x.Magnitude.Value <= y.Magnitude.Value;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GreaterThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator >=({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                $"=> x.Magnitude.Value >= y.Magnitude.Value;");
        }

        private void AppendWithMagnitude(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.WithMagnitude());
            Builder.AppendLine($"{indentation}static {Data.Scalar.FullyQualifiedName} global::SharpMeasures.IScalarQuantity<{Data.Scalar.FullyQualifiedName}>.WithMagnitude(global::SharpMeasures.Scalar magnitude) => new(magnitude);");
        }

        private void AppendComputationUtilityMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            Builder.AppendLine($$"""
                {{indentation}}/// <summary>Computes the represented magnitude based on a magnitude, <paramref name="magnitude"/>, expressed in a certain unit <paramref name="{{Data.UnitParameterName}}"/>.</summary>
                {{indentation}}/// <param name="magnitude">The magnitude expressed in a certain unit <paramref name="{{Data.UnitParameterName}}"/>.</param>
                {{indentation}}/// <param name="{{Data.UnitParameterName}}">The <see cref="{{Data.Unit.FullyQualifiedName}}"/> in which <paramref name="magnitude"/> is expressed.</param>
                """);

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, "private static global::SharpMeasures.Scalar ComputeRepresentedMagnitude",
                ConstructorComputation(Data), (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "magnitude"), (Data.Unit, Data.UnitParameterName));
        }

        private static string ConstructorComputation(DataModel data)
        {
            string unitMagnitude = $"{data.UnitParameterName}.{data.UnitQuantity.Name}.Magnitude";

            if (data.UseUnitBias)
            {
                return $"(magnitude - {data.UnitParameterName}.Bias) * {unitMagnitude}";
            }

            return $"magnitude * {unitMagnitude}";
        }

        private static string InUnitComputation(DataModel data)
        {
            string scaled = $"Magnitude / {data.UnitParameterName}.{data.UnitQuantity.Name}.Magnitude";

            if (data.UseUnitBias)
            {
                return $"{scaled} + {data.UnitParameterName}.Bias";
            }

            return scaled;
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
