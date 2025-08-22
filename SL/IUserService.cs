using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Result Add(ML.Usuario usuario);

        [OperationContract]
        Result Update(ML.Usuario usuario);

        [OperationContract]
        Result Delete(int IdUsuario);

        [ServiceKnownType(typeof(ML.Usuario))]
        [OperationContract]
        Result GetById(int IdUsuario);

        [ServiceKnownType(typeof(ML.Usuario))]
        [OperationContract]
        Result GetAll();
    }
}
