using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsOverlay.model
{
    public class Event
    {
        public string EventName { get; set; }
        public string Record { get; set; }
        public string HeatNumber { get; set; }

        public Dictionary<Lane, String> Lanes { get; set; }    

        public Event NextEvent { get; set; }     

        public Event PreviousEvent { get; set; }
           
        public void PutLane(Lane lane, string value)
        {
            if (Lanes == null)
            {
                Lanes = new Dictionary<Lane, String>();
            }           

            Lanes[lane] = value;
        }

        public String GetLane(Lane lane)
        {            
            if (Lanes.ContainsKey(lane))
            {
                return Lanes[lane];
            }
            else
            {
                return "";
            }           
        }

        public bool HasNextEvent()
        {
            return NextEvent != null;
        }

        public bool HasPreviousEvent()
        {
            return PreviousEvent != null;
        }
    }
}
