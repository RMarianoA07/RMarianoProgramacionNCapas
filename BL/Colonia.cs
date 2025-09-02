using DL_EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class Colonia
    {
        public static ML.Result ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var query = context.ColoniaGetByIdMunicipio(idMunicipio: IdMunicipio).ToList();
                    if (query.Count() > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var coloniaDB in query)
                        {
                            ML.Colonia colonia = new ML.Colonia()
                            {
                                IdColonia = coloniaDB.IdColonia,
                                Nombre = coloniaDB.Nombre,
                                IdMunicipio = coloniaDB.IdMunicipio.Value,
                                CodigoPostal = coloniaDB.CodigoPostal
                            };
                            result.Objects.Add(colonia);
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
