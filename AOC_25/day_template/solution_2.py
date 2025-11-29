import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
from solution_1 import *

import input_paths

testData = getLineStrings(input_paths.input2ExamplePath)
data = getLineStrings(input_paths.input2Path)

