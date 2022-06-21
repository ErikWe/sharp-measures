﻿namespace SharpMeasures.Generators.Units.Pipelines.Common;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Unit.Name}_Common.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private UsingsCollector UsingsCollector { get; }
        private InterfaceCollector InterfaceCollector { get; }

        private Composer(DataModel data)
        {
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, Data.Unit.Namespace);
            InterfaceCollector = InterfaceCollector.Delayed(Builder);

            UsingsCollector.AddUsings("SharpMeasures", Data.Quantity.Namespace, "System");

            if (Data.BiasTerm is false)
            {
                InterfaceCollector.AddInterface($"IComparable<{Data.Unit.Name}>");
            }

            if (Data.Unit.IsRecord is false)
            {
                InterfaceCollector.AddInterface($"IEquatable<{Data.Unit.Name}>");
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Unit.Namespace);

            UsingsCollector.MarkInsertionPoint();

            AppendDocumentation(new Indentation(0), Data.Documentation.Header());
            Builder.Append(Data.Unit.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            if (Data.BiasTerm)
            {
                ComposeBiasedTypeBlock(indentation);
            }
            else
            {
                ComposeUnbiasedTypeBlock(indentation);
            }
        }

        private void ComposeUnbiasedTypeBlock(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.RepresentedQuantity());
            Builder.AppendLine($"{indentation}public {Data.Quantity.FullyQualifiedName} {Data.Quantity.Name} {{ get; }}");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.Constructor());
            Builder.AppendLine($$"""
                {{indentation}}private {{Data.Unit.Name}}({{Data.Quantity.FullyQualifiedName}} {{Data.QuantityParameterName}})
                {{indentation}}{
                """);

            if (Data.Quantity.IsReferenceType)
            {
                Builder.AppendLine($"{indentation.Increased}ArgumentNullException.ThrowIfNull({Data.QuantityParameterName});");
                Builder.AppendLine();
            }

            Builder.AppendLine($$"""
                {{indentation.Increased}}{{Data.Quantity.Name}} = {{Data.QuantityParameterName}};
                {{indentation}}}
                """);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.ScaledBy());
            Builder.AppendLine($"{indentation}public {Data.Unit.Name} ScaledBy(Scalar scale) => new({Data.Quantity.Name} * scale);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.WithPrefix());
            Builder.AppendLine($$"""
                {{indentation}}/// <exception cref="ArgumentNullException"/>
                {{indentation}}public {{Data.Unit.Name}} WithPrefix<TPrefix>(TPrefix prefix) where TPrefix : IPrefix
                {{indentation}}{
                {{indentation.Increased}}ArgumentNullException.ThrowIfNull(prefix);

                {{indentation.Increased}}return ScaledBy(prefix.Factor);
                {{indentation}}}
                """);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.ToStringDocumentation());
            Builder.AppendLine($@"{indentation}public override string ToString() => $""{{typeof({Data.Unit.Name})}}: [{{{Data.Quantity.Name}}}]"";");

            Builder.AppendLine();

            if (Data.Unit.IsRecord is false)
            {
                ComposeEquality(indentation);

                Builder.AppendLine();

                ComposeGetHashCode(indentation);

                Builder.AppendLine();
            }

            ComposeComparisons(indentation);
        }

        private void ComposeBiasedTypeBlock(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.RepresentedQuantity());
            Builder.AppendLine($"{indentation}public {Data.Quantity.FullyQualifiedName} {Data.Quantity.Name} {{ get; }}");
            AppendDocumentation(indentation, Data.Documentation.Bias());
            Builder.AppendLine($"{indentation}public Scalar Bias {{ get; }}");

            AppendDocumentation(indentation, Data.Documentation.Constructor());

            if (Data.Quantity.IsReferenceType)
            {
                Builder.AppendLine($"""{indentation}/// <exception cref="ArgumentNullException"/>""");
            }

            Builder.AppendLine($$"""
                {{indentation}}private {{Data.Unit.Name}}({{Data.Quantity.FullyQualifiedName}} {{Data.QuantityParameterName}}, Scalar bias)
                {{indentation}}{
                """);

            if (Data.Quantity.IsReferenceType)
            {
                Builder.AppendLine($"{indentation.Increased}ArgumentNullException.ThrowIfNull({Data.QuantityParameterName});");
                Builder.AppendLine();
            }

            Builder.AppendLine($$"""
                {{indentation.Increased}}{{Data.Quantity.Name}} = {{Data.QuantityParameterName}};
                {{indentation.Increased}}Bias = bias;
                {{indentation}}}
                """);

            AppendDocumentation(indentation, Data.Documentation.ScaledBy());
            Builder.AppendLine($"{indentation}public {Data.Unit.Name} ScaledBy(Scalar scale) => new({Data.Quantity.Name} * scale, Bias / scale);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.WithBias());
            Builder.AppendLine($"{indentation}public {Data.Unit.Name} WithBias(Scalar bias) => new({Data.Quantity.Name}, Bias + bias);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.WithPrefix());
            Builder.AppendLine($$"""
                {{indentation}}/// <exception cref="ArgumentNullException"/>
                {{indentation}}public {{Data.Unit.Name}} WithPrefix<TPrefix>(TPrefix prefix) where TPrefix : IPrefix
                {{indentation}}{
                {{indentation.Increased}}ArgumentNullException.ThrowIfNull(prefix);

                {{indentation.Increased}}return ScaledBy(prefix.Factor);
                {{indentation}}}
                """);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.ToStringDocumentation());
            Builder.AppendLine($"{indentation}public override string ToString() => \"{{typeof({Data.Unit.Name})}}: ({{{Data.Quantity.Name}}} + {{Bias}})\";");

            if (Data.Unit.IsRecord is false)
            {
                Builder.AppendLine();

                ComposeEquality(indentation);
            }
        }

        private void ComposeEquality(Indentation indentation)
        {
            if (Data.Unit.IsReferenceType)
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
            string virtualText = Data.Unit.IsSealed ? string.Empty : " virtual";
            string biasEqualityText = Data.BiasTerm ? " && Bias == other.Bias" : string.Empty;

            AppendDocumentation(indentation, Data.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"{indentation}public{virtualText} bool Equals({Data.Unit.Name}? other) => other is not null " +
                $"&& {Data.Quantity.Name} == other.{Data.Quantity.Name}{biasEqualityText};");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Unit.Name);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Unit.Name}? lhs, {Data.Unit.Name}? rhs) => lhs?.Equals(rhs) ?? rhs is null;");

            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Unit.Name}? lhs, {Data.Unit.Name}? rhs) => (lhs == rhs) is false;");
        }

        private void ComposeValueTypeEquality(Indentation indentation)
        {
            string biasEqualityText = Data.BiasTerm ? " && Bias == other.Bias" : string.Empty;

            AppendDocumentation(indentation, Data.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($$"""
                {{indentation}}public bool Equals({{Data.Unit.Name}} other)
                {{indentation.Increased}}=> {{Data.Quantity.Name}} == other.{{Data.Quantity.Name}}{{biasEqualityText}};
                """);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Unit.Name);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Unit.Name} lhs, {Data.Unit.Name} rhs) => lhs.Equals(rhs);");

            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Unit.Name} lhs, {Data.Unit.Name} rhs) => (lhs == rhs) is false;");
        }

        private void ComposeGetHashCode(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.GetHashCodeDocumentation());
            Builder.Append($"{indentation}public override int GetHashCode() => ");

            if (Data.BiasTerm)
            {
                Builder.AppendLine($"({Data.Quantity.Name}, Bias).GetHashCode();");
            }
            else
            {
                Builder.AppendLine($"{Data.Quantity.Name}.GetHashCode();");
            }
        }

        private void ComposeComparisons(Indentation indentation)
        {
            if (Data.Unit.IsReferenceType)
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
            Builder.AppendLine($"{indentation}public int CompareTo({Data.Unit.Name}? other) " +
                $"=> {Data.Quantity.Name}.Magnitude.Value.CompareTo(other?.{Data.Quantity.Name}.Magnitude.Value);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.LessThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator <({Data.Unit.Name}? x, {Data.Unit.Name}? y) " +
                $"=> x?.{Data.Quantity.Name}.Magnitude.Value < y?.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.GreaterThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator >({Data.Unit.Name}? x, {Data.Unit.Name}? y) " +
                $"=> x?.{Data.Quantity.Name}.Magnitude.Value > y?.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.LessThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator <=({Data.Unit.Name}? x, {Data.Unit.Name}? y) " +
                $"=> x?.{Data.Quantity.Name}.Magnitude.Value <= y?.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.GreaterThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator >=({Data.Unit.Name}? x, {Data.Unit.Name}? y) " +
                $"=> x?.{Data.Quantity.Name}.Magnitude.Value >= y?.{Data.Quantity.Name}.Magnitude.Value;");
        }

        private void ComposeValueTypeComparisons(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.CompareToSameType());
            Builder.AppendLine($"{indentation}public int CompareTo({Data.Unit.Name} other) " +
                $"=> {Data.Quantity.Name}.Magnitude.Value.CompareTo(other.{Data.Quantity.Name}.Magnitude.Value);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.LessThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator <({Data.Unit.Name} x, {Data.Unit.Name} y) " +
                $"=> x.{Data.Quantity.Name}.Magnitude.Value < y.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.GreaterThanSameType());
            Builder.AppendLine($"{indentation}public static bool operator >({Data.Unit.Name} x, {Data.Unit.Name} y) " +
                $"=> x.{Data.Quantity.Name}.Magnitude.Value > y.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.LessThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator <=({Data.Unit.Name} x, {Data.Unit.Name} y) " +
                $"=> x.{Data.Quantity.Name}.Magnitude.Value <= y.{Data.Quantity.Name}.Magnitude.Value;");
            AppendDocumentation(indentation, Data.Documentation.GreaterThanOrEqualSameType());
            Builder.AppendLine($"{indentation}public static bool operator >=({Data.Unit.Name} x, {Data.Unit.Name} y) " +
                $"=> x.{Data.Quantity.Name}.Magnitude.Value >= y.{Data.Quantity.Name}.Magnitude.Value;");
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
