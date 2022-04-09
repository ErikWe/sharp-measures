﻿namespace SharpMeasures.SourceGeneration.Units.SourceBuilding;

using SharpMeasures.SourceGeneration.Units.Pipeline;
using SharpMeasures.SourceGeneration.Utility;

using System;
using System.Text;
using System.Threading;

internal static class BiasedSourceComposer
{
    public static string Compose(FifthStage.Result data, CancellationToken token)
    {
        string typeName = data.TypeSymbol.Name;
        
        string? quantityType = data.Parameters.BiasedParameters?.BiasedQuantity?.ToDisplayString();
        string? quantityName = data.Parameters.BiasedParameters?.BiasedQuantity?.Name;
        string? quantityParameterName = quantityName is null ? null : SourceBuildingUtility.ToParameterName(quantityName);

        string? unbiasedQuantityType = data.Parameters.BiasedParameters?.UnbiasedQuantity?.ToDisplayString();
        string? unbiasedQuantityName = data.Parameters.BiasedParameters?.UnbiasedQuantity?.Name;
        string? unbiasedQUantityParameterName = unbiasedQuantityName is not null ? SourceBuildingUtility.ToParameterName(unbiasedQuantityName) : null;

        StringBuilder source = new();

        SourceBuildingUtility.AppendAutoGeneratedHeader(source);
        SourceBuildingUtility.AppendNullableDirective(source);

        SourceBuildingUtility.AppendNamespace(source, data.TypeSymbol);
        SourceBuildingUtility.AppendTypeDefinition(source, data.TypeSymbol);

        SourceBuildingUtility.AppendBlock(source, typeBlock, indentLevel: 0);

        void typeBlock(StringBuilder source, string indentation)
        {
            DerivationsComposer.Append(source, data, token);
            UnitsComposer.Append(source, data, token);

            source.Append($"{indentation}public {unbiasedQuantityType} {unbiasedQuantityName} {{ get; }}{Environment.NewLine}");
            source.Append($"{indentation}public Scalar Offset {{ get; }}{Environment.NewLine}");

            source.Append($"{Environment.NewLine}{indentation}private {typeName}({unbiasedQuantityType} {unbiasedQUantityParameterName}, Scalar offset)");

            SourceBuildingUtility.AppendBlock(source, constructorBlock, indentLevel: 1);

            void constructorBlock(StringBuilder source, string indentation)
            {
                source.Append($"{indentation}{unbiasedQuantityName} = {unbiasedQUantityParameterName};{Environment.NewLine}");
                source.Append($"{indentation}Offset = offset;");
            }

            source.Append($"{Environment.NewLine}{indentation}public {typeName} WithPrefix(MetricPrefix prefix) " +
                $"=> ScaledBy(prefix.Scale);{Environment.NewLine}");
            source.Append($"{indentation}public {typeName} ScaledBy(Scalar scale) => ScaledBy(scale.Magnitude);{Environment.NewLine}");
            source.Append($"{indentation}public {typeName} ScaledBy(double scale) => new({unbiasedQuantityName} * scale, Offset / scale);{Environment.NewLine}");
            source.Append($"{indentation}public {typeName} OffsetBy(Scalar offset) => OffsetBy(offset.Magnitude);{Environment.NewLine}");
            source.Append($"{indentation}public {typeName} OffsetBy(double offset) => new({unbiasedQuantityName}, Offset + offset);{Environment.NewLine}");

            source.Append($"{Environment.NewLine}{indentation}public override string ToString() " +
                $"=> $\"{{typeof({quantityType})}}: {{{quantityName}}} + {{Offset}}\";{Environment.NewLine}");
        }

        return source.ToString();
    }
}
