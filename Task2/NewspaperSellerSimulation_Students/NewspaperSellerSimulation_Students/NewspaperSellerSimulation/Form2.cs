using NewspaperSellerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NewspaperSellerSimulation.Program;
namespace NewspaperSellerSimulation
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
            table2.Columns.Add("DayNo\r\n", typeof(int));
            table2.Columns.Add("RandomNewsDayType\r\n", typeof(int));
            table2.Columns.Add("NewsDayType", typeof(string)); // enum
            table2.Columns.Add("RandomDemand", typeof(int));
            table2.Columns.Add("Demand \r\n", typeof(int));
            table2.Columns.Add("DailyCost\r\n", typeof(decimal));
            table2.Columns.Add("SalesProfit\r\n", typeof(decimal));
            table2.Columns.Add("LostProfit\r\n", typeof(decimal));
            table2.Columns.Add("ScrapProfit\r\n", typeof(decimal));
            table2.Columns.Add("DailyNetProfit\r\n", typeof(decimal));
            
            for (int i = 0; i < SimSys.SimulationTable.Count; i++)
            {
                
                string type;
               
                    if (SimSys.SimulationTable[i].NewsDayType == Enums.DayType.Good)
                    {
                        type = "Good";
                    }
                    else if (SimSys.SimulationTable[i].NewsDayType == Enums.DayType.Fair)
                    {
                        type = "Fair";
                    }
                    else
                    {
                        type = "Poor";
                    }

                    table2.Rows.Add(
                         SimSys.SimulationTable[i].DayNo,
                         SimSys.SimulationTable[i].RandomNewsDayType,
                         type,
                         SimSys.SimulationTable[i].RandomDemand,
                         SimSys.SimulationTable[i].Demand,
                         SimSys.SimulationTable[i].DailyCost,
                         SimSys.SimulationTable[i].SalesProfit,
                         SimSys.SimulationTable[i].LostProfit,
                         SimSys.SimulationTable[i].ScrapProfit,
                         SimSys.SimulationTable[i].DailyNetProfit
                                                      );
                Console.WriteLine($"Customer {i} //////////////////////////");
            }

            dataGridView1.DataSource = table2;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
