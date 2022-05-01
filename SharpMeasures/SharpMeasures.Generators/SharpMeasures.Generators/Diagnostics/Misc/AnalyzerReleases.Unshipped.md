; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
Measure1000 | Usage | Warning | TypeNotPartial
Measure1001 | Usage | Warning | TypeNotScalarQuantity
Measure1002	| Usage | Warning | TypeNotUnbiasedScalarQuantity
Measure1003 | Usage | Warning | TypeNotUnit
Measure1100 | Usage | Warning | InvalidUnitDerivationExpression
Measure1101 | Usage | Warning | EmptyUnitDerivationSignature
Measure1200 | Usage | Warning | InvalidUnitName
Measure1201 | Usage | Warning | UnitNameNotRecognized
Measure1202 | Usage | Warning | InvalidUnitPluralForm
Measure1203 | Usage | Warning | DerivedUnitSignatureNotRecognized
Measure1204 | Usage | Warning | UnitListNotMatchingSignature
Measure1205 | Usage | Warning | UndefinedPrefix