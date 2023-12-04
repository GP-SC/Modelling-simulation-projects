using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryModels;
using InventoryTesting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static InventorySimulation.Form1;
namespace InventorySimulation
{
    static class Program
    {

        public static SimulationSystem simulationSystem = new SimulationSystem();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
        public static void Nmain()
        {
            setAnswer();
            fillDemands();
            fillLead();
            simtable();
            string result = TestingManager.Test(simulationSystem, Constants.FileNames.TestCase1);
            MessageBox.Show(result);
        }
        public static void setAnswer()
        {

            simulationSystem.NumberOfDays = NumberOfDays;
            simulationSystem.ReviewPeriod = ReviewPeriod;
            simulationSystem.StartInventoryQuantity = StartInventoryQuantity;
            simulationSystem.StartLeadDays = StartLeadDays;
            simulationSystem.StartOrderQuantity = StartOrderQuantity;
            simulationSystem.OrderUpTo = OrderUpTo;
        }
        static void fillDemands()
        {

            for (int row = 0; row < GlobTable.Rows.Count; row++)
            {
                Distribution td = new Distribution();
                Console.WriteLine("-----------------------------------Output------------------------");
                /*Console.WriteLine(GlobTable.Rows[row][i * 4] == null);*/
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
        static void fillLead()
        {

            for (int row = 0; row < GlobTable.Rows.Count; row++)
            {
                Distribution td = new Distribution();
                Console.WriteLine("-----------------------------------Output------------------------");
                /*Console.WriteLine(GlobTable.Rows[row][i * 4] == null);*/
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
        static int getDemands(int randomTime)
        {

            List<Distribution> tmp = simulationSystem.DemandDistribution;
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].MinRange <= randomTime && tmp[i].MaxRange >= randomTime)
                    return tmp[i].Value;
            }
            return -1;
        }
        static int getLeads(int randomTime)
        {

            List<Distribution> tmp = simulationSystem.LeadDaysDistribution;
            for (int i = 0; i < tmp.Count; i++)
            {
                if ((tmp[i].MinRange /10) <= randomTime && (tmp[i].MaxRange /10) >= randomTime)
                    return tmp[i].Value;
            }
            return -1;
        }
        static void simtable()
        {
            Random rndI = new Random();
            Random rndS = new Random();
            int num = rndI.Next(1, 101);
            int num2 = rndS.Next(1, 101);
            List<Distribution> demand = simulationSystem.DemandDistribution;
            List<Distribution> LeadTime = simulationSystem.LeadDaysDistribution;
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
                int randNumber1 = rndI.Next(1, 100);
                int randNumber2 = rndS.Next(1, 10);
                current.RandomDemand = randNumber1;
                current.RandomLeadDays = randNumber2;

                current.Day = i;
                current.Cycle = cylce;
                current.DayWithinCycle = (i % r) == 0 ? r : i ;
                current.BeginningInventory = (i==1)?inventory :ans[i-1].EndingInventory; //
                
                if (i == simulationSystem.StartLeadDays)
                {
                    inventory += order;
                    shortage = 0;
                    order = 0;
                }

                current.Demand = getDemands(randNumber1);
                current.EndingInventory = current.BeginningInventory < current.Demand ? 0 : current.BeginningInventory-current.Demand;

                /* current.BeginningInventory -= current.Demand;*/
                shortage = current.BeginningInventory >= current.Demand ? 0 : current.Demand- current.BeginningInventory;   ///
                current.ShortageQuantity = shortage;
                if (current.DayWithinCycle == r)
                {
                    current.OrderQuantity = m-current.EndingInventory+current.ShortageQuantity;
                    current.RandomLeadDays = randNumber2;
                    current.LeadDays = getLeads(randNumber2);
                    current.dayuntil = current.LeadDays;
                }
                else
                {
                    current.OrderQuantity = 0;
                    current.RandomLeadDays = 0;
                    current.LeadDays = 0;
                }
                

                endInventorySum += current.EndingInventory;
                shortageSum += current.ShortageQuantity;
                inventory = current.BeginningInventory;
                
                if (i % r == 0)
                {
                    cylce++;
                    order = m - (current.EndingInventory + current.ShortageQuantity);
                    leadDay = i + current.LeadDays;
                }
                ans.Add(current);

            }
            simulationSystem.SimulationTable = ans;
            simulationSystem.DemandDistribution = demand;
            simulationSystem.LeadDaysDistribution = LeadTime;
            simulationSystem.PerformanceMeasures.EndingInventoryAverage = endInventorySum / simulationSystem.NumberOfDays;
            simulationSystem.PerformanceMeasures.ShortageQuantityAverage = shortageSum / simulationSystem.NumberOfDays;

        }
    }
}
