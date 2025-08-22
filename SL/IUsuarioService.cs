using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUsuarioService
    {

        [OperationContract]
        Result Add(ML.Usuario usuario);

        [OperationContract]
        Result Update(ML.Usuario usuario);

        [OperationContract]
        Result Delete(int IdUsuario);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Result
    {
        [DataMember]
        public bool Correct { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public object Object { get; set; }

        [DataMember]
        public List<object> Objects { get; set; }

        [DataMember]
        public Exception Ex { get; set; }

    }
}
