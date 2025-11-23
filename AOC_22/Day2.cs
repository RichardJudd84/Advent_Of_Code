using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day2{
        //  Rock Paper  Scissors
        private static int A = 1 , B = 2, C=3;
        private static int X = A , Y = B, Z = C;
        private static Dictionary<string, int> scores = new Dictionary<string, int>{{"A X", 3 + X}, {"B Y", 3 + Y}, {"C Z", 3 + Z},
                                                                                    {"A Y", 6 + Y}, {"B Z", 6 + Z}, {"C X", 6 + X},
                                                                                    {"A Z", 0 + Z}, {"B X", 0 + X}, {"C Y", 0 + Y}};
        //X lose, Y draw, z win

        private static Dictionary<string, string> convertToScores = new Dictionary<string, string>{{"A X", "A Z"}, {"B Y", "B Y"}, {"C Z", "C X"},
                                                                                                {"A Y", "A X"}, {"B Z", "B Z"}, {"C X", "C Y"},
                                                                                                 {"A Z", "A Y"}, {"B X", "B X"}, {"C Y", "C Z"}};
        

        private static void result(String[] data){
            int res = 0;
            foreach(var row in data){
                int val;
                if (scores.TryGetValue(row, out val)){
                    //Console.WriteLine(row+ " :" + val);
                    res += val;
                }
                else {
                    Console.WriteLine(row + "something went wrong!");
                    break;
                }
            }
            Console.WriteLine(res);
        }
        private static void result2(String[] data){
            int res = 0;
            foreach(var row in data){
                int val;
                String score;
                score = convertToScores[row];
                if (scores.TryGetValue(score, out val)){
                    //Console.WriteLine(row+ " :" + val);
                    res += val;
                }
                else {
                    Console.WriteLine(row + "something went wrong!");
                    break;
                }
            }
            Console.WriteLine(res);
        }

        public static void run(){
            Console.WriteLine("Day 2");
            String[] data = Helpers.getData("./Data/d2.txt");
            result(data);
            result2(data);
            //Helpers.printList<string>(data);
            
        }
    }
    
}