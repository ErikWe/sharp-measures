#!/bin/sh

rm -r -f Reports/Coverage/*

mkdir -p Reports

echo "*" > Reports/.gitignore

dotnet test --settings coverage.runsettings

result=$?

if [ $result != 0 ]; then
    exit $result
fi

echo

reportgenerator -reports:`find Reports/Coverage -name *.cobertura.xml` -targetdir:Reports/Coverage

start Reports/Coverage/index.html

exit 0