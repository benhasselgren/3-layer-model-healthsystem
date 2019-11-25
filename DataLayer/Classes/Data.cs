using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataLayer.Classes
{
    class Data
    {
        //-------------------------------------------Methods-------------------------------------------

        //**************load_data method**************
        public List<List<String>> load_data(string path)
        {
            List<List<string>> data = new List<List<string>>();
            List<string> client_data_list = new List<string>();
            List<string> staff_data_list = new List<string>();
            List<string> visit_data_list = new List<string>();

            //Uses the FilePathSingleton path value to get the file to read
            StreamReader reader = new StreamReader(File.OpenRead(path));

            //Loop till end of file
            while (!reader.EndOfStream)
            {
                //Split the current line by delimiter (,)
                String line = reader.ReadLine();
                string field = "";
                var values = line.Split(',');

                if (values[0] == "c")
                {
                    //If the line is a client record then concatenate 7 fields to string
                    field = String.Format
                        (
                            "{0},{1},{2},{3},{4},{5},{6}",
                            values[1],
                            values[2],
                            values[3],
                            values[4],
                            values[5],
                            values[6],
                            values[7]
                        );
                    //Add client to list
                    client_data_list.Add(field);
                }
                if (values[0] == "s")
                {
                    //If the line is a staff record then concatenate 8 fields to string
                    field = String.Format
                        (
                            "{0},{1},{2},{3},{4},{5},{6},{7}",
                            values[1],
                            values[2],
                            values[3],
                            values[4],
                            values[5],
                            values[6],
                            values[7],
                            values[8]
                        );
                    //Add staff to list
                    staff_data_list.Add(field);
                }
                if (values[0] == "v")
                {
                    //If the line is a visit record then concatenate 4 fields to string
                    field = String.Format
                        (
                            "{0},{1},{2},{3}",
                            values[1],
                            values[2],
                            values[3],
                            values[4]
                        );
                    //Add visit to list
                    visit_data_list.Add(field);
                }
            }

            //Add the client,staff,visit lists to one list to return all the data to the business layer
            data.Add(client_data_list);
            data.Add(staff_data_list);
            data.Add(visit_data_list);

            return data;
        }

        //**************sava_data method**************
        public void save_data(List<string> client_data_list, List<string> staff_data_list, List<string> visit_data_list, string path)
        {
            //Create a new string builder (csv_sb) to append text to
            var csv_sb = new StringBuilder();

            if (client_data_list.Count() != 0)
            {
                //Append all clients to stringbuilder
                foreach (String c in client_data_list)
                {
                    csv_sb.AppendLine(c);
                }
            }
            if (staff_data_list.Count() != 0)
            {
                //Append all staff to stringbuilder
                foreach (String s in staff_data_list)
                {
                    csv_sb.AppendLine(s);
                }
            }
            if (visit_data_list.Count() != 0)
            {
                //Append all visits to stringbuilder
                foreach (String v in visit_data_list)
                {
                    csv_sb.AppendLine(v);
                }
            }

            //Write stringbuilder to csv file (Not .AppendAllText as that will add duplicate data to the file)
            File.WriteAllText(path, csv_sb.ToString());
        }
    }
}
