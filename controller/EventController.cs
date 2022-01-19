using ObsOverlay.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsOverlay.controller
{
    class EventController
    {          
        private FilePathConfiguration config;       
        private readonly int spacesToAppend = 4;
        private Event currentEvent;

        public EventController()
        {
            this.config = LoadFromConfig();
        }

        private FilePathConfiguration LoadFromConfig()
        {                                                  
            return FilePathConfiguration.LoadFromConfig();
        }

        public void SetConfig(FilePathConfiguration config)
        {
            this.config = config;
        }

        public FilePathConfiguration GetConfig()
        {
            return config;
        }

        public Event GetCurrentEvent()
        {
            return currentEvent;
        }

        public Event GoToPreviousEvent()
        {
            if (currentEvent.HasPreviousEvent())
            {
                currentEvent = currentEvent.PreviousEvent;
                WriteCurrentEvent();
            }
            
            return currentEvent;
        }

        public Event GoToNextEvent()
        {
            if (currentEvent.HasNextEvent())
            {
                currentEvent = currentEvent.NextEvent;
                WriteCurrentEvent();
            }

            return currentEvent;        
        }

        private void WriteCurrentEvent()
        {
            WriteToFile(config.EventName, currentEvent.EventName);
            WriteToFile(config.HeatNumber, currentEvent.HeatNumber);
            WriteToFile(config.Records, currentEvent.Record);
            WriteToFile(config.Lanes[Lane.Lane1], currentEvent.Lanes[Lane.Lane1]);
            WriteToFile(config.Lanes[Lane.Lane2], currentEvent.Lanes[Lane.Lane2]);
            WriteToFile(config.Lanes[Lane.Lane3], currentEvent.Lanes[Lane.Lane3]);
            WriteToFile(config.Lanes[Lane.Lane4], currentEvent.Lanes[Lane.Lane4]);
            WriteToFile(config.Lanes[Lane.Lane5], currentEvent.Lanes[Lane.Lane5]);
            WriteToFile(config.Lanes[Lane.Lane6], currentEvent.Lanes[Lane.Lane6]);
        }

        private void WriteToFile(String fileName, String text)
        {
            if (File.Exists(fileName) && !String.IsNullOrEmpty(text))
            {
                System.IO.File.WriteAllText(fileName, text.PadRight(spacesToAppend));
            }
        }    

        public void SetCurrentEvent(Event e)
        {
            currentEvent = e;
        }

        public Event SetEventName(String eventName)
        {
            currentEvent.EventName = eventName;
            WriteToFile(config.EventName, currentEvent.EventName);
            return currentEvent;
        }

        public Event SetLaneName(Lane lane, string name)
        {
            currentEvent.PutLane(lane, name);
            WriteToFile(config.GetLane(lane), name);
            return currentEvent;
        }      
    }
}
