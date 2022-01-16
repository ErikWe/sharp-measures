import os
import sys
import re
import json
import argparse
import shutil
from pathlib import Path
import GenerateScalar
import GenerateVectorN

parser = argparse.ArgumentParser(description='Generate all quantities.')
parser.add_argument('-destination', default='Quantities\\Generated', help='Relative path of destination directory.')
parser.add_argument('-definitions', default='AutoGeneration\\Quantities.json', help='Relative path of quantity definitions.')
parser.add_argument('-scalarTemplate', default='AutoGeneration\\Scalar.txt', help='Relative path of scalar template file.')
parser.add_argument('-vectorNTemplate', default='AutoGeneration\\VectorN.txt', help='Relative path of vectorN template file.')
parser.add_argument('-DESTROY', action='store_true', help='Enable overwriting existing content.')
args = parser.parse_args()

targetDirectory = str(Path(os.getcwd()).absolute()) + '\\' + args.destination
definitionsFile = str(Path(os.getcwd()).absolute()) + '\\' + args.definitions
scalarTemplateFile = str(Path(os.getcwd()).absolute()) + '\\' + args.scalarTemplate
vectorNTemplateFile = str(Path(os.getcwd()).absolute()) + '\\' + args.vectorNTemplate

f = open(definitionsFile, 'r')
definitionsRaw = f.read()
f.close()

f = open(scalarTemplateFile, 'r')
scalarTemplateRaw = f.read()
f.close()

f = open(vectorNTemplateFile, 'r')
vectorNTemplateRaw = f.read()
f.close()

json = json.loads(definitionsRaw)

if os.path.exists(targetDirectory):
    if len(os.listdir(targetDirectory)) != 0 and not args.DESTROY:
        print("The directory [\"" + targetDirectory + "\"] is not empty. To continue and LOSE EXISTING CONTENT, add the flag \'-DESTROY\'.")
        sys.exit()
    shutil.rmtree(targetDirectory)

os.makedirs(targetDirectory)

for quantityName, quantityData in json['scalars'].items():
    GenerateScalar.generate(targetDirectory, scalarTemplateRaw, json, quantityName, quantityData)

for quantityName, quantityData in json['vectors'].items():
    GenerateVectorN.generate(targetDirectory, vectorNTemplateRaw, json, quantityName, quantityData)