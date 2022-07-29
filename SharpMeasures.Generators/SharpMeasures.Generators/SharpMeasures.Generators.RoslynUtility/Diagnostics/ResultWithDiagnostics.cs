namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Collections;
using System.Collections.Generic;

public interface IResultWithDiagnostics<T> : IOptionalWithDiagnostics<T> { }

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

        T? IOptionalWithDiagnostics<T>.NullableResult => Result;

        public SimpleResultWithDiagnostics(T result, IEnumerable<Diagnostic> diagnostics)
        {
            Result = result;
            Diagnostics = diagnostics;
        }

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool IOptionalWithDiagnostics.HasResult => true;
        bool IOptionalWithDiagnostics.LacksResult => false;
    }
}
