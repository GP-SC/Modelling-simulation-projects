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
        public List<Distribution> buildDemand()
        {

           
            List<Distribution> result = new List<Distribution>();
            int n = GlobTable.Rows.Count;
            for (int i = 0; i < 5; i++)
            {
               
                Distribution currentDay = new Distribution();
                currentDay.Value = GlobTable.Rows[i].Field<int>("Demand");
                currentDay.Probability = GlobTable.Rows[i].Field<decimal>("Demand Probability");
                currentDay.CummProbability = GlobTable.Rows[i].Field<decimal>("Demand Cummulative Probability");
                (int, int) tmp = GlobTable.Rows[i].Field<(int, int)>("Demand Range");
                currentDay.MaxRange = tmp.Item2;
                currentDay.MinRange = tmp.Item1;
                result.Add(currentDay);
            }

            return result;
        }
        public List<Distribution> buildLeadTime()
        {

            List<Distribution> result = new List<Distribution>();
            for (int i = 0; i < 3; i++)
            {
                Distribution currentDay = new Distribution();
                currentDay.Value = GlobTable.Rows[i].Field<int>("Lead Time (Days)"); ;
                currentDay.Probability = GlobTable.Rows[i].Field<decimal>("Lead Time Probability");
                currentDay.CummProbability = GlobTable.Rows[i].Field<decimal>("Lead Time Cumulative Probability");
                (int, int) tmp = GlobTable.Rows[i].Field<(int, int)>("Lead Time Range");
                currentDay.MaxRange = tmp.Item2;
                currentDay.MinRange = tmp.Item1;
                result.Add(currentDay);
            }

            return result;
        }
        public List<SimulationCase> Run()
        {


            Random rand = new Random();
          
            List<Distribution> demand = new List<Distribution>(buildDemand());
            List<Distribution> LeadTime = new List<Distribution> (buildLeadTime());
            genertor gen = new genertor();
            List<SimulationCase> ans = new List<SimulationCase>();

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
           
            for (int i = 1; i <= n; i++)
            {
                SimulationCase current = new SimulationCase();
                current.Cycle = cylce;
                current.BeginningInventory = simulationSystem.StartInventoryQuantity;
                if (i == leadDay)
                {
                    inventory += order;
                    shortage = 0;
                    order = 0;
                }

                int randNumber1 = rand.Next(1, 100);
                int randNumber2 = rand.Next(1, 10);
                current.BeginningInventory = inventory ;
                current.RandomDemand = randNumber1;
                current.RandomLeadDays = randNumber2;
                current.LeadDays = gen.getLeadTime(randNumber2, ref LeadTime);
                current.Demand = gen.getDemand(randNumber1, ref demand);
                current.Day = i;
                current.DayWithinCycle = (i % r) == 0 ? r : i % r;
                current.BeginningInventory -= current.Demand;
                shortage = current.BeginningInventory >= 0 ? 0 : -current.BeginningInventory;
                current.ShortageQuantity = shortage;
                current.EndingInventory = current.BeginningInventory < 0 ? 0 : current.BeginningInventory;
                endInventorySum += current.EndingInventory;
                shortageSum += current.ShortageQuantity;
                inventory = current.BeginningInventory;
                current.OrderQuantity = order;  
                if (i % r == 0)
                {
                    cylce++;
                    order = m - (current.EndingInventory + current.ShortageQuantity);
                    leadDay = i + current.LeadDays;
                }
                ans.Add(current);
              
            }
            simulationSystem.DemandDistribution = demand;
            simulationSystem.LeadDaysDistribution = LeadTime;
            simulationSystem.PerformanceMeasures.EndingInventoryAverage = endInventorySum / simulationSystem.NumberOfDays;
            simulationSystem.PerformanceMeasures.ShortageQuantityAverage = shortageSum / simulationSystem.NumberOfDays;

            return ans;
        }
    }
}
