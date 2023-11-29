using MultiQueueModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static MultiQueueSimulation.Program;

namespace MultiQueueSimulation
{
    
    public partial class Form4 : Form
    {
        public static int currserv = 0;
        List<Chart> charts;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Multiple_charts();
             //c();
        }
      
    
        private Chart chart(int id) 
        {
            Chart chart1 = new Chart();
            // Set the Chart control's properties
            //chart1.Dock = DockStyle.Fill;
            Server server = SimSys.Servers[id];
            // Create a ChartArea and add it to the Chart control
            ChartArea chartArea1 = new ChartArea($"chartArea{server.ID}");
            chart1.ChartAreas.Add(chartArea1);
           
            Series series = new Series($"Server{server.ID}");
            chart1.Series.Add(series);
            chart1.Series[$"Server{server.ID}"]["PointWidth"] = "1";
            for (int j = 0; j < SimSys.StoppingNumber; j++)
            {
                if (server.TotalWorkingTime == 0)
                {

                    continue;

                }

                chart1.Series[$"Server{server.ID}"].Points.AddXY(j + 0.5, server.timeline[j]);

                // Display a message box with a message and an OK button
                //MessageBox.Show($"{}This is a simple message box.", "MessageBox Example", MessageBoxButtons.OK);

            }
            chart1.Titles.Add($"Server{server.ID} Status Over Time");
            chart1.ChartAreas[0].AxisX.Title = "Time";
            chart1.ChartAreas[0].AxisY.Title = "Server Status";
            chart1.ChartAreas[0].AxisY.Maximum = 1.25;  // Set Y-axis maximum to ensure proper visualization
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            return chart1;
        }
        private void Multiple_charts()
        {
            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.RowCount = 1;
            panel.ColumnCount = SimSys.NumberOfServers;
            Console.WriteLine(SimSys.NumberOfServers);
            this.Controls.Add(panel); 
            
            for (int i=1;i<SimSys.Servers.Count;i++) 
            {

                if (SimSys.Servers[i].TotalWorkingTime == 0) continue;
                Chart charta=chart(i);
                panel.Controls.Add(charta, i-1, 0);

            }
            

        }

     
    }
}
