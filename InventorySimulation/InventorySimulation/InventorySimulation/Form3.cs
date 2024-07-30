using InventoryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static InventorySimulation.Program;
namespace InventorySimulation
{
    public partial class Form3 : Form
    {
        private SimulationSystem simulationSystem;

        public Form3(SimulationSystem simulationSystem)
        {
            InitializeComponent();
            this.simulationSystem = simulationSystem;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            EIA.Text = simulationSystem.PerformanceMeasures.EndingInventoryAverage.ToString();
            SQA.Text = simulationSystem.PerformanceMeasures.ShortageQuantityAverage.ToString();
        }
    }
}
