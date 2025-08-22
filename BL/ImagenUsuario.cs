using DL_EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class ImagenUsuario
    {
        public static ML.Result Add(int IdUsuario, ML.ImagenUsuario imagenUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    DL_EF.ImagenUsuario imagenUsuarioDB = new DL_EF.ImagenUsuario
                    {
                        IdUsuario = IdUsuario,
                        Imagen = imagenUsuario.Imagen,
                        Nombre = imagenUsuario.Nombre,
                        Descripcion = imagenUsuario.Descripcion
                    };
                    context.ImagenUsuarios.Add(imagenUsuarioDB);
                    int NoFilas = context.SaveChanges();
                    if (NoFilas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se agrego la imagen :(";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetByUser(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try 
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var query = (from imagen in context.ImagenUsuarios
                                where imagen.IdUsuario == IdUsuario
                                select imagen).ToList();
                    if (query.Count() > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var imageDB in query) {
                            ML.ImagenUsuario imagenUsuario = new ML.ImagenUsuario
                            {
                                IdUsuario = imageDB.IdUsuario.Value,
                                IdImagenUsuario = imageDB.IdImagenUsuario,
                                Imagen = imageDB.Imagen,
                                Descripcion = imageDB.Descripcion,
                                Nombre = imageDB.Nombre
                            };
                            result.Objects.Add(imagenUsuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Sin resultados";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdImagenUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var query = (from u in context.ImagenUsuarios
                                 where u.IdImagenUsuario == IdImagenUsuario
                                 select u).First();
                    context.ImagenUsuarios.Remove(query);
                    int NoFilas = context.SaveChanges();
                    result.Correct = NoFilas > 0;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result DeleteByUser(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var imagenesUsuario = (from imagenU in context.ImagenUsuarios
                                           where imagenU.IdUsuario == IdUsuario
                                           select imagenU).ToList();
                    if (imagenesUsuario.Count() > 0)
                    {
                        context.ImagenUsuarios.RemoveRange(imagenesUsuario);
                        context.SaveChanges();
                    }
                    result.Correct= true;
                }
            }
            catch(Exception ex)
            {
                result.Ex =  ex;
                result.Correct = false;
            }
            return result;
        }
    }
}
