using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
//using System.Data.SqlClient;   // System.Data.dll
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;


namespace _952_PRLoogleClassLibrary
{

    /*
    public class Person
    {
        [Category("Data")]
        [DisplayName("Given name")]
        public string FirstName { get; set; }

        [DisplayName("Family name")]
        public string LastName { get; set; }

        public int Age { get; set; }

        public double Height { get; set; }

        public Mass Weight { get; set; }

        public Genders Gender { get; set; }

        public Color HairColor { get; set; }

        [Description("Check the box if the person owns a bicycle.")]
        public bool OwnsBicycle { get; set; }
    }
    */


        public class DatabaseMethods
    {
        public static string ExpandCommandText(SQLiteCommand cmd)
        {
            string tmp = cmd.CommandText.ToString();
            
            foreach (SQLiteParameter p in cmd.Parameters)
            {
                tmp = tmp.Replace(p.ParameterName.ToString(), "'" + p.Value.ToString() + "'");
            }

            return tmp;
        }

        public static Int64 NonQueryAndTwoStrings(bool returnInt, bool boolDeveloperMode, string stringButtonOKComplete, string stringDatabasePath, SQLiteConnection connRLPrivateFlexible)
        {
            //((MainWindow)App.Current.MainWindow).connRLPrivateFlexible

            SQLiteConnection OleDbConnection_ButtonOK = DatabaseMethods.reconnectPrivateFlexible(false, stringDatabasePath, connRLPrivateFlexible);
            using (SQLiteCommand cmdInsert = ConnectionCommandType(OleDbConnection_ButtonOK))
            {
                cmdInsert.CommandText = stringButtonOKComplete;

                //DatabaseMethods.writeDebug(cmdInsert.CommandText, true);

                OleDbConnection_ButtonOK.Open();
                if (!boolDeveloperMode) cmdInsert.ExecuteNonQuery();


                if (boolDeveloperMode) cmdInsert.ExecuteNonQuery();
                //  DatabaseMethods.writeDebug(stringDatabasePath, true)
            }

            Int64 intNewScalar = 0;
            if (returnInt)
            {
                //SqlCommand cmdNewID = new SqlCommand("SELECT @@IDENTITY", OleDbConnection_ButtonOK);///SELECT SCOPE_IDENTITY()
                using ( SQLiteCommand cmdNewID = new SQLiteCommand("SELECT last_insert_rowid()", OleDbConnection_ButtonOK))
                {
                    intNewScalar = (Int64)cmdNewID.ExecuteScalar();
                }
            }
            ///
            ///
           

            OleDbConnection_ButtonOK.Close();
            //OleDbConnection_ButtonOK.Dispose();
            //GC.Collect();

            return intNewScalar;
        }

        public static SQLiteCommand ConnectionCommandType(SQLiteConnection OleDbConnection_ButtonOK)
        {
            SQLiteCommand cmdInsert = new SQLiteCommand();
            cmdInsert.Connection = OleDbConnection_ButtonOK;
            cmdInsert.CommandType = System.Data.CommandType.Text;

            return cmdInsert;
        }



        public static void writeDebug(string x, bool AndShow)
        {

            string path = (System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\` aa PRLGoogle Backups");
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

            //string subdirectory_reversedatetothesecond = (path + ("\\" + (DateTime.Now.ToString("yyyyMMddHHmmss"))));
            //if (!System.IO.Directory.Exists(subdirectory_reversedatetothesecond)) System.IO.Directory.CreateDirectory(subdirectory_reversedatetothesecond);

            string FILE_NAME = (path + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");

            System.IO.File.Create(FILE_NAME).Dispose();

            System.IO.StreamWriter objWriter = new System.IO.StreamWriter(FILE_NAME, true);
            objWriter.WriteLine(x);
            objWriter.Close();

            if(AndShow)System.Diagnostics.Process.Start(FILE_NAME);
        }


        public static SQLiteConnection reconnectPrivateFlexible(bool azure, string path000000, SQLiteConnection connRLPrivateFlexible) //new methods here need to be updated in 1 2 3 4 5 places and one by the directives
        {
            //   if (!System.IO.File.Exists(path000000)) MessageBox.Show("pathTemp does not exist in 'reconnect'");

          // MessageBox.Show("Catch");
            string myConnectionString = "";

            if (azure) myConnectionString = "Server=tcp:prloogle.database.windows.net,1433;Initial Catalog=Database of Databases;Persist Security Info=False;User ID={joshua.lumley};Password={F44ff44zzz666};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            //  if (!azure) myConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path000000;
            //if (!azure) myConnectionString = "Data Source=PR-CHC-SRV\\PRSQL;Database=PRLoogle;Trusted_Connection=True;User Id=FFF;Password = FFF;Integrated Security=false;Persist Security Info=False;";

            string myString_Tilde_OrWithout = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Pedersen Read\~PRearch❤️ - General\Data Base File\PRLoogle_Data.db";

            if (!File.Exists(myString_Tilde_OrWithout))
            {
                myString_Tilde_OrWithout = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Pedersen Read\PRearch❤️ - General\Data Base File\PRLoogle_Data.db";
            }



            if (!azure) myConnectionString = "Data Source=" + myString_Tilde_OrWithout + "; Version=3";

            //if (!azure) myConnectionString = "Data Source=114.23.106.71\\PRSQL;Database=PRLoogle;Trusted_Connection=True;Connection Timeout=10;";
            //if (!azure) myConnectionString = "Data Source=192.168.0.7\\PRSQL;Database=PRLoogle;Trusted_Connection=True;Connection Timeout=10;";
            //if (!azure) myConnectionString = "Data Source=DESKTOP-SFK611F\\SQLEXPRESS;Database=PRLoogle;Trusted_Connection=True;Connection Timeout=5;";
             //sometimes there is recovery pending, 


           // if (!azure) myConnectionString = "Data Source=JOSHUA\\SQLEXPRESS;Database=PRLoogle;Trusted_Connection=True;Connection Timeout=1;";
            //if (!azure) myConnectionString = "Server=tcp:prloogle.database.windows.net,1433;Initial Catalog=Proogle2;Persist Security Info=False;User ID=Joshua.Lumley;Password=F44ff44zzz333;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            ///it works, it works, it works, it works, it works using sql identiifcation, but how the syncing works is a quandray, if it changes anywhere it needs to sync
            ///the above one is the one we need to change when switching between home and work and how do we remember it is in database methods
            ///the above one is the one we need to change when switching between home and work and how do we remember it is in database methods
            ///if we're going to work on new columns everthing that is done needs to be done again

            //MessageBox.Show("At least we are running it...");
            // myConnectionString = "Server=tcp:prloogle.database.windows.net,1433;Initial Catalog=Proogle2;Persist Security Info=False;User ID={joshua.lumley};Password={F44ff44zzz333};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=5;";
            //myConnectionString = "Server=tcp:prloogle.database.windows.net,1433;Initial Catalog=Proogle2;Persist Security Info=False;User ID='Joshua.Lumley';Password='F44ff44zzz333';MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=5;";
            connRLPrivateFlexible = new SQLiteConnection(myConnectionString);
             
            /*try
            {
                connRLPrivateFlexible.Open();
                connRLPrivateFlexible.Close();
            }

            #region catch and finally
            catch (Exception ex)
            {
                MessageBox.Show("Catch", "Failed due to: " + ex.Message);
            }
            finally
            {
            }
            #endregion*/
           // connRLPrivateFlexible.Close();
            return connRLPrivateFlexible;
        }

        /*  public static List<string> GetAllQueriesFromDataBase(OleDbConnection connRLPrivateFlexible, string theTableName)
          {
             // DataTable tables = connRLPrivateFlexible.GetSchema("Tables");

              DataRow recSpecificTable = connRLPrivateFlexible.GetSchema("Tables").AsEnumerable()
                  .Where(r => r.Field<string>("TABLE_NAME")
                  .StartsWith(theTableName)).FirstOrDefault();

              var queries = new List<string>();
              using (connRLPrivateFlexible)
              {
                  var dt = connRLPrivateFlexible.GetSchema("BASE TABLE");
                  queries = dt.AsEnumerable().Select(dr => dr.Field<string>(theTableName)).ToList();
              }

              return queries;
          }*/

        public static bool TestTableExistsExecuteNonQueryAsync(bool boolDeveloperMode, bool boolImWoman, bool PerformRowCheck, string theTableName, string thePath, List<string> QueryToAction, SQLiteConnection connRLPrivateFlexible)
        {
            ////////////////            TestTableExistsExecuteNonQueryAsync CREATES IF DOESN'T EXIST
            ///
            ////////////////    WOMAN means new database
            ////////////////    the first trip boolImWoman means, making 

            ////////////////    A RETurn of false means no rows, it doesn't mean the DB isn't there {i believe it to be more complicated than this}
            ////////////////    {i am not sure if the "SELECT TOP 1 * FROM [" + theTableName + "]" part is necessary}

            //alright i will explain it again, the first check is for if the table is there, 
            //alright i will explain it again, the second check is for if any rows are there
            //alright i will explain it again, the second flag for altering the table requires no rows therefore it is false
            //alright i will explain it again, the last part about woman i am now not sure is ever used, this is flagged for deletion

            ////////////////    1 VIEWING ParenT, with 0 row trip, is the TableOfDatabases when we do listBoxOCSelectAndItemSource
            ////////////////    2 MAKING ParenT, false row trip, checks it there before writing a new woman.
            ////////////////    3 MAKING WOMAN, false row trip, is for Womans Name database, and creates a database it it doesn't exist, informs if it does

            /// 
           // MessageBox.Show(theTableName + " progress a");
            connRLPrivateFlexible = DatabaseMethods.reconnectPrivateFlexible(false, thePath, connRLPrivateFlexible);
            connRLPrivateFlexible.Open();
            using (SQLiteCommand cmd = new SQLiteCommand())
            {


                cmd.Connection = connRLPrivateFlexible; cmd.CommandType = System.Data.CommandType.Text;
                SQLiteDataReader reader = null;


                try
                {
                    cmd.CommandText = "SELECT * FROM [" + theTableName + "] WHERE 1 = 0";
                    reader = cmd.ExecuteReader();
                }
                catch //(Exception ex)
                {
                    // System.Windows.MessageBox.Show(ex.Message); 
                    try
                    {
                        //cmd.CommandText = QueryToAction[0];  //are we going to reconstitute this 
                        //cmd.ExecuteNonQueryAsync();

                        for (int i = 0; i <= QueryToAction.Count - 1; i++)
                        {
                            if (QueryToAction[i] != "")
                            {
                                try
                                {
                                    //MessageBox.Show("progress 1.1");
                                    cmd.CommandText = QueryToAction[i];
                                    if (!boolDeveloperMode) cmd.ExecuteNonQuery();
                                    if (boolDeveloperMode) cmd.ExecuteNonQuery();
                                    //MessageBox.Show("progress 1.2");
                                }
                                catch (Exception ex3) { MessageBox.Show(ex3.Message); }
                            }
                        }

                        connRLPrivateFlexible.Close();
                        return false;
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show(ex2.Message);  //already exists 
                        connRLPrivateFlexible.Close();
                        return false;
                    }
                }
                reader.Close();

                //SQLiteCommand cmd = new SQLiteCommand();
                //cmd.Connection = connRLPrivateFlexible; cmd2.CommandType = System.Data.CommandType.Text;
                //SQLiteDataReader reader2 = null;

                //connRLPrivateFlexible.Open();
                cmd.CommandText = "SELECT * FROM [" + theTableName + "] LIMIT 1";
                reader = cmd.ExecuteReader();

                // MessageBox.Show("progress bbbbbbbbbbbbbbbbbbbbbbb");
                //return false;
                //lets try and describe what happens, if it returns false it knows not to do it anymore and returns

                if (PerformRowCheck)
                {
                    if (!reader.HasRows)
                    {
                        // reader.Close();
                        connRLPrivateFlexible.Close();
                        return false;
                    }
                }


                // reader.Close();

                //delete this below or not
                //delete this below or not
                // MessageBox.Show("progress ccccccccccc");

                if (boolImWoman)
                {
                    if (QueryToAction[0].ToLower().Contains("alter"))
                    {
                        try
                        {
                            cmd.CommandText = QueryToAction[0];
                            if (!boolDeveloperMode) cmd.ExecuteNonQuery();
                            if (boolDeveloperMode) cmd.ExecuteNonQuery();
                            connRLPrivateFlexible.Close();
                            return false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            connRLPrivateFlexible.Close();
                            return false;
                        }
                    }
                }
            }

            connRLPrivateFlexible.Close();

            //one it is a woman, two it contains the word alter

            return true;
        }

        public static DataTable listBoxOCSelectAndItemSource(bool boolDeveloperMode, bool Decending, bool boolRevit, SQLiteConnection connRLPrivateFlexible, string labelDataBaseDirectoryPath, string stringDatabaseName, string stringTableName)  //this is the database list
        {

                //this has to be updated in two places, i don't believe both of them are required but i do not know which one is the good one
                //this has to be updated in two places, i don't believe both of them are required but i do not know which one is the good one
                //this has to be updated in two places, i don't believe both of them are required but i do not know which one is the good one
                string stringYourBasicTable = "Directory of Databases:CREATE TABLE [Table Of Databases]([ID] AUTOINCREMENT NOT NULL PRIMARY KEY,[DatabaseName] VARCHAR(255) WITH COMPRESSION,[DatabasePath] LONGTEXT WITH COMPRESSION,[DatabaseNoReasonInteger] LONG DEFAULT 0, [boolSelected] BIT, [boolRevit] BIT)";
                string stringYourSearchStringsTable = "Directory of Databases:CREATE TABLE[Search Strings]([ID] AUTOINCREMENT NOT NULL PRIMARY KEY,[SearchString] LONGTEXT WITH COMPRESSION,[TargetDatabaseID] LONG DEFAULT 0,[TargetDatabaseName] VARCHAR(255) WITH COMPRESSION,[TargetDatabasePath] LONGTEXT WITH COMPRESSION,[TargetSort] LONG DEFAULT 0)";


                string pathServerDirectory = labelDataBaseDirectoryPath;
                //string stringDirectoryOfDatabases = stringDirectoryOfDatabases;

                string pathDirectoryDBsss = _20181201_Loaded.pathFullDB(_20181201_Loaded.pathParent(pathServerDirectory, stringDatabaseName), stringDatabaseName);

                List<string> liststringTheNonQuery = new List<string>();
                liststringTheNonQuery.Add(stringYourBasicTable.Substring(stringYourBasicTable.IndexOf(':') + 1));
                liststringTheNonQuery.Add(stringYourSearchStringsTable.Substring(stringYourSearchStringsTable.IndexOf(':') + 1));

                DataTable myDataTableNotAGrid = new DataTable();

                if (DatabaseMethods.TestTableExistsExecuteNonQueryAsync(boolDeveloperMode, false, true, stringTableName, pathDirectoryDBsss, liststringTheNonQuery, connRLPrivateFlexible))
                {
                }
                else
                {
                    return myDataTableNotAGrid; //this returns null here because if it is just created then there definitely will not be any rows so no point in continuing
                }
            
            connRLPrivateFlexible = DatabaseMethods.reconnectPrivateFlexible(false, pathDirectoryDBsss, connRLPrivateFlexible);
                string mySQLQuery = "SELECT * FROM [" + stringTableName + "] ORDER BY ID " + (Decending ? "DESC" : "");
                if (boolRevit) mySQLQuery = "SELECT * FROM [" + stringTableName + "] WHERE [boolRevit] ORDER BY ID DESC";
            using(SQLiteDataAdapter da2 = new SQLiteDataAdapter(mySQLQuery, connRLPrivateFlexible))
            {
                da2.Fill(myDataTableNotAGrid);
            }
  

                return myDataTableNotAGrid;

        }
    }
}
