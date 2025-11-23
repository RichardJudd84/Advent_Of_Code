using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent_of_code_22{
    public class Helpers
    {
        // Argument: String filepath
        // return: list<String>, text file in a list 1 element = 1 line
        public static String[] getData(String path){
            String[] data = File.ReadAllLines(path);
            return data;
        } 

        // Argument: list<String>
        // Returns: List<int>, Parses text rows with intigers if they cannot be parsed the value is 0
        public static int[] cleanNumrericData(String[] data){
            int[] cleanData = new int[data.Length];
            for(int i = 0; i < data.Length; i++){
                int value = 0;
                if (int.TryParse(data[i],out value)){
                    cleanData[i] = value;
                }
                else {
                    cleanData[i] = 0;
                }                
            }            
            return cleanData;
        }

        public static void printList<T>(T[] lst){
            foreach(T row in lst) Console.WriteLine(row);
        }

        public static void printGrid(String[,] grid){
            for(int y = 0; y<grid.GetLength(0); y++){
                for(int x = 0; x<grid.GetLength(1); x++){
                    Console.Write(grid[y,x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

}