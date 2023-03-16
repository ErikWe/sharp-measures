namespace SharpMeasures.Generators.Scalars;

using System;

internal static class UnitBaseInstanceNameInterpreter
{
    public static string InterpretName(string name)
    {
        if (name.StartsWith("Per", StringComparison.InvariantCulture) && char.IsUpper(name[3]))
        {
            return $"Once{name}";
        }

        return $"One{name}";
    }
}
