using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MultiQueueSimulation.Program;
namespace MultiQueueSimulation
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
            table2.Columns.Add("Customer num\r\n", typeof(int));
            table2.Columns.Add("Random Digit\r\n", typeof(int));
            table2.Columns.Add("Time Between Arrival", typeof(int));
            table2.Columns.Add("Time Arrival", typeof(int));
            table2.Columns.Add("Random Digits \r\n", typeof(int));
            table2.Columns.Add("AssignedServer\r\n", typeof(int));
            table2.Columns.Add("Time S Begain\r\n", typeof(decimal));
            table2.Columns.Add("service time\r\n", typeof(decimal));
            table2.Columns.Add("Service End\r\n", typeof(int));
            table2.Columns.Add("Time In Queue\r\n", typeof(int));
            //for (int i = 0; i < 5; i++) {
            //    table.Columns.Add($"S{i} Service Time\r\n", typeof(int));
            //    table.Columns.Add($"S{i} Probability\r\n", typeof(decimal));
            //}

            Console.WriteLine($"Customer {customers.Count} //////////////////////////");
            // dataGridView1.DataSource = table2;
            for (int i = 0;i<customers.Count;i++) {
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
            }

            dataGridView1.DataSource = table2;
           


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
