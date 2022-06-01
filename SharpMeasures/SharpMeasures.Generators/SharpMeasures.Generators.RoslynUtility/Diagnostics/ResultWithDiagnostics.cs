namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IResultWithDiagnostics<T> : IOptionalWithDiagnostics<T>
{
    new public abstract IResultWithDiagnostics<T> AddDiagnostics(IEnumerable<Diagnostic> diagnostics);
    new public abstract IResultWithDiagnostics<T> AddDiagnostics(params Diagnostic[] diagnostics);
    new public abstract IResultWithDiagnostics<T> ReplaceDiagnostics(IEnumerable<Diagnostic> diagnostics);
    new public abstract IResultWithDiagnostics<T> ReplaceDiagnostics(params Diagnostic[] diagnostics);
}

public static class ResultWithDiagnostics
{
    public static IResultWithDiagnostics<T> Construct<T>(T result, IEnumerable<Diagnostic> diagnostics)
    {
        return new SimpleResultWithDiagnostics<T>(result, diagnostics);
    }

    public static IResultWithDiagnostics<T> Construct<T>(T result, params Diagnostic[] diagnostics)
    {
        return Construct(result, diagnostics as IEnumerable<Diagnostic>);
    }

    public static IResultWithDiagnostics<T> Construct<T>(T result, Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return Construct(result);
        }

        return Construct(result, new[] { diagnostics });
    }

    private class SimpleResultWithDiagnostics<T> : IResultWithDiagnostics<T>
    {
        public T Result { get; }
        public IEnumerable<Diagnostic> Diagnostics { get; }

        public SimpleResultWithDiagnostics(T result, IEnumerable<Diagnostic> diagnostics)
        {
            Result = result;
            Diagnostics = diagnostics;
        }

        public SimpleResultWithDiagnostics<T> AddDiagnostics(IEnumerable<Diagnostic> diagnostics)
        {
            return new SimpleResultWithDiagnostics<T>(Result, Diagnostics.Concat(diagnostics));
        }

        public SimpleResultWithDiagnostics<T> AddDiagnostics(params Diagnostic[] diagnostics) => AddDiagnostics(diagnostics as IEnumerable<Diagnostic>);

        public SimpleResultWithDiagnostics<T> ReplaceDiagnostics(IEnumerable<Diagnostic> diagnostics)
        {
            return new SimpleResultWithDiagnostics<T>(Result, diagnostics);
        }

        public SimpleResultWithDiagnostics<T> ReplaceDiagnostics(params Diagnostic[] diagnostics) => ReplaceDiagnostics(diagnostics as IEnumerable<Diagnostic>);

        IOptionalWithDiagnostics IOptionalWithDiagnostics.AddDiagnostics(IEnumerable<Diagnostic> diagnostics) => AddDiagnostics(diagnostics);
        IOptionalWithDiagnostics IOptionalWithDiagnostics.AddDiagnostics(params Diagnostic[] diagnostics) => AddDiagnostics(diagnostics);
        IOptionalWithDiagnostics<T> IOptionalWithDiagnostics<T>.AddDiagnostics(IEnumerable<Diagnostic> diagnostics) => AddDiagnostics(diagnostics);
        IOptionalWithDiagnostics<T> IOptionalWithDiagnostics<T>.AddDiagnostics(params Diagnostic[] diagnostics) => AddDiagnostics(diagnostics);
        IResultWithDiagnostics<T> IResultWithDiagnostics<T>.AddDiagnostics(IEnumerable<Diagnostic> diagnostics) => AddDiagnostics(diagnostics);
        IResultWithDiagnostics<T> IResultWithDiagnostics<T>.AddDiagnostics(params Diagnostic[] diagnostics) => AddDiagnostics(diagnostics);
        IOptionalWithDiagnostics IOptionalWithDiagnostics.ReplaceDiagnostics(IEnumerable<Diagnostic> diagnostics) => ReplaceDiagnostics(diagnostics);
        IOptionalWithDiagnostics IOptionalWithDiagnostics.ReplaceDiagnostics(params Diagnostic[] diagnostics) => ReplaceDiagnostics(diagnostics);
        IOptionalWithDiagnostics<T> IOptionalWithDiagnostics<T>.ReplaceDiagnostics(IEnumerable<Diagnostic> diagnostics) => ReplaceDiagnostics(diagnostics);
        IOptionalWithDiagnostics<T> IOptionalWithDiagnostics<T>.ReplaceDiagnostics(params Diagnostic[] diagnostics) => ReplaceDiagnostics(diagnostics);
        IResultWithDiagnostics<T> IResultWithDiagnostics<T>.ReplaceDiagnostics(IEnumerable<Diagnostic> diagnostics) => ReplaceDiagnostics(diagnostics);
        IResultWithDiagnostics<T> IResultWithDiagnostics<T>.ReplaceDiagnostics(params Diagnostic[] diagnostics) => ReplaceDiagnostics(diagnostics);

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool IOptionalWithDiagnostics.HasResult => true;
        bool IOptionalWithDiagnostics.LacksResult => false;
    }
}
