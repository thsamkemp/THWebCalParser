// See https://aka.ms/new-console-template for more information
using System;
using System.Net;
using System.IO;
using ConsoleApp1;

Console.WriteLine("Testing!!");

HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://api.alertops.com/api/Schedule/Group/13501/32282/ExportGroupSchedule?APIKey=67ae8d34-40b1-42b1-a9e9-10325150241e");
//HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://api.alertops.com/api/Schedule/Group/13711/32282/ExportGroupSchedule?APIKey=67ae8d34-40b1-42b1-a9e9-10325150241e");
myRequest.Method = "GET";
WebResponse myResponse = myRequest.GetResponse();
StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
//string result = sr.ReadToEnd();

//Look for start and end of event
String eventMarkerStart = "BEGIN:VEVENT";
String eventMarkerEnd = "END:VEVENT";

string ical = sr.ReadToEnd();
char[] delim = { '\n' };
string[] lines = ical.Split(delim);
delim[0] = ':';

var event_list = new List<string>();
var event_details = new List<string>();

int event_start;
int event_end;

//var list_start = new List<int>();
//var list_end = new List<int>();
var list_combined = new List<int>();

//for (int i = 5; i < lines.Length; i++)
for (int i = 4; i < 150; i++)
{
    //event_list.Add(lines[i]);
    if (lines[i].Contains("BEGIN:VEVENT"))
    {
        event_start = i;
        //Console.WriteLine("S--------" + event_start);
        //list_start.Add(event_start);
        list_combined.Add(event_start);
    }

    if (lines[i].Contains("END:VEVENT"))
    {
        event_end = i;
        //Console.WriteLine("E---------" + event_end);
        //list_end.Add(event_end);
        list_combined.Add(event_end);
    }
    
}

var lineCollector = new formatLines();

for (int i = 4; i < lines.Length; i++)
{


    for (int j = 0; j < list_combined.Count; j += 2)
    {
        if (i >= list_combined[j] && i <= list_combined[j] + 1 && j + 1 < list_combined.Count)
        {
            Console.WriteLine("-----NEW EVENT------" + list_combined[j] + "--" + list_combined[j+1]);
            int event_length = list_combined[j + 1] - list_combined[j];
            Console.WriteLine("Event LENGTH: " + event_length);

            for (int k = 0; k < event_length; k++)
            {
                var temp = i + k;
                //Console.WriteLine(lines[temp]);
                lineCollector.ReadString(lines[temp]);
            }

        }
    }
    

}
Console.WriteLine(list_combined.Count);



sr.Close();
myResponse.Close();




//event_list.ForEach(Console.WriteLine);



