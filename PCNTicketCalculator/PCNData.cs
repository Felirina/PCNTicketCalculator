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
        int alreadyPaid = 0;


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

            // Insufficient Fee Paid<hr>Total time spent: 204<br>Grace period: 10<br>User is on the SIP white list: False<br>Maximum allowed minutes: 120<br>Count of bought tickets: 1<br>Total minutes covered by tickets: 181<br>User did bought a valid ticket but the paid fee was not enough to cover the whole parking session.<br>
            if (pcnReason == "Insufficient Fee Paid")
            {
                // Total minutes covered by tickets: 181
                string[] broken = pcnCandidateInfo.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);

                foreach(string piece in broken)
                {
                    if(piece.Contains("Total minutes covered by tickets:"))
                    {
                        string containedNumber = piece.Replace("Total minutes covered by tickets:", "").Trim();
                        int alreadyPaidMinutes = int.Parse(containedNumber);


                        alreadyPaid = CalculatedHourPrice(alreadyPaidMinutes);
                    }
                }
            }

            int minutes = (int)(exitDate - entryDate).TotalMinutes;

            calculatedSummary = CalculatedHourPrice(minutes);
        }

        public override string ToString()
        {
            string summaryInPounds = "£" + ( (calculatedSummary - alreadyPaid) / 100f).ToString("0.00");
            string alreadyPaidInPounds = "£" + (alreadyPaid / 100f).ToString("0.00");
            string stayLenght = FormatTimeSpan(exitDate - entryDate);

            return licensePlate + "\t" + entryDate.ToString("dd/MM/yyyy HH:mm") + "\t" + exitDate.ToString("dd/MM/yyyy HH:mm") + "\t" + summaryInPounds + "\t" + alreadyPaidInPounds + "\t" + stayLenght + "\t" + pcnReason;
        }

        private string FormatTimeSpan(TimeSpan span)
        {
            if (span.Days > 0 && span.Hours > 0)
            {
                if (span.Seconds > 30)
                    span = span.Add(new TimeSpan(0, 1, 0));

                span = span.Add(new TimeSpan(0, 0, -span.Seconds));

                if (span.Minutes > 30)
                    span = span.Add(new TimeSpan(1, 0, 0));

                span = span.Add(new TimeSpan(0, -span.Minutes, 0));
            }
            else if (span.Days < 1 && span.Hours > 0)
            {
                if (span.Seconds > 30)
                    span = span.Add(new TimeSpan(0, 1, 0));

                span = span.Add(new TimeSpan(0, 0, -span.Seconds));
            }

            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}  ", span.Days, span.Days == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}  ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}  ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }

        private int CalculatedHourPrice(int minutes)
        {
            if (minutes <= 120)
                return 200;
            else if (minutes <= 180)
                return 300;
            else if (minutes <= 240)
                return 400;
            else if (minutes <= 300)
                return 500;
            else if (minutes <= 360)
                return 600;
            else if (minutes <= 420)
                return 700;
            else if (minutes <= 480)
                return 800;
            else if (minutes <= 540)
                return 900;
            else if (minutes <= 1440)
                return 1000;
            else if (minutes <= 2880)
                return 2000;
            else
                return 8750;
        }
    }
}
