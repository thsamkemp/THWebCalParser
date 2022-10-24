// See https://aka.ms/new-console-template for more information
using System;
using System.Net;
using System.IO;
using ConsoleApp1;

Console.WriteLine("Testing!!");

string calendarLink = "https://api.alertops.com/api/Schedule/Group/13501/32282/ExportGroupSchedule?APIKey=67ae8d34-40b1-42b1-a9e9-10325150241e";

var myEvent = new OnCallEvents();
myEvent.ProcessCalendar(calendarLink);





//event_list.ForEach(Console.WriteLine);



