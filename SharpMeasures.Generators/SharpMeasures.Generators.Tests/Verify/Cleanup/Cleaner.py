import argparse
import glob
import os
import shutil

class Utility:
    
    def stateDirectoryPath():
        return os.getcwd() + '\State'

    def verifyDirectoryPath():
        return os.getcwd() + '\..\Snapshots'

    def stateDirectoryExists():
        return os.path.isdir(Utility.stateDirectoryPath())

    def verifyDirectoryExists():
        return os.path.isdir(Utility.verifyDirectoryPath())

    def deleteStateDirectory():
        print('Deleting state...')
        shutil.rmtree(Utility.stateDirectoryPath())
        print('State deleted.')

    def deleteVerifyDirectory():
        print('Deleting verification files...')
        shitil.rmtree(Utility.verifyDirectoryPath())
        print('Verification files deleted.')

    def moveToStateDirectory():
        print('Moving verification files...')
        shutil.move(Utility.verifyDirectoryPath(), Utility.stateDirectoryPath())
        print('Verification files moved.')

    def restoreVerifyDirectory():
        print('Restoring verification files...')
        shutil.move(Utility.stateDirectoryPath(), Utility.verifyDirectoryPath())
        print('Verification files restored.')

    def getVerifiedFiles():
        return glob.glob(Utility.verifyDirectoryPath() + '/**/*.verified.txt', recursive = True)

    def getAllSavedVerifiedFiles():
        return glob.glob(Utility.stateDirectoryPath() + '/**/*.verified.txt', recursive = True)

    def getCorrespondingSavedVerifiedFile(verifiedFile, issueWarning):
        savedFile = verifiedFile.replace('Snapshots', 'Cleanup/State')

        if os.path.isfile(savedFile):
            return savedFile

        if issueWarning:
            print(verifiedFile + ' has no corresponding verified file.')

        return False

    def getCorrespondingReceivedFile(verifiedFile):
        return verifiedFile.replace('verified', 'received')

class Starter:
    
    def run():
        if Utility.stateDirectoryExists():
            print('Restore, finish or delete the already started cleanup before starting another cleanup.')
            return

        Utility.moveToStateDirectory()

class Finisher:

    def run(verifiedWarnings):
        if Utility.stateDirectoryExists() is False:
            print('Cannot finish cleanup, as no started cleanup was found.')
            return

        if Utility.verifyDirectoryExists() is False:
            print('The cleanup cannot be finished until the tests are run.')
            return

        verifiedFiles = Utility.getVerifiedFiles()

        appliedFiles = 0
        savedFiles = len(Utility.getAllSavedVerifiedFiles())

        for verifiedFile in verifiedFiles:
            savedFile = Utility.getCorrespondingSavedVerifiedFile(verifiedFile, verifiedWarnings)

            if savedFile is False:
                continue

            appliedFiles += 1

            os.remove(verifiedFile)
            os.remove(Utility.getCorrespondingReceivedFile(verifiedFile))
            
            shutil.move(savedFile, verifiedFile)

        if appliedFiles == savedFiles:
            print('No unused files were found.')
        else:
            print('Removed ' + str(savedFiles - appliedFiles) + ' unused files.')

        Utility.deleteStateDirectory()

class Restorer:

    def verificationstring():
        return 'yes, restore'

    def attemptDeleteNewVerifyFiles():
        print('Tests has been run since the current cleanup was started. Overwrite the most recent files with the saved cleanup state? ("' + Restorer.verificationString() + '")')
        verification = input()

        if verification == Restorer.verificationString():
            Utility.deletetVerifyDirectory()
            return True

        return False

    def run():
        if Utility.stateDirectoryExists() is False:
            print('Cannot restore cleanup state, as no started cleanup was found.')
            return

        if Utility.verifyDirectoryExists() and (Restorer.attemptDeleteNewVerifyFiles() is False):
            return

        Utility.restoreVerifyDirectory()

class Deleter:

    def verificationString():
        return 'yes, delete'

    def getVerification():
        print('Delete state? Restore can be used to gracefully abort the started cleanup. ("' + Deleter.verificationString() + '")')
        verification = input()

        return verification == Deleter.verificationString()

    def run():
        if Utility.stateDirectoryExists() is False:
            print('Cleanup state cannot be deleted, as no started cleanup was found.')
            return

        if (Deleter.getVerification()):
            Utility.deleteStateDirectory()
        else:
            print('Exiting without deletion...')

parser = argparse.ArgumentParser(description = 'Delete unused Verify snapshots.')

parser.add_argument('mode', type = str, nargs = 1, choices = ['start', 'finish', 'restore', 'force-delete'], help = 'The mode')
parser.add_argument('-v', '--noVerifiedWarnings', action = 'store_true', help = 'Enables warnings for received files with no corresponding verified file.')

args = parser.parse_args()

if args.mode[0] == 'start':
    Starter.run()

if args.mode[0] == 'finish':
    Finisher.run(args.noVerifiedWarnings)

if args.mode[0] == 'restore':
    Restorer.run()

if args.mode[0] == 'force-delete':
    Deleter.run()