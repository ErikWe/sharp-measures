import re
from pathlib import Path

def readGenericTag(tag):
    return readTag(scalarGeneric, tag)

def readScalarTag(quantityName, tag):
    if quantityName in scalars:
        
        if not scalars[quantityName]:
            return readGenericTag(tag)

        content = readTag(scalars[quantityName], tag)
        return content if content else readGenericTag(tag)
    else:
        scalars[quantityName] = readQuantity(quantityName)
        return readScalarTag(quantityName, tag)

def readTag(text, tag):
    results = re.findall('(#Document:' + tag + '#\n)(.+?)(\n#\/Document:' + tag + '#)', text, flags=re.S)
    if len(results) == 0:
        return False

    return results[0][1]

def readQuantity(quantityName):
    try:
        f = open(str(Path(__file__).parent) + '\\Scalars\\' + quantityName + '.txt', 'r')
        text = f.read()
        f.close()
        return text
    except FileNotFoundError:
        return False

scalars = {}
vectors = {}

scalarGeneric = readQuantity('Generic')