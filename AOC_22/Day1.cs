using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day1{

        private static void result(int[] data){
            int vMax = 0, sum = 0;
            for(int i = 0; i<data.Length; i++){
                if (sum > vMax && data[i] == 0){
                    vMax = sum;
                    sum = 0;
                }
                else if (data[i] == 0){
                    sum = 0;
                }
                else{
                    sum += data[i]; 
                }
            }
            Console.WriteLine(vMax);
        }

        private static void result2(int[] data){
            List<int> cals = new List<int>();
            int sum = 0;
            for (int i = 0; i< data.Length; i++){
                if (data[i] == 0){
                    cals.Add(sum);
                    sum = 0;
                }
                else{
                    sum += data[i];
                }
            }
            cals.Sort();  
            int res =  cals[cals.Count()-1] +  cals[cals.Count()-2] +cals[cals.Count()-3];          
            Console.WriteLine(res);

        }


        public static void run(){
            Console.WriteLine("Day 1");
            String[] data = Helpers.getData("./Data/d1.txt");
            int[] cData = Helpers.cleanNumrericData(data);
            result(cData);
            result2(cData);
        }
    }
    
}