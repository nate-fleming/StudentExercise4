using StudentExercise3.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace StudentExercise3.Data
{
    class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercise4;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Student> GetAllStudents()
        {
           
            using (SqlConnection conn = Connection)
            {
               
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT s.Id, s.FirstName, s.LastName, s.SlackHandle, s.CohortId, c.Num, e.Exercise_Name, e.Exercise_Language, a.ExerciseId
                        FROM Student s
                        LEFT JOIN Cohort c ON s.CohortID = c.Id
                        JOIN Assignments a ON s.Id = a.StudentId
                        LEFT JOIN EXERCISE e ON a.ExerciseId = e.Id";  

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> students = new List<Student>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");

                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(slackHandleColumnPosition);

                        int cohortIdColumnPosition = reader.GetOrdinal("CohortId");
                        int cohortIdValue = reader.GetInt32(cohortIdColumnPosition);

                    

                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ExerciseId")),
                            Exercise_name = reader.GetString(reader.GetOrdinal("Exercise_Name")),
                            Exercise_language = reader.GetString(reader.GetOrdinal("Exercise_Language")),
                        };


                        if (!students.Any(s => s.Id == idValue))
                        {
                            Student student = new Student
                        {
                            Id = idValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            SlackHandle = slackHandleValue,
                            CohortId = cohortIdValue,
                            Cohort = new Cohort
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CohortId")),
                                Num = reader.GetInt32(reader.GetOrdinal("Num"))
                            },
                            Exercises = new List<Exercise>()
                        };

                        
                            student.Exercises.Add(exercise);
                            students.Add(student);
                        } else
                        {
                            students.Find(s => s.Id == idValue).Exercises.Add(exercise);
                        };

                    }

                    reader.Close();

                    return students;
                }
            }
        }

        public List<Exercise> GetExercises()
        {
            using (SqlConnection conn = Connection)
            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Exercise_Name, Exercise_Language FROM EXERCISE";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int exerciseNameColumnPosition = reader.GetOrdinal("Exercise_Name");
                        string exerciseNameValue = reader.GetString(exerciseNameColumnPosition);

                        int exerciseLanguageColumnPosition = reader.GetOrdinal("Exercise_Language");
                        string exerciseLanguageValue = reader.GetString(exerciseLanguageColumnPosition);


                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            Exercise_name = exerciseNameValue,
                            Exercise_language = exerciseLanguageValue
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }

        public List<Exercise> GetJSExercises()
        {
            using (SqlConnection conn = Connection)
            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Exercise_Name, Exercise_Language 
                                        FROM EXERCISE
                                        WHERE Exercise_Language = 'Javascript'";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int exerciseNameColumnPosition = reader.GetOrdinal("Exercise_Name");
                        string exerciseNameValue = reader.GetString(exerciseNameColumnPosition);

                        int exerciseLanguageColumnPosition = reader.GetOrdinal("Exercise_Language");
                        string exerciseLanguageValue = reader.GetString(exerciseLanguageColumnPosition);


                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            Exercise_name = exerciseNameValue,
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }

        public void AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // More string interpolation
                    cmd.CommandText = $"INSERT INTO Exercise (Exercise_Name, Exercise_Language) Values ('{exercise.Exercise_name}', '{exercise.Exercise_language}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Instructor> GetAllInstructors()
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT i.Id, i.FirstName, i.LastName, i.SlackHandle, i.Specialty, i.CohortId, c.Num
                        FROM Instructor i
                        LEFT JOIN Cohort c ON i.CohortID = c.Id";
                      

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");

                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(slackHandleColumnPosition);

                        int specialtyColumnPosition = reader.GetOrdinal("Specialty");
                        string specialtyValue = reader.GetString(specialtyColumnPosition);

                        int cohortIdColumnPosition = reader.GetOrdinal("CohortId");
                        int cohortIdValue = reader.GetInt32(cohortIdColumnPosition);

                        Instructor instructor = new Instructor
                        {
                            Id = idValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            SlackHandle = slackHandleValue,
                            Specialty = specialtyValue,
                            CohortId = cohortIdValue,
                            Cohort = new Cohort
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CohortId")),
                                Num = reader.GetInt32(reader.GetOrdinal("Num"))
                            },

                        };

                        instructors.Add(instructor);
                    }

                    reader.Close();

                    return instructors;
                }
            }
        }

        public void AddInstructor(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // More string interpolation
                    cmd.CommandText = $"INSERT INTO Instructor (FirstName, LastName, SlackHandle, Specialty, CohortId ) Values ('{instructor.FirstName}', '{instructor.LastName}', '{instructor.SlackHandle}', '{instructor.Specialty}', '{instructor.CohortId}' )";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddAssignment(int exerciseId, int studentId, int instructorId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // More string interpolation
                    cmd.CommandText = $"INSERT INTO Assignments (ExerciseId, StudentId, InstructorId ) Values ({exerciseId}, {studentId},{instructorId})";
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
