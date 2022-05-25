namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IResultWithDiagnostics<T> : IOptionalWithDiagnostics<T>
{
    new public abstract IResultWithDiagnostics<T> Update(IOptionalWithDiagnostics<T> potentialUpdate);
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

    public static IResultWithDiagnostics<T> WithoutDiagnostics<T>(T result)
    {
        return Construct(result, Array.Empty<Diagnostic>());
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

        public IResultWithDiagnostics<T> Update(T result)
        {
            return new SimpleResultWithDiagnostics<T>(result, Diagnostics);
        }

        public IResultWithDiagnostics<T> Update(IResultWithDiagnostics<T> update)
        {
            return new SimpleResultWithDiagnostics<T>(update.Result, Diagnostics.Concat(update.Diagnostics));
        }

        public IResultWithDiagnostics<T> Update(IOptionalWithDiagnostics<T> update)
        {
            if (update.HasResult)
            {
                return new SimpleResultWithDiagnostics<T>(update.Result, Diagnostics.Concat(update.Diagnostics));
            }

            return new SimpleResultWithDiagnostics<T>(Result, Diagnostics.Concat(update.Diagnostics));
        }

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool IOptionalWithDiagnostics.HasResult => true;
        bool IOptionalWithDiagnostics.LacksResult => false;

        IOptionalWithDiagnostics<T> IOptionalWithDiagnostics<T>.Update(IOptionalWithDiagnostics<T> update) => Update(update);
    }
}
