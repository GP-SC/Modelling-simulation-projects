using InventoryModels;
using InventoryTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static InventorySimulation.Form1;

namespace InventorySimulation
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static SimulationSystem Nmain()
        {
            SimulationSystem simulationSystem = new SimulationSystem();
            Build build = new Build();
            build.setAnswer(ref simulationSystem);
            build.Run(ref simulationSystem);
            
            string testCase = "";

            if (path.EndsWith("TestCase1.txt"))
            {
                testCase = Constants.FileNames.TestCase1;
            }
            else if (path.EndsWith("TestCase2.txt"))
            {
                testCase = Constants.FileNames.TestCase2;
            }

            string result = TestingManager.Test(simulationSystem, testCase);
            MessageBox.Show(result);
            
            return simulationSystem;
        }

    }
}