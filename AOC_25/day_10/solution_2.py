import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))
from utils.helpers import *
import input_paths
from solution_1 import *
from itertools import combinations_with_replacement

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)


def getLeastClicks(lightData: Dict):
    buttons = lightData['buttons']
    targetCode = lightData['last']
    currentCode = [0]* len(targetCode)
    clicks = 0
    print('getting least clicks', buttons, targetCode)
    leastClicks = getLeastClicksRecursive(buttons, targetCode, currentCode, clicks)
    return leastClicks
    

def getLeastClicksRecursive(buttons: List[List[int]], targetCode: List[int], currentCode: List[int], clicks: int, depth = 0):
    if(targetCode == currentCode):
        #print(targetCode, currentCode, clicks, depth)
        return clicks
    res = getNextLowestIndex(targetCode, currentCode)
    if(res == False):
        return None
    else:
        remainingClicks, indexOfLowest = res
        newCodes = getNewCodes(buttons,currentCode, remainingClicks, indexOfLowest)
        lowest = None
        for newCode in newCodes:
            if(isIvalidCode(targetCode, newCode)):
               print('invalid', targetCode, newCode)
               continue
            newClicks = getLeastClicksRecursive(buttons, targetCode, newCode, clicks + remainingClicks, depth +1 )
            if(newClicks !=None):
                if(lowest == None):
                    lowest = newClicks
                elif(newClicks < lowest):
                    lowest = newClicks
                #print('newClicks',lowest,  newClicks, depth)
        return lowest
    
def isIvalidCode(targetCode: List[int], currentCode: List[int]):
    for i in range(len(targetCode)):
        if(currentCode[i] > targetCode[i]):
            return True
    return False


def getNextLowestIndex(targetCode: List[int], currentCode: List[int]):
    remaining = [(targetCode[i] - currentCode[i] , i) for  i in range(len(targetCode)) if (targetCode[i] - currentCode[i]) > 0]
    if(len(remaining) == 0):
        return False
    remainingClicks, indexOfLowest = min(remaining)
    return (remainingClicks, indexOfLowest)

def getNewCodes(buttons: List[List[int]], newCode: List[int], remainingClicks:int, indexOfLowest:int):
    paths = []
    filteredButtons = [button for button in buttons if indexOfLowest in button]
    combos = [b for b in combinations_with_replacement(filteredButtons, remainingClicks)]
    # for every combo create shortest paths
    for combo in combos:
        newData = newCode.copy()
        for button in combo:
            for i in button:
                newData[i] += 1
        if(newData not in paths):
            paths.append(newData)
    return paths
   

if(__name__ == '__main__'):
    d = data
    parsedData = parseLines(d)
    sum = 0
    for lightData in parsedData:
        r = getLeastClicks(lightData)
        print(r)
        sum+=r
        break

    print(sum)

