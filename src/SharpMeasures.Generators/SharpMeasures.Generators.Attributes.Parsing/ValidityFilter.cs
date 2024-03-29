﻿namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IValidityFilter<in TContext, TDefinition> where TContext : IValidationContext
{
    public abstract IResultWithDiagnostics<IReadOnlyList<TDefinition>> Filter(TContext context, IEnumerable<TDefinition> definitions);
    public abstract IOptionalWithDiagnostics<TDefinition> Filter(TContext context, TDefinition definition);

    public abstract IValidityWithDiagnostics Validate(TContext context, IEnumerable<TDefinition> definitions);
    public abstract IValidityWithDiagnostics Validate(TContext context, TDefinition definition);
}

public static class ValidityFilter
{
    public static IValidityFilter<TContext, TDefinition> Create<TContext, TDefinition>(IValidator<TContext, TDefinition> validator) where TContext : IValidationContext
    {
        return new SimpleFilter<TContext, TDefinition>(validator);
    }

    public static IValidityFilter<TContext, TDefinition> Create<TContext, TDefinition>(IActionableValidator<TContext, TDefinition> validator) where TContext : IValidationContext
    {
        return new ActionableFilter<TContext, TDefinition>(validator);
    }

    private class SimpleFilter<TContext, TDefinition> : IValidityFilter<TContext, TDefinition> where TContext : IValidationContext
    {
        private IValidator<TContext, TDefinition> Validator { get; }

        public SimpleFilter(IValidator<TContext, TDefinition> validator)
        {
            Validator = validator;
        }

        public IResultWithDiagnostics<IReadOnlyList<TDefinition>> Filter(TContext context, IEnumerable<TDefinition> definitions)
        {
            List<TDefinition> validDefinitions = new();
            IEnumerable<Diagnostic> diagnostics = Array.Empty<Diagnostic>();

            foreach (var definition in definitions)
            {
                var validity = Validate(context, definition);

                diagnostics = diagnostics.Concat(validity.Diagnostics);

                if (validity.IsValid)
                {
                    validDefinitions.Add(definition);
                }
            }

            return ResultWithDiagnostics.Construct(validDefinitions as IReadOnlyList<TDefinition>, diagnostics);
        }

        public IOptionalWithDiagnostics<TDefinition> Filter(TContext context, TDefinition definition)
        {
            return Validate(context, definition).Transform(definition);
        }

        public IValidityWithDiagnostics Validate(TContext context, IEnumerable<TDefinition> definitions)
        {
            var validity = ValidityWithDiagnostics.Valid;

            foreach (var definition in definitions)
            {
                validity = validity.Validate(() => Validate(context, definitions));
            }

            return validity;
        }

        public virtual IValidityWithDiagnostics Validate(TContext context, TDefinition definition)
        {
            return Validator.Validate(context, definition);
        }
    }

    private sealed class ActionableFilter<TContext, TDefinition> : SimpleFilter<TContext, TDefinition> where TContext : IValidationContext
    {
        private IActionableValidator<TContext, TDefinition> Validator { get; }

        public ActionableFilter(IActionableValidator<TContext, TDefinition> validator) : base(validator)
        {
            Validator = validator;
        }

        public override IValidityWithDiagnostics Validate(TContext context, TDefinition definition)
        {
            Validator.OnStartValidation(context, definition);

            var validity = base.Validate(context, definition);

            InvokeResultingCall(context, definition, validity);

            return validity;
        }

        private void InvokeResultingCall(TContext context, TDefinition definition, IValidityWithDiagnostics validity)
        {
            if (validity.IsValid)
            {
                Validator.OnValidated(context, definition);
            }

            Validator.OnInvalidated(context, definition);
        }
    }
}
