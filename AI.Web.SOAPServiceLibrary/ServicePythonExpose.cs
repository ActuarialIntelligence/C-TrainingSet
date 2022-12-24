using AI.Web.SOAPServiceLibrary.DomainObjects;
using System.Runtime.InteropServices;
using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Types;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System;

namespace AI.Web.SOAPServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServicePythonExpose : IServicePythonExpose
    {
        internal ObjectStorePatternDominObject objectStorePatternDominObject 
        { 
            get 
            {
                return objectStorePatternDominObject == null ? new ObjectStorePatternDominObject() : objectStorePatternDominObject;
            }  
            private set 
            { 
                this.objectStorePatternDominObject = objectStorePatternDominObject; 
            } 
        }
        internal ObjectByteStorePatternDominObject objectByteStorePatternDominObject 
        {
            get
            {
                return objectByteStorePatternDominObject == null ? new ObjectByteStorePatternDominObject() : objectByteStorePatternDominObject;
            }
            private set
            {
                this.objectByteStorePatternDominObject = objectByteStorePatternDominObject;
            }
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
        public void assignInitialObjects(ObjectStorePatternDominObject objectStorePatternDominObject,
            ObjectByteStorePatternDominObject objectByteStorePatternDominObject
            )
        {
            this.objectStorePatternDominObject = objectStorePatternDominObject;
            this.objectByteStorePatternDominObject = objectByteStorePatternDominObject;

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


    }
}
