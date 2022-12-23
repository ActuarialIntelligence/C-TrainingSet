using AI.Web.SOAPServiceLibrary.DomainObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AI.Web.SOAPServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServicePythonExpose : IServicePythonExpose
    {
        public ObjectStorePatternDominObject objectStorePatternDominObject { get;  private set; }
        public ObjectByteStorePatternDominObject objectByteStorePatternDominObject { get; private set; }


        public void assignInitialObjects(ObjectStorePatternDominObject objectStorePatternDominObject,
            ObjectByteStorePatternDominObject objectByteStorePatternDominObject
            )
        {
            this.objectStorePatternDominObject = objectStorePatternDominObject;
            this.objectByteStorePatternDominObject = objectByteStorePatternDominObject;

        }
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

            return string.Format("Errors || Results: {0}", errors + " || " + results);
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

            return string.Format("Errors || Results: {0}", errors + " || " + results);
        }
        public string ExecutePython()
        {
            var pythonLocation = ConfigurationManager.AppSettings["pythonexecuterlocation"];
            var scriptPath = ConfigurationManager.AppSettings["scriptlocation"];
            var errors = "";
            var results = "";

            PythonRunner.RunPythonScript(pythonLocation, scriptPath, out errors, out results);

            return string.Format("Errors || Results: {0}", errors + " || " + results);
        }

        public void CreateStringEntry(CompositeIdentifier ID ,string data)
        {
            objectStorePatternDominObject.Insert(new Identifier(ID.key,ID.ID,ID.dateTime), data);
        }

        public void CreateBytesEntry(CompositeIdentifier ID, byte[] bytes)
        {
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
