using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.classes
{
    class VisitFactory
    {
        //-------------------------------------------Methods-------------------------------------------

        //**************create_visit method**************
        public Visit create_visit(int[] staff, int patient, int type, string dateTime, List<Client> client_list, List<Staff> staff_list, List<Visit> visit_list, List<VisitType> visit_types_list)
        {
            //Find the visit type using input
            VisitType visitType_match = visit_types_list.FirstOrDefault(v => v.type_id == type);
            if (visitType_match == null)
            {
                //If the visit type does not exist then throw an exception
                throw new Exception("Visit type does not exist!");
            }

            //Find the client using input
            var client_match = client_list.FirstOrDefault(c => c.client_id == patient);
            if (client_match == null)
            {
                //If the client does not exist then throw an exception
                throw new Exception("Client  does not exist!");
            }
            //Will store staff that meet requirements in this list
            List<Staff> staff_found_list = new List<Staff>();

            for (int x = 0; x < staff.Length; x++)
            {
                var staff_found = staff_list.FirstOrDefault(s => s.staff_id == staff[x]);

                if (staff_found != null)
                {
                    //Get the staff using the id provided
                    var visit_found = from v in visit_list where v.date == Convert.ToDateTime(dateTime) select v;

                    //Check to see staff have valid category type for visit
                    if (!(visitType_match.staff_required.Contains(staff_found.category)))
                    {
                        //else If the staff type does not exist then throw an exception
                        throw new Exception("Staff type does not match visit requirements!");
                    }
                    //Loop through all the visits found
                    foreach (Visit v in visit_found)
                    {
                        //If another visit contains a staff that that day then remove that staff from the staff_copy_list
                        if (v.staff.Contains(staff_found))
                        {
                            throw new Exception("Staff not available that day, due to time clash!");
                        }
                    }

                    //If passed all requirements then add staff to the list of found staff
                    staff_found_list.Add(staff_found);
                }
                else
                {
                    throw new Exception("Staff does not exist!");
                }
            }

            //Add the new visit to the visits list if everything passes
            Visit visit = new Visit(client_match, staff_found_list, visitType_match, Convert.ToDateTime(dateTime));

            return visit;
        }

        //**************load_visit method**************
        public Visit load_visit(String v, List<Client> client_list, List<Staff> staff_list, List<VisitType> visit_types_list)
        {
            //Find the existing client
            var fields = v.Split(',');
            var client = client_list.FirstOrDefault(c => c.client_id == Int32.Parse(fields[0]));

            //Find the existing staff
            List<Staff> staff_list_match = new List<Staff>();
            var staff_id_list = fields[1].Split('#');
            foreach (string s_id in staff_id_list)
            {
                var staff = staff_list.FirstOrDefault(s => s.staff_id == Int32.Parse(s_id));
                staff_list_match.Add(staff);
            }

            //Find the visit type
            var visit_type = visit_types_list.FirstOrDefault(vt => vt.type_id == Int32.Parse(fields[2]));

            //Create the visit
            Visit visit = new Visit(client, staff_list_match, visit_type, Convert.ToDateTime(fields[3]));

            return visit;
        }
    }
}
