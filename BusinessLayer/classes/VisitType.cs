using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class VisitType
    {
        //-------------------------------------------Instance variables-------------------------------------------
        public int type_id { get; set; }
        public List<String> staff_required { get; set; }
        public int duration { get; set; }
        public string type_name { get; set; }

        //-------------------------------------------Constructor-------------------------------------------
        public VisitType(int type, List<string> staff_required, int duration, string type_name)
        {
            this.type_id = type;
            this.staff_required = staff_required;
            this.duration = duration;
            this.type_name = type_name;
        }
    }
}
