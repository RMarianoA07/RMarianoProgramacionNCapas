using ML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAllREST();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Sin resultados";
            }


            return View(usuario);
        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result resultImages = BL.ImagenUsuario.DeleteByUser(IdUsuario);
            if (resultImages.Correct)
            {
                ML.Result resultDireccion = BL.Direccion.DeleteByIdUsuario(IdUsuario);
                if (resultDireccion.Correct)
                {
                    ML.Result resultUsuario = BL.Usuario.DeleteLinq(IdUsuario);
                    if (resultUsuario.Correct)
                    {
                        TempData["Mensaje"] = "¡El usuario fue eliminado correctamente!";
                    }
                }
            }

            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario, bool? IsDeleteImages)
         {
            if (IsDeleteImages ?? false)
            {
                Session.Remove("ListaImagenes");
                Session.Remove("ModelUsuario");
            }
            ML.Usuario usuario = new ML.Usuario();
            if (Session["ModelUsuario"] != null)
            {
                usuario = (ML.Usuario)Session["ModelUsuario"];
            }
            else
            {
                usuario.Rol = new ML.Rol();
                usuario.ImagenUsuario = new ML.ImagenUsuario();
                usuario.Direccion = new ML.Direccion
                {
                    Colonia = new ML.Colonia
                    {
                        Colonias = new List<object>(),
                        Municipio = new ML.Municipio
                        {
                            Municipios = new List<object>(),
                            Estado = new ML.Estado
                            {
                                Estados = new List<object>()
                            }
                        }
                    }
                };
            }
            if (IdUsuario > 0)
            {
                if (Session["ModelUsuario"] != null)
                {
                    ML.Usuario resultImages = LoadImages(IdUsuario.Value);
                    usuario.ImagenUsuario = resultImages.ImagenUsuario;
                }
                else
                {
                    ML.Result result = BL.Usuario.GetByIdREST(IdUsuario.Value);
                    if (result.Correct)
                    {
                        usuario = (ML.Usuario)result.Object;
                        ML.Usuario resultImages = LoadImages(IdUsuario.Value);
                        usuario.ImagenUsuario = resultImages.ImagenUsuario;
                    }
                }
            }
            else
            {
                if (Session["ListaImagenes"] != null)
                {
                    List<object> listImagenes = Session["ListaImagenes"] as List<object>;
                    usuario.ImagenUsuario.ImagenesUsuarios = listImagenes;
                }
            }
            //Agregar Informacion de Roles, Estados, Municipios, Colonias en DropDownList
            usuario = AddInfoDropdownList(usuario);

            return View(usuario);
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario, string Action)
        {
            if (Action == "AgregarUsuario")
            {
                if (usuario.IdUsuario == 0)
                {
                    ML.Result result = BL.Usuario.AddLinq(usuario);
                    if (result.Correct)
                    {
                        usuario.ImagenUsuario = new ML.ImagenUsuario();
                        int IdUsuario = (int)result.Object;
                        usuario.IdUsuario = IdUsuario;
                        if (Session["ListaImagenes"] != null)
                        {
                            usuario.ImagenUsuario.ImagenesUsuarios = Session["ListaImagenes"] as List<object>;
                            if (usuario.ImagenUsuario.ImagenesUsuarios.Count > 0)
                            {
                                foreach (ML.ImagenUsuario imagen in usuario.ImagenUsuario.ImagenesUsuarios)
                                {
                                    BL.ImagenUsuario.Add(IdUsuario, imagen);
                                }
                                Session.Remove("ListaImagenes");
                            }
                        }
                        BL.Direccion.Add(usuario);
                        TempData["Mensaje"] = "¡Usuario agregado correctamente!";
                    }
                }
                else
                {
                    BL.Usuario.UpdateLinq(usuario);
                    if (usuario.Direccion.IdDireccion > 0)
                    {
                        BL.Direccion.Update(usuario);
                    }
                    else
                    {
                        BL.Direccion.Add(usuario);
                    }
                    if (Session["ListaImagenes"] != null)
                    {
                        foreach (ML.ImagenUsuario imagenUsuario in Session["ListaImagenes"] as List<object>)
                        {
                            BL.ImagenUsuario.Add(IdUsuario: usuario.IdUsuario, imagenUsuario: imagenUsuario);
                        }
                        Session.Remove("ListaImagenes");
                    }
                    TempData["Mensaje"] = "¡Usuario actualizado correctamente!";
                }
            } else
            {
                usuario.ImagenUsuario.ImagenesUsuarios = new List<object>();

                HttpPostedFileBase imagen = Request.Files["imgUsuarioInput"];

                #region ConvertStreamToByte
                if (imagen.ContentLength > 0)
                {
                    MemoryStream target = new MemoryStream();
                    imagen.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();
                    usuario.ImagenUsuario.IdImagenUsuarioT = Guid.NewGuid().ToString();
                    usuario.ImagenUsuario.Imagen = data;
                    usuario.ImagenUsuario.Nombre = imagen.FileName;
                }
                #endregion

                if (Session["ListaImagenes"] != null)
                {
                    usuario.ImagenUsuario.ImagenesUsuarios = Session["ListaImagenes"] as List<object>;
                }
                usuario.ImagenUsuario.ImagenesUsuarios.Add(usuario.ImagenUsuario);
                Session["ListaImagenes"] = usuario.ImagenUsuario.ImagenesUsuarios;
                Session["ModelUsuario"] = usuario;
                return RedirectToAction("Form", new { usuario.IdUsuario });
            }

            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public ActionResult DeleteImage(int IdUsuario, string IdImagenUsuarioT, int IdImagenUsuario)
        {
            ML.ImagenUsuario imagenUsuario = new ML.ImagenUsuario();

            if (IdUsuario > 0 && IdImagenUsuario > 0)
            {
                BL.ImagenUsuario.Delete(IdImagenUsuario);
            }
            else
            {
                imagenUsuario.ImagenesUsuarios = new List<object>();
                imagenUsuario.ImagenesUsuarios = Session["ListaImagenes"] as List<object>;

                foreach (ML.ImagenUsuario imagen in imagenUsuario.ImagenesUsuarios)
                {
                    if (imagen.IdImagenUsuarioT == IdImagenUsuarioT)
                    {
                        imagenUsuario.ImagenesUsuarios.Remove(imagen);
                        break;
                    }
                }
            }
            return RedirectToAction("Form", new { IdUsuario });
        }
        [HttpPost]
        public ActionResult AddImage(ML.Usuario usuario)
        {
            usuario.ImagenUsuario.ImagenesUsuarios = new List<object>();

            HttpPostedFileBase imagen = Request.Files["imgUsuarioInput"];

            #region ConvertStreamToByte
            if (imagen.ContentLength > 0)
            {
                MemoryStream target = new MemoryStream();
                imagen.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                usuario.ImagenUsuario.IdImagenUsuarioT = Guid.NewGuid().ToString();
                usuario.ImagenUsuario.Imagen = data;
                usuario.ImagenUsuario.Nombre = imagen.FileName;
            }
            #endregion

            if (Session["ListaImagenes"] != null)
            {
                usuario.ImagenUsuario.ImagenesUsuarios = Session["ListaImagenes"] as List<object>;
            }
            usuario.ImagenUsuario.ImagenesUsuarios.Add(usuario.ImagenUsuario);
            Session["ListaImagenes"] = usuario.ImagenUsuario.ImagenesUsuarios;
            return RedirectToAction("Form", new { usuario.IdUsuario, usuario1 = usuario });
        }
        [HttpGet]
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(IdEstado);

            return Json(resultMunicipio, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(IdMunicipio);

            return Json(resultColonia, JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        public static ML.Usuario AddInfoDropdownList(ML.Usuario usuario)
        {
            usuario.Rol.Roles = new List<object>();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = new List<object>();
            usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
            usuario.Direccion.Colonia.Colonias = new List<object>();

            ML.Result resultRoles = BL.Rol.GetAll();
            if (resultRoles.Correct)
            {
                usuario.Rol.Roles = resultRoles.Objects;
            }
            ML.Result resultEstados = BL.Estado.EstadoGetAll();
            if (resultEstados.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
            }
            ML.Result resultMunicipios = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            if (resultMunicipios.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
            }
            ML.Result resultColonias = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            if (resultColonias.Correct)
            {
                usuario.Direccion.Colonia.Colonias = resultColonias.Objects;
            }
            return usuario;
        }
        public ML.Usuario LoadImages(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.ImagenUsuario = new ML.ImagenUsuario
            {
                ImagenesUsuarios = new List<object>()
            };

            List<object> listaImagenes = new List<object>();

            ML.Result resultImages = BL.ImagenUsuario.GetByUser(IdUsuario);
            if (resultImages.Correct)
            {
                foreach (ML.ImagenUsuario imagenUsuario in resultImages.Objects)
                {
                    listaImagenes.Add(imagenUsuario);
                }
            }

            if (Session["ListaImagenes"] != null)
            {
                foreach (ML.ImagenUsuario imagenUsuario in Session["ListaImagenes"] as List<object>)
                {
                    listaImagenes.Add(imagenUsuario);
                }
            }
            usuario.ImagenUsuario.ImagenesUsuarios = listaImagenes;

            return usuario;
        }
    }
}