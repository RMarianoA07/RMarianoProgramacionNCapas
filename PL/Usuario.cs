using System;
using System.Collections.Generic;
using System.Linq;

namespace PL
{
    internal class Usuario
    {
        public static void Add() {

            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("\nIngresa Nombre de Usuario. Ej. Luis123");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("\nIngresa tu nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("\nIngresa tu apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("\nIngresa tu apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("\nIngresa tu correo electrónico");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("\nIngresa tu contraseña");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("\nIngresa tu sexo Ej. H/M");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("\nIngresa tu numero telefonico");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("\nIngresa tu número celular");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("\nIngresa tu fecha de nacimiento Ej. 24/06/2025");
            usuario.FechaNacimiento = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("\n¿Conoces tu curp? \n1. Si \n2. No");
            int Flag = int.Parse(Console.ReadLine());

            if(Flag == 1)
            {
                Console.WriteLine("Ingresa tu CURP");
                usuario.CURP = Console.ReadLine();
            }

            Console.WriteLine("\nIngresa el Id del Rol");
            List<ML.Rol> listRols = BL.Usuario.GetRols();
            listRols.ForEach(rol =>
            {
                Console.WriteLine(rol.IdRol + " " + rol.Nombre);
            });
            int IdRol = int.Parse(Console.ReadLine());
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = IdRol;

            //ML.Result result = BL.Usuario.Add(usuario);
            //ML.Result result = BL.Usuario.AddEF(usuario);
            //ML.Result result = BL.Usuario.AddLinq(usuario);
            UserService.UserServiceClient userService = new UserService.UserServiceClient();
            var result = userService.Add(usuario);
            //var result = usuarioClient.Add(usuario);

            if (result.Correct)
            {
                Console.WriteLine("\n¡Registro exitoso!\n");
            }
            else
            {
                Console.WriteLine("Hubo un error al registrar:" + result.ErrorMessage);
            }
        }
        public static void Update() {

            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("\nIngresa el ID a actualizar");
            usuario.IdUsuario = int.Parse(Console.ReadLine());
            //ML.Result resultGet = BL.Usuario.GetById(usuario.IdUsuario);
            //ML.Result resultGet = BL.Usuario.GetByIdEF(usuario.IdUsuario);
            UserService.UserServiceClient userService = new UserService.UserServiceClient();
            var resultGet = userService.GetById(usuario.IdUsuario);


            if (resultGet.Correct)
            {
                Console.WriteLine("\nIngresa el nuevo Nombre de Usuario. Ej. Luis123");
                usuario.UserName = Console.ReadLine();

                Console.WriteLine("\nIngresa el nuevo nombre");
                usuario.Nombre = Console.ReadLine();

                Console.WriteLine("\nIngresa el nuevo apellido paterno");
                usuario.ApellidoPaterno = Console.ReadLine();

                Console.WriteLine("\nIngresa el nuevo apellido materno");
                usuario.ApellidoMaterno = Console.ReadLine();

                Console.WriteLine("\nIngresa el nuevo correo electrónico");
                usuario.Email = Console.ReadLine();

                Console.WriteLine("\nIngresa la nueva contraseña");
                usuario.Password = Console.ReadLine();

                Console.WriteLine("\nIngresa la actualización de sexo. Ej. H/M");
                usuario.Sexo = Console.ReadLine();

                Console.WriteLine("\nIngresa la actualización de numero telefonico");
                usuario.Telefono = Console.ReadLine();

                Console.WriteLine("\nIngresa la actualización de número celular");
                usuario.Celular = Console.ReadLine();

                Console.WriteLine("\nIngresa tu fecha de nacimiento Ej. 24/06/2025");
                usuario.FechaNacimiento = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("\n¿Deseas actualizar tu CURP? \n1. Si \n2. No");
                int Flag = int.Parse(Console.ReadLine());

                if (Flag == 1)
                {
                    Console.WriteLine("Ingresa tu CURP");
                    usuario.CURP = Console.ReadLine();
                }

                Console.WriteLine("\nIngresa el nuevo Id del Rol");
                List<ML.Rol> listRols = BL.Usuario.GetRols();
                listRols.ForEach(rol =>
                {
                    Console.WriteLine(rol.IdRol + " " + rol.Nombre);
                });
                int IdRol = int.Parse(Console.ReadLine());

                usuario.Rol = new ML.Rol();
                usuario.Rol.IdRol = IdRol;
                //ML.Result resultUpdate = BL.Usuario.Update(usuario);
                //ML.Result resultUpdate = BL.Usuario.UpdateEF(usuario);
                //ML.Result resultUpdate = BL.Usuario.UpdateLinq(usuario);
                var resultUpdate = userService.Update(usuario);

                if (resultUpdate.Correct)
                {
                    Console.WriteLine("\n¡Actualización exitosa!\n");
                }
                else
                {
                    Console.WriteLine("Hubo un error al actualizar:" + resultUpdate.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine("\n¡Usuario no encontrado!\n");
            }

            
        }
        public static void Delete()
        {
            Console.WriteLine("\nIngresa el usuario a eliminar");
            int IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.Delete(IdUsuario);
            //ML.Result result = BL.Usuario.DeleteEF(IdUsuario);
            //ML.Result result = BL.Usuario.DeleteLinq(IdUsuario);
            //UsuarioService.UsuarioServiceClient usuarioServiceClient = new UsuarioService.UsuarioServiceClient();
            //var result = usuarioServiceClient.Delete(IdUsuario);
            UserService.UserServiceClient userService = new UserService.UserServiceClient();
            var result = userService.Delete(IdUsuario);


            //UsuarioServiceClient usuarioClient = new UsuarioServiceClient();
            //var result2 = usuarioClient.Delete(IdUsuario);
            if (result.Correct)
            {
                Console.WriteLine("\n¡Eliminación exitosa!\n");
            }
            else
            {
                Console.WriteLine("Imposible eliminar el usuario:" + result.ErrorMessage);
            }
        }
        public static void GetAll()
        {
            //ML.Result result = BL.Usuario.GetAll();
            //ML.Result result = BL.Usuario.GetAllEF();
            //ML.Result result = BL.Usuario.GetAllLinq();
            UserService.UserServiceClient usersService = new UserService.UserServiceClient();
            var result = usersService.GetAll();
            if (result.Correct)
            {
                if(result.Objects.Count() >= 1)
                {
                    Console.WriteLine("\n--- Lista de Usuarios :) ---");

                    var listUser = new List<ML.Usuario>();
                    foreach (ML.Usuario usuario in result.Objects)
                    {
                        listUser.Add(usuario);
                        Console.WriteLine("\n----------------------------");
                        Console.WriteLine($"IdUsuario: {usuario.IdUsuario}");
                        Console.WriteLine($"UserName: {usuario.UserName}");
                        Console.WriteLine($"Nombre: {usuario.Nombre}");
                        Console.WriteLine($"Apellido Paterno: {usuario.ApellidoPaterno}");
                        Console.WriteLine($"Apellido Materno: {usuario.ApellidoMaterno}");
                        Console.WriteLine($"Email: {usuario.Email}");
                        Console.WriteLine($"Password: {usuario.Password}");
                        Console.WriteLine($"Sexo: {usuario.Sexo}");
                        Console.WriteLine($"Telefono: {usuario.Telefono}");
                        Console.WriteLine($"Celular: {usuario.Celular}");
                        Console.WriteLine($"FechaNacimiento: {usuario.FechaNacimiento}");
                        Console.WriteLine($"CURP: {usuario.CURP}");
                        Console.WriteLine($"Rol: {usuario.Rol.Nombre}");
                        Console.WriteLine("----------------------------\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n--- Sin registro de usuarios :( ---\n");
                }
            }
            else
            {
                Console.WriteLine("\nError al obtener los usuarios: " + result.ErrorMessage + "\n");
            }
        }
        public static void GetById()
        {
            Console.WriteLine("\nIngresa el Id del usuario");
            int IdUsuario = int.Parse(Console.ReadLine());
            //ML.Result result = BL.Usuario.GetById(IdUsuario);
            //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            //ML.Result result = BL.Usuario.GetByIdLinq(IdUsuario);
            UserService.UserServiceClient userService = new UserService.UserServiceClient();
            var result = userService.GetById(IdUsuario);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario) result.Object;

                Console.WriteLine("\n------USUARIO------------");
                Console.WriteLine($"UserName: {usuario.UserName}");
                Console.WriteLine($"Nombre: {usuario.Nombre}");
                Console.WriteLine($"Apellido Paterno: {usuario.ApellidoPaterno}");
                Console.WriteLine($"Apellido Materno: {usuario.ApellidoMaterno}");
                Console.WriteLine($"Email: {usuario.Email}");
                Console.WriteLine($"Password: {usuario.Password}");
                Console.WriteLine($"Sexo: {usuario.Sexo}");
                Console.WriteLine($"Telefono: {usuario.Telefono}");
                Console.WriteLine($"Celular: {usuario.Celular}");
                Console.WriteLine($"FechaNacimiento: {usuario.FechaNacimiento}");
                Console.WriteLine($"CURP: {usuario.CURP}");
                Console.WriteLine($"NombreRol: {usuario.Rol.Nombre}");

                Console.WriteLine("---------------------------\n");

            }
            else
            {
                Console.WriteLine("Error: " + result.ErrorMessage);
            }

        }
        public static void ProbarLambdas()
        {
            BL.Lambdas.Funciones.GetResults();
        }
    }
}
