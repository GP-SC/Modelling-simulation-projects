using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using NewspaperSellerModels;
using NewspaperSellerTesting;
using static NewspaperSellerSimulation.Program;

namespace NewspaperSellerSimulation
{
    public partial class Form1 : Form
    {
        DataTable table = new DataTable();
        public static int NumOfNewspapers;
        public static int NumOfRecords;
        public static decimal PurchasePrice;
        public static decimal ScrapPrice;
        public static decimal SellingPrice;
        public static DataTable GlobTable;

        public Form1()
        {
            InitializeComponent();
            InitializeDataTable();
            dataGridView1.DataSource = table;
        }

        private void InitializeDataTable()
        {
            table.Columns.Add("DayType", typeof(string));
            table.Columns.Add("Probability", typeof(decimal));
            table.Columns.Add("Cummulative Probability", typeof(decimal));
            table.Columns.Add("Range", typeof((int, int)));
            table.Columns.Add("Demand", typeof(int));
            table.Columns.Add("Probability of Good", typeof(decimal));
            table.Columns.Add("Probability of Fair", typeof(decimal));
            table.Columns.Add("Probability of Poor", typeof(decimal));
            table.Columns.Add("Good Range", typeof((int, int)));
            table.Columns.Add("Fair Range", typeof((int, int)));
            table.Columns.Add("Poor Range", typeof((int, int)));
        }
        public static string path;
        public SimulationSystem Simsys { get; private set; }
        public static string testCases(string path, SimulationSystem sys)
        {
            string tc = "";
            if (path == "TestCase1")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase1);
            if (path == "TestCase2")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase2);
            if (path == "TestCase3")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase3);
            if (path == "TestCase4")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase4);
            if (path == "TestCase5")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase5);
            if (path == "TestCase6")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase6);
            if (path == "TestCase7")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase7);
            if (path == "TestCase8")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase8);
            if (path == "TestCase9")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase9);
            if (path == "TestCase10")
                tc = TestingManager.Test(sys, Constants.FileNames.TestCase10);
            return tc;
        }
        private void Browse_btn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Browse_TB.Text = openFileDialog1.FileName;
            }
        }

        private void Import_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Browse_TB.Text))
            {
                MessageBox.Show("No file is found!");
                return;
            }

            string[] lines = File.ReadAllLines(Browse_TB.Text);
            ProcessFileLines(lines);
            GlobTable = table;
            path = Path.GetFileNameWithoutExtension(Browse_TB.Text);
        }

        private void ProcessFileLines(string[] lines)
        {
            bool isDayTypeSection = false;
            decimal cummulativeProbability = 0;
            decimal cummulativeProbabilityGood = 0;
            decimal cummulativeProbabilityFair = 0;
            decimal cummulativeProbabilityPoor = 0;
            int rowCount = 0;

            foreach (string line in lines)
            {
                if (line.StartsWith("DayTypeDistributions"))
                {
                    isDayTypeSection = true;
                    continue;
                }

                if (isDayTypeSection && line.Contains(","))
                {
                    ProcessDayTypeLine(line, ref cummulativeProbability);
                }
                else if (line.StartsWith("DemandDistributions"))
                {
                    isDayTypeSection = false;
                    cummulativeProbability = 0;
                }
                else if (!isDayTypeSection && line.Contains(","))
                {
                    ProcessDemandLine(line, ref cummulativeProbabilityGood, ref cummulativeProbabilityFair, ref cummulativeProbabilityPoor, ref rowCount);
                }
            }

            ProcessTextBoxes(lines);
        }

        private void ProcessDayTypeLine(string line, ref decimal cummulativeProbability)
        {
            string[] values = line.Split(',');
            for (int i = 0; i < 3; i++)
            {
                string name = Enum.GetName(typeof(Enums.DayType), i);
                decimal probability = decimal.Parse(values[i]);
                cummulativeProbability += probability;
                (int, int) interval = ((int)((cummulativeProbability - probability) * 100) + 1, (int)(cummulativeProbability * 100));
                table.Rows.Add(name, probability, cummulativeProbability, interval);
            }
        }
        private void ProcessDemandLine(string line, ref decimal cummProbGood, ref decimal cummProbFair, ref decimal cummProbPoor, ref int rowCount)
        {
            string[] values = line.Split(',');
            decimal demand = int.Parse(values[0]);
            UpdateDemandProbabilities(values, ref cummProbGood, ref cummProbFair, ref cummProbPoor);
            UpdateTableRow(rowCount, demand, cummProbGood, cummProbFair, cummProbPoor, values);
            rowCount++;
            if (rowCount > 2) table.Rows.Add();
        }

        private void UpdateDemandProbabilities(string[] values, ref decimal cummProbGood, ref decimal cummProbFair, ref decimal cummProbPoor)
        {
            cummProbGood += decimal.Parse(values[1]);
            cummProbFair += decimal.Parse(values[2]);
            cummProbPoor += decimal.Parse(values[3]);
        }

        private void UpdateTableRow(int row, decimal demand, decimal cummProbGood, decimal cummProbFair, decimal cummProbPoor, string[] values)
        {
            table.Rows[row][4] = demand;
            table.Rows[row][5] = cummProbGood;
            table.Rows[row][6] = cummProbFair;
            table.Rows[row][7] = cummProbPoor;
            table.Rows[row][8] = CreateRange(cummProbGood, values[1]);
            table.Rows[row][9] = CreateRange(cummProbFair, values[2]);
            table.Rows[row][10] = CreateRange(cummProbPoor, values[3]);
        }

        private (int, int) CreateRange(decimal cummulativeProbability, string value)
        {
            return ((int)((cummulativeProbability - decimal.Parse(value)) * 100) + 1, (int)(cummulativeProbability * 100));
        }

        private void ProcessTextBoxes(string[] lines)
        {
            NumOfNewspapers = int.Parse(lines[1]);
            NNP_tb.Text = NumOfNewspapers.ToString();

            NumOfRecords = int.Parse(lines[4]);
            NR_tb.Text = NumOfRecords.ToString();

            PurchasePrice = decimal.Parse(lines[7]);
            PP_tb.Text = PurchasePrice.ToString();

            ScrapPrice = decimal.Parse(lines[10]);
            ScrapPrice_tb.Text = ScrapPrice.ToString();

            SellingPrice = decimal.Parse(lines[13]);
            SellingPrice_tb.Text = SellingPrice.ToString();
        }

        private void ST_btn_Click(object sender, EventArgs e)
        {
            Nmain();
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
