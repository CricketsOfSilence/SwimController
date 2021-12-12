using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwimController
{
    public class ConfigurationModel
    {

        public ConfigurationModel()
        {
            LaneFilePaths = new string[6];
        }

        public string HeatSheetFilePath { get; set; }
        public string EventNameFilePath { get; set; }

        public string HeatNumberFilePath { get; set; }
        public string RecordFilePath { get; set; }
        public string[] LaneFilePaths { get; set; }
    }
}
