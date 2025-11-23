using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day4{       

        private static void result(String[] data){
            int sum=0;
            foreach(var row in data){
                String[] eZones = row.Split(new char[]{'-',','});
                int[] eIZones = new int[4];
                for(var i = 0; i< eZones.Length; i++) int.TryParse(eZones[i], out eIZones[i]);
                if((eIZones[0] >= eIZones[2] && eIZones[1] <= eIZones[3]) || (eIZones[0] <= eIZones[2] && eIZones[1] >= eIZones[3])) sum+=1;
            }
            Console.WriteLine(sum);
        }

        private static void result2(String[] data){
             int sum=0;
            foreach(var row in data){
                String[] eZones = row.Split(new char[]{'-',','});
                int[] eIZones = new int[4];
                for(var i = 0; i< eZones.Length; i++) int.TryParse(eZones[i], out eIZones[i]);
                if(eIZones[0] >= eIZones[2] && eIZones[0] <= eIZones[3]) sum+=1; // if start of first is within second
                else if(eIZones[2] >= eIZones[0] && eIZones[2] <= eIZones[1]) sum+=1; // if start of second is within first
                else if(eIZones[1] >= eIZones[2] && eIZones[1] <= eIZones[3]) sum+=1; // if end of first is within second
                else if(eIZones[3] >= eIZones[0] && eIZones[3] <= eIZones[1]) sum+=1; // if end of second is withiin first
            }
            Console.WriteLine(sum);
        }


        public static void run(){
            Console.WriteLine("Day 4");
            var data = Helpers.getData("./Data/d4.txt");
            result(data);
            result2(data);
       
        }
    }
    
}