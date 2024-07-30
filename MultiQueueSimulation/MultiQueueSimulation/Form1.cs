using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueModels;
using MultiQueueTesting;
using System.IO;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static MultiQueueModels.Enums;
using System.Runtime.InteropServices;
using static MultiQueueSimulation.Program;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MultiQueueSimulation
{

    
    public partial class Form1 : Form
    {
        public static DataTable GlobTable;
        public static int N_servers;
        public static int selection;
        public static int StopCriteria;
        public static int StoppingNum;
        public static DataTable defaulttable;
        public static bool flag = false;
        public Form1()
        {
            InitializeComponent();
        }
        DataTable table = new DataTable();
        Enums enums = new Enums();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // add columns to datatable
            table.Columns.Add("InterarrivalDistribution\r\n", typeof(int));
            table.Columns.Add("Probability\r\n", typeof(decimal));
            table.Columns.Add("Cummulative Probability", typeof(decimal));
            table.Columns.Add("Range", typeof((int,int)));
            table.Columns.Add($"S{1} Service Time\r\n", typeof(int));
            table.Columns.Add($"S{1} Probability\r\n", typeof(decimal));
            table.Columns.Add($"S{1} Cumm Probability\r\n", typeof(decimal));
            table.Columns.Add($"S{1} Range\r\n", typeof((int, int)));


          
            dataGridView1.DataSource = table;
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "") MessageBox.Show("No file is found!");
            else
            {
                string[] lines = File.ReadAllLines(textBox2.Text);
                int section = 0; // To keep track of the current section (0 for Interarrival, 1-5 for Service)
                int rowCount = 0;
                bool foundInterarrival = false;
                txtboxes(foundInterarrival);
                int n_servers = int.Parse(textBox3.Text);
                bool[] boolArray = new bool[n_servers + 1];
                decimal CummProb = 0;
                data();
                foreach (string line in lines)
                {
                    if (line.StartsWith("InterarrivalDistribution"))
                    {
                        foundInterarrival = true;
                        continue; // Skip the section header
                    }
                    if (foundInterarrival)
                    {
                        if (line.IndexOf(",") != -1)
                        {
                            string[] values = line.Split(',');
                            CummProb = decimal.Parse(values[1]) + CummProb;
                            (int, int) interval = ((int)((CummProb - decimal.Parse(values[1])) * 100) + 1, (int)(CummProb * 100));
                            table.Rows.Add(values[0], values[1], CummProb, interval);

                        }
                    }
                    if (line.StartsWith("ServiceDistribution_Server"))
                    {
                        foundInterarrival = false;
                        section = int.Parse(line.Substring(line.Length - 1));
                        rowCount = 0;
                        boolArray[section - 1] = false;
                        boolArray[section] = true;
                        CummProb = 0;
                    }
                    if (boolArray[section])
                    {
                        if (line.IndexOf(",") != -1)
                        {
                            string[] values = line.Split(',');
                            decimal value1 = decimal.Parse(values[0]);
                            decimal value2 = decimal.Parse(values[1]);
                            table.Rows[rowCount][4 + (section - 1) * 4 + 0] = value1;
                            table.Rows[rowCount][4 + (section - 1) * 4 + 1] = value2;
                            CummProb = value2 + CummProb;
                            table.Rows[rowCount][4 + (section - 1) * 4 + 2] = CummProb;
                            table.Rows[rowCount][4 + (section - 1) * 4 + 3] = ((int)((CummProb - value2) * 100) + 1, (int)(CummProb * 100));
                            rowCount++;
                        }
                    }
                    GlobTable = table;
                }
            }
            

        }
        private void txtboxes(bool foundInterarrival)
        {
             // Update with the actual path to your file
            string[] lines = File.ReadAllLines(textBox2.Text);
            
            foreach (string line in lines) { 
                if (line.StartsWith("InterarrivalDistribution"))
            {
                foundInterarrival = true;
                continue; // Skip the section header
            }
                
                if (!foundInterarrival)
                {

                    if (int.TryParse(lines[1], out int numberOfServers))
                    {
                        textBox3.Text = numberOfServers.ToString();
                        N_servers = numberOfServers;
                    }
                    if (int.TryParse(lines[4], out int stoppingNumber))
                    {
                        textBox5.Text = stoppingNumber.ToString();
                        StoppingNum = stoppingNumber;
                    }
                    if (int.TryParse(lines[7], out int stoppingCriteria))
                    {
                        string stoppingCriteriaName = Enum.GetName(typeof(StoppingCriteria), stoppingCriteria);
                        textBox1.Text = stoppingCriteriaName.ToString();
                        StopCriteria = stoppingCriteria;
                    }

                    if (int.TryParse(lines[10], out int selectionMethod))
                    {
                        
                        string selectedMethodName = Enum.GetName(typeof(SelectionMethod), selectionMethod);
                        //  textBox4.Text = selectionMethod.ToString();
                        textBox4.Text = selectedMethodName.ToString();
                        selection = selectionMethod;

                    }
                                           
                }
            }
          
        }
        private void data()
        {
            int value;
            int.TryParse(textBox3.Text, out value);
            for (int i = 2; i <=value ; i++)
            {
                table.Columns.Add($"S{i} Service Time\r\n", typeof(int));
                table.Columns.Add($"S{i} Probability\r\n", typeof(decimal));
                table.Columns.Add($"S{i} Cumm Probability\r\n", typeof(decimal));
                table.Columns.Add($"S{i} Range\r\n", typeof((int, int)));
            }
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Nmain();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int section = 0; // To keep track of the current section (0 for Interarrival, 1-5 for Service)
            int rowCount = 0;
            bool foundInterarrival = false;
            int n_servers = int.Parse(textBox3.Text);
            bool[] boolArray = new bool[n_servers + 1];
            decimal CummProb = 0;
            
            GlobTable = dataGridView1.DataSource as DataTable;

            for(int i=0;i<(int)(GlobTable.Columns.Count/4);i++)
            {
                CummProb = 0;
                for (int row = 0; row < GlobTable.Rows.Count; row++) 
                {
                    decimal prob = decimal.Parse(GlobTable.Rows[row][i*4+1].ToString());
                    CummProb= prob+CummProb;
                    GlobTable.Rows[row][i * 4 + 2]=CummProb;
                    (int ,int)interval=((int)((CummProb-prob)*100)+1, (int)(CummProb * 100));
                    GlobTable.Rows[row][i * 4 + 3] = interval;
                }
            }
            dataGridView1.DataSource = GlobTable;
            /*string[] values = line.Split(',');
            decimal value1 = decimal.Parse(values[0]);
            decimal value2 = decimal.Parse(values[1]);
            table.Rows[rowCount][4 + (section - 1) * 4 + 0] = value1;
            table.Rows[rowCount][4 + (section - 1) * 4 + 1] = value2;
            CummProb = value2 + CummProb;
            table.Rows[rowCount][4 + (section - 1) * 4 + 2] = CummProb;
            table.Rows[rowCount][4 + (section - 1) * 4 + 3] = ((int)((CummProb - value2) * 100) + 1, (int)(CummProb * 100));
            rowCount++;*/

        }

        private void button2_Click(object sender, EventArgs e)
        {
            N_servers= int.Parse(textBox3.Text);
            data();
        }
        //public static int N_servers;
        //public static int selection;
        //public static int StopCriteria;
        //public static int StoppingNum;

        private void button5_Click(object sender, EventArgs e)
        {
                int stoppingCriteria = int.Parse(textBox1.Text);                   
                string stoppingCriteriaName = Enum.GetName(typeof(StoppingCriteria), stoppingCriteria);
                textBox1.Text = stoppingCriteriaName.ToString();
                StopCriteria = stoppingCriteria;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int selectionMethod=int.Parse(textBox4.Text);
            string selectedMethodName = Enum.GetName(typeof(SelectionMethod), selectionMethod);
            textBox4.Text = selectedMethodName.ToString();
            selection = selectionMethod;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int stoppingNumber = int.Parse(textBox5.Text);
            textBox5.Text = stoppingNumber.ToString();
            StoppingNum = stoppingNumber;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

