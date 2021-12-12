using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SwimController.loader
{
    public class HtmlLoader
    {

        private const string EVENT_NAME_REGEX = @"Event\s+\d+\s+(Girls|Boys)\s+(\d{1,} (Yard|mtr))\s+(Medley Relay|Freestyle|IM|Butterfly|Backstroke|Freestyle Relay|Diving|Breaststroke)";
        private const string RECORDS_REGEX = @"{0} (\d+ \d+ \d+";

        public static List<Event> Load(String fileLocation)
        {
            List<Event> events = new List<Event>();
            
            if (!File.Exists(fileLocation))
            {
                throw new FileNotFoundException(String.Format("Can't find {0}.", fileLocation));
            }

            string data = File.ReadAllText(fileLocation);
            // String all HTML
            data = Regex.Replace(data, "<.+?>", " ");
            // Trim all data to first event
            data = data.Substring(data.IndexOf("Event"));
            // Remove extra spaces
            data = Regex.Replace(data, @"\s{2,}", " ");

            int totalEvents = new HashSet<Match>((IEnumerable<Match>)Regex.Matches(data, @"Event \d+") ).Count;

            for (int i = 0; i < totalEvents; i++)
            {
                string eventData = data.Remove(0, data.IndexOf("Event", 1));
                if (eventData.IndexOf("relay", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                {

                } 
                else
                {
                    Event e = new Event();
                    e.EventName = Regex.Match(eventData, EVENT_NAME_REGEX).Value;
                    
                    if (e.EventName.IndexOf("diving", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        
                    }
                    else
                    {

                    }
                }
            }
            
            return events;
        }
    }
}
