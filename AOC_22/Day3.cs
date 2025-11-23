using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day3{
         private static int getCharVal(char ch){
            int val = (int)ch;
            if (val <97) val -= 38;
            else val -= 96;
            return val;
        }

        private static int getDuplicateVals(string row){
            int mid  = 0;
            String dups = "";
            if (row.Length%2 == 0) mid = row.Length/2;
            else Console.WriteLine("something went wrong!");

            String comp1 = row.Substring(0,mid), comp2 = row.Substring(mid);

            foreach(char ch in comp1){
                if (comp2.Contains(ch)){
                    dups += ch;
                }
            }

            return getCharVal(dups[0]);
        }

        private static String getBadges(string[] data){
            String badges = "";
            
            for(int i = 0; i<data.Length; i+=3){
                String s1 = data[i], s2 = data[i+1], s3 = data[i+2];
                foreach(char ch in s1){
                    if (s2.Contains(ch) && s3.Contains(ch)){
                        badges += ch;
                        break;
                    }
                }
            }
            return badges;
        }

        private static void result(String[] data){
            int sum = 0;
            foreach(var row in data) sum += getDuplicateVals(row);
            Console.WriteLine(sum);
        }

        private static void result2(String[] data){
            int sum = 0;
            foreach(char ch in getBadges(data)) sum += getCharVal(ch); 
            Console.WriteLine(sum);
        }


        public static void run(){
            Console.WriteLine("Day 3");
            var data = Helpers.getData("./Data/d3.txt");
            result(data);
            result2(data);
        }
    }
    
}