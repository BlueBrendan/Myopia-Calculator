using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Welcome to the Myopia Calculator\n\nEnter your far point of your left eye in centimeters: ");
            double leftFarPoint = Convert.ToDouble(Console.ReadLine());
            while (leftFarPoint < 1)
            {
                Console.Write("You cannot have a far point below 1! Enter another value: ");
                leftFarPoint = Convert.ToDouble(Console.ReadLine());
            }
            double leftRefractiveError = calculateRefractiveError(leftFarPoint);
            Console.Write("Enter your far point of your right eye in centimeters: ");
            double rightFarPoint = Convert.ToDouble(Console.ReadLine());
            while (rightFarPoint < 1)
            {
                Console.Write("You cannot have far point below 1! Enter another value: ");
                rightFarPoint = Convert.ToDouble(Console.ReadLine());
            }
            double rightRefractiveError = calculateRefractiveError(rightFarPoint);
            String leftEyeStatus = myopiaSeverity(leftRefractiveError);
            String rightEyeStatus = myopiaSeverity(rightRefractiveError);
            Console.Write("\nBased on your far point, we can determine that: \n1. You require " + leftRefractiveError + " diopters of spherical correction in the left eye\n2. You require " + rightRefractiveError + " diopters of spherical correction in the right eye\n3. You have " + leftEyeStatus + " in the left eye\n3. You have " + rightEyeStatus + " in the right eye");
            Console.ReadLine();
        }
        public static double calculateRefractiveError(double farPoint)
        {
            //This method calculates the amount of refractive error using the far point in centimeters
            double refractiveError = 100 / farPoint;
            refractiveError = Math.Round(refractiveError, 2, MidpointRounding.AwayFromZero);
            //Sends the refractive error value to be rounded to follow international standards (quarter diopters in the US)
            refractiveError = roundQuarterDiopter(refractiveError);
            return refractiveError;
        }
        public static double roundQuarterDiopter(double value)
        {
            double difference = (Math.Ceiling(value) - value) - (value - Math.Floor(value));
            if (difference > 0)
            {
                //The value of refractive error is closer to the floor than it is to the ceiling, so it must be below the 0.5D increment 
                difference = ((Math.Floor(value)+0.5)-value) - (value - Math.Floor(value));
                if (difference > 0)
                {
                    //The value of refractive error is closer to the floor than it is to the 0.5D increment, so it must be either at 0 or 0.25D
                    difference = ((Math.Floor(value) + 0.25) - value) - (value - Math.Floor(value));
                    if (difference > 0)
                    {
                        //The value of refractive error must be at the 0D increment, round down
                        value = Math.Floor(value);
                    }
                    else if (difference < 0)
                    {
                        //The value of refractive error must be at the 0.25D increment, round up
                        value = Math.Floor(value) + 0.25D;
                    }
                }
                else if (difference < 0)
                {
                    //The value of refractive error is closer to the 0.5D increment than it is to the floor, so it must either be at 0.25D or 0.5D
                    difference = ((Math.Floor(value) + 0.50) - value) - (value - (Math.Floor(value)+0.25));
                    if (difference > 0)
                    {
                        //The value of refractive error must be at the 0.25D increment, round down
                        value = Math.Floor(value)+0.25;
                    }
                    else if (difference < 0)
                    {
                        //The value of refractive error must be at the 0.50D increment, round up
                        value = Math.Floor(value)+0.5;
                    }
                }
                else
                {
                    //The value of refractive error must be at the 0.25D increment
                    if (Math.Floor(value) + 0.25 == 0.25)
                        value = 0;
                }
            }
            else if (difference < 0)
            {
                //The value of refractive error is closer to the ceiling than it is to the floor, so it must be above the 0.5D increment 
                difference = (Math.Ceiling(value) - value) - (value - (Math.Floor(value)+0.5));
                if (difference > 0)
                {
                    //The value of refractive error is closer to the 0.5D increment than it is to the ceiling, so it must be either at 0.5D or 0.75D
                    difference = ((Math.Floor(value) + 0.75) - value) - (value - (Math.Floor(value)+0.5));
                    if (difference > 0)
                    {
                        //The value of refractive error must be at the 0.5D increment, round down
                        value = Math.Floor(value)+0.5;
                    }
                    else if (difference < 0)
                    {
                        //The value of refractive error must be at the 0.75D increment, round up
                        value = Math.Floor(value) + 0.75;
                    }
                }
                else if (difference < 0)
                {
                    //The value of refractive erro is closer to the ceiling increment than it is to the 0.5D increment, so it must either be at 0.75D or 0D
                    difference = (Math.Ceiling(value)-value) - (value-(Math.Floor(value) + 0.75));
                    if (difference > 0)
                    {
                        //The value of refractive error must be at the 0.75D increment, round down
                        value = Math.Floor(value) + 0.75;
                    }
                    else if (difference < 0)
                    {
                        //The value of refractive error must be at the 0D increment, round up
                        value = Math.Ceiling(value);
                    }
                }
            }
            return value; 
        }
        public static string myopiaSeverity(double value)
        {
            if (value == 0)
                return "no myopia";
            else if (value > 0 && value < 2 || value == 2)
                return "low myopia";
            else if (value > 2 && value < 6 || value == 6)
                return "moderate myopia";
            else if (value > 6 && value < 10 || value == 10)
                return "high myopia";
            else
                return "very high myopia";
        }
    }
}
