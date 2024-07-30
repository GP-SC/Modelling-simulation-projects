using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueTesting;
using MultiQueueModels;
using static MultiQueueSimulation.Form1;
using System.Data;
using static MultiQueueModels.Enums;
using System.Runtime.InteropServices;

namespace MultiQueueSimulation
{
    

    static class Program
    {
        public static List<SimulationCase> customers = new List<SimulationCase>();
        public static SimulationSystem SimSys = new SimulationSystem();
        public static bool[] vis;
        public static int TotalQueueTime = 0;
        public static int TotalRunTime = 0;
        public static int Nwaited = 0;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            //SimulationSystem system = new SimulationSystem();
            //string result = TestingManager.Test(system, Constants.FileNames.TestCase1);
            //MessageBox.Show(result);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Nmain();


        }
        public static void Nmain() {
            
            fillServers();
            Console.WriteLine("//////////////////////////");

            fillInterArrivals();
            Console.WriteLine("//////////////////////////");
            //PreformanceMeasure();
            OutPut();
            Console.WriteLine("//////////////////////////");
            // Print();
            string result = TestingManager.Test(SimSys, Constants.FileNames.TestCase2);
            MessageBox.Show(result);

        }
         static int getTimeOfSegment(int randomTime)
        {

            List<TimeDistribution> tmp = SimSys.InterarrivalDistribution;
            for(int i = 0; i < tmp.Count;i++)
            {
                if (tmp[i].MinRange <= randomTime && tmp[i].MaxRange >= randomTime)
                    return tmp[i].Time;
            }
            return -1;
        }
         static int getTimeOfSegmentServer(int randomTime,int SID)
        {

            List<TimeDistribution> tmp = SimSys.Servers[SID].TimeDistribution;
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].MinRange <= randomTime && tmp[i].MaxRange >= randomTime)
                    return tmp[i].Time;
            }
            return -1;
        }
      //static void idel(int id) {
      //  for(int i=0;i<SimSys.Servers.Count;i++)
      //      {
      //          if (i == id)
      //          {
      //              continue;
      //          }
      //          SimSys.Servers[i].tim.Add(new times(0,0,0));
      //      }
      //  }
         static (int,int) getIndexOfServer(int arrivaltime)
        {
            
            List<Server> server = SimSys.Servers;
            int min = 1000000000;
            int idx=0;
            SelectionMethod ss= SimSys.SelectionMethod;
            if (Enums.SelectionMethod.HighestPriority.ToString() == Enum.GetName(typeof(Enums.SelectionMethod), selection))
            { 
                for (int i = 1; i < server.Count;i++ )
                {
                    if (arrivaltime >= server[i].FinishTime) vis[i] = false;
                }
                for (int i = 1;i < server.Count; i++)
                {
                    if (!vis[i])
                    {
                        vis[i] = true;

                        
                        return (i,arrivaltime);
                    }      
                }
                for(int i=1;i< server.Count; i++)
                {
                    if (server[i].FinishTime<min)
                    {
                        min= server[i].FinishTime;
                        idx = i;
                    }
                }
                SimSys.Servers = server;
              
                return (idx, min);

            }
            else if (Enums.SelectionMethod.Random.ToString() == Enum.GetName(typeof(Enums.SelectionMethod), selection))
            {
                List<int>idxlist= new List<int>();
                for (int i = 1; i < server.Count; i++)
                {
                    if (arrivaltime >= server[i].FinishTime) { vis[i] = false; idxlist.Add(i);}
                }
                Random rnd= new Random();
                if(idxlist.Count> 0)
                {
                    idx = rnd.Next(0, idxlist.Count);
                   
                    return (idxlist[idx], arrivaltime);
                }
                else
                {
                    for (int i = 1; i < server.Count; i++)
                    {
                        if (server[i].FinishTime < min)
                        {
                            min = server[i].FinishTime;
                            idx = i;

                        }
                    }
                 
                    return (idx, min);

                }
            }
            else 
            {
                int minworktime = 9999;
                for (int i = 1; i < server.Count; i++) 
                {
                    if (server[i].TotalWorkingTime<=minworktime)
                    {
                        idx= i;
                        minworktime = server[i].TotalWorkingTime;
                        min = arrivaltime<server[i].FinishTime? server[i].FinishTime : arrivaltime;
                    }
                }
               
                return (idx, min);
            }
            
            
        }
         static void OutPut()
        {
            Random rndI = new Random();
            Random rndS = new Random();

            /*
             1 - random digit for the customer                                  done
             2 - get the interarrival time in the time distibution              done
             3 - get the time of the arrival                                    done
             4 - do the same random for the servers
             5 - check for the selection method to assign to each custmor
             6 - get the service time for each customer
             7 - time of begin (t) and end (t + service time)
             8 - time in queue.(if there is no available server it means that it will wait in queue)
             9 - sort according to time of finish  
             */
            
            if (Enum.GetName(typeof(Enums.StoppingCriteria) , StopCriteria)==Enums.StoppingCriteria.NumberOfCustomers.ToString())
            {

                vis = new bool[SimSys.Servers.Count + 1];
                for (int i = 0; i < SimSys.StoppingNumber; i++)
                {

                    int num = rndI.Next(1,101);
                    int num2 = rndS.Next(1,101);
                    SimulationCase tmp = new SimulationCase();
                    Enums enums = new Enums();

                    tmp.CustomerNumber = i + 1;
                    tmp.RandomInterArrival = i == 0 ? 1 : num;
                    tmp.InterArrival = i == 0 ? 0 : getTimeOfSegment(num);
                    tmp.ArrivalTime = tmp.InterArrival + (i == 0 ? 0 : customers[i - 1].ArrivalTime);
                    tmp.RandomService = num2;
                    (int, int) Times = getIndexOfServer(tmp.ArrivalTime);
                    tmp.AssignedServer.ID = Times.Item1;
                    tmp.StartTime = Times.Item2;
                    tmp.ServiceTime = getTimeOfSegmentServer(tmp.RandomService, tmp.AssignedServer.ID);
                    SimSys.Servers[tmp.AssignedServer.ID].TotalWorkingTime+=tmp.ServiceTime;
                    

                    //tmp.StartTime = tmp.ArrivalTime;// this need to be modified with case customer wait in queue if all servers basy
                    tmp.EndTime = tmp.StartTime + tmp.ServiceTime;
                    SimSys.Servers[tmp.AssignedServer.ID].FinishTime = tmp.EndTime;
                    tmp.TimeInQueue = tmp.StartTime - tmp.ArrivalTime;
                    TotalQueueTime += tmp.TimeInQueue;
                    if (tmp.TimeInQueue > 0) Nwaited++;
                    TotalRunTime = Math.Max(TotalRunTime, tmp.EndTime);
                    SimSys.Servers[tmp.AssignedServer.ID].NumCustomers++;
                    busy(tmp.AssignedServer.ID, tmp.StartTime, tmp.EndTime);
                    customers.Add(tmp);
                    SimSys.SimulationTable.Add(tmp);
                    //idel(tmp.AssignedServer.ID);


                }
            }
            else
            {

                // we want to find some thing have the last finsh time in the sys  
                // cheack arrival time for each customer less than sim_time  && cheack arrival_Time + service_Time<=Sim_Time 
                vis = new bool[SimSys.Servers.Count + 1];
                int i = 0;
                TotalRunTime = SimSys.StoppingNumber;
                while(true)
                {

                    int num = rndI.Next(1,101);
                    int num2 = rndS.Next(1,101);
                    SimulationCase tmp = new SimulationCase();
                    Enums enums = new Enums();

                    tmp.CustomerNumber = i + 1;
                    tmp.RandomInterArrival = i == 0 ? 1 : num;
                    tmp.InterArrival = i == 0 ? 0 : getTimeOfSegment(num);
                    tmp.ArrivalTime = tmp.InterArrival + (i == 0 ? 0 : customers[i - 1].ArrivalTime);
                    if (tmp.ArrivalTime > SimSys.StoppingNumber) break;
                    tmp.RandomService = num2;
                    (int, int) Times = getIndexOfServer(tmp.ArrivalTime);
                    tmp.AssignedServer.ID = Times.Item1;
                    tmp.StartTime = Times.Item2;
                    //tmp.AssignedServer.ID = chosseServer(tmp.ArrivalTime);
                    tmp.ServiceTime = getTimeOfSegmentServer(tmp.RandomService, tmp.AssignedServer.ID);
                    SimSys.Servers[tmp.AssignedServer.ID].TotalWorkingTime += tmp.ServiceTime;
                   // SimSys.Servers[tmp.AssignedServer.ID].NumCustomers+=1;
                    tmp.StartTime = tmp.ArrivalTime;// this need to be modified with case customer wait in queue if all servers basy
                    tmp.EndTime = tmp.StartTime + tmp.ServiceTime;
                    if (tmp.EndTime > SimSys.StoppingNumber) break;
                    //Check if Stopping Time by finish or Arrival time
                    SimSys.Servers[tmp.AssignedServer.ID].FinishTime = tmp.EndTime;
                    tmp.TimeInQueue = tmp.StartTime - tmp.ArrivalTime;
                    TotalQueueTime += tmp.TimeInQueue;
                    if (tmp.TimeInQueue > 0) Nwaited++;
                    SimSys.Servers[tmp.AssignedServer.ID].NumCustomers++;
                    busy(tmp.AssignedServer.ID, tmp.StartTime, tmp.EndTime);
                    //idel(tmp.AssignedServer.ID);
                    customers.Add(tmp);
                    SimSys.SimulationTable.Add(tmp);
                    i++;
                }
               
            }
            PreformanceMeasure();

        }

        public static void PreformanceMeasure()
        {
            
            SimSys.PerformanceMeasures.AverageWaitingTime = (decimal)TotalQueueTime / (decimal)customers.Count;
            //SimSys.PerformanceMeasures.AverageWaitingTime.Equals(TotalQueueTime / customers.Count);
            Console.WriteLine("//////////bla bla//////////////////");// every time 0
            //SimSys.PerformanceMeasures= p;
            Console.WriteLine(TotalQueueTime);// every time 0
            Console.WriteLine(customers.Count);// every time 0
           // Console.WriteLine(AverageWaitingTime);// every time 0

            SimSys.PerformanceMeasures.WaitingProbability= (decimal)Nwaited / (decimal)customers.Count;
            SimSys.PerformanceMeasures.MaxQueueLength = CalcMaxQueue();
            for (int i=1;i<SimSys.Servers.Count;i++) 
            {
                
                SimSys.Servers[i].AverageServiceTime= SimSys.Servers[i].NumCustomers ==0 ? 0 : (decimal)SimSys.Servers[i].TotalWorkingTime/ (decimal)SimSys.Servers[i].NumCustomers;
                SimSys.Servers[i].IdleProbability = (decimal)(TotalRunTime-SimSys.Servers[i].TotalWorkingTime) / (decimal)TotalRunTime;
                SimSys.Servers[i].Utilization = (decimal)(SimSys.Servers[i].TotalWorkingTime) / (decimal)TotalRunTime;
            }
            
        }
        public static int CalcMaxQueue()
        {
            
            bool Waiting = false;
            int MaxWait = 0;
            int currlen = 0;
            Queue<SimulationCase> waiting= new Queue<SimulationCase>();
            foreach (SimulationCase sim in customers)
            {
                waiting.Enqueue(sim);
               
                while (waiting.Count != 0 && waiting.First().StartTime<=sim.ArrivalTime )
                {
                    waiting.Dequeue();
                }
                MaxWait=Math.Max(MaxWait, waiting.Count);
               
            }
            return MaxWait;
        }

        static void fillInterArrivals()
        {

            for (int row = 0; row < GlobTable.Rows.Count; row++)
            {
                TimeDistribution td = new TimeDistribution();
                td.Time = GlobTable.Rows[row].Field<int>($"InterarrivalDistribution\r\n");
                td.Probability = GlobTable.Rows[row].Field<decimal>($"Probability\r\n");
                td.CummProbability = GlobTable.Rows[row].Field<decimal>("Cummulative Probability");
                (int, int) interval = GlobTable.Rows[row].Field<(int, int)>("Range");
                td.MinRange = interval.Item1;
                td.MaxRange = interval.Item2;
                SimSys.InterarrivalDistribution.Add(td);
            }
            //for (int row = 0; row < GlobTable.Rows.Count; row++)
            //{
            //    TimeDistribution td = new TimeDistribution();
            //    td.Time = GlobTable.Rows[row].Field<int>($"InterarrivalDistribution\r\n");
            //    td.Probability = GlobTable.Rows[row].Field<decimal>($"Probability\r\n");
            //    td.CummProbability = GlobTable.Rows[row].Field<decimal>("Cummulative Probability");
            //    (int, int) interval = GlobTable.Rows[row].Field<(int, int)>("Range");
            //    td.MinRange = interval.Item1;
            //    td.MaxRange = interval.Item2;
            //    Console.WriteLine($"Row Number {row}");
            //    Console.WriteLine( SimSys.InterarrivalDistribution[row].Time);
            //    Console.WriteLine(SimSys.InterarrivalDistribution[row].Probability);
            //}
        }
         static void fillServers() 
        {
            SimSys.Servers.Add(new Server());
            //SimSys=new SimulationSystem();
            for (int i =1;i<= N_servers;i++) 
          {
                Server Serv = new Server();     
                for (int row=0;row<GlobTable.Rows.Count; row++)
                {
                    TimeDistribution td=new TimeDistribution();
                    Console.WriteLine("-----------------------------------Output------------------------");
                    Console.WriteLine(GlobTable.Rows[row][i*4]==null);
                    if (GlobTable.Rows[row].Field<int>($"S{i} Service Time\r\n").ToString()=="") break;
                    td.Time= GlobTable.Rows[row].Field<int>($"S{i} Service Time\r\n");
                    td.Probability = GlobTable.Rows[row].Field<decimal>($"S{i} Probability\r\n");
                    td.CummProbability= GlobTable.Rows[row].Field<decimal>($"S{i} Cumm Probability\r\n");
                    (int, int) interval = GlobTable.Rows[row].Field<(int, int)>($"S{i} Range\r\n");
                    td.MinRange = interval.Item1;
                    td.MaxRange = interval.Item2;
                    Serv.TimeDistribution.Add(td);
                    if (td.MaxRange == 100) break;

                }
                Serv.ID = i;
                Serv.FinishTime = 0;
                SimSys.Servers.Add( Serv );
           }
         
            SimSys.NumberOfServers = N_servers;
            SimSys.StoppingNumber = StoppingNum;
            if (selection == 1)
            {
                SimSys.SelectionMethod.Equals ( Enums.SelectionMethod.HighestPriority);
            }
            else if(selection == 2) 
            {
                SimSys.SelectionMethod.Equals(Enums.SelectionMethod.Random);
            }
            else
            {
                SimSys.SelectionMethod.Equals(Enums.SelectionMethod.LeastUtilization);
            }
            //SimSys.SelectionMethod = (SelectionMethod)selection;
            if (StopCriteria == 1)
            {
                SimSys.StoppingCriteria.Equals(Enums.StoppingCriteria.NumberOfCustomers);
            }
            else
            {
                SimSys.StoppingCriteria.Equals(Enums.StoppingCriteria.SimulationEndTime);
            }
            SimSys.StoppingCriteria = (StoppingCriteria)StopCriteria;
            Console.WriteLine("-----------Moza------------");
            Console.WriteLine(SimSys.NumberOfServers);
            Console.WriteLine(SimSys.StoppingNumber);
            Console.WriteLine(SimSys.SelectionMethod);
            Console.WriteLine(SimSys.StoppingCriteria);
        }
        static void busy(int j, int Stime, int Etime)
        {
            for (int i =Stime; i < Etime; i++) 
            {
                SimSys.Servers[j].timeline[i] = 1;
            }
            
     
        }
        static void Print()
        {
            Console.WriteLine(customers.Count);
            for(int i = 0;i< customers.Count; i++)
            {
                Console.WriteLine($"Customer {i} //////////////////////////");
                Console.WriteLine(customers[i].CustomerNumber);//1
                Console.WriteLine(customers[i].RandomInterArrival);//2
                Console.WriteLine(customers[i].InterArrival);//3
                Console.WriteLine(customers[i].ArrivalTime);//4
                Console.WriteLine(customers[i].RandomService);//5
                Console.WriteLine(customers[i].ServiceTime);//6
                Console.WriteLine(customers[i].AssignedServer.ID);//7
                Console.WriteLine(customers[i].StartTime);//8
                Console.WriteLine(customers[i].EndTime);//9
                Console.WriteLine(customers[i].TimeInQueue);//10
                Console.WriteLine("**********************************************************");

            }

        }
    }
}
