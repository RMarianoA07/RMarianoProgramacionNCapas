using DL_EF;
using System;
using System.Linq;

namespace BL
{
    public class Direccion {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try 
            {
                using(RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    int RowAffected = context.DireccionAdd(
                        calle: usuario.Direccion.Calle,
                        numeroExterior: usuario.Direccion.NumeroExterior,
                        numeroInterior: usuario.Direccion.NumeroInterior,
                        idColonia: usuario.Direccion.Colonia.IdColonia,
                        idUsuario: usuario.IdUsuario
                    );
                    if ( RowAffected > 0 )
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Ocurrio un error al agregar la direccion";
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex) {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    int RowAffected = context.DireccionUpdate(
                        idDireccion: usuario.Direccion.IdDireccion,
                        calle: usuario.Direccion.Calle,
                        numeroExterior: usuario.Direccion.NumeroExterior,
                        numeroInterior: usuario.Direccion.NumeroInterior,
                        idColonia: usuario.Direccion.Colonia.IdColonia,
                        idUsuario: usuario.IdUsuario
                    );
                    if (RowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Ocurrio un error al agregar la direccion";
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
        public static ML.Result GetByIdUsuario(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var direccionDB = context.DireccionGetByIdUsuario(idUsuario: IdUsuario).FirstOrDefault();
                    if (direccionDB != null)
                    {
                        ML.Direccion direccion = new ML.Direccion
                        {
                            IdDireccion = direccionDB.IdDireccion,
                            Calle = direccionDB.Calle,
                            NumeroExterior = direccionDB.NumeroExterior,
                            NumeroInterior = direccionDB.NumeroInterior,
                            Colonia = new ML.Colonia()
                            {
                                IdColonia = direccionDB.IdColonia.Value,
                                Nombre = direccionDB.NombreColonia,
                                Municipio = new ML.Municipio
                                {
                                    IdMunicipio = direccionDB.IdMunicipio,
                                    Nombre = direccionDB.NombreMunicipio,
                                    Estado = new ML.Estado()
                                    {
                                        IdEstado = direccionDB.IdEstado,
                                        Nombre = direccionDB.NombreEstado
                                    }
                                }
                            }
                        };
                        result.Object =direccion;
                        result.Correct = true;
                    }
                    
                    else
                    {
                        result.ErrorMessage = "Ocurrio un error al obtener la direccion";
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
        public static ML.Result DeleteByIdUsuario(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var direcciones = (from direccion in context.Direccions
                                 where direccion.IdUsuario == IdUsuario
                                 select direccion).ToList();
                    if(direcciones.Count > 0)
                    {
                        context.Direccions.RemoveRange(direcciones);
                        context.SaveChanges();
                    }
                    result.Correct = true;
                }

            }catch(Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                result.Correct=false;
            }
            return result;
        }
    }
}
