using InventoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventorySimulation.Form1;
using System.Data;
using static InventorySimulation.Program;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;

namespace InventorySimulation
{
     public class Build
    {
        public Build() { }
        public  void setAnswer(ref SimulationSystem simulationSystem)
        {
            simulationSystem.NumberOfDays = NumberOfDays;
            simulationSystem.ReviewPeriod = ReviewPeriod;
            simulationSystem.StartInventoryQuantity = StartInventoryQuantity;
            simulationSystem.StartLeadDays = StartLeadDays;
            simulationSystem.StartOrderQuantity = StartOrderQuantity;
            simulationSystem.OrderUpTo = OrderUpTo;
        }

        public void buildDemands(ref SimulationSystem simulationSystem)
        {
            for (int row = 0; row < GlobTable.Rows.Count; row++)
            {
                Distribution td = new Distribution();
                Console.WriteLine("-----------------------------------Output------------------------");
                /*Console.WriteLine(GlobTable.Rows[row][day * 4] == null);*/
                if (GlobTable.Rows[row].Field<int>("Demand").ToString() == "") break;
                td.Value = GlobTable.Rows[row].Field<int>("Demand");
                td.Probability = GlobTable.Rows[row].Field<decimal>("Demand Probability");
                td.CummProbability = GlobTable.Rows[row].Field<decimal>("Demand Cummulative Probability");
                (int, int) interval = GlobTable.Rows[row].Field<(int, int)>("Demand Range");
                td.MinRange = interval.Item1;
                td.MaxRange = interval.Item2;
                simulationSystem.DemandDistribution.Add(td);
                if (td.MaxRange == 100) break;
            }
        }

        public void buildLead(ref SimulationSystem simulationSystem)
        {
            for (int row = 0; row < GlobTable.Rows.Count; row++)
            {
                Distribution td = new Distribution();
                Console.WriteLine("-----------------------------------Output------------------------");
                /*Console.WriteLine(GlobTable.Rows[row][day * 4] == null);*/
                if (GlobTable.Rows[row].Field<int>("Lead Time (Days)").ToString() == "") break;
                td.Value = GlobTable.Rows[row].Field<int>("Lead Time (Days)");
                td.Probability = GlobTable.Rows[row].Field<decimal>("Lead Time Probability");
                td.CummProbability = GlobTable.Rows[row].Field<decimal>("Lead Time Cumulative Probability");
                (int, int) interval = GlobTable.Rows[row].Field<(int, int)>("Lead Time Range");
                td.MinRange = interval.Item1;
                td.MaxRange = interval.Item2;
                simulationSystem.LeadDaysDistribution.Add(td);
                if (td.MaxRange == 100) break;
            }
        }
        public void Run(ref SimulationSystem simulationSystem)
        {
            buildLead(ref simulationSystem);
            buildDemands(ref simulationSystem);
            Random rndI = new Random();
            Random rndS = new Random();
            List<Distribution> demand = simulationSystem.DemandDistribution;
            List<Distribution> LeadTime = simulationSystem.LeadDaysDistribution;
            List<SimulationCase> answer = new List<SimulationCase>();
            genertor gen = new genertor();

            int n = simulationSystem.NumberOfDays;
            int m = simulationSystem.OrderUpTo;
            int r = simulationSystem.ReviewPeriod;
            int leadDay = simulationSystem.StartLeadDays + 1;
            int cylce = 1;
            int shortage = 0;
            int order = simulationSystem.StartOrderQuantity;
            int inventory = simulationSystem.StartInventoryQuantity;
            decimal shortageSum = 0;
            decimal endInventorySum = 0;
        
            for (int day = 1; day <= n; day++)
            {
                SimulationCase current = new SimulationCase();
                int randNumber1 = rndI.Next(1, 100);
                int randNumber2 = rndS.Next(1, 10);
                current.RandomDemand = randNumber1;
                current.RandomLeadDays = randNumber2;

                current.Day = day;
                current.Cycle = cylce;
                current.DayWithinCycle = (day % r) == 0 ? r : day % r;
                current.Demand = gen.getDemand(randNumber1, ref demand);
                current.BeginningInventory = (day == 1) ? inventory : (answer[day - 2].EndingInventory);

                if (leadDay == day)
                {
                    current.BeginningInventory += order;
                }


                current.EndingInventory = current.BeginningInventory < current.Demand ? 0 : current.BeginningInventory - current.Demand - shortage;
                if (current.EndingInventory < 0)
                {
                    shortage = current.Demand + shortage - current.BeginningInventory;
                }
                else
                {
                    shortage = current.BeginningInventory >= current.Demand ? 0 : current.Demand - current.BeginningInventory + shortage;
                }
                current.EndingInventory = Math.Max(0, current.EndingInventory);
                current.ShortageQuantity += shortage;

                endInventorySum += current.EndingInventory;

                shortageSum += current.ShortageQuantity;

                if (day % r == 0)
                {
                    cylce++;
                    current.OrderQuantity = m - current.EndingInventory + current.ShortageQuantity;
                    order = current.OrderQuantity;
                    current.LeadDays = gen.getLeadTime(randNumber2, ref LeadTime);
                    current.dayuntil = current.LeadDays;
                    leadDay = day + 1 + current.LeadDays;
                }
                answer.Add(current);
            }
            simulationSystem.SimulationTable = answer;
            simulationSystem.DemandDistribution = demand;
            simulationSystem.LeadDaysDistribution = LeadTime;
            simulationSystem.PerformanceMeasures.EndingInventoryAverage = endInventorySum / simulationSystem.NumberOfDays;
            simulationSystem.PerformanceMeasures.ShortageQuantityAverage = shortageSum / simulationSystem.NumberOfDays;
        }
    }
}
