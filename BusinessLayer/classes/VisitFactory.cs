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
            //Will store staff available in this list
            List<Staff> staff_found_list = new List<Staff>();

            //Find the visit type using input
            VisitType visitType_match = visit_types_list.FirstOrDefault(v => v.type_id == type);
            if (visitType_match == null)
            {
                //If the visit type does not exist then throw an exception
                throw new Exception();
            }

            //Find the client using input
            var client_match = client_list.FirstOrDefault(c => c.client_id == patient);
            if (client_match == null)
            {
                //If the client type does not exist then throw an exception
                throw new Exception();
            }

            //Create temporary list to store staff
            List<Staff> staff_list_copy = staff_list.ToList();

            //Find all visits with same dateTime field
            var visit_query = from v in visit_list where v.date == Convert.ToDateTime(dateTime) select v;

            //Remove the staff that are not available on that time of day
            //Loop through all the staff if visit_query has any visits in it
            if (visit_query.Count() >= 1)
            {
                foreach (Staff s in staff_list_copy)
                {
                    //Loop through all the visits found
                    foreach (Visit v in visit_query)
                    {
                        //If another visit contains a staff that that day then remove that staff from the staff_copy_list
                        if (v.staff.Contains(s))
                        {
                            staff_list_copy.Remove(s);
                        }
                    }
                }
            }


            //Search for available staff and if the correct staff are'nt available then catch exception
            for (int i = 1; i <= visitType_match.staff_required.Count; i++)
            {
                //Search for a staff with matches the requirements of the client
                var staff_match = staff_list_copy.FirstOrDefault(s => visitType_match.staff_required.Contains(s.category));

                if (staff_match == null)
                {
                    //If the staff type does not exist then throw an exception
                    throw new Exception();
                }
                else
                {
                    //Add the staff to the found list
                    staff_found_list.Add(staff_match);
                    //Remove him from the copy list as he/she is now not available (Prevents staff from being used multiple times)
                    staff_list_copy.Remove(staff_match);
                }
            }

            //Add the new visit to the visits list
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
