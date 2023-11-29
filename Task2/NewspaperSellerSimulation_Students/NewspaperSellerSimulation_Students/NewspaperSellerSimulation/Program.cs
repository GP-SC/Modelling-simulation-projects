using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerModels;
using NewspaperSellerTesting;
using static NewspaperSellerSimulation.Form1;


namespace NewspaperSellerSimulation
{
    static class Program
    {
        public static SimulationSystem SimSys = new SimulationSystem();
        public static DataTable OutGlobTable;
        public static Decimal cumSales = 0;
        public static Decimal cumLost = 0;
        public static Decimal cumScrap = 0;
        public static Decimal cumProfit = 0;
        public static Decimal cumCost = 0;
        public static int excessDemand = 0;
        public static int unsold = 0;

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
            //fillDays();
            Console.WriteLine("//////////////////////////");

            //fillInterArrivals();
            Console.WriteLine("//////////////////////////");
            
            //OutPut();
            Console.WriteLine("//////////////////////////");
            fillDayDist();

            fillDemandDist();

            OutPut();

            PreformanceMeasure();

            /* string result = TestingManager.Test(SimSys, Constants.FileNames.TestCase3);
             MessageBox.Show(result);*/
            string result = Form1.testCases(path, SimSys);
            MessageBox.Show(result);

        }

        private static void fillDayDist()
        {
            for (int i = 0; i < 3; i++)
            {
                DayTypeDistribution td = new DayTypeDistribution();
                string dayTypeString = GlobTable.Rows[i].Field<string>("DayType");
                if (Enum.TryParse(dayTypeString, out Enums.DayType dayType))
                    td.DayType = dayType;
                else
                    td.DayType = Enums.DayType.Good;
                Console.WriteLine("//////////////////////////");
                Console.WriteLine(td.DayType);
                Console.WriteLine("//////////////////////////");
                td.Probability = GlobTable.Rows[i].Field<decimal>("Probability");
                td.CummProbability = GlobTable.Rows[i].Field<decimal>("Cummulative Probability");
                (int, int) interval = GlobTable.Rows[i].Field<(int, int)>("Range");
                td.MinRange = interval.Item1;
                td.MaxRange = interval.Item2;

                SimSys.DayTypeDistributions.Add(td);
                
            }
        }

        private static void fillDemandDist()
        {
            List<DemandDistribution> dd = new List<DemandDistribution>();

            for (int row = 0; row < 7; row++)
            {
                DayTypeDistribution good = new DayTypeDistribution();
                DayTypeDistribution fair = new DayTypeDistribution();
                DayTypeDistribution poor = new DayTypeDistribution();

                DemandDistribution distribution = new DemandDistribution();

                distribution.Demand = GlobTable.Rows[row].Field<int>("Demand");

                good.DayType = Enums.DayType.Good;
                fair.DayType = Enums.DayType.Fair;
                poor.DayType = Enums.DayType.Poor;

                good.CummProbability = GlobTable.Rows[row].Field<decimal>("Probability of Good");
                fair.CummProbability = GlobTable.Rows[row].Field<decimal>("Probability of Fair");
                poor.CummProbability = GlobTable.Rows[row].Field<decimal>("Probability of Poor");

                // Extract intervals as tuples
                (int, int) goodInterval = GlobTable.Rows[row].Field<(int, int)>("Good Range");
                (int, int) fairInterval = GlobTable.Rows[row].Field<(int, int)>("Fair Range");
                (int, int) poorInterval = GlobTable.Rows[row].Field<(int, int)>("Poor Range");

                // Assign ranges to distributions
                good.MinRange = goodInterval.Item1;
                good.MaxRange = goodInterval.Item2;

                fair.MinRange = fairInterval.Item1;
                fair.MaxRange = fairInterval.Item2;

                poor.MinRange = poorInterval.Item1;
                poor.MaxRange = poorInterval.Item2;

                // Add the distributions to the 'DemandDistribution'
                distribution.DayTypeDistributions.Add(good);
                distribution.DayTypeDistributions.Add(fair);
                distribution.DayTypeDistributions.Add(poor);

                // Finally, add the 'DemandDistribution' to the list
                dd.Add(distribution);
            }

            // Update the simulation system's demand distributions
            SimSys.DemandDistributions = dd;
        }

        private static void OutPut()
        {
            Random rndI = new Random();
            Random rndS = new Random();

            for (int i = 1; i <= NumOfRecords; i++)
            {
                int num = rndI.Next(1,101);
                int num2 = rndS.Next(1,101);

                SimulationCase tmp = new SimulationCase();

                tmp.DayNo = i;
                tmp.RandomNewsDayType = num;
                tmp.NewsDayType = getDayType(num);
                //Console.WriteLine(tmp.RandomNewsDayType);
                //Console.WriteLine(tmp.NewsDayType);
                tmp.RandomDemand = num2;
                tmp.Demand = getDemands(num2, tmp.NewsDayType);
                //Console.WriteLine(tmp.RandomDemand);
                //Console.WriteLine(tmp.Demand);
                tmp.DailyCost = NumOfNewspapers * PurchasePrice;
                cumCost += tmp.DailyCost;

                if (NumOfNewspapers > tmp.Demand)
                {
                    tmp.SalesProfit = tmp.Demand * SellingPrice;
                    cumSales += tmp.SalesProfit;
                }
                else
                {
                    tmp.SalesProfit = NumOfNewspapers * SellingPrice;
                    cumSales += tmp.SalesProfit;
                }

                if ((tmp.Demand - NumOfNewspapers) * (SellingPrice - PurchasePrice) > 0)
                {
                    tmp.LostProfit = (tmp.Demand - NumOfNewspapers) * (SellingPrice - PurchasePrice);
                    cumLost += tmp.LostProfit;
                    excessDemand++;
                }
                else tmp.LostProfit = 0;



                if ((NumOfNewspapers - tmp.Demand) * ScrapPrice > 0)
                {
                    tmp.ScrapProfit = (NumOfNewspapers - tmp.Demand) * ScrapPrice;
                    cumScrap += tmp.ScrapProfit;
                    unsold++;
                }
                else tmp.ScrapProfit = 0;


                tmp.DailyNetProfit = tmp.SalesProfit - tmp.DailyCost - tmp.LostProfit + tmp.ScrapProfit;
                cumProfit += tmp.DailyNetProfit;

                SimSys.SimulationTable.Add(tmp);
            }
        }

        public static void PreformanceMeasure()
        {
            PerformanceMeasures performanceMeasures = new PerformanceMeasures();
            
            performanceMeasures.TotalCost = cumCost;
            performanceMeasures.TotalSalesProfit = cumSales;
            performanceMeasures.TotalLostProfit = cumLost;
            performanceMeasures.TotalScrapProfit = cumScrap;
            performanceMeasures.TotalNetProfit = cumProfit;
            performanceMeasures.DaysWithMoreDemand = excessDemand;
            performanceMeasures.DaysWithUnsoldPapers = unsold;

            SimSys.PerformanceMeasures = performanceMeasures;
        }

        private static Enums.DayType getDayType(int randomNum)
        {
            List<DayTypeDistribution> distributions = SimSys.DayTypeDistributions;
            foreach (var distribution in distributions)
            {
                if (distribution.MinRange <= randomNum && distribution.MaxRange >= randomNum)
                    return distribution.DayType;
            }
            return Enums.DayType.Good;
        }

        private static int getDemands(int randomNum, Enums.DayType dayType)
        {
            List<DemandDistribution> distributions = SimSys.DemandDistributions;

            foreach (var distribution in distributions)
            {
                foreach (var dayTypeDistribution in distribution.DayTypeDistributions)
                {
                    if (dayTypeDistribution.DayType == dayType)
                    {
                        if (dayTypeDistribution.MinRange <= randomNum &&
                            dayTypeDistribution.MaxRange >= randomNum)
                        {
                            return distribution.Demand;
                        }
                    }
                }
            }
            return -1;
        }
    }
}
