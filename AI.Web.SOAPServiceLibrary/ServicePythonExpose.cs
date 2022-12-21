using AI.Web.SOAPServiceLibrary.DomainObjects;
using System.Configuration;

namespace AI.Web.SOAPServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServicePythonExpose : IServicePythonExpose
    {
        private ObjectStorePatternDominObject objectStorePatternDominObject;
        private ObjectByteStorePatternDominObject objectByteStorePatternDominObject;

        public string ExecutePython(string script)
        {
            var pythonLocation = ConfigurationManager.AppSettings["pythonexecuterlocation"];
            var scriptPath = ConfigurationManager.AppSettings["scriptlocation"];
            var errors = "";
            var results = "";

            PythonRunner.RunPythonScript(pythonLocation, scriptPath, out errors, out results);

            return string.Format("Errors || Results: {0}", errors + " || " + results);
        }

        public void CreateStringEntry(Identifier ID ,string data)
        {
            objectStorePatternDominObject.Insert(ID, data);
        }

        public void CreateBytesEntry(Identifier ID, byte[] bytes)
        {
            objectByteStorePatternDominObject.Insert(ID, bytes);
        }


    }
}
