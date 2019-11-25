using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.classes
{
    public class Visit
    {
        //-------------------------------------------Instance variables-------------------------------------------
        public Client client { set; get; }
        public List<Staff> staff { get; set; }
        public VisitType type { get; set; }
        public DateTime date { get; set; }

        //-------------------------------------------Constructor-------------------------------------------
        public Visit(Client client, List<Staff> staff, VisitType type, DateTime date)
        {
            this.client = client;
            this.staff = staff;
            this.type = type;
            this.date = date;
        }

        //-------------------------------------------Methods-------------------------------------------

        //**************ToString method**************
        public override string ToString()
        {
            string staff_names = "";

            //For every staff in the staff list append their name to a string
            foreach(Staff s in staff)
            {
                if ((this.staff.IndexOf(s) + 1) == this.staff.Count)
                {
                    staff_names += string.Format("{0} {1}", s.first_name, s.last_name);
                }
                else
                {
                    //Add a comma and space to end of staff name if it's not the last staff in the list
                    staff_names += string.Format("{0} {1}, ", s.first_name, s.last_name);
                }

            }

            return String.Format
                (
                    "Client: {0} {1}, Staff: {2}, Type: {3}, Date: {4}",
                    this.client.first_name,
                    this.client.last_name,
                    staff_names,
                    this.type.type_name.ToString(),
                    this.date.ToString()
                );
        }

        //**************to_csv_string method**************
        public string to_csv_string()
        {
            string staff_ids = "";

            //For every staff in the staff list append their name to a string
            foreach (Staff s in this.staff)
            {
                if ((this.staff.IndexOf(s)+1) == this.staff.Count)
                {
                    staff_ids += string.Format("{0}", s.staff_id);
                }
                else
                {
                    //Add a character (later used to split the staff when reading file) to end of staff id if it's not the last staff in the list
                    staff_ids += string.Format("{0}#", s.staff_id);
                }  
            }

            return String.Format
                (
                    "v,{0},{1},{2},{3}",
                    this.client.client_id,
                    staff_ids,
                    this.type.type_id.ToString(),
                    this.date.ToString()
                );
        }
    }
}
