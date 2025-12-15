import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))
from utils.helpers import *
import input_paths
from solution_1 import *

testData = getLineStrings(input_paths.input2ExamplePath)
data = getLineStrings(input_paths.input1Path)

def getReverseDict(nodeDict: dict[str,List[str]]):
    reverseDict :dict[str,set[str]] = {}

    for sourceNode, targetNodes in nodeDict.items():
        for targetNode in targetNodes:
            if(targetNode in reverseDict):
                reverseDict[targetNode].add(sourceNode)
            else:
                reverseDict[targetNode] = {sourceNode}
    return reverseDict

def getPathSetDict(nodeDict: dict[str,List[str]]):
    pathSetDict: dict[str, set[str]] = { key:set() for key in nodeDict }
    pathSetDict['out'] = set()
    return pathSetDict
    
def printDict(d: dict):
    for k ,v in d.items():
        print(k, v)
    print()

def setPathSetDict(reverseDict :dict[str,set[str]], forwardsDict :dict[str,set[str]], pathSetDict: dict[str, set[str]]):
    explored: set[str] = set()
    nextNodes = ['out']
    while(len(nextNodes)>0):
        
        currentNode = nextNodes.pop(0)
        
        if(currentNode in explored):
            continue
        else:
            explored.add(currentNode)

        # add next nodes to que
        if(currentNode != 'svr'):

            nextNodes.extend(reverseDict[currentNode])

        if(currentNode in forwardsDict):
            if(currentNode == 'fft'):
                print('adding', currentNode)
            previousNodes = forwardsDict[currentNode]
            for previousNode in previousNodes:
                pathSetDict[currentNode].add(previousNode)
                for path in pathSetDict[previousNode]:
                    pathSetDict[currentNode].add(path)

    return pathSetDict

def getPathsRecursive(pathDict: dict, pathSetDict: dict[str, set[str]], key:str, hasfft= False, hasDac = False):
    if(key == 'fft'):
        hasfft = True
    if(key == 'dac'):
        hasDac = True
    if key == 'out':
        print('valid')
        return 1
    
    hasOut, hasfftV, hasDacV = isValidPath(pathSetDict, key)
    if(not hasOut):
        print('Invalid no out')
        return 0
    
    if(not ((hasfft or hasfftV) and (hasDac or hasDacV))):
        print('Invalid fft dac')
        return 0
    

    else:
        nextPaths = pathDict[key]
        nPaths = 0
        for pathKey in nextPaths:
            nPaths += getPathsRecursive(pathDict, pathSetDict, pathKey, hasfft, hasDac)
        return nPaths 


def isValidPath(pathSetDict: dict[str, set[str]], key:str):
    pathKeys = pathSetDict[key]
    hasOut =  'out' in pathKeys
    hasfft = 'fft' in pathKeys
    hasDac = 'dac' in pathKeys
    return hasOut, hasfft, hasDac

         

    
if(__name__ == '__main__'):
    d = data
    pathDict = parseData(d)
    #printDict(pathDict)
    reverseDict = getReverseDict(pathDict)
    #printDict(reverseDict)
    pathSetDict = getPathSetDict(pathDict)
    #print(pathSetDict)
    pathSetDict = setPathSetDict(reverseDict,pathDict,pathSetDict)
    pathSetDict = setPathSetDict(reverseDict,pathDict,pathSetDict)
    pathSetDict = setPathSetDict(reverseDict,pathDict,pathSetDict)
    pathSetDict = setPathSetDict(reverseDict,pathDict,pathSetDict)
    pathSetDict = setPathSetDict(reverseDict,pathDict,pathSetDict)
    #print(pathSetDict)

    nr = getPathsRecursive(pathDict ,pathSetDict,'svr')
    print(nr)


