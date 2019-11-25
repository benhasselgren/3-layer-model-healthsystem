using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    interface PersonI
    {
        //-------------------------------------------Propeties-------------------------------------------
        string first_name { get; set; }
        string last_name { get; set; }
        string address_1 { get; set; }
        string address_2 { get; set; }

        //-------------------------------------------Method Definitions-------------------------------------------
        string to_csv_string();
        string ToString();
    }
}
