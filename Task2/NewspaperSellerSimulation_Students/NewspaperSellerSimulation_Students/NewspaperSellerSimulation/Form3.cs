using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static NewspaperSellerSimulation.Program;

namespace NewspaperSellerSimulation
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            FilPerformance();
        }
        void FilPerformance()
        {
            TSR_lb.Text = SimSys.PerformanceMeasures.TotalSalesProfit.ToString();
            TCNP_lb.Text= SimSys.PerformanceMeasures.TotalCost.ToString();
            TLPED_lb.Text =SimSys.PerformanceMeasures.TotalLostProfit.ToString();
            TSSP_lb.Text  =SimSys.PerformanceMeasures.TotalScrapProfit.ToString();
            NP_lb.Text    =SimSys.PerformanceMeasures.TotalNetProfit.ToString();
            NODHED_lb.Text=SimSys.PerformanceMeasures.DaysWithMoreDemand.ToString();
            NODHUP_lb.Text=SimSys.PerformanceMeasures.DaysWithUnsoldPapers.ToString();

        }
    }
}
