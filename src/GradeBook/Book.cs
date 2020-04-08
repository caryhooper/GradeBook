using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);
    public class NamedObject  //by default derives from System.Object class (aka : object)
    {
        public NamedObject(string name) //this requires a name
        {
            Name = name;
        }
        public string Name
        {
            get;
            set;
        }
    }
    //interface object begins with I.  
    //interface expresses what is required in certain code
    //needs interface added to obejct class
    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;

    }
    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {

        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade); //abstract method.  Has method, but method not implemented.

        public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book, IBook
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using(var writer = File.AppendText($"{this.Name}.txt")) //using statements C# compiler will clean things up once complete
            {
                writer.WriteLine(grade.ToString()); //If error occurs while writing line, file will stay opened forever!  So, we use using statement
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
                //writer.Dispose(); //Commented because using statement automatically disposes once complete.
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }
            return result;
        }
    }

    public class InMemoryBook : Book, IBook
    {
        public InMemoryBook(string name) : base(name) //base() accesses a constructor on the base class
        {
            grades = new List<double>();
            Name = name;
        }

        //What are the operations/behavior of this sort of class?
        //What state/data does it hold?
        public override void AddGrade(double grade)
        {
            //do the add grade
            //may validate input here.
            if (grade >= 0 && grade <= 100)
            {
                Console.WriteLine("Grade is valid.");
                grades.Add(grade);
                if (GradeAdded!= null) //This means nobody is listening / no delegate is created
                {
                    GradeAdded(this, new EventArgs());
                }
                //If event needs to be accessed outside of program or in another area.
                //How do we let them know when a grade is added to a grade book.. Events
            }
            else {
                Console.WriteLine("Invalid Value");
            }
        }

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            for (var index = 0; index < grades.Count; index += 1){
                result.Add(grades[index]);
            }
            return result;
        }

        private List<double> grades;
        private string name; //public member has uppercase name
        //public string Name{
        //  get; 
        //  private set; //makes the set property read-only
        //}
        //readonly string category = "Science"; //Field that can only be initialized in the constructor.
        public const string CATEGORY = "Science";
    }
}
