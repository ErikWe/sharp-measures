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
Measure1102 | Usage | Warning | DuplicateUnitDerivationSignature
Measure1200 | Usage | Warning | InvalidUnitName
Measure1201 | Usage | Warning | UnitNameNotRecognized
Measure1202 | Usage | Warning | DuplicateUnitName
Measure1203 | Usage | Warning | InvalidUnitPluralForm
Measure1204 | Usage | Warning | DerivedUnitSignatureNotRecognized
Measure1205 | Usage | Warning | UnitListNotMatchingSignature
Measure1206 | Usage | Warning | UndefinedPrefix
Measure1900 | Documentation | Info | UnresolvedDocumentationDependency
Measure1901 | Documentation | Info | NoMatchingDocumentationFile
Measure1902 | Documentation | Info | DocumentationFileMissingRequestedTag