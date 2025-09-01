using DL_EF;
using ML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        /*FUNCIONES DE CRUD USANDO SQL CLIENT (SQL CONNECTION. SQL COMMAND)*/
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.Get()))
                {

                    SqlCommand cmd = new SqlCommand("UsuarioAdd", context)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);

                    context.Open();
                    int NoFilas = cmd.ExecuteNonQuery();
                    if (NoFilas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public static ML.Result Update(ML.Usuario usuario)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.Get()))
                {

                    SqlCommand cmd = new SqlCommand("UsuarioUpdate", context)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);

                    context.Open();
                    int NoFilas = cmd.ExecuteNonQuery();
                    if (NoFilas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Sin registros afectados";
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
        public static ML.Result Delete(int IdUsuario) {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.Get()))
                {

                    SqlCommand cmd = new SqlCommand("UsuarioDelete", context)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    context.Open();
                    int NoFilas = cmd.ExecuteNonQuery();
                    if (NoFilas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Sin registros afectados";
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result
            {
                Objects = new List<object>()
            };
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.Get()))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("UsuarioGetAll", context);

                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        ML.Usuario usuario = new ML.Usuario
                        {
                            Rol = new ML.Rol(),
                            IdUsuario = Convert.ToInt32(row["IdUsuario"]),
                            UserName = row["UserName"].ToString(),
                            Nombre = row["Nombre"].ToString(),
                            ApellidoPaterno = row["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = row["ApellidoMaterno"].ToString(),
                            Email = row["Email"].ToString(),
                            Password = row["Password"].ToString(),
                            Sexo = row["Sexo"].ToString(),
                            Telefono = row["Telefono"].ToString(),
                            Celular = row["Celular"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                            CURP = row["CURP"].ToString()
                        };
                        usuario.Rol.IdRol = Convert.ToInt32(row["IdRol"]);
                        result.Objects.Add(usuario);
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result
            {
                Objects = new List<object>()
            };

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.Get()))
                using (SqlCommand cmd = new SqlCommand("UsuarioGetById", context))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        ML.Usuario usuario = new ML.Usuario
                        {
                            UserName = row["UserName"].ToString(),
                            Nombre = row["Nombre"].ToString(),
                            ApellidoPaterno = row["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = row["ApellidoMaterno"].ToString(),
                            Email = row["Email"].ToString(),
                            Password = row["Password"].ToString(),
                            Sexo = row["Sexo"].ToString(),
                            Telefono = row["Telefono"].ToString(),
                            Celular = row["Celular"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                            CURP = row["CURP"].ToString(),
                            Rol = new ML.Rol()
                            {
                                IdRol = Convert.ToInt32(row["IdRol"])
                            }
                        };

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario no encontrado.";
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
        /*FUNCIONES DE CRUD USANDO ENTITY FRAMEWORK(EF)*/
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    int NoFilas = context.UsuarioAdd(
                        userName: usuario.UserName,
                        nombre: usuario.Nombre,
                        apellidoPaterno: usuario.ApellidoPaterno,
                        apellidoMaterno: usuario.ApellidoMaterno,
                        email: usuario.Email,
                        password: usuario.Password,
                        sexo: usuario.Sexo,
                        telefono: usuario.Telefono,
                        celular: usuario.Celular,
                        fechaNacimiento: usuario.FechaNacimiento.ToString(),
                        cURP: usuario.CURP,
                        idRol: usuario.Rol.IdRol
                     );
                    if (NoFilas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var ListUsers = context.UsuarioGetAll("", "", "", "").ToList();

                    if (ListUsers.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var UsuarioDB in ListUsers)
                        {
                            ML.Usuario usuario = new ML.Usuario
                            {
                                IdUsuario = UsuarioDB.IdUsuario,
                                UserName = UsuarioDB.UserName,
                                Nombre = UsuarioDB.NombreUsuario,
                                ApellidoPaterno = UsuarioDB.ApellidoPaterno,
                                ApellidoMaterno = UsuarioDB.ApellidoMaterno,
                                Email = UsuarioDB.Email,
                                Password = UsuarioDB.Password,
                                Sexo = UsuarioDB.Sexo,
                                Telefono = UsuarioDB.Telefono,
                                Celular = UsuarioDB.Celular,
                                CURP = UsuarioDB.CURP
                            };
                            usuario.Rol = new ML.Rol
                            {
                                IdRol = UsuarioDB.IdRol,
                                Nombre = UsuarioDB.NombreRol
                            };
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron usuarios";
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
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {

                    var objUsuario = context.UsuarioGetById(IdUsuario).FirstOrDefault();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario
                        {
                            IdUsuario = objUsuario.IdUsuario,
                            UserName = objUsuario.UserName,
                            Nombre = objUsuario.NombreUsuario,
                            ApellidoPaterno = objUsuario.ApellidoPaterno,
                            ApellidoMaterno = objUsuario.ApellidoMaterno,
                            Email = objUsuario.Email,
                            Password = objUsuario.Password,
                            Sexo = objUsuario.Sexo,
                            Telefono = objUsuario.Telefono,
                            Celular = objUsuario.Celular,
                            CURP = objUsuario.CURP,
                            Rol = new ML.Rol
                            {
                                IdRol = objUsuario.IdRol.Value
                            }
                        };
                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro al usuario";
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
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    int NoFilas = context.UsuarioUpdate(
                        idUsuario: usuario.IdUsuario,
                        userName: usuario.UserName,
                        nombre: usuario.Nombre,
                        apellidoPaterno: usuario.ApellidoPaterno,
                        apellidoMaterno: usuario.ApellidoMaterno,
                        email: usuario.Email,
                        password: usuario.Password,
                        sexo: usuario.Sexo,
                        telefono: usuario.Telefono,
                        celular: usuario.Celular,
                        cURP: usuario.CURP,
                        fechaNacimiento: usuario.FechaNacimiento.ToString(),
                        idRol: usuario.Rol.IdRol);
                    if (NoFilas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    int NoFilas = context.UsuarioDelete(idUsuario: IdUsuario);

                    if (NoFilas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Sin registros afectados";
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
        public static List<ML.Rol> GetRols()
        {
            List<ML.Rol> listRols = new List<ML.Rol>();
            using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
            {
                var query = from r in context.Rols
                            select r;
                if (query != null & query.ToList().Count > 0)
                {
                    foreach (var r in query.ToList())
                    {
                        ML.Rol rol = new ML.Rol
                        {
                            IdRol = r.IdRol,
                            Nombre = r.Nombre
                        };
                        listRols.Add(rol);
                    }
                }
            }
            return listRols;
        }
        /*FUNCIONES DE CRUD USANDO ENTITY FRAMEWORK(EF) & Linq*/
        public static ML.Result GetAllLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var query = from u in context.Usuarios
                                join r in context.Rols on u.IdRol equals r.IdRol
                                select new
                                {
                                    u.IdUsuario,
                                    NombreUsuario = u.Nombre,
                                    u.UserName,
                                    u.ApellidoPaterno,
                                    u.ApellidoMaterno,
                                    u.Email,
                                    u.Password,
                                    u.Sexo,
                                    u.Telefono,
                                    u.Celular,
                                    u.FechaNacimiento,
                                    u.CURP,
                                    r.IdRol,
                                    NombreRol = r.Nombre
                                };
                    if (query != null && query.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario
                            {
                                IdUsuario = obj.IdUsuario,
                                UserName = obj.UserName,
                                Nombre = obj.NombreUsuario,
                                ApellidoPaterno = obj.ApellidoPaterno,
                                ApellidoMaterno = obj.ApellidoMaterno,
                                Email = obj.Email,
                                Password = obj.Password,
                                Sexo = obj.Sexo,
                                Telefono = obj.Telefono,
                                Celular = obj.Celular,
                                FechaNacimiento = obj.FechaNacimiento,
                                CURP = obj.CURP,
                                Rol = new ML.Rol()
                                {
                                    IdRol = obj.IdRol,
                                    Nombre = obj.NombreRol
                                }
                            };

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudieron obtener los usuarios";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetByIdLinq(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {


                    var query = (from usuario in context.Usuarios where usuario.IdUsuario == IdUsuario
                                 join direccion in context.Direccions
                                 on usuario.IdUsuario equals direccion.IdUsuario into direcciones
                                 from direccion in direcciones.DefaultIfEmpty()

                                 join rol in context.Rols
                                 on usuario.IdRol equals rol.IdRol into roles
                                 from rol in roles.DefaultIfEmpty()

                                 join colonia in context.Colonias
                                 on direccion.IdColonia equals colonia.IdColonia into colonias
                                 from colonia in colonias.DefaultIfEmpty()

                                 join municipio in context.Municipios
                                 on colonia.IdMunicipio equals municipio.IdMunicipio into municipios
                                 from municipio in municipios.DefaultIfEmpty()

                                 join estado in context.Estadoes
                                 on municipio.IdEstado equals estado.IdEstado into estados
                                 from estado in estados.DefaultIfEmpty()

                                 select new
                                 {
                                     usuario,
                                     rol,
                                     direccion,
                                     colonia,
                                     municipio,
                                     estado
                                 }).FirstOrDefault();
                    if (query != null)
                    {
                        if (query.usuario != null)
                        {
                            ML.Usuario usuario = new ML.Usuario
                            {
                                IdUsuario = query.usuario.IdUsuario,
                                UserName = query.usuario.UserName,
                                Nombre = query.usuario.Nombre,
                                ApellidoPaterno = query.usuario.ApellidoPaterno,
                                ApellidoMaterno = query.usuario.ApellidoMaterno,
                                Email = query.usuario.Email,
                                Password = query.usuario.Password,
                                Sexo = query.usuario.Sexo?.Trim(),
                                Telefono = query.usuario.Telefono,
                                Celular = query.usuario.Celular,
                                FechaNacimiento = query.usuario.FechaNacimiento,
                                CURP = query.usuario.CURP,
                                Rol = new ML.Rol()
                                {
                                    IdRol = query.rol.IdRol,
                                    Nombre = query.rol.Nombre
                                },
                                ImagenUsuario = new ML.ImagenUsuario(),
                                Direccion = new ML.Direccion
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
                                }

                            };
                            if (query.direccion != null)
                            {
                                usuario.Direccion = new ML.Direccion
                                {
                                    IdDireccion = query.direccion.IdDireccion,
                                    Calle = query.direccion.Calle,
                                    NumeroExterior = query.direccion.NumeroExterior,
                                    NumeroInterior = query.direccion.NumeroInterior,
                                    Colonia = new ML.Colonia
                                    {
                                        IdColonia = query.colonia.IdColonia,
                                        Municipio = new ML.Municipio
                                        {
                                            IdMunicipio = query.municipio.IdMunicipio,
                                            Estado = new ML.Estado
                                            {
                                                IdEstado = query.estado.IdEstado
                                            }
                                        }
                                    }
                                };
                            }
                            result.Object = usuario;
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario no encontrado";
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
        public static ML.Result AddLinq(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var usuarioEF = new DL_EF.Usuario()
                    {

                        UserName = usuario.UserName,
                        Nombre = usuario.Nombre,
                        ApellidoPaterno = usuario.ApellidoPaterno,
                        ApellidoMaterno = usuario.ApellidoMaterno,
                        Email = usuario.Email,
                        Password = usuario.Password,
                        Sexo = usuario.Sexo,
                        Telefono = usuario.Telefono,
                        Celular = usuario.Celular,
                        FechaNacimiento = Convert.ToDateTime(usuario.FechaNacimiento),
                        CURP = usuario.CURP,
                        IdRol = usuario.Rol.IdRol,
                    };

                    context.Usuarios.Add(usuarioEF);
                    int NoFilas = context.SaveChanges();
                    if (NoFilas > 0)
                    {
                        result.Object = usuarioEF.IdUsuario; //Agregar el parametro del IdUsuario al Object(Boxing)
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public static ML.Result UpdateLinq(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var query = (from u in context.Usuarios
                                 where u.IdUsuario == usuario.IdUsuario
                                 select u).SingleOrDefault();
                    if (query != null)
                    {
                        query.Nombre = usuario.Nombre;
                        query.UserName = usuario.UserName;
                        query.Nombre = usuario.Nombre;
                        query.ApellidoPaterno = usuario.ApellidoPaterno;
                        query.ApellidoMaterno = usuario.ApellidoMaterno;
                        query.Email = usuario.Email;
                        query.Password = usuario.Password;
                        query.Sexo = usuario.Sexo;
                        query.Telefono = usuario.Telefono;
                        query.Celular = usuario.Celular;
                        query.FechaNacimiento = usuario.FechaNacimiento;
                        query.CURP = usuario.CURP;
                        query.IdRol = usuario.Rol.IdRol ?? null;
                        int NoFilas = context.SaveChanges();
                        if (NoFilas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el usuario";
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
        public static ML.Result DeleteLinq(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (RMarianoProgramacionNCapasEntities context = new RMarianoProgramacionNCapasEntities())
                {
                    var usuario = (from u in context.Usuarios
                                   where u.IdUsuario == IdUsuario
                                   select u).First();
                    context.Usuarios.Remove(usuario);
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
        //Metodo asincrono
        public static async Task<ML.Result> GetAllUsuariosAsync()
        {
            ML.Result resultGetAll = new ML.Result();
            var apiBaseUrl = "http://localhost:50/RWebAPI/api/usuario/GetAll";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiBaseUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        resultGetAll.Objects = new List<object>();
                        string content = await response.Content.ReadAsStringAsync();
                        List<ML.Usuario> usuarios = JsonConvert.DeserializeObject<List<ML.Usuario>>(content);
                        foreach (ML.Usuario usuario in usuarios)
                        {
                            resultGetAll.Objects.Add(usuario);
                        }
                        resultGetAll.Correct = true;
                    }
                    else
                    {
                        resultGetAll.ErrorMessage = "No se obtuvo el resultado";
                        resultGetAll.Correct = false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    resultGetAll.Ex = ex;
                    resultGetAll.ErrorMessage = ex.Message;
                }
            }
            return resultGetAll;
        }
        public static ML.Result GetAllREST()
        {
            ML.Result resultGetAll = new ML.Result();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"]);

                    var responseTask = client.GetAsync("GetAll");

                    responseTask.Wait(); //abrir otro hilo

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<List<object>>();
                        readTask.Wait();
                        resultGetAll.Objects = new List<object>();
                        foreach (var resultItem in readTask.Result)
                        {
                            ML.Usuario usuario = JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            resultGetAll.Objects.Add(usuario);
                        }
                        resultGetAll.Correct = true;
                    }
                    else
                    {
                        resultGetAll.ErrorMessage = "No se obtuvo el resultado";
                        resultGetAll.Correct = false;
                    }
                }
                
            }
            catch (Exception ex)
            {
                resultGetAll.Ex = ex;
                resultGetAll.ErrorMessage = ex.Message;
            }
            return resultGetAll;
        }
        public static ML.Result GetByIdREST(int IdUsuario)
        {
            ML.Result resultGetById = new ML.Result();
            try 
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"]);

                    var responseTask = client.GetAsync("?IdUsuario=" + IdUsuario);

                    responseTask.Wait(); //abrir otro hilo

                    HttpResponseMessage resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<object>();
                        readTask.Wait();
                        ML.Usuario usuario = JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.ToString());
                        resultGetById.Object = usuario;
                        resultGetById.Correct = true;
                    }
                    else
                    {
                        resultGetById.ErrorMessage = "No se obtuvo el resultado";
                        resultGetById.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                resultGetById.Correct = false;
                resultGetById.ErrorMessage = ex.Message;
                resultGetById.Ex = ex;
            }
            return resultGetById;
        }

        public static ML.Result AddREST(ML.Usuario usuario)
        {
            ML.Result resultAdd = new ML.Result();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"]);
                    var response = client.PostAsJsonAsync("",usuario);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        resultAdd.Correct = true;
                        resultAdd.Object = result.Content.ToString();
                    }
                    else
                    {
                        resultAdd.Correct = false;
                        resultAdd.Ex = response.Exception;
                        resultAdd.ErrorMessage = result.RequestMessage.Content.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                resultAdd.ErrorMessage = ex.Message;
                resultAdd.Ex = ex;
            }
            return resultAdd;
        }
    }
}
