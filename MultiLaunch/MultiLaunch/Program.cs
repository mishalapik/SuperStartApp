using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLaunch
{
    class Program
    {
        private int id { get; set; }
        private string preset_name { get; }
        private string program_path { get; set; }
        private string program_icon { get; set;}

        public Program() { }

        public Program(int id, string preset_name, string program_path, string program_icon)
        {
            this.id = id;
            this.preset_name = preset_name;
            this.program_path = program_path;
            this.program_icon = program_icon;
        }
    }
}
