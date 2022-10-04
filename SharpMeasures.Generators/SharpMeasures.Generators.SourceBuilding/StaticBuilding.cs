﻿namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public static class StaticBuilding
{
    public static string NullArgumentGuard(string parameter) => $"global::System.ArgumentNullException.ThrowIfNull({parameter});";

    public static void AppendHeaderAndDirectives(StringBuilder source, bool includeVersion = true, bool includeTimestamp = true)
    {
        AppendAutoGeneratedHeader(source, Assembly.GetCallingAssembly(), includeVersion, includeTimestamp);

        AppendNullableDirective(source);
    }

    public static void AppendAutoGeneratedHeader(StringBuilder source, bool includeVersion = true, bool includeTimestamp = true) => AppendAutoGeneratedHeader(source, Assembly.GetCallingAssembly(), includeVersion, includeTimestamp);
    private static void AppendAutoGeneratedHeader(StringBuilder source, Assembly callingAssembly, bool includeVersion, bool includeTimestamp)
    {
        string versionText = includeVersion ? $" v{callingAssembly.GetName().Version}" : string.Empty;
        string timestampText = includeTimestamp ? $" ({DateTime.UtcNow} UTC)" : string.Empty;

        source.AppendLine($$"""
            //----------------------
            // <auto-generated>
            //      This file was generated by {{callingAssembly.GetName().Name}}{{versionText}}{{timestampText}}.
            //
            //      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
            // </auto-generated>
            //----------------------

            """);
    }

    public static void AppendNullableDirective(StringBuilder source)
    {
        source.AppendLine($"#nullable enable");

        source.AppendLine();
    }

    public static void AppendNullArgumentGuard(StringBuilder source, Indentation indentation, string parameterName, bool finalNewLine = true)
    {
        source.AppendLine($"{indentation}{NullArgumentGuard(parameterName)}");

        if (finalNewLine)
        {
            source.AppendLine();
        }
    }

    public static void AppendNullArgumentGuardForReferenceTypeParameters(StringBuilder source, Indentation indentation, params (NamedType Type, string Name)[] parameters) => AppendNullArgumentGuardForReferenceTypeParameters(source, indentation, parameters: parameters);
    public static void AppendNullArgumentGuardForReferenceTypeParameters(StringBuilder source, Indentation indentation, bool finalNewLine = true, params (NamedType Type, string Name)[] parameters) => AppendNullArgumentGuardForReferenceTypeParameters(source, indentation, parameters, finalNewLine);
    public static void AppendNullArgumentGuardForReferenceTypeParameters(StringBuilder source, Indentation indentation, IEnumerable<(NamedType Type, string Name)> parameters, bool finalNewLine = true)
    {
        bool anyReferenceType = false;

        foreach ((var parameterType, var parameterName) in parameters)
        {
            if (parameterType.IsReferenceType)
            {
                AppendNullArgumentGuard(source, indentation, parameterName, finalNewLine: false);

                anyReferenceType = true;
            }
        }

        if (anyReferenceType && finalNewLine)
        {
            source.AppendLine();
        }
    }

    public static void AppendSingleLineMethodWithPotentialNullArgumentGuards(StringBuilder source, Indentation indentation, string methodNameAndModifiers, string expression, params (NamedType Type, string Name)[] parameters)
        => AppendSingleLineMethodWithPotentialNullArgumentGuards(source, indentation, methodNameAndModifiers, expression, parameters as IEnumerable<(NamedType, string)>);

    public static void AppendSingleLineMethodWithPotentialNullArgumentGuards(StringBuilder source, Indentation indentation, string methodNameAndModifiers, string expression, IEnumerable<(NamedType Type, string Name)> parameters)
    {
        DocumentationBuilding.AppendArgumentNullExceptionTagIfReferenceType(source, indentation, parameters.Select(static (parameter) => parameter.Type));

        StringBuilder signature = new();
        IterativeBuilding.AppendEnumerable(signature, prefix: "(", parameters.Select(static ((NamedType Type, string Name) parameter) => $"{parameter.Type.FullyQualifiedName} {parameter.Name}"), separator: ", ", postfix: ")", removeFixedIfEmpty: false);

        if (parameters.Any(static (parameter) => parameter.Type.IsReferenceType))
        {
            source.AppendLine($$"""
                {{indentation}}{{methodNameAndModifiers}}{{signature}}
                {{indentation}}{
                """);

            AppendNullArgumentGuardForReferenceTypeParameters(source, indentation.Increased, parameters: parameters);

            source.AppendLine($$"""
                {{indentation.Increased}}return {{expression}};
                {{indentation}}}
                """);

            return;
        }

        source.AppendLine($"{indentation}{methodNameAndModifiers}{signature} => {expression};");
    }

    public static void AppendEqualsObjectMethod(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public override bool Equals(object? obj) => obj is {type} other && Equals(other);");
    public static void AppendReferenceTypeEqualityOperator(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public static bool operator ==({type}? lhs, {type}? rhs) => lhs?.Equals(rhs) ?? rhs is null;");
    public static void AppendReferenceTypeInequalityOperator(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public static bool operator !=({type}? lhs, {type}? rhs) => (lhs == rhs) is false;");
    public static void AppendValueTypeEqualityOperator(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public static bool operator ==({type} lhs, {type} rhs) => lhs.Equals(rhs);");
    public static void AppendValueTypeInequalityOperator(StringBuilder source, Indentation indentation, string type) => source.AppendLine($"{indentation}public static bool operator !=({type} lhs, {type} rhs) => (lhs == rhs) is false;");
}
