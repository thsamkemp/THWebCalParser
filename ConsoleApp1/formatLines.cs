using System.Globalization;

namespace ConsoleApp1;
using System;
using System.IO;


public class formatLines
{
    public string myLine;
    
    public static void Main(string[] args)
    {
        
    }

    public void ReadString(string theLine)
    {
        if (theLine.Contains("DT"))
        {
            string cleanLine = theLine.Split(':').Last();
            cleanLine = cleanLine.Replace("T", "");
            cleanLine = cleanLine.Replace("Z", "");
            //cleanLine = cleanLine.Replace("\r", "");
            cleanLine = cleanLine.Trim(new char[] { '\n', '\r' });
            FormatDate(cleanLine);
        }
        else if (theLine.Contains("SUMMARY:"))
        {
            string cleanLine = theLine.Split(':').Last();
            cleanLine = cleanLine.Trim(new char[] { '\n', '\r' });
            cleanLine = cleanLine.Replace("\n", " ");
            cleanLine = cleanLine.Replace("\\", "");
            //cleanLine = cleanLine.Replace("\",  "");
            Console.WriteLine(cleanLine);
        }
        else if (theLine.Contains("UID:"))
        {
            string cleanLine = theLine.Split(':').Last();
            cleanLine = cleanLine.Trim(new char[] { '\n', '\r' });
            Console.WriteLine(cleanLine);
        }
        else if (theLine.Contains("BEGING:"))
        {
            //no nothing
        }
        else if (theLine.Contains("END:"))
        {
            //no nothing
        }
        else if (theLine.Contains("CATEGORIES:"))
        {
            //no nothing
        }
        else if (theLine.Contains("CREATED:"))
        {
            //no nothing
        }
        else if (theLine.Contains("COLOR:"))
        {
            //no nothing
        }
        else if (theLine.Contains("SEQUENCE:"))
        {
            //no nothing
        }
        else if (theLine.Contains("DESCRIPTION:"))
        {
            /*
            string cleanLine = theLine.Split(':').Last();
            cleanLine = cleanLine.Trim(new char[] { '\n', '\r' });
            cleanLine = cleanLine.Replace("\n", " ");
            cleanLine = cleanLine.Replace("\\", "");
            Console.WriteLine(cleanLine);
            */
        }
        else
        {
            string cleanLine = theLine.Split(':').Last();
            cleanLine = cleanLine.Trim(new char[] { '\n', '\r' });
            Console.WriteLine(cleanLine);
        }
    }

    public void FormatDate(string dateLine)
    {
        CultureInfo cultureInfoProvider = new CultureInfo("en-US");
        DateTime dt = DateTime.ParseExact(dateLine, "yyyyMMddHHmmss", cultureInfoProvider);
        Console.WriteLine(dt.ToString());
    }
}