namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IResultWithDiagnostics<T> : IOptionalWithDiagnostics<T>
{
    new public abstract IResultWithDiagnostics<T> AddDiagnostics(params IEnumerable<Diagnostic>[] diagnostics);

    new public delegate IResultWithDiagnostics<TNew> DTransform<TNew>(T result);
    public abstract IResultWithDiagnostics<TNew> Merge<TNew>(DTransform<TNew> transform);
    new public abstract IResultWithDiagnostics<TNew> Transform<TNew>(DResult<TNew> transform);
}

public static class ResultWithDiagnostics
{
    public static IResultWithDiagnostics<T> Construct<T>(T result, IEnumerable<Diagnostic> diagnostics) => new SimpleResultWithDiagnostics<T>(result, diagnostics);
    public static IResultWithDiagnostics<T> Construct<T>(T result, params Diagnostic[] diagnostics) => Construct(result, diagnostics as IEnumerable<Diagnostic>);
    public static IResultWithDiagnostics<T> Construct<T>(T result, Diagnostic? diagnostics)
    {
        if (diagnostics is null)
        {
            return Construct(result);
        }

        return Construct(result, new[] { diagnostics });
    }

    public static IResultWithDiagnostics<T> ConditionalDiagnostics<T>(T result, bool condition, Func<IEnumerable<Diagnostic>> diagnosticsDelegate)
    {
        if (condition)
        {
            return Construct(result);
        }

        return Construct(result, diagnosticsDelegate());
    }

    public static IResultWithDiagnostics<T> ConditionalDiagnostics<T>(T result, bool condition, Func<Diagnostic?> diagnosticsDelegate)
    {
        if (condition)
        {
            return Construct(result);
        }

        return Construct(result, diagnosticsDelegate());
    }

    private sealed class SimpleResultWithDiagnostics<T> : IResultWithDiagnostics<T>
    {
        public T Result { get; }
        public IEnumerable<Diagnostic> Diagnostics { get; }

        public SimpleResultWithDiagnostics(T result, IEnumerable<Diagnostic> diagnostics)
        {
            Result = result;
            Diagnostics = diagnostics;
        }

        public IResultWithDiagnostics<T> AddDiagnostics(params IEnumerable<Diagnostic>[] diagnostics) => new SimpleResultWithDiagnostics<T>(Result, Diagnostics.Concat(diagnostics.SelectMany(static (diagnostics) => diagnostics)));

        public IOptionalWithDiagnostics<T> Validate(IOptionalWithDiagnostics<T>.DValidator validator)
        {
            var validity = validator(Result);
            var allDiagnostics = Diagnostics.Concat(validity.Diagnostics);

            if (validity.IsInvalid)
            {
                return OptionalWithDiagnostics.Empty<T>(allDiagnostics);
            }

            return new SimpleResultWithDiagnostics<T>(Result, allDiagnostics);
        }

        public IValidityWithDiagnostics Reduce() => ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics);
        public IValidityWithDiagnostics Reduce(IOptionalWithDiagnostics<T>.DValidator validator) => Validate(validator).Reduce();

        public IResultWithDiagnostics<TNew> Merge<TNew>(IResultWithDiagnostics<T>.DTransform<TNew> transform)
        {
            var result = transform(Result);

            return new SimpleResultWithDiagnostics<TNew>(result.Result, Diagnostics.Concat(result.Diagnostics));
        }

        public IResultWithDiagnostics<TNew> Transform<TNew>(IOptionalWithDiagnostics<T>.DResult<TNew> transform)
        {
            var result = transform(Result);

            return new SimpleResultWithDiagnostics<TNew>(result, Diagnostics);
        }

        public IEnumerator<Diagnostic> GetEnumerator() => Diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics.AsEmptyOptional<TNew>() => OptionalWithDiagnostics.Empty<TNew>(Diagnostics);
        IOptionalWithDiagnostics<T> IOptionalWithDiagnostics<T>.AddDiagnostics(params IEnumerable<Diagnostic>[] diagnostics) => AddDiagnostics(diagnostics);

        bool IOptionalWithDiagnostics.HasResult => true;
        bool IOptionalWithDiagnostics.LacksResult => false;

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Merge<TNew>(IOptionalWithDiagnostics<T>.DTransform<TNew> transform)
        {
            var result = transform(Result);
            var allDiagnostics = Diagnostics.Concat(result.Diagnostics);

            if (result.HasResult)
            {
                return new SimpleResultWithDiagnostics<TNew>(result.Result, allDiagnostics);
            }

            return OptionalWithDiagnostics.Empty<TNew>(allDiagnostics);
        }

        IOptionalWithDiagnostics<TNew> IOptionalWithDiagnostics<T>.Transform<TNew>(IOptionalWithDiagnostics<T>.DResult<TNew> transform)
        {
            var result = transform(Result);

            return new SimpleResultWithDiagnostics<TNew>(result, Diagnostics);
        }
    }
}
