//var cities = new RandomItemGenerator<string>();
//cities.Add("Zurich");
//cities.Add("New York");
//cities.Add("London");

//for(int i = 0; i < 10; i++)
//{
//    var randomCity = cities.Get();
//    Console.WriteLine(randomCity);
//}

//var numbers = new RandomItemGenerator<int>();
//numbers.Add(7);
//numbers.Add(9);
//numbers.Add(20);
//numbers.Add(19);
//numbers.Add(99);
//numbers.Add(12);
//numbers.Add(54);
//numbers.Add(632);


//for(int i = 0; i < 10; i++)
//{
//    var randomNumber = numbers.Get();
//    Console.WriteLine(randomNumber);
//}



//public class RandomItemGenerator<T>
//{
//    private readonly List<T> _items = new List<T>();
//    private readonly Random _random = new Random();

//    public void Add(T item)
//    {
//        _items.Add(item);
//    }

//    public T Get()
//    {
//        var index = _random.Next(0, _items.Count);
//        return _items[index];
//    }
//}


using System;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            //generic = "not specific to a particular data type"
            //      add <T> to : classes, methods, fields, etc;
            //      allows for code reusability for different data types 



            int[] array = { 1, 2, 3 };
            double[] doubleArray = { 1.0, 2.0, 3.0 };
            string[] stringArray = { "one", "two", "three"};

            DisplayElements(array);
            DisplayElements(doubleArray);
            DisplayElements(stringArray);




            Console.ReadKey();
        }
        //commmented because we are going to use generics to allow all datatypes making a generic method 
        //public static void DisplayElements(int[] array)
        //{
        //    foreach(int item in array)
        //    {
        //        Console.Write(item + " ");
        //    }
        //    Console.WriteLine();
        //}
        //public static void DisplayElements(double[] array)
        //{
        //    foreach (double item in array)
        //    {
        //        Console.Write(item + " ");
        //    }
        //    Console.WriteLine();
        //}
        public static void DisplayElements<Thing>(Thing[] array) //this works like all using all 3 methods into 1 
        {
            foreach (Thing item in array)
            {
                Console.Write(item + " ");
            } 
            Console.WriteLine();

        }
    }
}