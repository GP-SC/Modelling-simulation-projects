using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class genertor
    {

       
       
         public int getDemand(int randomNum ,ref List<Distribution> dist)
        {

            foreach(var  current in dist)
            {
                if(current.MinRange <= randomNum && current.MaxRange >= randomNum)
                {
                    return current.Value;
                }
            }
            return -1;
        }
        public  int getLeadTime(int randomNum, ref List<Distribution> dist)
        {

            foreach (var current in dist)
            {
                if (current.MinRange <= randomNum && current.MaxRange >= randomNum)
                {
                    return current.Value;
                }
            }
            return -1;
        }

    }
}
