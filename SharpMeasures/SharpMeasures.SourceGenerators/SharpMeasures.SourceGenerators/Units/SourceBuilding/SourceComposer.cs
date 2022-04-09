﻿namespace SharpMeasures.SourceGeneration.Units.SourceBuilding;

using SharpMeasures.SourceGeneration.Units.Pipeline;
using SharpMeasures.SourceGeneration.Utility;

using System;
using System.Text;
using System.Threading;

internal static class SourceComposer
{
    public static string Compose(FifthStage.Result data, CancellationToken token)
    {
        string typeName = data.TypeSymbol.Name;
        string? quantityType = data.Parameters.UnbiasedParameters?.Quantity?.ToDisplayString();
        string? quantityName = data.Parameters.UnbiasedParameters?.Quantity?.Name;
        string? quantityParameterName = quantityName is null ? null : SourceBuildingUtility.ToParameterName(quantityName);

        StringBuilder source = new();

        SourceBuildingUtility.AppendAutoGeneratedHeader(source);
        SourceBuildingUtility.AppendNullableDirective(source);

        SourceBuildingUtility.AppendNamespace(source, data.TypeSymbol);

        SourceBuildingUtility.AppendUsings(source, data.TypeSymbol, new string[]
        {
            "SharpMeasures.Units"
        });

        SourceBuildingUtility.AppendTypeDefinition(source, data.TypeSymbol);

        SourceBuildingUtility.AppendInterfaceImplementation(source, new string[]
        {
            $"System.IComparable<{typeName}>"
        });

        SourceBuildingUtility.AppendBlock(source, typeBlock, indentLevel: 0);

        void typeBlock(StringBuilder source, string indentation)
        {
            DerivationsComposer.Append(source, data, token);
            UnitsComposer.Append(source, data, token);

            source.Append($"{indentation}public {quantityType} {quantityName} {{ get; }}{Environment.NewLine}");

            source.Append($"{Environment.NewLine}{indentation}private {typeName}({quantityType} {quantityParameterName})");

            SourceBuildingUtility.AppendBlock(source, constructorBlock, indentLevel: 1);

            void constructorBlock(StringBuilder source, string indentation)
            {
                source.Append($"{indentation}{quantityName} = {quantityParameterName};");
            }

            source.Append($"{Environment.NewLine}{indentation}public {typeName} WithPrefix(MetricPrefix prefix) " +
                $"=> ScaledBy(prefix.Scale);{Environment.NewLine}");
            source.Append($"{indentation}public {typeName} ScaledBy(Scalar scale) => ScaledBy(scale.Magnitude);{Environment.NewLine}");
            source.Append($"{indentation}public {typeName} ScaledBy(double scale) => new({quantityName} * scale);{Environment.NewLine}");

            source.Append($"{Environment.NewLine}{indentation}public int CompareTo({typeName} other) => {quantityName}.CompareTo(other.{quantityName});{Environment.NewLine}");
            source.Append($"{indentation}public override string ToString() => $\"{{typeof({typeName})}}: {{{quantityName}}}\";{Environment.NewLine}");

            source.Append($"{Environment.NewLine}{indentation}public static bool operator <({typeName} x, {typeName} y) " +
                $"=> x.{quantityName} < y.{quantityName};");
            source.Append($"{Environment.NewLine}{indentation}public static bool operator >({typeName} x, {typeName} y) " +
                $"=> x.{quantityName} > y.{quantityName};");
            source.Append($"{Environment.NewLine}{indentation}public static bool operator <=({typeName} x, {typeName} y) " +
                $"=> x.{quantityName} <= y.{quantityName};");
            source.Append($"{Environment.NewLine}{indentation}public static bool operator >=({typeName} x, {typeName} y) " +
                $"=> x.{quantityName} >= y.{quantityName};");
        }

        return source.ToString();
    }
}
