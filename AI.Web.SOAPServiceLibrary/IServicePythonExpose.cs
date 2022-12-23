using AI.Web.SOAPServiceLibrary.DomainObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;


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
        string ExecutePythonScript(string script);

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

        // TODO: Add your service operations here
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
