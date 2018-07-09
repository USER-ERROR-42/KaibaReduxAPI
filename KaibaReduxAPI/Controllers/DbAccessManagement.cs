using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using KaibaReduxAPI.Models;

namespace KaibaReduxAPI.Controllers
{
    public class DbAccessManagement
    // This class contains all the database access methods
    {
        // This is the connection string that points to the database. It is a constant so we add the readonly keyword
        // To properly run the connection string needs the server name of your SQL server instance.
        // Replace "DESKTOP-PPEIFCP" with your server's name to properly configure the connection 
        private static readonly string CONNECTION_STRING = "Data Source=DESKTOP-PPEIFCP;Initial Catalog=kaibaredux;Trusted_Connection=yes;";

        // This is the SQL connection object, which is used to execute operations on the DB
        private SqlConnection connection;



        public string[] getMenus()
        // returns a string array containing the menu names
        {
            // Declare the string array that we will eventually return
            // This ensures that it is available at the highest scope
            string[] results = null;

            // Declare a string list to hold the data we get from the DB
            // Note how you must declare the list's data type: <string>
            List<string> menuList = new List<string>();

            // Use a try catch here because it's very likely that the connection could fail and throw an error
            try
            {
                // open the connection
                OpenDb();

                // Define the SQL command statement
                // Web simply want to retrieve all the menus
                string commandString = "SELECT * FROM t_menu";

                // Create the SQL command object, give it the command string and the connection object
                SqlCommand command = new SqlCommand(commandString, connection);

                // Execute the command, since this is a select use SqlCommand.ExecuteReader()
                // It will return a SQLDataReader object, which we assign to the variable "dataReader"
                SqlDataReader dataReader = command.ExecuteReader();

                // A DataReader allows you to read one row at a time
                // You can then call SqlDataReader.Read(), which will allow you to access the next row
                // it returns true as long as there is another row to access
                // it will return false when there are no further rows to access

                // By placing the SqlDataReader.Read() call inside a while, we can keep reading the row data until there are no further rows
                while (dataReader.Read())
                {
                    // We only want the menu name, so we get that from each row
                    // The ToString() method ensures that we recieve a string 
                    String menuName = dataReader["menuName"].ToString();

                    // add that string to the list
                    menuList.Add(menuName);
                }

                // Now menuList contains a list of the menu names, but we need to return an array
                // use List.ToArray() to convert the current list into an array containing the same elements
                results = menuList.ToArray();
            }
            catch (Exception ex)
            // If there is an Exception (aka an error) then the catch block is executed
            {
                // Write the error to the console
                // The "DB-DEBUG:" is just there to make finding that message in the console easier
                System.Diagnostics.Debug.WriteLine("DB-DEBUG: " + ex.Message);

                // If there was an error we still need to return something
                // set the results variable equal to an error message
                results = new string[] { "Database Error" };
            }
            finally
            {
                // whether there was an error or not, we need to close the connection
                // that's what the finally block is for
                CloseDb();
            }

            // lastly return the result
            // it's good practice to always have only a single return statement at the end of the method
            return results;
        }

        public void GetSectionInMenu(string id)
        // takes a menuID and returns a menu object containing all the information about that menu
        {
            // TODO finish
            // have this call getItemsInSection(id) for each section
            // which then calls getPricelinesForItem(id) for each item
        }


        private void OpenDb()
        // Opens the database connection. This must be done before every db operation.
        {
            if (connection == null)
            {
                connection = new SqlConnection(CONNECTION_STRING);
                connection.Open();
            }
        }

        private void CloseDb()
        // Closes the database connection. This should be done after every database operation, whether it suceeded or not
        // This is a general good programming practice, as it frees up system resources (makes sure you're not opening a new connection every time, but not closing them)
        {
            // check if the connection is already null
            // If the connection was null and we tried to close it, we would get a NullPointerException
            if (connection != null)
            {
                // if it isn't null, then we need to close it
                connection.Close();

                // and set it to null
                connection = null;
            }
        }


        public static bool DBTest()
        //a function to test whether the connection can be opened and closed without an error
        {
            // Declare a connection object
            SqlConnection cnn;

            // Instantiate connection object
            // Give it the connection string constant
            cnn = new SqlConnection(CONNECTION_STRING);

            // Try opening and closing the connection
            try
            {
                cnn.Open();
                cnn.Close();
                // If connection opened and closed without errors, output a confirmation to the console
                System.Diagnostics.Debug.WriteLine("DB-DEBUG: Connection worked");
                return true;
            }
            catch (Exception ex)
            {
                // If there was an error, output it to the console
                System.Diagnostics.Debug.WriteLine("DB-DEBUG: " + ex.Message);
            }
            return false;
        }

        public static string DBTest2()
        // Try outputting information from the db to the page
        {
            SqlConnection cnn;
            cnn = new SqlConnection(CONNECTION_STRING);
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
            SqlConnection cnn;
            cnn = new SqlConnection(CONNECTION_STRING);
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
