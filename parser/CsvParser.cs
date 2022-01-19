using ObsOverlay;
using ObsOverlay.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsOverlay.parser
{
    public class CsvParser
    {
        public static Event Load(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            string[] lastLine = lines[lines.Length - 1].Split(',');
            Event temp = new Event()
            {
                EventName = lastLine[0],
                Record = lastLine[1],
                Lanes = new Dictionary<Lane, String>()
                {
                    { Lane.Lane1, lastLine[2] },
                    { Lane.Lane2, lastLine[3] },
                    { Lane.Lane3, lastLine[4] },
                    { Lane.Lane4, lastLine[5] },
                    { Lane.Lane5, lastLine[6] },
                    { Lane.Lane6, lastLine[7] }
                },
                PreviousEvent = null,
                NextEvent = null
            };

            for (int i = lines.Length - 2; i >= 0; i--)
            {
                string[] split = lines[i].Split(',');
                Event e = new Event
                {
                    EventName = split[0],
                    Record = split[1],
                    Lanes = new Dictionary<Lane, String>()
                    {
                        { Lane.Lane1, split[2] },
                        { Lane.Lane2, split[3] },
                        { Lane.Lane3, split[4] },
                        { Lane.Lane4, split[5] },
                        { Lane.Lane5, split[6] },
                        { Lane.Lane6, split[7] }
                    },
                    PreviousEvent = null,
                    NextEvent = temp
                };
                // Set the previous event for the event we created in the previous loop.                
                temp.PreviousEvent = e;

                // Iterate the list
                temp = e;
            }

            // Should be the first event
            return temp;
        }
    }
}
