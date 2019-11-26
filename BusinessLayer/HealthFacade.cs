using BusinessLayer.classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    //-------------------------------------------Static Data Classes-------------------------------------------
    public static class visitTypes
    {
        public const int assessment = 0;
        public const int medication = 1;
        public const int bath = 2;
        public const int meal = 3;
    }

    public static class categories
    {
        public const string gp = "General Practitioner";
        public const string cn = "Community Nurse";
        public const string sw = "Social Worker";
        public const string cw = "Care Worker";

    }

    //-------------------------------------------Health Facade Class-------------------------------------------
    public class HealthFacade
    {

        //-------------------------------------------Instance variables-------------------------------------------
        private List<Staff> staff_list = new List<Staff>();
        private List<Client> client_list = new List<Client>();
        private List<VisitType> visit_types_list = new List<VisitType>();
        private List<Visit> visit_list = new List<Visit>();
        private DataLayer.DataFacade healthSystemData = new DataLayer.DataFacade();
        private VisitFactory visit_factory = new VisitFactory();

        //-------------------------------------------Methods-------------------------------------------

        //**************CreateVisitTypes method**************
        public void createVisitTypes()
        {
            //Create the lists of staff_required for every visit type
            List<string> staff_required1 = new List<string>();
            staff_required1.Add(categories.sw);
            staff_required1.Add(categories.gp);

            List<string> staff_required2 = new List<string>();
            staff_required2.Add(categories.cn);

            List<string> staff_required3 = new List<string>();
            staff_required3.Add(categories.cw);
            staff_required3.Add(categories.cw);

            List<string> staff_required4 = new List<string>();
            staff_required4.Add(categories.cw);

            //Create a VisitType object for every visit type in visitTypes class
            VisitType assessmentType = new VisitType(visitTypes.assessment, staff_required1, 60, "Assessment");
            VisitType medicationtype = new VisitType(visitTypes.medication, staff_required2, 20, "Medication");
            VisitType bathType = new VisitType(visitTypes.bath, staff_required3, 30, "Bath");
            VisitType mealType = new VisitType(visitTypes.meal, staff_required4, 30, "Meal");

            //Add VisitType object to list
            visit_types_list.Add(assessmentType);
            visit_types_list.Add(medicationtype);
            visit_types_list.Add(bathType);
            visit_types_list.Add(mealType);
        }

        //**************addStaff method**************
        public Boolean addStaff(int id, string firstName, string surname, string address1, string address2, string category, double baseLocLat, double baseLocLon)
        {
            //Make sure the staff does not already exist and if he/she does do not create the staff
            bool exists = staff_list.Any(s => s.staff_id == id);

            if (!exists)
            {
                //Create a staff object and add to list
                Staff staff = new Staff(firstName, surname, address1, address2, id, category, new Tuple<double, double>(baseLocLat, baseLocLon));
                staff_list.Add(staff);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        //**************addClient method**************
        public Boolean addClient(int id, string firstName, string surname, string address1, string address2, double locLat, double locLon)
        {
            //Make sure the client does not already exist and if he/she does do not create the client
            bool exists = client_list.Any(c => c.client_id == id);

            if (!exists)
            {
                //Create a client object and add to list
                Client client = new Client(firstName, surname, address1, address2, id, new Tuple<double, double>(locLat, locLon));
                client_list.Add(client);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        //**************addVisit method**************
        public Boolean addVisit(int[] staff, int patient, int type, string dateTime)
        {
            
            //Create visit types if they have not been created yet
            if(visit_types_list.Count() == 0)
            {
                createVisitTypes();
            }

            try
            {
                //Use the visit factory create_visit method to attemt to create a visit
                Visit visit = visit_factory.create_visit(staff, patient, type, dateTime, client_list, staff_list, visit_list, visit_types_list);

                //Add visit to list
                visit_list.Add(visit);

                //If no errors thrown, assum OK
                return true;
            }
            catch(Exception ex)
            {
                //If an error is thrown from the visit factory, then assum WRONG
                throw new Exception(ex.Message);
            }
        }

        //**************getStaffList method**************
        public String getStaffList()
        {
            String result = "";

            foreach(Staff staff in staff_list)
            {
                result += staff.ToString() + '\n';
            }
            return result;
        }

        //**************getClientList method**************
        public String getClientList()
        {
            String result = "";

            foreach (Client client in client_list)
            {
                result += client.ToString() + '\n';
            }
            return result;
        }

        //**************getVisitList method**************
        public String getVisitList()
        {
            String result = "\n";

            foreach (Visit visit in visit_list)
            {
                result += visit.ToString() + '\n';
            }
            return result;
        }

        //**************clear method**************
        public void clear()
        {
            //Clear all the data from the lists
            client_list.Clear();
            staff_list.Clear();
            visit_list.Clear();
        }

        //**************load method**************
        public Boolean load()
        {
            //Create visit types if they have not been created yet
            if (visit_types_list.Count() == 0)
            {
                createVisitTypes();
            }

            //Get the data from the csv file. (Note Load_data() returns a list of list of strings which are the csv lines)
            List<List<string>> data = healthSystemData.load_data();
            //Call the load_helper method
            load_helper(data[0], data[1], data[2]);
            return true;
        }

        //**************save method**************
        public bool save()
        {
            //Call helper function to convert data to csv friendly strings
            List<List<string>> data = save_helper();
            //Save data to file
            healthSystemData.save_data(data[0], data[1], data[2]);
            return true;
        }

        //-------------------------------------------Helper methods-------------------------------------------

        //**************load_helper method**************
        private void load_helper(List<string> client_data_list, List<string> staff_data_list, List<string> visit_data_list)
        {
            if (client_data_list.Count != 0)
            {
                foreach (string c in client_data_list)
                {
                    // !NOTE! - fields[4] is first paramater because of where the client_id is saved in the csv file.
                    var fields = c.Split(',');
                    var result = addClient(Int32.Parse(fields[4]), fields[0], fields[1], fields[2], fields[3], Double.Parse(fields[5]), Double.Parse(fields[6]));
                }
            }
            if (staff_data_list.Count != 0)
            {
                foreach (string s in staff_data_list)
                {
                    // !NOTE! - fields[4] is first paramater because of where the staff_id is saved in the csv file.
                    var fields = s.Split(',');
                    var result = addStaff(Int32.Parse(fields[4]), fields[0], fields[1], fields[2], fields[3], fields[5], Double.Parse(fields[6]), Double.Parse(fields[7]));
                }
            }
            if (visit_data_list.Count != 0)
            {
                foreach (string v in visit_data_list)
                {
                    //Create the visit using visit_factory load_visit method
                    Visit visit = visit_factory.load_visit(v, client_list, staff_list, visit_types_list);

                    //If a visit with the same client name and same date/time exists, then don't add visit to the list
                    var exists = visit_list.FirstOrDefault(vl => vl.client == visit.client && vl.date == visit.date);
                    
                    if (exists == null)
                    {
                        visit_list.Add(visit);
                    }
                }
            }
        }

        //**************save_helper method**************
        private List<List<string>> save_helper()
        {
            List<List<string>> data = new List<List<string>>();
            List<string> client_data_list = new List<string>();
            List<string> staff_data_list = new List<string>();
            List<string> visit_data_list = new List<string>();

            if (client_list.Count() != 0)
            {
                //Convert all clients to csv friendly strings
                foreach (Client c in client_list)
                {
                    client_data_list.Add(c.to_csv_string());
                }
            }
            if (staff_list.Count() != 0)
            {
                //Convert all staff to csv friendly strings
                foreach (Staff s in staff_list)
                {
                    staff_data_list.Add(s.to_csv_string());
                }
            }
            if (visit_list.Count() != 0)
            {
                //Convert all vists to csv friendly strings
                foreach (Visit v in visit_list)
                {
                    visit_data_list.Add(v.to_csv_string());
                }
            }

            //Add the lists to one list and return the list
            data.Add(client_data_list);
            data.Add(staff_data_list);
            data.Add(visit_data_list);
            return data;
        }
    }
}
