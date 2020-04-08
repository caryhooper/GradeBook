using System;
using System.Collections.Generic;
//Requirements:
//read scores of individual student, then compute statistics of these scores
//Grades entered as floating point 0 to 100
// stats show highest grade, lowest grade, avg grade

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new DiskBook("Cary's Grade Book");
            book.GradeAdded += OnGradeAdded;//subscribe method to event delegate



            // Loop to enter a grade in a loop.
            EnterGrades(book);

            var stats = book.GetStatistics();
            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The average grade is {stats.Average:N2}"); //N2 is like a formatting string.
            Console.WriteLine($"The highest grade is {stats.High:N2}");
            Console.WriteLine($"The lowest grade is {stats.Low:N2}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        private static void EnterGrades(IBook book)
        {
            var done = false;
            do
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    //throw new ArgumentException($"Invalid {nameof(input)}: {input}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }

            } while (!done);
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added...");
        }
    }
}
//inheritance
//polymorphism
//encapsulation

