using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static InventorySimulation.Program;


namespace InventorySimulation
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DataTable table2 = new DataTable();

        private void Form2_Load(object sender, EventArgs e)
        {
            table2.Columns.Add("Day\r\n", typeof(int));
            table2.Columns.Add("Cycle\r\n", typeof(int));
            table2.Columns.Add("Day within cycle\r\n", typeof(int));
            table2.Columns.Add("Beginning Inventory\r\n", typeof(int));
            table2.Columns.Add("Random Digit for Demand\r\n", typeof(int));
            table2.Columns.Add("Demand\r\n", typeof(int));
            table2.Columns.Add("Ending Inventory\r\n", typeof(int));
            table2.Columns.Add("Shortage Quantity\r\n", typeof(int));
            table2.Columns.Add("Order Quantity\r\n", typeof(int));
            table2.Columns.Add("Random Digit for LeadDays\r\n", typeof(int));
            table2.Columns.Add("Lead Time\r\n", typeof(int));
            table2.Columns.Add("Days until Order arrives\r\n", typeof(int));
            // gahndy hena hatstagdm el class el enta 3amlt fill data and calculation on it
            for (int i = 0; i <simulationSystem.SimulationTable.Count; i++)
            {
               
                table2.Rows.Add(
                         simulationSystem.SimulationTable[i].Day,
                         simulationSystem.SimulationTable[i].Cycle,
                         simulationSystem.SimulationTable[i].DayWithinCycle,
                         simulationSystem.SimulationTable[i].BeginningInventory,
                         simulationSystem.SimulationTable[i].RandomDemand,
                         simulationSystem.SimulationTable[i].Demand,
                         simulationSystem.SimulationTable[i].EndingInventory,
                         simulationSystem.SimulationTable[i].ShortageQuantity,
                         simulationSystem.SimulationTable[i].OrderQuantity,
                         simulationSystem.SimulationTable[i].RandomLeadDays,
                         simulationSystem.SimulationTable[i].LeadDays,
                         simulationSystem.SimulationTable[i].Day // hena feeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeh haga nasa
                                );
                Console.WriteLine($"Customer {i} //////////////////////////");
            }

            dataGridView1.DataSource = table2;
        }

        private void PM_btn_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
