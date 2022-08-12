namespace SharpMeasures.Generators.Scalars.Pipelines.Common;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Scalar.Name}_Common.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string Compose(DataModel data)
        {
            Composer composer = new(data);
            composer.Compose();
            return composer.Retrieve();
        }

        private StringBuilder Builder { get; } = new();

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            AppendDocumentation(new Indentation(0), Data.Documentation.Header());
            Builder.Append(Data.Scalar.ComposeDeclaration());

            AppendInterfaces();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void AppendInterfaces()
        {
            List<string> interfaceNames = new()
            {
                $"global::System.IComparable<{Data.Scalar.FullyQualifiedName}>",
                $"global::SharpMeasures.IScalarQuantity<{Data.Scalar.FullyQualifiedName}>"
            };

            if (Data.Scalar.IsRecord is false)
            {
                interfaceNames.Add($"global::System.IEquatable<{Data.Scalar.FullyQualifiedName}>");
            }

            InterfaceBuilding.AppendInterfaceImplementationOnNewLines(Builder, new Indentation(1), interfaceNames);
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            if (Data.UseUnitBias is false)
            {
                AppendDocumentation(indentation, Data.Documentation.Zero());
                Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} Zero {{ get; }} = new(0);");

                Builder.AppendLine();
            }

            AppendDocumentation(indentation, Data.Documentation.Magnitude());
            Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar Magnitude {{ get; }}");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.ScalarConstructor());
            Builder.AppendLine($$"""
                {{indentation}}public {{Data.Scalar.Name}}(global::SharpMeasures.Scalar magnitude)
                {{indentation}}{
                {{indentation.Increased}}Magnitude = magnitude;
                {{indentation}}}
                """);

            Builder.AppendLine();

            ComposeScalarAndUnitConstructor(indentation);

            Builder.AppendLine();

            ComposeInUnit(indentation);

            Builder.AppendLine();

            ComposeToString(indentation);

            Builder.AppendLine();

            if (Data.Scalar.IsRecord is false)
            {
                ComposeEquality(indentation);

                Builder.AppendLine();

                ComposeGetHashCode(indentation);

                Builder.AppendLine();
            }

            ComposeComparisons(indentation);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.WithMagnitude());
            Builder.AppendLine($"{indentation}static {Data.Scalar.FullyQualifiedName} global::SharpMeasures.IScalarQuantity<{Data.Scalar.FullyQualifiedName}>.WithMagnitude(global::SharpMeasures.Scalar magnitude) => new(magnitude);");

            Builder.AppendLine();

            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <summary>Computes the represented magnitude based on a magnitude, <paramref name="magnitude"/>, expressed in
                    {{indentation}}/// a certain unit <paramref name="{{Data.UnitParameterName}}"/>.</summary>
                    {{indentation}}/// <param name="magnitude">The magnitude expressed in a certain unit <paramref name="{{Data.UnitParameterName}}"/>.</param>
                    {{indentation}}/// <param name="{{Data.UnitParameterName}}">The <see cref="{{Data.Unit.FullyQualifiedName}}"/> in which <paramref name="magnitude"/> is expressed.</param>
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}private static global::SharpMeasures.Scalar ComputeRepresentedMagnitude(global::SharpMeasures.Scalar magnitude, {{Data.Unit.FullyQualifiedName}} {{Data.UnitParameterName}})
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull({{Data.UnitParameterName}});
                    
                    {{indentation.Increased}}return {{ConstructorComputation(Data)}};
                    {{indentation}}}
                    """);
            }
        }

        private void ComposeScalarAndUnitConstructor(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.ScalarAndUnitConstructor());

            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($"""{indentation}/// <exception cref="global::System.ArgumentNullException"/>""");
            }

            Builder.AppendLine($"{indentation}public {Data.Scalar.Name}(global::SharpMeasures.Scalar magnitude, {Data.Unit.FullyQualifiedName} {Data.UnitParameterName})");

            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($"{indentation.Increased}: this(ComputeRepresentedMagnitude(magnitude, {Data.UnitParameterName})) {{ }}");
            }
            else
            {
                Builder.AppendLine($"{indentation.Increased}: this({ConstructorComputation(Data)}) {{ }}");
            }
        }

        private void ComposeInUnit(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.InUnit());

            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($"""{indentation}/// <exception cref="global::System.ArgumentNullException"/>""");
            }

            Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar InUnit({Data.Unit.FullyQualifiedName} {Data.UnitParameterName})");
            
            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull({{Data.UnitParameterName}}); 
                    
                    {{indentation.Increased}}return new({{InUnitComputation(Data)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation.Increased}=> new({InUnitComputation(Data)});");
            }
        }

        private void ComposeToString(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.ToStringDocumentation());
            Builder.Append($$"""{{indentation}}public override string ToString() => $"{typeof({{Data.Scalar.FullyQualifiedName}})}: [{""");

            if (Data.DefaultUnit is not null)
            {
                Builder.Append($"InUnit({Data.Unit.FullyQualifiedName}.{Data.DefaultUnit.Name}).Value");
            }
            else
            {
                Builder.Append("Magnitude.Value");
            }

            Builder.Append('}');

            if (Data.DefaultUnitSymbol is not null)
            {
                Builder.Append($" [{Data.DefaultUnitSymbol}]");
            }

            Builder.AppendLine($"]\";");
        }

        private void ComposeEquality(Indentation indentation)
        {
            if (Data.Scalar.IsReferenceType)
            {
                ComposeReferenceTypeEquality(indentation);
            }
            else
            {
                ComposeValueTypeEquality(indentation);
            }
        }

        private void ComposeReferenceTypeEquality(Indentation indentation)
        {
            string virtualText = Data.Scalar.IsSealed ? string.Empty : " virtual";

            AppendDocumentation(indentation, Data.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"{indentation}public{virtualText} bool Equals({Data.Scalar.FullyQualifiedName}? other) => other is not null && Magnitude.Value == other.Magnitude.Value;");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Scalar.FullyQualifiedName);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Scalar.FullyQualifiedName}? lhs, {Data.Scalar.FullyQualifiedName}? rhs) => lhs?.Equals(rhs) ?? rhs is null;");

            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Scalar.FullyQualifiedName}? lhs, {Data.Scalar.FullyQualifiedName}? rhs) => (lhs == rhs) is false;");
        }

        private void ComposeValueTypeEquality(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($$"""{{indentation}}public bool Equals({{Data.Scalar.FullyQualifiedName}} other) => Magnitude.Value == other.Magnitude.Value;""");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Scalar.FullyQualifiedName);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Scalar.FullyQualifiedName} lhs, {Data.Scalar.FullyQualifiedName} rhs) => lhs.Equals(rhs);");

            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Scalar.FullyQualifiedName} lhs, {Data.Scalar.FullyQualifiedName} rhs) => (lhs == rhs) is false;");
        }

        private void ComposeGetHashCode(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.GetHashCodeDocumentation());
            Builder.AppendLine($"{indentation}public override int GetHashCode() => Magnitude.GetHashCode();");
        }

        private void ComposeComparisons(Indentation indentation)
        {
            if (Data.Scalar.IsReferenceType)
            {
                ComposeReferenceTypeComparisons(indentation);
            }
            else
            {
                ComposeValueTypeComparisons(indentation);
            }
        }

        private void ComposeReferenceTypeComparisons(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.CompareToSameType());
            Builder.AppendLine($"{indentation}public int CompareTo({Data.Scalar.FullyQualifiedName}? other) => Magnitude.Value.CompareTo(other?.Magnitude.Value);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.LessThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator <({Data.Scalar.FullyQualifiedName}? x, {Data.Scalar.FullyQualifiedName}? y) " +
                $"=> x?.Magnitude.Value < y?.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.GreaterThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator >({Data.Scalar.FullyQualifiedName}? x, {Data.Scalar.FullyQualifiedName}? y) " +
                $"=> x?.Magnitude.Value > y?.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.LessThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator <=({Data.Scalar.FullyQualifiedName}? x, {Data.Scalar.FullyQualifiedName}? y) " +
                $"=> x?.Magnitude.Value <= y?.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.GreaterThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator >=({Data.Scalar.FullyQualifiedName}? x, {Data.Scalar.FullyQualifiedName}? y) " +
                $"=> x?.Magnitude.Value >= y?.Magnitude.Value;");
        }

        private void ComposeValueTypeComparisons(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.CompareToSameType());
            Builder.AppendLine($"{indentation}public int CompareTo({Data.Scalar.FullyQualifiedName} other) => Magnitude.Value.CompareTo(other.Magnitude.Value);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.LessThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator <({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                $"=> x.Magnitude.Value < y.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.GreaterThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator >({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                $"=> x.Magnitude.Value > y.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.LessThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator <=({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                $"=> x.Magnitude.Value <= y.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.GreaterThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator >=({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                $"=> x.Magnitude.Value >= y.Magnitude.Value;");
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

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
