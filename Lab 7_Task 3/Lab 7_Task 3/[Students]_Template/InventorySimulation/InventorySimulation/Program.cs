using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryModels;
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
         public static  void setAnswer()
        {
            Build b = new Build();
            simulationSystem.NumberOfDays = NumberOfDays;
            simulationSystem.ReviewPeriod = ReviewPeriod;
            simulationSystem.StartInventoryQuantity = StartInventoryQuantity;
            simulationSystem.StartLeadDays = StartLeadDays;
            simulationSystem.StartOrderQuantity = StartOrderQuantity;
            simulationSystem.OrderUpTo = OrderUpTo;
            simulationSystem.SimulationTable = b.Run();
        }
        
       
    }
}
