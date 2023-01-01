using AI.Web.SOAPServiceLibrary.DomainObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Types;


namespace AI.Web.SOAPServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServicePythonExpose
    {
        [OperationContract]
        void assignInitialObjects(ObjectStorePatternDominObject objectStorePatternDominObject,
            ObjectByteStorePatternDominObject objectByteStorePatternDominObject
            );

        [OperationContract]
        string ExecuteSelectForInsertIntoInMemmoryObjectStoreFromDataStore(string selectStatement, string connectionstring, char delimiter, string generalTagKey);

        [OperationContract]
        string ExecutePythonScript(string script);

        [OperationContract]
        string ExecuteCSharpScript(string script,string[] librariesToInclude);

        [OperationContract]
        string ExecutePythonScriptByLocation(string adhocscript, string location);

        [OperationContract]
        string ExecutePythonScriptByLocationWithArguments(string adhocscript, string location, string arguments);

        [OperationContract]
        string ExecutePython();

        [OperationContract]
        void CreateStringEntry(CompositeIdentifier ID, string data);

        [OperationContract]
        void CreateBytesEntry(CompositeIdentifier ID, byte[] data);

        [OperationContract]
        IList<string> RetrieveStringEntry(CompositeIdentifier ID);

        [OperationContract]
        IList<byte[]> RetrieveBytesEntry(CompositeIdentifier ID);
        [OperationContract]
        IList<string> RetrieveStringEntryWhere(string key);

        [OperationContract]
        IList<byte[]> RetrieveBytesEntryWhere(string key);

        [OperationContract]
        void LoadIntoSpark(IList<string> data, IDictionary<string, DataType> fields);
        // TODO: Add your service operations here

        [OperationContract]
        string ExecuteinShell(string command);
        [OperationContract]
        void EndShell();

        [OperationContract]
        string ArrayStringPythonFromList(IList<string> column, bool isInt);
        [OperationContract]
        IList<string> GetColumn(IList<string> allRows, char delimiter, int columnIndex);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "AI.Web.SOAPServiceLibrary.ContractType".
    [DataContract]
    public class CompositeIdentifier
    {

        [DataMember]
        public DateTime dateTime { get;  set; }
        [DataMember]
        public string key { get;  set; }
        [DataMember]
        public Guid ID { get;  set; }
    }
}
