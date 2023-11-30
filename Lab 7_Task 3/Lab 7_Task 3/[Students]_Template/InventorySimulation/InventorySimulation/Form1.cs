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
using InventoryTesting;

namespace InventorySimulation
{
    public partial class Form1 : Form
    {
        DataTable table = new DataTable();
        public static int OrderUpTo;
        public static int ReviewPeriod;
        public static int StartInventoryQuantity;
        public static int StartLeadDays;
        public static int StartOrderQuantity;
        public static int NumberOfDays;
        public static DataTable GlobTable;
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
            table.Columns.Add("Demand Random Digit Assignment\r\n", typeof((int, int)));
            table.Columns.Add("Lead Time (Days)\r\n", typeof(int));
            table.Columns.Add("Days Probability", typeof(decimal));
            table.Columns.Add("DaysCumulative Probability\r\n", typeof(decimal));
            table.Columns.Add("Days Random Digit Assignment\r\n", typeof((int, int)));
           
        }

        private void BR_btn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Browse_TB.Text = openFileDialog1.FileName;
            }
        }

        private void import_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Browse_TB.Text))
            {
                MessageBox.Show("No file is found!");
                return;
            }

            string[] lines = File.ReadAllLines(Browse_TB.Text);
            ProcessFileLines(lines);
            GlobTable = table;
            /*path = Path.GetFileNameWithoutExtension(Browse_TB.Text);*/
        }
        private void ProcessFileLines(string[] lines)
        {
            bool isDayTypeSection = false;
            decimal cummulativeProbability = 0;
            decimal cummulativeProbabilitylead = 0;
          
           
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
                    ProcessDemandLine(line, ref cummulativeProbability, ref rowCount);

                   /*ProcessDemandLine(line, ref cummulativeProbability);*/
                }
                else if (line.StartsWith("LeadDaysDistribution"))
                {
                    isDayTypeSection = false;
                    cummulativeProbability = 0;
                }
                else if (!isDayTypeSection && line.Contains(","))
                {
                    /*ProcessDemandLine(line, ref cummulativeProbabilityGood, ref cummulativeProbabilityFair, ref cummulativeProbabilityPoor, ref rowCount);*/
                }
            }

            ProcessTextBoxes(lines);
        }
     
        private void ProcessDemandLine(string line, ref decimal cummProb, ref int rowCount)
        {
            string[] values = line.Split(',');
            decimal demand = int.Parse(values[0]);
            UpdateDemandProbabilities(values, ref cummProb);
          //  UpdateTableRow(rowCount, demand, cummProb,  values);
            rowCount++;
            if (rowCount > 2) table.Rows.Add();
        }
        private void UpdateDemandProbabilities(string[] values, ref decimal cummProbGood)
        {
            cummProbGood += decimal.Parse(values[1]);
           /* cummProbFair += decimal.Parse(values[2]);
            cummProbPoor += decimal.Parse(values[3]);*/
        }

        private void UpdateTableRow(int row, decimal demand, decimal cummProb, string[] values)
        {
            table.Rows[row][4] = demand;
            table.Rows[row][5] = cummProb;
            /*table.Rows[row][6] = cummProbFair;
            table.Rows[row][7] = cummProbPoor;*/
           // table.Rows[row][8] = CreateRange(cummProbGood, values[1]);
      
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
    }
}
