﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace KaibaReduxAPI.Controllers
{
    public class DbAccessManagement
    {
        public static bool DBTest()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-PPEIFCP;Initial Catalog=kaibaredux;Trusted_Connection=yes;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                cnn.Close();
                System.Diagnostics.Debug.WriteLine("Connection worked");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public static string DBTest2()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-PPEIFCP;Initial Catalog=kaibaredux;Trusted_Connection=yes;";
            cnn = new SqlConnection(connetionString);
            try
            {
                string result = " ";
                cnn.Open();

                string sqlString = "SELECT * FROM t_item i, t_priceline p WHERE i.itemID = p.itemID";

                SqlCommand myCommand = new SqlCommand(sqlString, cnn);

                SqlDataReader myReader = myCommand.ExecuteReader();
                bool firstTime = true;
                while (myReader.Read())
                {
                    if (!firstTime)
                    {
                        result += ", ";
                    }
                    firstTime = false;

                    result += "[";
                    result += (myReader["itemName"].ToString()) + ",";
                    result += (myReader["itemDescription"].ToString()) + ",";
                    result += (myReader["itemPicturePath"].ToString()) + ",";
                    result += (myReader["pricelinePrice"].ToString()) + "";
                    result += "]";
                }

                result += " ";

                cnn.Close();
                System.Diagnostics.Debug.WriteLine("Connection worked");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        public static string DBTest3()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-PPEIFCP;Initial Catalog=kaibaredux;Trusted_Connection=yes;";
            cnn = new SqlConnection(connetionString);
            try
            {
                string result = " ";
                cnn.Open();

                string sqlString = "SELECT * FROM t_item i, t_priceline p WHERE i.itemID = p.itemID";

                SqlCommand myCommand = new SqlCommand(sqlString, cnn);

                SqlDataReader myReader = myCommand.ExecuteReader();
                bool firstTime = true;
                while (myReader.Read())
                {
                    if (!firstTime)
                    {
                        result += ", ";
                    }
                    firstTime = false;

                    result += "\"item\": { ";
                    result += "\"name\" : \"" + (myReader["itemName"].ToString()) + "\",";
                    result += "\"description\" : \"" + (myReader["itemDescription"].ToString()) + "\",";
                    result += "\"picturePath\" : \"" + (myReader["itemPicturePath"].ToString()) + "\",";
                    result += "\"price\" : \"" + (myReader["pricelinePrice"].ToString()) + "";
                    result += "}";

                }

                result += " ";

                cnn.Close();
                System.Diagnostics.Debug.WriteLine("Connection worked");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ex.Message;
            }
        }
    }
}