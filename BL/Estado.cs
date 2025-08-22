using DL_EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result EstadoGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var query = context.EstadoGetAll().ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var estadoDB in query)
                        {
                            ML.Estado estado = new ML.Estado
                            {
                                IdEstado = estadoDB.IdEstado,
                                Nombre = estadoDB.Nombre
                            };
                            result.Objects.Add(estado);
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
                result.Ex = ex;
            }
            return result;
        }

    }
}
