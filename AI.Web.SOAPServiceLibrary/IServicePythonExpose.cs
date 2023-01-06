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
        #region Python/C# Executors
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
        #endregion

        #region Single Object Store Operations Write/Read
        [OperationContract]
        void CreateStringEntry(CompositeIdentifier ID, string data);

        [OperationContract]
        void CreateBytesEntry(CompositeIdentifier ID, byte[] data);

        [OperationContract]
        IList<string> RetrieveStringEntry(CompositeIdentifier ID);

        [OperationContract]
        IList<byte[]> RetrieveBytesEntry(CompositeIdentifier ID);
        #endregion

        #region multi-retrieve by where
        [OperationContract]
        IList<string> RetrieveStringEntryWhere(string key);

        [OperationContract]
        IList<byte[]> RetrieveBytesEntryWhere(string key);
        [OperationContract]
        IList<string> GetColumn(char delimiter, int columnIndex);


//        [OperationContract]
//        Dictionary<Identifier, string> GetObjectListWhereLambda
//    (Func<KeyValuePair<Identifier, string>, bool> predicate);
//        [OperationContract]
//        Dictionary<Identifier, byte[]> GetObjectBytesListWhereLambda
//(Func<KeyValuePair<Identifier, byte[]>, bool> predicate);
        #endregion

        #region Spark entries
        [OperationContract]
        void LoadIntoSpark(IList<string> data, IDictionary<string, DataType> fields);
        // TODO: Add your service operations here
        #endregion

        #region Python Shell 
        [OperationContract]
        string ExecuteinShell(string command);
        [OperationContract]
        void EndShell();
        #endregion

        #region PythonHelpers

        [OperationContract]
        string ArrayStringPythonFromList(IList<string> column, bool isInt);
        [OperationContract]
        string ConvertInMemoryObjectTableToPythonVectorArrayText(bool IsInt, char delimiter);

        #endregion

        #region Select From Data Stores and Auto Insert into inmemory store 
        [OperationContract]
        string ExecuteSqlSelectForInsertIntoInMemmoryObjectStoreFromDataStore
    (string selectStatement, string connectionstring, char delimiter, string generalTagKey);

        [OperationContract]
        string GetColumnsFrominMemoryObjectTempTableAndReplaceOriginalTable(int[] columns,char delimiter);
        [OperationContract]
        string CreateinMemoryObjectTempTableWithCsvData(IList<string> data);
        #endregion

        #region AggregateInmemoryObjectStoreModel with LinQ
        [OperationContract]
        IList<IList<double>> ParseExpressionAgainstInMemmoryModel
            (string expression, string tag);
        [OperationContract]
                    double SumColumnAgainstInMemmoryModel
                (string expression, string tag, int index);
        #endregion

        #region InMemoryTable Object Helpers
        [OperationContract]
        IList<string> ReturnTabularizedInMemoryObjectWhereTagsLike(string tags, char delimiter);
        [OperationContract]
        string CreateAndReturnInMemoryDoubleObjectTableOfTableFormedByGetByTagsCommand(string tags);
        #endregion
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
