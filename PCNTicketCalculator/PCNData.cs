using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCNTicketCalculator
{
    public class PCNData
    {
        string licensePlate;
        DateTime entryDate;
        DateTime exitDatep;
        string pcnReason;
        string pcnCandidateInfo;
        int calculatedSummary;

        // It is an constructor.
        // It's main task to create our class. It's name is same as the class name ( public class PCNData).
        public PCNData(string dataLine)
        {

        }
    }
}
