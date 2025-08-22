using DL_EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities()) { 
                    var listRoles = (from r in context.Rols
                                select r).ToList();
                    if (listRoles.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var rolBD in listRoles) 
                        { 
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = rolBD.IdRol;
                            rol.Nombre = rolBD.Nombre;
                            result.Objects.Add(rol);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "No se encontraron resultados";
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
