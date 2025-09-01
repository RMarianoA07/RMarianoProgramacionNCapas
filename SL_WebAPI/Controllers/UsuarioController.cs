using System;
using System.Net;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        // GET api/values
        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                ML.Result resultGetAll = BL.Usuario.GetAllLinq();
                if (resultGetAll.Correct)
                {
                    return Content(HttpStatusCode.OK, resultGetAll);
                }
                else
                {
                    if (!string.IsNullOrEmpty(resultGetAll.ErrorMessage))
                    {
                        return Content(HttpStatusCode.InternalServerError, resultGetAll.ErrorMessage);
                    }
                    return Content(HttpStatusCode.InternalServerError, resultGetAll.Ex?.Message);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/values/5
        [HttpGet]
        public IHttpActionResult GetById(int IdUsuario)
        {
            if (IdUsuario > 0)
            {
                ML.Result resultGetById = BL.Usuario.GetByIdLinq(IdUsuario);
                if (resultGetById.Correct)
                {
                    return Content(HttpStatusCode.OK, resultGetById);
                }
                return Content(HttpStatusCode.ExpectationFailed, resultGetById.ErrorMessage);
            }
            return Content(HttpStatusCode.BadRequest, "Es necesario el identificador del usuario");
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult Add([FromBody] ML.Usuario usuario)
        {
            try
            {
                ML.Result resultAdd = BL.Usuario.AddLinq(usuario);
                if (resultAdd.Correct)
                {
                    return Content(HttpStatusCode.OK, resultAdd);
                }
                return Content(HttpStatusCode.ExpectationFailed, resultAdd.ErrorMessage);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IHttpActionResult Update([FromBody] ML.Usuario usuario)
        {
            try
            {
                ML.Result resultUpdate = BL.Usuario.UpdateLinq(usuario);
                if (resultUpdate.Correct)
                {
                    return Content(HttpStatusCode.OK, resultUpdate);
                }
                return Content(HttpStatusCode.ExpectationFailed, resultUpdate.ErrorMessage);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public IHttpActionResult Delete(int IdUsuario)
        {
            try
            {
                ML.Result resultDelete = BL.Usuario.DeleteLinq(IdUsuario);
                if (resultDelete.Correct)
                {
                    return Content(HttpStatusCode.OK, resultDelete);
                }
                return Content(HttpStatusCode.ExpectationFailed, resultDelete.ErrorMessage);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
