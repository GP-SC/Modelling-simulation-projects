using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            /* for (int i = 0; i < customers.Count; i++)
             {
                 //table2.Rows[i][0] = customers[i].CustomerNumber;
                 //table2.Rows[i][1] = customers[i].RandomInterArrival;
                 //table2.Rows[i][2] = customers[i].InterArrival;
                 //table2.Rows[i][3] = customers[i].ArrivalTime;
                 //table2.Rows[i][4] = customers[i].RandomService;
                 //table2.Rows[i][5] = customers[i].AssignedServer.ID;
                 //table2.Rows[i][6] = customers[i].StartTime;
                 //table2.Rows[i][7] = customers[i].ServiceTime;
                 //table2.Rows[i][8] = customers[i].EndTime;
                 //table2.Rows[i][9] = customers[i].TimeInQueue;
                 table2.Rows.Add(
                          customers[i].CustomerNumber,
                          customers[i].RandomInterArrival,
                          customers[i].InterArrival,
                          customers[i].ArrivalTime,
                          customers[i].RandomService,
                          customers[i].AssignedServer.ID,
                          customers[i].StartTime,
                          customers[i].ServiceTime,
                          customers[i].EndTime,
                          customers[i].TimeInQueue
                                                       );
                 Console.WriteLine($"Customer {i} //////////////////////////");
             }*/

            dataGridView1.DataSource = table2;
        }
    }
}
