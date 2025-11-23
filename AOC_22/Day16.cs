using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_code_22{

    class Valve: IComparable{
        public static Dictionary<Valve, int> valveflows = new Dictionary<Valve, int>();
        public Dictionary<Valve, int> valveDistances = new Dictionary<Valve, int>();
        public enum MOVES {OPEN_VALVE, MOVE, NO_TURNS};
        public String ID{get; set;}
        public int flowRate{get; set;}
        public List<Valve> tunnels{get; set;} = new List<Valve>();

        public bool ValveOn{get; set;} = false;

        public Valve(String ID, int flowRate){
            this.ID = ID;
            this.flowRate = flowRate;
            if(this.flowRate>0) Valve.valveflows.Add(this, this.flowRate);
        }
        public Valve(String ID, int flowRate, List<Valve> tunnels){
            this.ID = ID;
            this.flowRate = flowRate;
            this.tunnels = tunnels;
            if(this.flowRate>0) Valve.valveflows.Add(this, this.flowRate);
        }

        public Valve move(Valve nextValve){
            return nextValve;
        }

        public int getBestSequence(List<Valve> vs, int move , int score, int bestScore){
            Console.WriteLine(this.ID);
            move-=1; 
            if(this.valveDistances.Count == 0) getValveDistances();
            
            if (vs.Count == 1){
                score += vs[0].flowRate * (move-1);
                if(score > bestScore){
                    bestScore = score;
                }
                return bestScore;
            }
            else{
                foreach(Valve v in vs){
                    if(move-this.valveDistances[v]-1 < 0) continue;
                    int nextScore = score + (v.flowRate * (move-this.valveDistances[v]-1));
                    List<Valve> vc = new List<Valve>();
                    vc.AddRange(vs);
                    vc.Remove(v);
                    nextScore = v.getBestSequence(vc, move, nextScore, bestScore); 
                    if(nextScore > bestScore) bestScore = nextScore;
                }
            }
            return bestScore;
        }

        public void getValveDistances(){
            foreach(var v in valveflows.Keys){
                valveDistances.Add(v,findDistanceToValve(v));
            }
        }

        public int findDistanceToValve(Valve target){
            Queue<(Valve, int)> dfsQ = new Queue<(Valve, int)>();
            HashSet<Valve> explored = new HashSet<Valve>{this};
            if(target == this) return 0;
            bool found = false;

            dfsQ.Enqueue((this,1));
            while(!found){
                var dq = dfsQ.Dequeue();
                Valve current = dq.Item1;
                int moves = dq.Item2;
                foreach(Valve nextValve in current.tunnels){
                    if(nextValve == target) return moves;
                    else dfsQ.Enqueue((nextValve, moves+1));
                }
            }
            return -1;
        }

        public int CompareTo(object? obj)
        {
            return 0;
        }
    }

    class Day16{
        private static Dictionary<String, List<String>> valveTunnels = new Dictionary<string, List<string>>();
        private static Dictionary<String, Valve> valves = new Dictionary<string, Valve>();
        public static void parseData(String[] data){
            
            foreach(var row in data){
                var pd = (from num in Regex.Matches(row, @"[A-Z]{2}|(\-?\d+)") select num.Value).ToList();
                String name = pd[0];
                int flowRate = int.Parse(pd[1]);
                var tunnels = pd.GetRange(2, pd.Count-2);
                valveTunnels[name] = tunnels;
                valves[name] = new Valve(name, flowRate);
            }

            foreach(var valve in valves){
                List<Valve> tValves = new List<Valve>();
                foreach(var tunnel in valveTunnels[valve.Key]){
                    tValves.Add(valves[tunnel]);
                }
                valve.Value.tunnels = tValves;

                /*
                Console.Write($"key: {valve.Key}: v name {valve.Value.ID}, v flow {valve.Value.flowRate},\nTunnels: ");
                foreach(var tunnel in valve.Value.tunnels){
                    Console.Write($"{tunnel.ID}, ");  
                }
                Console.WriteLine();
                Console.WriteLine();*/
            }
        }


        public static void run(){
            Console.WriteLine("Day 16");
            String[] data = Helpers.getData("./Data/d16t.txt");
            parseData(data);

            Valve startV = valves["AA"];
            int bestScore = startV.getBestSequence(Valve.valveflows.Keys.ToList(), 30, 0, 0);
            Console.WriteLine(bestScore);
        }
    }
    
}