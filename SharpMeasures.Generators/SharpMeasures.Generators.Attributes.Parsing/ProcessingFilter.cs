namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IProcessingFilter<in TContext, in TDefinition, TProduct> where TContext : IProcessingContext
{
    public abstract IResultWithDiagnostics<IReadOnlyList<TProduct>> Filter(TContext context, IEnumerable<TDefinition> definitions);
    public abstract IOptionalWithDiagnostics<TProduct> Filter(TContext context, TDefinition definition);
}

public interface IReprocessingFilter<in TContext, in TDefinition, TProduct> where TContext : IProcessingContext
{
    public abstract IOptionalWithDiagnostics<TProduct> Filter(TContext context, IEnumerable<TDefinition> definitions, TProduct initialProduct);
}

public static class ProcessingFilter
{
    public static IProcessingFilter<TContext, TDefinition, TProduct> Create<TContext, TDefinition, TProduct>(IProcesser<TContext, TDefinition, TProduct> processer) where TContext : IProcessingContext
    {
        return new SimpleProcessingFilter<TContext, TDefinition, TProduct>(processer);
    }

    public static IProcessingFilter<TContext, TDefinition, TProduct> Create<TContext, TDefinition, TProduct>(IActionableProcesser<TContext, TDefinition, TProduct> processer) where TContext : IProcessingContext
    {
        return new ActionableProcessingFilter<TContext, TDefinition, TProduct>(processer);
    }

    public static IReprocessingFilter<TContext, TDefinitionw, TProduct> Create<TContext, TDefinitionw, TProduct>(IReprocesser<TContext, TDefinitionw, TProduct> reprocesser) where TContext : IProcessingContext
    {
        return new SimpleReprocessingFilter<TContext, TDefinitionw, TProduct>(reprocesser);
    }

    public static IReprocessingFilter<TContext, TDefinitionw, TProduct> Create<TContext, TDefinitionw, TProduct>(IActionableReprocesser<TContext, TDefinitionw, TProduct> reprocesser) where TContext : IProcessingContext
    {
        return new ActionableReprocessingFilter<TContext, TDefinitionw, TProduct>(reprocesser);
    }

    private class SimpleProcessingFilter<TContext, TDefinition, TProduct> : IProcessingFilter<TContext, TDefinition, TProduct> where TContext : IProcessingContext
    {
        private IProcesser<TContext, TDefinition, TProduct> Processer { get; }

        public SimpleProcessingFilter(IProcesser<TContext, TDefinition, TProduct> processer)
        {
            Processer = processer;
        }

        public IResultWithDiagnostics<IReadOnlyList<TProduct>> Filter(TContext context, IEnumerable<TDefinition> definitions)
        {
            List<TProduct> products = new();
            IEnumerable<Diagnostic> diagnostics = Array.Empty<Diagnostic>();

            foreach (TDefinition definition in definitions)
            {
                var result = Process(context, definition);
                diagnostics = diagnostics.Concat(result.Diagnostics);

                if (result.HasResult)
                {
                    products.Add(result.Result);
                }
            }

            return ResultWithDiagnostics.Construct(products as IReadOnlyList<TProduct>, diagnostics);
        }

        public IOptionalWithDiagnostics<TProduct> Filter(TContext context, TDefinition definition)
        {
            return Process(context, definition);
        }

        protected virtual IOptionalWithDiagnostics<TProduct> Process(TContext context, TDefinition definition)
        {
            return Processer.Process(context, definition);
        }
    }

    private sealed class ActionableProcessingFilter<TContext, TDefinition, TProduct> : SimpleProcessingFilter<TContext, TDefinition, TProduct> where TContext : IProcessingContext
    {
        private IActionableProcesser<TContext, TDefinition, TProduct> Processer { get; }

        public ActionableProcessingFilter(IActionableProcesser<TContext, TDefinition, TProduct> processer) : base(processer)
        {
            Processer = processer;
        }

        protected override IOptionalWithDiagnostics<TProduct> Process(TContext context, TDefinition definition)
        {
            Processer.OnStartProcessing(context, definition);
            var result = base.Process(context, definition);

            InvokeResultingCall(context, definition, result);

            return result;
        }

        private void InvokeResultingCall(TContext context, TDefinition definition, IOptionalWithDiagnostics<TProduct> result)
        {
            if (result.HasResult)
            {
                Processer.OnSuccessfulProcess(context, definition, result.Result);

                return;
            }
                
            Processer.OnUnsuccessfulProcess(context, definition);
        }
    }

    private class SimpleReprocessingFilter<TContext, TDefinition, TProduct> : IReprocessingFilter<TContext, TDefinition, TProduct> where TContext : IProcessingContext
    {
        private IReprocesser<TContext, TDefinition, TProduct> Reprocesser { get; }

        public SimpleReprocessingFilter(IReprocesser<TContext, TDefinition, TProduct> reprocesser)
        {
            Reprocesser = reprocesser;
        }

        public IOptionalWithDiagnostics<TProduct> Filter(TContext context, IEnumerable<TDefinition> definitions, TProduct product)
        {
            IOptionalWithDiagnostics<TProduct> result = OptionalWithDiagnostics.Result(product);
            IEnumerable<Diagnostic> allDiagnostics = result.Diagnostics;

            foreach (TDefinition definition in definitions)
            {
                result = Reprocess(context, definition, result.Result);
                allDiagnostics = allDiagnostics.Concat(result.Diagnostics);

                if (result.LacksResult)
                {
                    return result;
                }
            }

            return result;
        }

        protected virtual IOptionalWithDiagnostics<TProduct> Reprocess(TContext context, TDefinition definition, TProduct product)
        {
            return Reprocesser.Reprocess(context, definition, product);
        }
    }

    private sealed class ActionableReprocessingFilter<TContext, TDefinition, TProduct> : SimpleReprocessingFilter<TContext, TDefinition, TProduct> where TContext : IProcessingContext
    {
        private IActionableReprocesser<TContext, TDefinition, TProduct> Reprocesser { get; }

        public ActionableReprocessingFilter(IActionableReprocesser<TContext, TDefinition, TProduct> reprocesser) : base(reprocesser)
        {
            Reprocesser = reprocesser;
        }

        protected override IOptionalWithDiagnostics<TProduct> Reprocess(TContext context, TDefinition definition, TProduct product)
        {
            Reprocesser.OnStartReprocessing(context, definition, product);
            var result = base.Reprocess(context, definition, product);

            InvokeResultingCall(context, definition, result);

            return result;
        }

        private void InvokeResultingCall(TContext context, TDefinition definition, IOptionalWithDiagnostics<TProduct> product)
        {
            if (product.HasResult)
            {
                Reprocesser.OnSuccessfulReprocess(context, definition, product.Result);

                return;
            }

            Reprocesser.OnUnsuccessfulReprocess(context, definition, product.Result);
        }
    }
}
