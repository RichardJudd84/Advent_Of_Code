import sys
import os
from typing import List
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))
from utils.helpers import *
import input_paths
from solution_1 import *

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def mergeAllRanges(ranges: List[tuple[str]]):
    ranges = sortRanges(ranges)
    previous = ranges[0]
    mergedRanges: List[tuple[str]] = []

    for i in range(1,len(ranges)):
        current = ranges[i]
        previousEnd = int(previous[1])
        currentStart = int(current[0])
        if( currentStart <= previousEnd + 1 ):
            previous = mergeRanges(previous, current)
        else: 
            mergedRanges.append(previous)
            previous = current

    mergedRanges.append(previous)
    return mergedRanges
    

def mergeRanges(range1: tuple[str], range2: tuple[str]):
    start1, end1 = range1 
    start2, end2 = range2 
    return (min(start1,start2), max(end1,end2))

def sortRanges(ranges: List[tuple[str]]):
    ranges.sort(key=lambda x: int(x[0]))
    return ranges

def getNumberInRange(range: tuple[str]):
    return int(range[1]) - int(range[0]) + 1



if(__name__ == '__main__'):
    ranges, ingredients = getIngedientsAndRanges(data)
    mergedRanges = mergeAllRanges(ranges)

    sum = 0

    for r in mergedRanges:
        sum += getNumberInRange(r)

    print(sum)


# 336185947296474 to Low