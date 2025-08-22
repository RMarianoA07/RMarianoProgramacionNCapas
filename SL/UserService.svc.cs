namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        public Result Add(ML.Usuario usuario)
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

        public Result GetAll()
        {
            ML.Result resultGetAll = BL.Usuario.GetAllLinq();
            return new Result
            {
                Correct = resultGetAll.Correct,
                Ex = resultGetAll.Ex,
                Objects = resultGetAll.Objects,
                ErrorMessage = resultGetAll.ErrorMessage,
                Object = resultGetAll.Object
            };
        }

        public Result GetById(int IdUsuario)
        {
            ML.Result resultGetById = BL.Usuario.GetByIdLinq(IdUsuario);
            return new Result
            {
                Correct = resultGetById.Correct,
                Ex = resultGetById.Ex,
                Object = resultGetById.Object,
                Objects = resultGetById.Objects,
                ErrorMessage= resultGetById.ErrorMessage
            };
        }

        public Result Update(ML.Usuario usuario)
        {
            ML.Result resultUpdate = BL.Usuario.UpdateLinq(usuario);
            return new Result
            {
                Correct = resultUpdate.Correct,
                Ex = resultUpdate.Ex,
                Object = resultUpdate.Object,
                Objects = resultUpdate.Objects,
                ErrorMessage = resultUpdate.ErrorMessage
            };
        }
        
    }
}
