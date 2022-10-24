namespace ConsoleApp1;
using System;
using System.Net;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections;


public class OnCallEvents
{
    
    public static void Main(string[] args)
    {
        
    }
    
    public static DataTable CreateDataTable<T>(IEnumerable<T> list)
    {
        Type type = typeof(T);
        var properties = type.GetProperties();      
    
        DataTable dataTable = new DataTable();
        dataTable.TableName = typeof(T).FullName;
        foreach (PropertyInfo info in properties)
        {
            dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
        }
    
        foreach (T entity in list)
        {
            object[] values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(entity);
            }
        
            dataTable.Rows.Add(values);
        }
    
        return dataTable;
    }

    public void ProcessCalendar(string myCalendar)
    {
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(myCalendar);
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
        var arList1 = new ArrayList();

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
                        //lineCollector.ReadString(lines[temp]);
                        arList1.Add(lineCollector.ReadString(lines[temp]));
                    }

                }
            }
            

        }
        Console.WriteLine(list_combined.Count);

        viewList(arList1);

        sr.Close();
        myResponse.Close();
    }

    public void viewList(ArrayList eventLists)
    {
        foreach (var item in eventLists)
        {
            if (item.ToString() == String.Empty)
            {
                //DO NOTHING
            }
            else
            {
                Console.WriteLine(item);
            }
           
        }
    }
    
}