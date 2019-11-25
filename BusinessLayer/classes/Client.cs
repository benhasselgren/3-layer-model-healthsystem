using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Client:PersonI
    {
        //-------------------------------------------Interface Implementation-------------------------------------------
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }

        //-------------------------------------------Instance variables-------------------------------------------
        public int client_id { get; set; }
        public Tuple<double, double> location { get; set; }

        //-------------------------------------------Constructor-------------------------------------------
        public Client(string first_name, string last_name, string address_1, string address_2, int client_id, Tuple<double, double> location)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.address_1 = address_1;
            this.address_2 = address_2;
            this.client_id = client_id;
            this.location = location;
        }

        //-------------------------------------------Methods-------------------------------------------

        //**************ToString method**************
        public override string ToString()
        {
            return String.Format
                (
                    "Firstname: {0}, Surname: {1}, Address 1: {2}, Address 2: {3}, Client ID: {4}, Location: {5},{6}",
                    this.first_name,
                    this.last_name,
                    this.address_1,
                    this.address_2,
                    this.client_id.ToString(),
                    this.location.Item1.ToString(),
                    this.location.Item2.ToString()
                );
        }

        //**************to_csv_string method**************
        public string to_csv_string()
        {
            return String.Format
                (
                    "c,{0},{1},{2},{3},{4},{5},{6}",
                    this.first_name,
                    this.last_name,
                    this.address_1,
                    this.address_2,
                    this.client_id.ToString(),
                    this.location.Item1.ToString(),
                    this.location.Item2.ToString()
                );
        }
    }
}
