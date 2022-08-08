import argparse
import os

class Utility:
    
    def stateExists():
        return os.path.isdir(os.getcwd() + '/State')

class Starter:
    
    def run():
        if Utility.stateExists():
            print('Restore or finish already started cleanup before starting another process.')

parser = argparse.ArgumentParser(description = 'Delete unused Verify snapshots.')

parser.add_argument('mode', type = str, nargs = 1, choices = ['start', 'restore', 'finish'], help = 'The mode')

args = parser.parse_args()

if (args.mode[0] == 'start'):
    Starter.run()