using AI.Web.SOAPServiceLibrary.DomainObjects;
using System.Runtime.InteropServices;
using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Types;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System;
using System.Diagnostics;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace AI.Web.SOAPServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServicePythonExpose : IServicePythonExpose
    {
        private static ObjectStorePatternDominObject objectStorePatternDominObject;
        private static ObjectByteStorePatternDominObject objectByteStorePatternDominObject;
        private static ObjectStorePatternDoubleDominObject objectStorePatternDoubleDominObject;
        private static IList<string> inMemoryObjectTempTable { get; set; }
        public IList<string> GetColumn(char delimiter, int columnIndex)
        {
            var column = new List<string>();
            foreach(var row in objectStorePatternDominObject.rows)
            {
                column.Add(row.Value.Split(delimiter)[columnIndex]);
            }
            return column;
        }

        public IList<string> ReturnTabularizedInMemoryObjectWhereTagsLike(string tags, char delimiter)
        {
            var tagsListSpaceDelimited = tags.Split(' ');
            var tables = new List<IList<string>>();

            inMemoryObjectTempTable = new List<string>();
            AddTablesToTablesListWhereLikeTags(tagsListSpaceDelimited, tables);
            ConcatenateRowByRowEachTableAndAssignToInMemoryModel('|', tables);
            return inMemoryObjectTempTable;
        }

        public string CreateAndReturnInMemoryDoubleObjectTableOfTableFormedByGetByTagsCommand(string tags, char delimiter)
        {
            var result= objectStorePatternDoubleDominObject.CreateConCatenatedDoubleInmemoryTableAndReturnResults
                (ReturnTabularizedInMemoryObjectWhereTagsLike(tags,'|'));
            return result;
        }

        private static void ConcatenateRowByRowEachTableAndAssignToInMemoryModel(char delimiter, List<IList<string>> tables)
        {
            var tblCounter = 0;
            foreach (var tbl in tables)
            {
                for (int j = 0; j < tbl.Count; j++)
                {
                    if (tblCounter != 0)
                    {
                        tables[0][j] += delimiter.ToString() + tables[tblCounter][j];
                    }
                }
                tblCounter++;
            }
            inMemoryObjectTempTable = tables[0];
        }

        private static void AddTablesToTablesListWhereLikeTags(string[] tagsList, List<IList<string>> tables)
        {
            foreach (var tag in tagsList)
            {
                var table = objectStorePatternDominObject
                    .GetObjectListwhereKeyLike(tag).Select(s => s.Value).ToList();

                tables.Add(table);
            }
        }


        public string ConvertInMemoryObjectTableToPythonVectorArrayText(bool IsInt,char delimiter)
        {
            var arrayString = "";
            foreach (var value in inMemoryObjectTempTable)
            {
                arrayString += ArrayStringPythonFromList(value.Split(delimiter), IsInt) + ",";
            }           
            return arrayString;
        }
        public string ArrayStringPythonFromList(IList<string> column, bool isInt)
        {
            var len = column.Count();
            var cntr = 1;
            var arrayString = "[";
            foreach (var value in column)
            {
                if (!isInt)
                {
                    if (cntr < len)
                    {
                        arrayString += "\"" + value + "\",";
                    }
                    else { arrayString += "\"" + value + "\""; }

                    cntr++;
                }
                else
                {
                    if (cntr < len)
                    {
                        arrayString +=  value + ",";
                    }
                    else { arrayString += value; }

                    cntr++;
                }
            }



            arrayString += "]";
            return arrayString;
        }



        public void LoadIntoSpark(IList<string> data, IDictionary<string, DataType> fields)
        {
            var structFields = new List<StructField>();
            foreach (var kvp in fields)
            {
                structFields.Add(new StructField(kvp.Key, kvp.Value));
            }

            var spark = SparkSession.Builder().GetOrCreate();
            var newNamesDataFrame = spark.CreateDataFrame(
                new List<GenericRow> { new GenericRow(data.ToArray()) },
                    new StructType(structFields));

            newNamesDataFrame.Show();
        }
        public void assignInitialObjects(ObjectStorePatternDominObject objectStorePatternDominObjects,
            ObjectByteStorePatternDominObject objectByteStorePatternDominObjects
            )
        {
            objectStorePatternDominObject = objectStorePatternDominObjects;
            objectByteStorePatternDominObject = objectByteStorePatternDominObjects;

        }

        #region testScript
        internal string tempScript = "import numpy as np\nfrom matplotlib import pyplot as plt\nfrom sklearn.linear_model import LogisticRegression\nx = np.array([[0],[0.3],[0.6],[0.8],[1]])\ny = np.array([0,0,1,1,1])\nmodel = LogisticRegression()\nmodel.fit(x,y)\nprint(\"Intercept\",model.intercept_)\npred = model.predict_proba(x)[:,1]\nprint(\"Prediction\",pred)\nprint(\"Pred1\",pred[0])";
        #endregion
        public string ExecutePythonScript(string adhocscript)
        {
            var pythonLocation = ConfigurationManager.AppSettings["pythonexecuterlocation"];
            var sw = new StreamWriter(ConfigurationManager.AppSettings["scriptlocation"]);
            sw.Write(adhocscript);
            sw.Close();
            var scriptPath = ConfigurationManager.AppSettings["scriptlocation"];
            var errors = "";
            var results = "";

            PythonRunner.RunPythonScript(pythonLocation, scriptPath, out errors, out results);

            return string.Format("Errors {0} | Results: {1}", errors, results);
        }

        public string ExecuteCSharpScript(string script, string[] librariesToInclude)
        {
            var results = "";

            results = CsharpRunner.compile(script, librariesToInclude);

            return results;
        }
        public string ExecutePythonScriptByLocation(string adhocscript, string location)
        {
            var pythonLocation = ConfigurationManager.AppSettings["pythonexecuterlocation"];
            var sw = new StreamWriter(location);
            sw.Write(adhocscript);
            sw.Close();
            var scriptPath = location;
            var errors = "";
            var results = "";

            PythonRunner.RunPythonScript(pythonLocation, scriptPath, out errors, out results);

            return string.Format("Errors {0} | Results: {1}", errors , results);
        }

        public string ExecutePythonScriptByLocationWithArguments(string adhocscript, string location, string arguments)
        {
            var pythonLocation = ConfigurationManager.AppSettings["pythonexecuterlocation"];
            var sw = new StreamWriter(location);
            sw.Write(adhocscript);
            sw.Close();
            var scriptPath = location;
            var errors = "";
            var results = "";

            PythonRunner.RunPythonScriptWithArguments(pythonLocation, scriptPath, arguments, out errors, out results);

            return string.Format("Errors {0} | Results: {1}", errors, results);
        }
        public string ExecutePython()
        {
            var pythonLocation = ConfigurationManager.AppSettings["pythonexecuterlocation"];
            var scriptPath = ConfigurationManager.AppSettings["scriptlocation"];
            var errors = "";
            var results = "";

            PythonRunner.RunPythonScript(pythonLocation, scriptPath, out errors, out results);

            return string.Format("Errors {0} | Results: {1}", errors, results);
        }


//        public Dictionary<Identifier, string> GetObjectListWhereLambda
//(Func<KeyValuePair<Identifier, string>, bool> predicate)
//        {
//            var result = objectStorePatternDominObject.GetObjectListWhereLambda(predicate).ToDictionary(v => v.Key, u => u.Value);
//            return result;
//        }

//        public Dictionary<Identifier, byte[]> GetObjectBytesListWhereLambda
//(Func<KeyValuePair<Identifier, byte[]>, bool> predicate)
//        {
//            var result = objectByteStorePatternDominObject.GetObjectBytesListWhereLambda(predicate).ToDictionary(v => v.Key, u => u.Value);
//            return result;
//        }


        public IList<IList<double>> ParseExpressionAgainstInMemmoryModel
            (string expression,string tag)
        {
            if (expression != "NULL" && expression != null)
            {
                var objectStoreDbl = new ObjectStorePatternDoubleDominObject(objectStorePatternDominObject.GetObjectListwhereKeyIs(tag));
                return CsharpRunner.ParseExpressionAgainstInMemmoryModel(objectStoreDbl.rosDbl, expression);
            }
            else
            {
                return new List<IList<double>>();
            }
        }

        public double SumColumnAgainstInMemmoryModel
    (string expression, string tag, int index)
        {
            var objectStoreDbl = new ObjectStorePatternDoubleDominObject(objectStorePatternDominObject.GetObjectListwhereKeyIs(tag));
            var aggregate = 0d;
            foreach(var row in objectStoreDbl.rosDbl)
            {
                aggregate += row[index];
            }

            return aggregate;
        }

        public void CreateStringEntry(CompositeIdentifier ID ,string data)
        {
            objectStorePatternDominObject.Insert(new Identifier(ID.key,ID.ID,ID.dateTime), data);
        }

        public unsafe void CreateBytesEntry(CompositeIdentifier ID, byte[] bytes)
        {
            //foreach(byte t in bytes) { byte* p = &t; }\\ Future Reference
            objectByteStorePatternDominObject.Insert(new Identifier(ID.key, ID.ID, ID.dateTime), bytes);
        }

        public IList<string> RetrieveStringEntry(CompositeIdentifier ID)
        {
            return objectStorePatternDominObject.Getwhere(new Identifier(ID.key, ID.ID, ID.dateTime));
        }

        public IList<byte[]> RetrieveBytesEntry(CompositeIdentifier ID)
        {
            return objectByteStorePatternDominObject.Getwhere(new Identifier(ID.key, ID.ID, ID.dateTime));
        }

        private Process prc;
        public string ExecuteinShell(string command)
        {
            prc = new Process();
            prc.StartInfo.FileName = "cmd.exe";
            prc.StartInfo.RedirectStandardInput = true;
            prc.StartInfo.RedirectStandardOutput = true;
            prc.StartInfo.CreateNoWindow = true;
            prc.StartInfo.UseShellExecute = false;
            prc.StartInfo.Arguments = "/c python";
            prc.Start();
            prc.StandardInput.WriteLine(command);
            prc.StandardInput.Flush();
            prc.StandardInput.Close();
            prc.WaitForExit();
            string output = "";
            using (StreamReader reader = prc.StandardOutput)
            {
                output = reader.ReadToEnd();
            }
            return output;
        }
        public void EndShell()
        {
            prc.Close();
            prc.Dispose();
        }

        public string ExecuteSelectForInsertIntoInMemmoryObjectStoreFromDataStore
            (string selectStatement, string connectionstring, char delimiter, string generalTagKey)
        {
            try
            {
                DbDataReader dr;
                string rows = "";
                using (OdbcConnection conn =
                 new OdbcConnection(connectionstring))//Example: "DSN=Hive;UID=user-name;PWD=password"
                {
                    conn.OpenAsync().Wait();
                    OdbcCommand cmd = conn.CreateCommand();
                    cmd.CommandText = selectStatement;//Example: "SELECT obs_date, avg(temp) FROM weather GROUP BY obs_date;"
                    dr = cmd.ExecuteReader();
                    var columnCount = dr.FieldCount;

                    rows = InsertSelectedRowsIntoObjectStoreList(delimiter, rows, dr, columnCount, generalTagKey);
                    conn.Close();
                }
                return rows;
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            
        }

        public string ExecuteSqlSelectForInsertIntoInMemmoryObjectStoreFromDataStore
    (string selectStatement, string connectionstring, char delimiter, string generalTagKey)
        {
            try
            {
                SqlDataReader dr;
                string rows = "";
                using (SqlConnection conn =
                 new SqlConnection(connectionstring))//Example: "DSN=Hive;UID=user-name;PWD=password"
                {
                    conn.OpenAsync().Wait();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = selectStatement;//Example: "SELECT obs_date, avg(temp) FROM weather GROUP BY obs_date;"
                    dr = cmd.ExecuteReader();
                    var columnCount = dr.FieldCount;
                    if (objectStorePatternDominObject == null)
                    {
                        objectStorePatternDominObject = new ObjectStorePatternDominObject();
                    }
                    rows = InsertSelectedRowsIntoObjectStoreList(delimiter, rows, dr, columnCount, generalTagKey);
                    conn.Close();
                }
                return rows;
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }



        private string InsertSelectedRowsIntoObjectStoreList(char delimiter,string rows, DbDataReader dr, int columnCount, string generalTagKey)
        {
            while (dr.Read())
            {
                var row = "";
                for (int i = 0; i < columnCount; i++)
                {
                    if (i == columnCount - 1)
                    {
                        row += dr[i];
                        
                    }
                    else
                    {
                        row += dr[i] + delimiter.ToString();
                    }    
                }
                objectStorePatternDominObject.Insert(new Identifier(generalTagKey, new Guid(), DateTime.Now), row);
                rows += row + "\n";
            }
            return rows;
        }
        public IList<string> RetrieveStringEntryWhere(string key)
        {
            return objectStorePatternDominObject.GetwhereKeyIs(key);
        }

        public IList<byte[]> RetrieveBytesEntryWhere(string key)
        {
            return objectByteStorePatternDominObject.GetwhereKeyIs(key);
        }

        public string ClearStringBasedDataStore()
        {
            objectStorePatternDominObject = new ObjectStorePatternDominObject();
            return "success";
        }
        public string ClearByteBasedDataStore()
        {
            objectByteStorePatternDominObject = new ObjectByteStorePatternDominObject();
            return "success";
        }
    }
}
