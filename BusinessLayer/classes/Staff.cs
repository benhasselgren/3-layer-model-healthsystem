using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Staff : PersonI
    {

        //-------------------------------------------Interface Implementation-------------------------------------------
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        //-------------------------------------------Instance variables-------------------------------------------
        public int staff_id { get; set; }
        public string category { get; set; }
        public Tuple<double, double> base_location { get; set; }

        //-------------------------------------------Constructor-------------------------------------------
        public Staff(string first_name, string last_name, string address_1, string address_2, int staff_id, string category, Tuple<double, double> base_location)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.address_1 = address_1;
            this.address_2 = address_2;
            this.staff_id = staff_id;
            this.category = category;
            this.base_location = base_location;
        }

        //-------------------------------------------Methods-------------------------------------------

        //**************ToString method**************
        public override string ToString()
        {
            return String.Format
                (
                    "Firstname: {0}, Surname: {1}, Address 1: {2}, Address 2: {3}, Staff ID: {4}, Category: {5}, Base Location: {6},{7}", 
                    this.first_name,
                    this.last_name,
                    this.address_1,
                    this.address_2,
                    this.staff_id.ToString(),
                    this.category,
                    this.base_location.Item1.ToString(),
                    this.base_location.Item2.ToString()
                );
        }

        //**************to_csv_string method**************
        public string to_csv_string()
        {
            return String.Format
                (
                    "s,{0},{1},{2},{3},{4},{5},{6},{7}",
                    this.first_name,
                    this.last_name,
                    this.address_1,
                    this.address_2,
                    this.staff_id.ToString(),
                    this.category,
                    this.base_location.Item1.ToString(),
                    this.base_location.Item2.ToString()
                );
        }
    }
}
