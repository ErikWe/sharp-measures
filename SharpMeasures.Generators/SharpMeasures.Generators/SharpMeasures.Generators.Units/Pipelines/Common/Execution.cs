namespace SharpMeasures.Generators.Units.Pipelines.Common;

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

        private InterfaceCollector InterfaceCollector { get; }

        private Composer(DataModel data)
        {
            Data = data;

            InterfaceCollector = InterfaceCollector.Delayed(Builder);

            if (Data.BiasTerm is false)
            {
                InterfaceCollector.AddInterface($"global::System.IComparable<{Data.Unit.Name}>");
            }

            if (Data.Unit.IsRecord is false)
            {
                InterfaceCollector.AddInterface($"global::System.IEquatable<{Data.Unit.Name}>");
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Unit.Namespace);

            AppendDocumentation(new Indentation(0), Data.Documentation.Header());
            Builder.Append(Data.Unit.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            ComposeProperties(indentation);

            Builder.AppendLine();

            ComposeConstructor(indentation);

            Builder.AppendLine();

            ComposeScaledBy(indentation);

            Builder.AppendLine();

            ComposeWithPrefix(indentation);

            Builder.AppendLine();

            ComposeWithBias(indentation);

            Builder.AppendLine();

            ComposeToString(indentation);

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

        private void ComposeProperties(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.RepresentedQuantity());
            Builder.AppendLine($"{indentation}public {Data.Quantity.FullyQualifiedName} {Data.Quantity.Name} {{ get; }}");

            if (Data.BiasTerm)
            {
                AppendDocumentation(indentation, Data.Documentation.Bias());
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar Bias {{ get; }}");
            }
        }

        private void ComposeConstructor(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.Constructor());

            if (Data.Quantity.IsReferenceType)
            {
                Builder.AppendLine($"""{indentation}/// <exception cref="global::System.ArgumentNullException"/>""");
            }

            if (Data.BiasTerm)
            {
                Builder.AppendLine($"{indentation}private {Data.Unit.Name}({Data.Quantity.FullyQualifiedName} {Data.QuantityParameterName}, global::SharpMeasures.Scalar bias)");
            }
            else
            {
                Builder.AppendLine($"{indentation}private {Data.Unit.Name}({Data.Quantity.FullyQualifiedName} {Data.QuantityParameterName})");
            }

            BlockBuilding.AppendOpeningBracket(Builder, indentation);

            if (Data.Quantity.IsReferenceType)
            {
                Builder.AppendLine($"{indentation.Increased}global::System.ArgumentNullException.ThrowIfNull({Data.QuantityParameterName});");
                Builder.AppendLine();
            }

            Builder.AppendLine($"{indentation.Increased}{Data.Quantity.Name} = {Data.QuantityParameterName};");

            if (Data.BiasTerm)
            {
                Builder.AppendLine($"{indentation.Increased}Bias = bias;");
            }

            BlockBuilding.AppendClosingBracket(Builder, indentation);
        }

        private void ComposeScaledBy(Indentation indentation)
        {
            string expression = Data.BiasTerm ? $"{Data.Quantity.Name} * scale, Bias / scale" : $"{Data.Quantity.Name} * scale";

            AppendDocumentation(indentation, Data.Documentation.ScaledBy());
            Builder.AppendLine($"{indentation}public {Data.Unit.Name} ScaledBy(global::SharpMeasures.Scalar scale) => new({expression});");
        }

        private void ComposeWithPrefix(Indentation indentation)
        {
            string expression = Data.BiasTerm ? $"{Data.Quantity.Name} * prefix.Factor, Bias / prefix.Factor" : $"{Data.Quantity.Name} * prefix.Factor";

            AppendDocumentation(indentation, Data.Documentation.WithPrefix());
            Builder.AppendLine($$"""
                {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                {{indentation}}public {{Data.Unit.Name}} WithPrefix<TPrefix>(TPrefix prefix) where TPrefix : global::SharpMeasures.IPrefix
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(prefix);

                {{indentation.Increased}}return new({{expression}});
                {{indentation}}}
                """);
        }

        private void ComposeWithBias(Indentation indentation)
        {
            if (Data.BiasTerm is false)
            {
                return;
            }

            AppendDocumentation(indentation, Data.Documentation.WithBias());
            Builder.AppendLine($"{indentation}public {Data.Unit.Name} WithBias(global::SharpMeasures.Scalar bias) => new({Data.Quantity.Name}, Bias + bias);");
        }

        private void ComposeToString(Indentation indentation)
        {
            string expression = Data.BiasTerm
                ? $"{{typeof({Data.Unit.Name})}}: [{{{Data.Quantity.Name}}} + {{Bias}}]"
                : $"{{typeof({Data.Unit.Name})}}: [{{{Data.Quantity.Name}}}]";

            AppendDocumentation(indentation, Data.Documentation.ToStringDocumentation());
            Builder.AppendLine($"{indentation}public override string ToString() => \"{expression}\";");
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
            Builder.AppendLine($"{indentation}public{virtualText} bool Equals({Data.Unit.Name}? other) => other is not null && {Data.Quantity.Name} == other.{Data.Quantity.Name}{biasEqualityText};");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Unit.Name);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualitySameTypeOperator());
            StaticBuilding.AppendReferenceTypeEqualityOperator(Builder, indentation, Data.Unit.Name);
            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
            StaticBuilding.AppendReferenceTypeInequalityOperator(Builder, indentation, Data.Unit.Name);
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
            StaticBuilding.AppendValueTypeEqualityOperator(Builder, indentation, Data.Unit.Name);
            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
            StaticBuilding.AppendValueTypeInequalityOperator(Builder, indentation, Data.Unit.Name);
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
