import sys
import re
from pathlib import Path
from . import Utility
from . import DocsReader

def ModifyText(text, quantityName, quantityData):
    unit = Utility.getUnitName(quantityName, quantityData)
    bases = Utility.getBases(quantityName, quantityData)
    inverse = Utility.getInverseQuantity(quantityName, quantityData)
    square = Utility.getSquareQuantity(quantityName, quantityData)
    cube = Utility.getCubeQuantity(quantityName, quantityData)
    squareRoot = Utility.getSquareRootQuantity(quantityName, quantityData)
    cubeRoot = Utility.getCubeRootQuantity(quantityName, quantityData)
    vector = Utility.getVectorName(quantityName, quantityData)

    text = text.replace('#Quantity#', quantityName)
    text = text.replace('#quantity#', Utility.lowerCase(quantityName))
    text = text.replace('#Unit#', unit)
    text = text.replace('#unit#', Utility.lowerCase(unit))

    if vector:
        text = text.replace('#VectorQuantity#', vector)
        text = text.replace('#vectorQuantity#', Utility.lowerCase(vector))

    if inverse:
        text = text.replace('#InverseQuantity#', inverse)
        text = text.replace('#inverseQuantity#', Utility.lowerCase(inverse))

    if square:
        text = text.replace('#SquareQuantity#', square)
        text = text.replace('#squareQuantity#', Utility.lowerCase(square))

    if cube:
        text = text.replace('#CubeQuantity#', cube)
        text = text.replace('#cubeQuantity#', Utility.lowerCase(cube))

    if squareRoot:
        text = text.replace('#SquareRootQuantity#', squareRoot)
        text = text.replace('#squareRootQuantity#', Utility.lowerCase(squareRoot))

    if cubeRoot:
        text = text.replace('#CubeRootQuantity#', cubeRoot)
        text = text.replace('#CubeRootQuantity#', Utility.lowerCase(cubeRoot))

    for i in range(10):
        text = text.replace('#Base' + str(i) + '#', bases['singular'][i % len(bases['singular'])])
        text = text.replace('#Base' + str(i) + 's#', bases['plural'][i % len(bases['plural'])])

    return text

def GetText(tag, quantityName):
    return DocsReader.readScalarTag(quantityName, tag)

def ProduceDocumentationLines(line, quantityName, quantityData):
    tag = line.split('#Document:')[1].split('#')[0]
    text = GetText(tag, quantityName)

    if not text:
        if '#Document:In' in line:
            return ModifyText(ProduceInUnitLines(line, quantityName, quantityData), quantityName, quantityData).split('\n')
        elif '#Document:One' in line:
            return ModifyText(ProduceOneUnitLines(line, quantityName, quantityData), quantityName, quantityData).split('\n')
        return []

    return ModifyText(text, quantityName, quantityData).split('\n')

def ProduceInUnitLines(line, quantityName, quantityData):
    unit = line.split('#Document:In')[1].split('#')[0]

    for possibleUnit in quantityData['units']:
        if not 'singular' in possibleUnit:
            continue

        plural = Utility.parsePlural(possibleUnit['singular'], possibleUnit['plural'])
        if unit == plural:
            text = GetText('InUnit', quantityName)
            text = text.replace('#UnitName#', possibleUnit['singular'])
            text = text.replace('#unitName#', Utility.lowerCase(possibleUnit['singular']))
            return text

def ProduceOneUnitLines(line, quantityName, quantityData):
    unit = line.split('#Document:One')[1].split('#')[0]

    for possibleUnit in quantityData['units']:
        if not 'singular' in possibleUnit:
            continue

        if unit == possibleUnit['singular']:
            text = GetText('OneUnit', quantityName)
            text = text.replace('#UnitName#', possibleUnit['singular'])
            text = text.replace('#unitName#', Utility.lowerCase(possibleUnit['singular']))
            return text

def CorrectLine(line):
    if line[:4] == '/// ':
        return line

    if line[:3] == '///':
        return '/// ' + line[3:]

    if line[:2] == '//':
        if line[2] == ' ':
            return '/// ' + line[3:]
        else:
            return '/// ' + line[2:]

    if line[0] == '/':
        if line[1] == ' ':
            return '/// ' + line[2:]
        else:
            return '/// ' + line[1:]

    if line[0] == ' ':
        return '/// ' + line[1:]
    else:
        return '/// ' + line

def documentationPass(text, quantityName, quantityData):
    rebuilt = ""
    modified = False

    for line in text.split('\n'):
        if '#' in line:
            indent = re.findall('[ ]*', line)[0]

        if '#Document:' in line:
            modified = True
            for documentLine in ProduceDocumentationLines(line, quantityName, quantityData):
                rebuilt += indent + CorrectLine(documentLine) + '\n'
        else:
            rebuilt += line + '\n'

    return rebuilt, modified

def document(text, quantityName, quantityData):
    while True:
        updatedText, modified = documentationPass(text, quantityName, quantityData)
        if modified:
            text = updatedText
        else:
            return text