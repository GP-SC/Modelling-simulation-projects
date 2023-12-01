using InventoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventorySimulation.Form1;
using System.Data;
using static InventorySimulation.Program;
namespace InventorySimulation
{
     public class Build
    {
        public List<Distribution> buildDemand()
        {

            List<Distribution> result = new List<Distribution>();
            for (int i = 0; i < simulationSystem.NumberOfDays; i++)
            {
                Distribution currentDay = new Distribution();
                currentDay.Value = i;
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
            for (int i = 0; i < simulationSystem.NumberOfDays; i++)
            {
                Distribution currentDay = new Distribution();
                currentDay.Value = i;
                currentDay.Probability = GlobTable.Rows[i].Field<decimal>("Lead Time Probability");
                currentDay.CummProbability = GlobTable.Rows[i].Field<decimal>("Lead Time Cummulative Probability");
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
          
            List<Distribution> demand = buildDemand();
            List<Distribution> LeadTime = buildLeadTime();
            genertor gen = new genertor();
            List<SimulationCase> ans = new List<SimulationCase>();
            SimulationCase current = new SimulationCase();
            current.Cycle = 1;
            current.BeginningInventory = simulationSystem.StartInventoryQuantity;
            int n = simulationSystem.NumberOfDays;
            int m = simulationSystem.OrderUpTo;
            int leadDay = 1;
            decimal shortageSum = 0;
            decimal endInventorySum = 0;

            for (int i = 1; i <= n; i++)
            {
                if (i == leadDay)
                {
                    current.BeginningInventory += current.OrderQuantity;
                    current.ShortageQuantity = 0;
                }

                int randNumber1 = rand.Next(1, 100);
                int randNumber2 = rand.Next(1, 10);

                current.RandomDemand = randNumber1;
                current.RandomLeadDays = randNumber2;
                current.LeadDays = gen.getLeadTime(randNumber2, ref LeadTime);
                current.Demand = gen.getDemand(randNumber1, ref demand);
                current.Day = i;
                current.DayWithinCycle = (i % simulationSystem.ReviewPeriod) == 0 ? simulationSystem.ReviewPeriod : i % simulationSystem.ReviewPeriod;
                current.BeginningInventory -= current.Demand;
                current.ShortageQuantity = current.BeginningInventory >= 0 ? 0 : -current.BeginningInventory;
                current.EndingInventory = current.BeginningInventory < 0 ? 0 : current.BeginningInventory;
                endInventorySum += current.EndingInventory;
                shortageSum += current.ShortageQuantity;

                if (i % simulationSystem.ReviewPeriod == 0)
                {
                    current.Cycle++;
                    current.OrderQuantity = m - (current.EndingInventory + current.ShortageQuantity);
                    leadDay = i + current.LeadDays;
                }
                ans.Add(current);
            }
            simulationSystem.DemandDistribution = demand;
            simulationSystem.LeadDaysDistribution = LeadTime;

            simulationSystem.PerformanceMeasures.EndingInventoryAverage = endInventorySum / (decimal)simulationSystem.NumberOfDays;
            simulationSystem.PerformanceMeasures.ShortageQuantityAverage = shortageSum / (decimal)simulationSystem.NumberOfDays;

            return ans;
        }
    }
}
