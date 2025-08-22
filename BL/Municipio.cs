using DL_EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class Municipio
    {
        public static ML.Result MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var query = context.MunicipioGetByIdEstado(idEstado: IdEstado).ToList();
                    if (query.Count() > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var municipioDB in query)
                        {
                            ML.Municipio municipio = new ML.Municipio()
                            {
                                IdMunicipio = municipioDB.IdMunicipio,
                                Nombre = municipioDB.Nombre,
                                IdEstado = municipioDB.IdEstado.Value
                            };
                            result.Objects.Add(municipio);
                        }
                        result.Correct = true;
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

    }
}
