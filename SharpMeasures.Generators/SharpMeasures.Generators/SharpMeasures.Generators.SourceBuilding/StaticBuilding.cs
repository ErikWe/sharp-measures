﻿namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Reflection;
using System.Text;

public static class StaticBuilding
{
    public static void AppendHeaderAndDirectives(StringBuilder source, bool includeVersion = true, bool includeTimestamp = true)
    {
        AppendAutoGeneratedHeader(source, Assembly.GetCallingAssembly(), includeVersion, includeTimestamp);
        AppendNullableDirective(source);
    }

    public static void AppendAutoGeneratedHeader(StringBuilder source, bool includeVersion = true, bool includeTimestamp = true)
    {
        AppendAutoGeneratedHeader(source, Assembly.GetCallingAssembly(), includeVersion, includeTimestamp);
    }

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

    public static void AppendEqualsObjectMethod(StringBuilder source, Indentation indentation, string type)
    {
        source.AppendLine($"{indentation}public override bool Equals(object? obj) => return obj is {type} other && Equals(other);");
    }

    public static void AppendReferenceTypeEqualityOperator(StringBuilder source, Indentation indentation, string type)
    {
        source.AppendLine($"{indentation}public static bool operator ==({type}? lhs, {type}? rhs) => lhs?.Equals(rhs) ?? rhs is null;");
    }

    public static void AppendReferenceTypeInequalityOperator(StringBuilder source, Indentation indentation, string type)
    {
        source.AppendLine($"{indentation}public static bool operator !=({type}? lhs, {type}? rhs) => (lhs == rhs) is false;");
    }

    public static void AppendValueTypeEqualityOperator(StringBuilder source, Indentation indentation, string type)
    {
        source.AppendLine($"{indentation}public static bool operator ==({type} lhs, {type} rhs) => lhs.Equals(rhs);");
    }

    public static void AppendValueTypeInequalityOperator(StringBuilder source, Indentation indentation, string type)
    {
        source.AppendLine($"{indentation}public static bool operator !=({type} lhs, {type} rhs) => (lhs == rhs) is false;");
    }
}
