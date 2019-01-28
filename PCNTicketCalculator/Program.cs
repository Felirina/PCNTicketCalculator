using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace PCNTicketCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PCNData> pcnDataList = new List<PCNData>();

            using (StreamReader sr = new StreamReader("RWY_Raw.txt"))
            {
                while (sr.EndOfStream == false)
                {
                    // We can read the file rows the data.
                    string readText = sr.ReadLine();

                    PCNData pcnData = new PCNData(readText);
                    pcnDataList.Add(pcnData);
                }

            }

            using (StreamWriter sw = new StreamWriter("RWY_Processed.txt"))
            {
                sw.WriteLine("License plate" + "\t" + "Entry date" + "\t" + "Exit date" + "\t" + "Revenue lost" + "\t" + "Ticket bought" + "\t" + "Stay lenght" + "\t" + "PCN reason");

                foreach (PCNData pcnD in pcnDataList)
                {
                    sw.WriteLine(pcnD.ToString());
                }
            }


        }
    }
}
