using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class times
    {
        public times(int busy,int stime,int etime) {
            this.IsBusy = busy;
            this.STime= stime;
            this.FTime = etime;
        }
        public int STime { get; set; }
        public int IsBusy { get; set; }
        public int FTime { get; set; }
    }
    public class Server
    {
        public Server()
        {
            this.TimeDistribution = new List<TimeDistribution>();
        }

        public int ID { get; set; }
        public decimal IdleProbability { get; set; }
        public decimal AverageServiceTime { get; set; } 
        public decimal Utilization { get; set; }

        public List<TimeDistribution> TimeDistribution;

        //optional if needed use them
        public int FinishTime { get; set; }
        public int TotalWorkingTime { get; set; }
        public int NumCustomers { get; set; }

        public int [] timeline=new int[100000];
    }

}
