using System;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Selecciona la opción a realizar: " +
                "\n1. Ver usuarios" +
                "\n2. Consultar usuario" +
                "\n3. Agregar usuario" +
                "\n4. Actualizar usuario" +
                "\n5. Eliminar usuario" +
                "\n6. Probar Lambdas" +
                "\n7. Salir");
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    PL.Usuario.GetAll();
                    break;
                case 2:
                    Console.WriteLine("\nSeleccionar usuario");
                    PL.Usuario.GetById();
                    break;
                case 3:
                    Console.WriteLine("\nAgrega un nuevo usuario");
                    PL.Usuario.Add();
                    break;
                case 4:
                    Console.WriteLine("\nActualiza información del usuario");
                    PL.Usuario.Update();
                    break;
                case 5:
                    Console.WriteLine("\nElimina un usuario");
                    PL.Usuario.Delete();
                    break;
                case 6:
                    Console.WriteLine("\nProbar Lambdas");
                    PL.Usuario.ProbarLambdas();
                    break;
                case 7:
                    Console.WriteLine("\nBye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nOpción no válida.");
                    break;
            }
            Main(args);
        }
    }
}
