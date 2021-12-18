import os
import sys
import subprocess
import argparse
import time
import webbrowser
import shutil
from pathlib import Path

parser = argparse.ArgumentParser(description='Produce a code coverage report.')
parser.add_argument('--collector', '-c', default='XPlat Code Coverage', help="Name of the collector.")
parser.add_argument('--keepResult', '-k', action='store_true', help="Do not remove the results file.")
parser.add_argument('--doNotOpen', '-o', action='store_true', help='Do not open the coverage report.')
parser.add_argument('--keepExisting', '-e', action='store_true', help='Keep already existing coverage reports.')
args = parser.parse_args()

result = subprocess.run(["dotnet", "test", "--collect", args.collector], capture_output = True, text = True)

path = None

useNextLine = False
for line in str(result.stdout).splitlines():
	if useNextLine:
		path = line.strip()
		break
	if "Attachments:" in line:
		useNextLine = True

if path == None:
	print('Failed to collect tests. Ensure that the command is issued from within the test project.')
	sys.exit()

coverageReportDirectory = path.split("\\TestResults")[0] + "\\CoverageReports"
directory = coverageReportDirectory + "\\" + time.strftime("%Y%m%d-%H%M%S")

if not os.path.exists(path):
	os.makedirs(path)

result = subprocess.run(["reportgenerator", "-reports:\"" + path + "\"", "-targetdir:\"" + directory + "\"", "-reporttypes:Html"], capture_output = True)

if not args.keepResult:
	Path(path).unlink()
	Path(path).parent.rmdir()

	if len(os.listdir(path.split("TestResults")[0] + "TestResults")) == 0:
		Path(path.split("TestResults")[0] + "TestResults").rmdir()

if not args.keepExisting:
	dirsToRemove = []
	for dirpath, dirnames, filenames in os.walk(coverageReportDirectory):
		if not dirpath == directory and not dirpath == coverageReportDirectory:
			dirsToRemove.append(dirpath)
	for dirpath in dirsToRemove:
		shutil.rmtree(dirpath)

if not args.doNotOpen:
	webbrowser.open('file://' + os.path.realpath(directory + "\\index.html"))