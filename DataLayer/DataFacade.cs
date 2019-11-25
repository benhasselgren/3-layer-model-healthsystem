using DataLayer.classes;
using DataLayer.Classes;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public class DataFacade
    {
        //-------------------------------------------Instance variables-------------------------------------------
        FilePathSingleton file = new FilePathSingleton();
        Data healthSystemData = new Data();

        //-------------------------------------------Methods-------------------------------------------

        //**************load_data method**************
        public List<List<String>> load_data()
        {
            //Create a list of lists of string to store data read from the file
            List<List<string>> data = new List<List<string>>();

            //Call the load data method to get the data from the file
            data = healthSystemData.load_data(file.path);
        
            //Return the data
            return data;
        }

        //**************sava_data method**************
        public void save_data(List<string> client_data_list, List<string> staff_data_list, List<string> visit_data_list)
        {
            //Call the save_data method to save data to the file
            healthSystemData.save_data(client_data_list, staff_data_list, visit_data_list, file.path);
        }
    }
}
