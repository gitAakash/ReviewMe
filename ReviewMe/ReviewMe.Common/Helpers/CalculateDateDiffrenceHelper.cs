using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.Common.Helpers
{
    public class CalculateDateDiffrenceHelper
    {
        public static string DifferenceBetTwoDates(DateTime FromDate, DateTime ToDate)
        {
            string returnString = string.Empty;

            int totalDays = (ToDate.Day - FromDate.Day);
            int totalMonths = (ToDate.Month - FromDate.Month) + 12 * (ToDate.Year - FromDate.Year);
            int totalYear = ToDate.Year - FromDate.Year;

            TimeSpan span = (ToDate - FromDate);
            int totalHrs = span.Hours;
            int totalMinute = span.Minutes;
            int totalSecond = span.Seconds;

            if(totalYear > 0)
            {
                if(totalYear == 1)
                {
                    returnString = totalYear + " year ago";
                }
                else
                {
                    returnString = totalYear + " years ago";          
                }                   
            }
            else if(totalMonths > 0)
            {
                if(totalMonths == 1)
                {
                    returnString = totalMonths + " month ago";
                }
                else
                {
                    returnString = totalMonths + " months ago";
                }
            }
            else if(totalDays > 0)
            {
                if(totalDays==1)
                {
                    returnString = totalDays + " day ago";
                }
                else
                {
                    returnString = totalDays + " days ago";
                }
            }
            else if (totalHrs> 0)
            {
                if(totalHrs == 1)
                {
                    returnString = totalHrs + " hr ago";
                }
                else
                {
                    returnString = totalHrs + " hrs ago";
                }
            }
            else if (totalMinute > 0)
            {
                if (totalMinute == 1)
                {
                    returnString = totalMinute + " minute ago";
                }
                else
                {
                    returnString = totalMinute + " minutes ago";
                }
            }
            else if (totalSecond > 0)
            {
                if (totalSecond == 1)
                {
                    returnString = totalSecond + " second ago";
                }
                else
                {
                    returnString = totalSecond + " seconds ago";
                }
            }
            
            return returnString;

        }
    }
}
