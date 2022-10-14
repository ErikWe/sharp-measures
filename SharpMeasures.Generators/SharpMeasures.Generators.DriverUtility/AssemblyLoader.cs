namespace SharpMeasures.Generators.DriverUtility;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

internal static class AssemblyLoader
{
    private static ReadOnlyCollection<Assembly>? CachedAssemblyList { get; set; }
    public static ReadOnlyCollection<Assembly> ReferencedAssemblies => CachedAssemblyList ??= GetReferencedAssemblies();

    /// <credit>https://dotnetcoretutorials.com/2020/07/03/getting-assemblies-is-harder-than-you-think-in-c/</credit>
    private static ReadOnlyCollection<Assembly> GetReferencedAssemblies()
    {
        Queue<Assembly> unresolvedAssemblies = new();
        List<Assembly> collectedAssemblies = new();

        HashSet<string> resolvedAssemblyNames = new();

        if (Assembly.GetEntryAssembly() is not Assembly entryAssembly)
        {
            return collectedAssemblies.AsReadOnly();
        }

        unresolvedAssemblies.Enqueue(entryAssembly);

        while (unresolvedAssemblies.Any())
        {
            Assembly targetAssembly = unresolvedAssemblies.Dequeue();

            foreach (AssemblyName assemblyName in targetAssembly.GetReferencedAssemblies())
            {
                if (resolvedAssemblyNames.Contains(assemblyName.FullName) is false)
                {
                    resolvedAssemblyNames.Add(assemblyName.FullName);

                    try
                    {
                        Assembly assembly = Assembly.Load(assemblyName);

                        unresolvedAssemblies.Enqueue(assembly);
                        collectedAssemblies.Add(assembly);
                    }
                    catch (FileLoadException) { }
                    catch (FileNotFoundException) { }
                }
            }
        }
        
        foreach (Assembly domainAssembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            collectedAssemblies.Add(domainAssembly);
        }
        
        return collectedAssemblies.AsReadOnly();
    }
}
