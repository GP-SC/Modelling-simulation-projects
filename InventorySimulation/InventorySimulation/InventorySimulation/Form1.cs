using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryModels;
using InventoryTesting;
using static System.Collections.Specialized.BitVector32;
using static InventorySimulation.Program;

namespace InventorySimulation
{
    public partial class Form1 : Form
    {
        public DataTable table = new DataTable();
        public static int OrderUpTo;
        public static int ReviewPeriod;
        public static int StartInventoryQuantity;
        public static int StartLeadDays;
        public static int StartOrderQuantity;
        public static int NumberOfDays;
        public static DataTable GlobTable;
        public static string path;
        int count = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeDataTable();
            dataGridView1.DataSource = table;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void InitializeDataTable()
        {
            table.Columns.Add("Demand", typeof(int));
            table.Columns.Add("Demand Probability", typeof(decimal));
            table.Columns.Add("Demand Cummulative Probability", typeof(decimal));
            table.Columns.Add("Demand Range", typeof((int, int)));
            table.Columns.Add("Lead Time (Days)", typeof(int));
            table.Columns.Add("Lead Time Probability", typeof(decimal));
            table.Columns.Add("Lead Time Cumulative Probability", typeof(decimal));
            table.Columns.Add("Lead Time Range", typeof((int, int)));
           
        }

        private void BR_btn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Browse_TB.Text = openFileDialog1.FileName;
                path = Browse_TB.Text;
            }
        }

        private void import_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Browse_TB.Text))
            {
                MessageBox.Show("No file is found!");
                return;
            }
            if (count > 0) GlobTable.Rows.Clear();

            string[] lines = File.ReadAllLines(Browse_TB.Text);
            ProcessFileLines(lines);
            GlobTable = table;
            count++;
            /*path = Path.GetFileNameWithoutExtension(Browse_TB.Text);*/
        }
        private void ProcessFileLines(string[] lines)
        {
            bool isDayTypeSection = false;
            decimal cummulativeProbabilityD = 0;
            decimal cummulativeProbabilitylead = 0;
            int section = 0;

            int rowCount = 0;

            foreach (string line in lines)
            {
                if (line.StartsWith("DemandDistribution"))
                {
                    isDayTypeSection = true;
                    continue;
                }

                if (isDayTypeSection && line.Contains(","))
                {
                    string[] values = line.Split(',');
                    cummulativeProbabilityD = decimal.Parse(values[1]) + cummulativeProbabilityD;
                    (int, int) interval = ((int)((cummulativeProbabilityD - decimal.Parse(values[1])) * 100) + 1, (int)(cummulativeProbabilityD * 100));
                    table.Rows.Add(values[0], values[1], cummulativeProbabilityD, interval);

                    /*ProcessDemandLine(line, ref cummulativeProbability);*/
                }
                else if (line.StartsWith("LeadDaysDistribution"))
                {
                    isDayTypeSection = false;
                    cummulativeProbabilitylead = 0;
                    //section = int.Parse(line.Substring(line.Length - 1));
                }
                else if (!isDayTypeSection && line.Contains(","))
                {
                    string[] values = line.Split(',');
                    decimal value1 = decimal.Parse(values[0]);
                    decimal value2 = decimal.Parse(values[1]);
                    table.Rows[rowCount][4 ] = value1;
                    table.Rows[rowCount][5] = value2;
                    cummulativeProbabilitylead = value2 + cummulativeProbabilitylead;
                    table.Rows[rowCount][6] = cummulativeProbabilitylead;
                    table.Rows[rowCount][7] = ((int)((cummulativeProbabilitylead - value2) * 100) + 1, (int)(cummulativeProbabilitylead * 100));
                    rowCount++;
                    // ProcessDemandLine(line,ref cummulativeProbabilitylead,ref rowCount);
                }
            }

            ProcessTextBoxes(lines);
            GlobTable = table;
        }
     
    

      
        private void ProcessTextBoxes(string[] lines)
        {
            OrderUpTo = int.Parse(lines[1]);
            m_txtbox.Text = OrderUpTo.ToString();

            ReviewPeriod = int.Parse(lines[4]);
            n_txtbox.Text = ReviewPeriod.ToString();

            StartInventoryQuantity = int.Parse(lines[7]);
            BIQ_txtbox.Text = StartInventoryQuantity.ToString();

            StartLeadDays = int.Parse(lines[10]);
            FOAA_txtbox.Text = StartLeadDays.ToString();

            StartOrderQuantity = int.Parse(lines[13]);
            FOQuant_txtbox.Text = StartOrderQuantity.ToString();

            NumberOfDays = int.Parse(lines[16]);
            noDays_txtbox.Text = NumberOfDays.ToString();
           

        }

        private void sim_btn_Click(object sender, EventArgs e)
        {
            SimulationSystem s = Nmain();
            Form2 form2 = new Form2(s);
            form2.Show();
        }
    }
}
