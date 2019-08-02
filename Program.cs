using StudentExercise3.Data;
using StudentExercise3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace UserInputExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();

            List<Student> students = repository.GetAllStudents();
            List<Exercise> exercises = repository.GetExercises();
            List<Exercise> jsExercises = repository.GetJSExercises();
            List<Instructor> instructors = repository.GetAllInstructors();

            Exercise fizzBuzz = new Exercise
            {
                Exercise_name = "FizzBuzz",
                Exercise_language = "JavaScript"
            };

            GetStudents("Students", students);

            GetAllExercises(exercises);

            JSExercises(jsExercises);

            //repository.AddExercise(fizzBuzz);

            //GetAllExercises(exercises);

            Instructor kristen = new Instructor
            {
                FirstName = "Kristen",
                LastName = "Norris",
                SlackHandle = "KDawg",
                Specialty = "SQL",
                CohortId = 1
            };

            //repository.AddInstructor(kristen);

            GetInstructors("Instructors", instructors);

            repository.AddAssignment(2, 14, 5);

            
        }

        public static void GetStudents(string name, List<Student> students)
        {
            Console.WriteLine($"{name}");
            students.ForEach(student =>
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} with the slack handle {student.SlackHandle} is in Cohort{student.Cohort.Num}");
                student.Exercises.ForEach(e =>
                {
                    Console.WriteLine($"{e.Exercise_name}");

                });
            });
        }

        public static void GetAllExercises(List<Exercise> exercises)
        {
            Console.WriteLine("Exercises");
            exercises.ForEach(e =>
            {
                Console.WriteLine($"{e.Exercise_name} in {e.Exercise_language}");
            });
        }

        public static void JSExercises(List<Exercise> exercises)
        {
            Console.WriteLine("JavaScript Exercises");
            exercises.ForEach(e =>
            {
                Console.WriteLine($"{e.Exercise_name}");
            });
        }

        public static void GetInstructors(string name, List<Instructor> instructors)
        {
            Console.WriteLine($"{name}");
            instructors.ForEach(i =>
            {
                Console.WriteLine($"{i.FirstName} {i.LastName} with the slack handle {i.SlackHandle} specializes in {i.Specialty} and teaches Cohort{i.Cohort.Num}");
            });
        }
    }
}
