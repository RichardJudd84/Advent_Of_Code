import sys
import os  
from typing import *
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def parseLines(data: List[str]):
    parsedData = []
    for line in data:
        buttons = line.split()
        lights = buttons.pop(0)
        last = buttons.pop()

        lights = [light == '#' for light in lights]
        buttons = [[int(b) for b in button[1:-1].split(',')] for button in buttons]
        last = [int(n) for n in last[1:-1].split(',')]

        data = {
            'lights' : lights[1:-1],
            'buttons' : buttons,
            'last' : last
        }
        parsedData.append(data)
    return parsedData

def handleLights(lightData: Dict):
    lights = lightData['lights']
    buttons = lightData['buttons']
    n = len(buttons)

    nButtonPresses: List[int] = [] 

    combos = allSubsets(range(n))
    for combo in combos: 
        test: List[bool] = [False] * len(lights)
        for i in combo: 
            button = buttons[i]
            toggleLights(test,button)
        if(test == lights):
            nButtonPresses.append(len(combo))
    return min(nButtonPresses)


def toggleLights(lights: List[bool], button: List[int]):
    for i in button:
        lights[i] = not lights[i]
    return lights


def allSubsets(arr):
    n = len(arr)
    result = []
    for mask in range(1 << n):
        subset = [arr[i] for i in range(n) if (mask >> i) & 1]
        result.append(subset)
    return result


if(__name__ == '__main__'):
    d = data
    parsedData = parseLines(d)
    sum = 0
    for lightData in parsedData:
        sum += handleLights(lightData)
    print(sum)
        
        