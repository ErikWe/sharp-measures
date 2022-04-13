namespace SharpMeasures.Generators.Units.SourceBuilding;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Units.Pipeline;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

internal static class UnitsComposer
{
    public static void Append(StringBuilder source, FourthStage.Result data, CancellationToken token)
    {
        HashSet<string> addedInstances = new();

        int initialLength = source.Length;

        AppendIndependentInstances(source, data, addedInstances, data.UnitInstances.Fixed, AppendUnitExpression, token);
        AppendIndependentInstances(source, data, addedInstances, data.UnitInstances.Derived, AppendUnitExpression, token);
        AppendDependantInstances(source, data, addedInstances, token);
        
        if (source.Length > initialLength)
        {
            source.Append(Environment.NewLine);
        }
    }

    private static void AppendIndependentInstances<T>(StringBuilder source, FourthStage.Result data, HashSet<string> addedInstances, IEnumerable<T> instances,
        Action<StringBuilder, FourthStage.Result, T> expressionAppender, CancellationToken token)
        where T : IUnitInstanceAttributeParameters
    {
        foreach (T parameters in instances)
        {
            AppendInstance(source, data, addedInstances, parameters, expressionAppender, token);
        }
    }

    private static void AppendDependantInstances(StringBuilder source, FourthStage.Result data, HashSet<string> addedInstances, CancellationToken token)
    {
        List<IDerivedUnitInstanceAttributeParameters> unaddedInstances = new();

        AppendDependantInstances(source, data, addedInstances, unaddedInstances, data.UnitInstances.Alias, AppendUnitExpression, token);
        AppendDependantInstances(source, data, addedInstances, unaddedInstances, data.UnitInstances.Scaled, AppendUnitExpression, token);
        AppendDependantInstances(source, data, addedInstances, unaddedInstances, data.UnitInstances.Prefixed, AppendUnitExpression, token);
        AppendDependantInstances(source, data, addedInstances, unaddedInstances, data.UnitInstances.Offset, AppendUnitExpression, token);

        AppendUnaddedInstances(source, data, addedInstances, unaddedInstances, token);
    }

    private static void AppendDependantInstances<T>(StringBuilder source, FourthStage.Result data, HashSet<string> addedInstances,
        List<IDerivedUnitInstanceAttributeParameters> unaddedInstances, IEnumerable<T> instances,
        Action<StringBuilder, FourthStage.Result, T> expressionAppender, CancellationToken token)
        where T : IDerivedUnitInstanceAttributeParameters, IUnitInstanceAttributeParameters
    {
        foreach (T parameters in instances)
        {
            if (addedInstances.Contains(parameters.DerivedFrom))
            {
                AppendInstance(source, data, addedInstances, parameters, expressionAppender, token);
            }
            else
            {
                unaddedInstances.Add(parameters);
            }
        }
    }

    private static void AppendUnaddedInstances<T>(StringBuilder source, FourthStage.Result data, HashSet<string> addedInstances,
        List<T> unaddedInstances, CancellationToken token)
        where T : IDerivedUnitInstanceAttributeParameters, IUnitInstanceAttributeParameters
    {
        int initialCount = unaddedInstances.Count;

        for (int i = 0; i < unaddedInstances.Count; i++)
        {
            if (addedInstances.Contains(unaddedInstances[i].DerivedFrom))
            {
                AppendInstance(source, data, addedInstances, unaddedInstances[i], AppendUnitExpression, token);
                unaddedInstances.RemoveAt(i--);
            }
        }

        if (unaddedInstances.Count < initialCount)
        {
            AppendUnaddedInstances(source, data, addedInstances, unaddedInstances, token);
        }
    }

    private static void AppendInstance<T>(StringBuilder source, FourthStage.Result data, HashSet<string> addedInstances, T instance,
        Action<StringBuilder, FourthStage.Result, T> expressionAppender, CancellationToken _)
        where T : IUnitInstanceAttributeParameters
    {
        addedInstances.Add(instance.Name);

        source.Append($"\tpublic static {data.TypeSymbol.ToDisplayString()} {instance.Name} {{ get; }} = ");
        expressionAppender(source, data, instance);
        source.Append($";{Environment.NewLine}");
    }

    private static void AppendUnitExpression<T>(StringBuilder source, FourthStage.Result data, T parameters)
    {
        switch (parameters)
        {
            case AliasUnitInstanceAttributeParameters aliasInstance:
                AppendUnitExpression(source, data, aliasInstance);
                break;
            case ScaledUnitInstanceAttributeParameters scaledInstance:
                AppendUnitExpression(source, data, scaledInstance);
                break;
            case PrefixedUnitInstanceAttributeParameters prefixedInstance:
                AppendUnitExpression(source, data, prefixedInstance);
                break;
            case OffsetUnitInstanceAttributeParameters offsetInstance:
                AppendUnitExpression(source, data, offsetInstance);
                break;
        }
    }

    private static void AppendUnitExpression(StringBuilder source, FourthStage.Result data, FixedUnitInstanceAttributeParameters parameters)
    {
        source.Append($"new(new {data.Parameters.Quantity?.ToDisplayString()}({parameters.Value}))");
    }

    private static void AppendUnitExpression(StringBuilder source, FourthStage.Result data, DerivedUnitInstanceAttributeParameters parameters)
    {
        SourceBuildingUtility.AppendEnumerable(source, "From(", arguments(), ", ", ")");

        IEnumerable<string> arguments()
        {
            for (int i = 0; i < parameters.Signature.Count && i < parameters.UnitInstanceNames.Count; i++)
            {
                yield return $"{parameters.Signature[i]?.ToDisplayString()}.{parameters.UnitInstanceNames[i]}";
            }
        }
    }

    private static void AppendUnitExpression(StringBuilder source, FourthStage.Result data, AliasUnitInstanceAttributeParameters parameters)
    {
        source.Append($"{data.TypeSymbol.ToDisplayString()}.{parameters.AliasOf}");
    }

    private static void AppendUnitExpression(StringBuilder source, FourthStage.Result data, ScaledUnitInstanceAttributeParameters parameters)
    {
        source.Append($"{data.TypeSymbol.ToDisplayString()}.{parameters.From}.ScaledBy({parameters.Scale})");
    }

    private static void AppendUnitExpression(StringBuilder source, FourthStage.Result data, PrefixedUnitInstanceAttributeParameters parameters)
    {
        source.Append($"{data.TypeSymbol.ToDisplayString()}.{parameters.From}.WithPrefix(MetricPrefix.{parameters.Prefix})");
    }

    private static void AppendUnitExpression(StringBuilder source, FourthStage.Result data, OffsetUnitInstanceAttributeParameters parameters)
    {
        source.Append($"{data.TypeSymbol.ToDisplayString()}.{parameters.From}.OffsetBy({parameters.Offset})");
    }
}
