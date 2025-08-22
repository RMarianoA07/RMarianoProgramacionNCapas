using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static BL.Lambdas;

namespace BL
{
    public class Lambdas
    {
        
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
        }
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Funciones
        {
            public static void GetResults()
            {
                var products = new List<Product>
                {
                new Product { Id = 1, Name = "Laptop", Price = 1000, Stock = 10 },
                new Product { Id = 2, Name = "Smartphone", Price = 800, Stock = 20 },
                new Product { Id = 3, Name = "Tablet", Price = 400, Stock = 15 },
                new Product { Id = 4, Name = "Monitor", Price = 300, Stock = 8 },
                new Product { Id = 5, Name = "Keyboard", Price = 50, Stock = 50 },
                new Product { Id = 6, Name = "Mouse", Price = 30, Stock = 70 }
                };

                var customers = new List<Customer>
                {
                new Customer { Id = 1, Name = "Alice", Age = 28 },
                new Customer { Id = 2, Name = "Bob", Age = 35 },
                new Customer { Id = 3, Name = "Charlie", Age = 22 },
                new Customer { Id = 4, Name = "Diana", Age = 40 }
                };


                //1. Obtener productos con precio mayor a $500.
                //Where()
                var ProductosMayor500 = products.Where(prod => prod.Price > 500).ToList();
                Console.WriteLine("\n1. Obtener productos con precio mayor a $500.");

                foreach (var producto in ProductosMayor500)
                {
                    Console.WriteLine("--Producto:: " + producto.Name);
                }
                Console.WriteLine("\n");

                //2. Obtener los nombres de los productos con stock menor a 20.
                var PoductosStockMenos20 = products.Where(prod => prod.Stock < 20).ToList();
                Console.WriteLine("\n2. Obtener los nombres de los productos con stock menor a 20.");
                foreach (var prod in PoductosStockMenos20)
                {
                    Console.WriteLine("--Producto:: " + prod.Name + " Stock:: " + prod.Stock);
                }
                Console.WriteLine("\n");

                //3. Calcular el precio promedio de todos los productos.
                //Sum() & Count()
                var PrecioPromedioProductos = products.Sum(prod => prod.Price) / products.Count();
                Console.WriteLine("\n3. Calcular el precio promedio de todos los productos.");
                Console.WriteLine("--Precio promedio:: $" + PrecioPromedioProductos);
                Console.WriteLine("\n");

                //4. Agrupar los productos por su stock (bajo: <20, medio: 20-50, alto: >50).
                Console.WriteLine("\n4. Agrupar los productos por su stock (bajo: <20, medio: 20-50, alto: >50).");
                var rangos = new List<(int Min, int Max)>
                {
                    (0, 19),
                    (20, 50),
                    (51, int.MaxValue)
                };
                var resultado = rangos.Select(rango => new
                {
                    Etiqueta = $"{rango.Min}–{rango.Max}",
                    Valores = products.Where(producto => producto.Stock >= rango.Min && producto.Stock <= rango.Max).ToList()
                });

                // Imprimir agrupaciones
                foreach (var grupo in resultado)
                {
                    Console.WriteLine($"\nRango {grupo.Etiqueta}:");

                    foreach (var producto in grupo.Valores)
                    {
                        Console.WriteLine($"--Producto::{producto.Name} Stock::{producto.Stock}");
                    }
                }
                Console.WriteLine("\n");

                //5. Obtener el producto más caro.
                var ProductoMasCaro = products.Where(prod => prod.Price == products.Max(producto => producto.Price)).First();
                Console.WriteLine("\n5. Obtener el producto más caro.");
                Console.WriteLine("--ProductoMasCaro: " + ProductoMasCaro.Name + " Precio:: " + ProductoMasCaro.Price);
                Console.WriteLine("\n");

                //6.Verificar si algún producto tiene stock mayor a 60.
                var ProductoStockMayorA60 = products.Where(prod => prod.Stock > 60);
                var ThereAreProductoStockMayorA60 = products.Exists(prod => prod.Stock > 60);
                Console.WriteLine("\n6.Verificar si algún producto tiene stock mayor a 60."); 
                Console.WriteLine("\nProductos con Stock mayor a 60");
                foreach(var producto in ProductoStockMayorA60)
                {
                    Console.WriteLine("--Producto:: " + producto.Name + " Stock:: " + producto.Stock);
                }
                Console.WriteLine("\n--¿Existen Productos con stock mayor a 60? " + (ThereAreProductoStockMayorA60));
                
                Console.WriteLine("\n");

                //7.Verificar si todos los productos tienen precio mayor a 20.
                var ProductosSonMayoraA20 = products.All(prod => prod.Price > 20);
                Console.WriteLine("\n7. Verificar si todos los productos tienen precio mayor a $20.");
                Console.WriteLine("\nLista de productos con precio");
                foreach (Product producto in products)
                {
                    Console.WriteLine("--Producto:: " + producto.Name + " - Precio: $" + producto.Price);
                }
                Console.WriteLine("\n--¿Todos los Productos Tienen Precio Mayor A 20? " + ProductosSonMayoraA20);
                Console.WriteLine("\n");

                //8.Sumar el stock total de todos los productos.
                var StockTotal = products.Sum(prod => prod.Stock);
                Console.WriteLine("\n8. Sumar el stock total de todos los productos.\r\n");
                Console.WriteLine("--StockTotal: " + StockTotal);
                Console.WriteLine("\n");

                //9.Obtener los productos ordenados por precio de forma ascendente.
                var ProductosOrdenadosAscendente = products.OrderBy(prod => prod.Price).ToList();
                Console.WriteLine("\n9. Obtener los productos ordenados por precio de forma ascendente.\r\n");
                foreach (var prod in ProductosOrdenadosAscendente)
                {
                    Console.WriteLine("--Producto: " + prod.Name + " - Precio: " + prod.Price);
                }
                Console.WriteLine("\n");

                //10.Obtener los clientes mayores de 30 años.
                var ClientesMayoresA30 = customers.Where(cust => cust.Age > 30).Select(cust => cust.Name);
                Console.WriteLine("\n10. Obtener los clientes mayores de 30 años.");
                foreach (var cliente in ClientesMayoresA30)
                {
                    Console.WriteLine("--Cliente: " + cliente);
                }
                Console.WriteLine("\n");
            }
        }
            
    }

}
