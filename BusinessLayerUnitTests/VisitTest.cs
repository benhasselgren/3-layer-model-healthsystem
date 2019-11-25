using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer.classes;
using System.Collections.Generic;
using BusinessLayer;

namespace BusinessLayerUnitTests
{
    [TestClass]
    public class VisitTest
    {
        //-------------------------------------------Instance variables-------------------------------------------
        private HealthFacade healthSystem = new HealthFacade();

        //-------------------------------------------Methods-------------------------------------------

        //**************toStringTest_should_be_equal method**************
        [TestMethod]
        public void toStringTest_should_be_equal()
        {
            Visit v = create_visit();
            string actual_result = v.ToString();
            string expected_result = "Client: Claire Wentworth, Staff: James Henry, Mary Jones, Type: Assessment, Date: 01/01/2020 09:00:00";

            //Both values should match
            Assert.AreEqual(expected_result, actual_result, "The strings don't match");
        }

        //**************toStringTest_should_not_be_equal method**************
        [TestMethod]
        public void toStringTest_should_not_be_equal()
        {
            Visit v = create_visit();
            string actual_result = v.ToString();
            //Mistakes are No comma between staff, printing type id instead of type name, time format is wrong(does not include seconds)
            string unexpected_result = "Client: Claire Wentworth, Staff: James Henry Mary Jones, Type: 0, Date: 01/01/2020 09:00";

            //Both values should not match
            Assert.AreNotEqual(unexpected_result, actual_result, "The strings do match");
        }

        //**************toCsvStringTest_should_be_equal method**************
        [TestMethod]
        public void toCsvStringTest_should_be_equal()
        {
            Visit v = create_visit();
            string actual_result = v.to_csv_string();
            string expected_result = "v,1,1#4,0,01/01/2020 09:00:00";

            //Both values should match
            Assert.AreEqual(expected_result, actual_result, "The strings don't match");
        }

        //**************toCsvStringTest_should_not_be_equal method**************
        [TestMethod]
        public void toCsvStringTest_should_not_be_equal()
        {
            Visit v = create_visit();
            string actual_result = v.to_csv_string();
            //Mistakes are it uses ses c instead of v(c is used to identify clients), no hastag character used to seperate staff id's, fate time format is wrong
            string unexpected_result = "c,1,3,2,1,01/05/2020 09:00";

            //Both values should not match
            Assert.AreNotEqual(unexpected_result, actual_result, "The strings do match");
        }

        //-------------------------------------------Helper Methods-------------------------------------------

        //**************create_visit helper method**************
        private Visit create_visit()
        {
            //Create a list of staff types
            List<string> staff_required = new List<string>();
            staff_required.Add(categories.sw);
            staff_required.Add(categories.gp);

            //Create a visit type
            VisitType assessmentType = new VisitType(visitTypes.assessment, staff_required, 60, "Assessment");
            //Create staff and add to list
            List<Staff> staff_available = new List<Staff>();
            Staff s1 = new Staff("James", "Henry", "21 Accia Road", "Edinburgh", 1, "General Practitioner", new Tuple<double, double>(55.932221, -3.214164));
            Staff s2 = new Staff("Mary", "Jones", "21 Accia Road", "Edinburgh", 4, "Social Worker", new Tuple<double, double>(55.932221, -3.214164));
            staff_available.Add(s1);
            staff_available.Add(s2);
            //Create client
            Client c1 = new Client("Claire", "Wentworth", "1 Low Rd", "Edinburgh", 1, new Tuple<double, double>(55.937894, -3.194088));
            //Create visit
            Visit v = new Visit(c1, staff_available, assessmentType, Convert.ToDateTime("01/01/2020 09:00"));

            return v;
        }
    }
}
