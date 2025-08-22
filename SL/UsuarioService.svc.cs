using ML;
using System;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class UsuarioService : IUsuarioService
    {
        public Result Add(Usuario usuario)
        {
            ML.Result resultAdd = BL.Usuario.AddLinq(usuario);

            return new Result
            {
                Correct = resultAdd.Correct,
                Ex = resultAdd.Ex,
                Object = resultAdd.Object,
                Objects = resultAdd.Objects,
                ErrorMessage = resultAdd.ErrorMessage
            };
        }

        public Result Delete(int IdUsuario)
        {
            ML.Result resultDelete = BL.Usuario.DeleteLinq(IdUsuario);

            return new Result
            {
                Correct = resultDelete.Correct,
                Ex = resultDelete.Ex,
                Object = resultDelete.Object,
                Objects = resultDelete.Objects,
                ErrorMessage = resultDelete.ErrorMessage
            };
        }
   

        public Result Update(Usuario usuario)
        {
            ML.Result resultUpdate = BL.Usuario.UpdateLinq(usuario);
            return new Result {
                Correct = resultUpdate.Correct,
                Ex = resultUpdate.Ex,
                Object = resultUpdate.Object,
                Objects = resultUpdate.Objects,
                ErrorMessage = resultUpdate.ErrorMessage
            };
        }
    }
}
