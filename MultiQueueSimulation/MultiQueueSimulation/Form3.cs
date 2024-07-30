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
using System.Windows.Forms.DataVisualization.Charting;
using MultiQueueModels;

namespace MultiQueueSimulation
{
    public partial class Form3 : Form
    {
        DataTable table3 = new DataTable();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label6.Text = SimSys.PerformanceMeasures.AverageWaitingTime.ToString();
            MaxContainer.Text = SimSys.PerformanceMeasures.MaxQueueLength.ToString();
            Console.WriteLine(SimSys.PerformanceMeasures.AverageWaitingTime.ToString());
            label7.Text = SimSys.PerformanceMeasures.WaitingProbability.ToString();
            Console.WriteLine(SimSys.PerformanceMeasures.WaitingProbability.ToString());

            table3.Columns.Add("Server num\r\n", typeof(int));
            table3.Columns.Add("Avg service time", typeof(decimal));
            table3.Columns.Add("Probability of Idle server", typeof(decimal));
            table3.Columns.Add("Utilization\r\n", typeof(decimal));
            for (int i = 1; i < SimSys.Servers.Count; i++)
            {
               
                table3.Rows.Add(
                        SimSys.Servers[i].ID,
                        SimSys.Servers[i].AverageServiceTime,
                        SimSys.Servers[i].IdleProbability ,                               
                        SimSys.Servers[i].Utilization
                    );
            }

            dataGridView1.DataSource = table3;
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }
    }
}
