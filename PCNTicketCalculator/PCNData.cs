using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCNTicketCalculator
{
    public class PCNData
    {
        string licensePlate;
        DateTime entryDate;
        DateTime exitDate;
        string pcnReason;
        string pcnCandidateInfo;
        int calculatedSummary;

        // It is an constructor.
        // It's main task to create our class. It's name is same as the class name ( public class PCNData).
        public PCNData(string dataLine)
        {
            //The split metod broken the string (string dataLine).
            string[] tableBroken = dataLine.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

            licensePlate = tableBroken[0];

            // Sample: 23/01/2019 10:14
            // CultureInfo.Invariant Culture = Do what I want (what I write :))
            entryDate = DateTime.ParseExact(tableBroken[1], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            exitDate = DateTime.ParseExact(tableBroken[2], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            pcnReason = tableBroken[3];
            pcnCandidateInfo = tableBroken[4];

        }
    }
}
