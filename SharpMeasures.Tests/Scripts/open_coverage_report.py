import os
import sys
import subprocess
import argparse
import time
import webbrowser
import shutil
from pathlib import Path

parser = argparse.ArgumentParser(description='Open the most recent code coverage report.')
parser.add_argument('--directory', '-d', default='./CoverageReports', help="Path to the directory containing coverage reports")
args = parser.parse_args()

mostRecent = None
mostRecentTimestamp = 0

for dirpath, dirnames, filenames in os.walk(args.directory):
	if not dirpath == args.directory:
		timestamp = os.path.getmtime(dirpath)
		
		if timestamp > mostRecentTimestamp:
			mostRecent = dirpath
			mostRecentTimestamp = timestamp

if mostRecent == None:
	print('No appropriate coverage report was found.')
else:
	webbrowser.open('file://' + os.path.realpath(mostRecent + "\\index.html"))