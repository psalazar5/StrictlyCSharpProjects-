using System;

namespace BMICalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the BMI Calculator!");

            double weight = GetWeight();
            double height = GetHeight();

            double BMI = calculateBMI(weight, height);
            Console.WriteLine("Your final BMI is: " + Math.Round(BMI, 2));
        }


        static double GetWeight()
        {
            Console.WriteLine("Enter your weight in pounds: ");
            double weight = double.Parse(Console.ReadLine());
            return weight;
        }

        static double GetHeight()
        {
            Console.WriteLine("Enter your height in feet and inches (ex. 5'8 \"): ");
            string input = Console.ReadLine();
            string[] split = input.Split("'");
            double feet = Double.Parse(split[0]);
            double inches = Double.Parse(split[1].Trim('"')); //trim removes ' " '
            double height = (feet * 12) + inches;
            return height;
        }

        static double calculateBMI(double weight, double height)
        {
            //Convert inches to meters // amount of meters based on amount of inches // use kilograms in weight and divide by meters squared
            double meterHeight = height * 0.0254; // in each inch there are 0.0254 centimeters.

            //Convert our weight into kilgrams 
            double kilogramWeight = weight * 0.453592; //Getting the weight into kilograms 
            double bmi = kilogramWeight / (meterHeight * meterHeight); //meterHieght squared
            return bmi;
        }

    }
}