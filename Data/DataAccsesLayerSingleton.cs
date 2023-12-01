using Data.Models;
using Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataAccsesLayerSingleton
    {
        #region Singleton
        private DataAccsesLayerSingleton() { }
        private static DataAccsesLayerSingleton instance;
        public static DataAccsesLayerSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataAccsesLayerSingleton();
                }
                return instance;
            }
        }
        #endregion
        #region seed
        public void Seed()
        {
            using var ctx = new StudentsDBContext();
            ctx.Add(new Student
            {
                Name = "Marin Chitac",
                Age = 43,
                Address = new Address
                {
                    City = "Iasi",
                    Street = "Revolutiei",
                    Nr = 32
                }
            });
            ctx.Add(new Student
            {
                Name = "Florin Dumitrescu",
                Age = 38,
                Address = new Address
                {
                    City = "Bucuresti",
                    Street = "Petei ",
                    Nr = 14
                }
            });
            ctx.Add(new Student
            {
                Name = "Ionel Lupu",
                Age = 23,
                Address = new Address
                {
                    City = "Cluj",
                    Street = "Centru",
                    Nr = 14
                }
            });
            ctx.Add(new Student
            {
                Name = "MIhael Popa",
                Age = 18,
                Address = new Address
                {
                    City = "Deva",
                    Street = "Municipala",
                    Nr = 14
                }
            });
            ctx.Add(new Student
            {
                Name = "Alexandra Stan",
                Age = 19,
                Address = new Address
                {
                    City = "Oradea",
                    Street = "Aradului",
                    Nr = 56
                }
            });

            ctx.SaveChanges();
        }
        #endregion

        public IEnumerable<Student> GetAllStudents()
        {
            using var ctx = new StudentsDBContext();
            return ctx.Students.ToList();

        }
        public Student GetStudentById(int id)
        {
            using var ctx = new StudentsDBContext();
            var student = ctx.Students.FirstOrDefault(s => s.Id == id);
            if (student== null)
            {
                throw new InvalidIdException($"Student with id :{id} not found ");
            }
            return student;

        }
        /* public Student CreatStudent( string name,int age)
         {
             using var ctx = new StudentsDBContext();
             var student = new Student { Name = name, Age = age };
             ctx.Add(student);
             ctx.SaveChanges();
             return student;
         }*/

        public Student CreatStudent(Student student)
        {
            using var ctx = new StudentsDBContext();
            if (ctx.Students.Any(s => s.Id == student.Id))
            {
                //to do throw new exception
            }
            ctx.Add(student);
            ctx.SaveChanges();
            return student;
        }
        public Student UpdateStudent(Student studentToUpdate)
        {
            using var ctx = new StudentsDBContext();

            var student = ctx.Students.FirstOrDefault(s => s.Id == studentToUpdate.Id);
            if (student == null)
            {
                student = new Student();
                ctx.Add(student);
            }
            student.Name = studentToUpdate.Name;
            student.Age = studentToUpdate.Age;
            ctx.SaveChanges();
            return student;
        }
        public bool UpdateStudentAddress(int studentId,Address addressToUpdate)
        {
            using var ctx = new StudentsDBContext();
            var student = ctx.Students.Include(s=>s.Address).FirstOrDefault(s => s.Id == studentId);
            if(student == null)
            {
                //throw Exp
            }
            var created = false;
            if (student.Address == null)
            {
              student.Address= new Address();
                created= true;
            }
            student.Address.Street = addressToUpdate.Street;
            student.Address.City = addressToUpdate.City;
            student.Address.Nr = addressToUpdate.Nr;
            ctx.SaveChanges();
            return created;
        }
        public void DeleteStudent(int studentId)
        {
            using var ctx = new StudentsDBContext();
            var student = ctx.Students.FirstOrDefault(s=>s.Id == studentId);
            if (student == null)
            {
                throw new InvalidIdException($"Student with id :{studentId} not found ");
            }
            ctx.Students.Remove(student);
            ctx.SaveChanges();
        }

        //Part 2

        public Subject CreatSubject(Subject subject)
        {
            using var ctx = new StudentsDBContext();
            if (ctx.Subjects.Any(s => s.Id == subject.Id))
            {
                //to do throw new exception
            }
            ctx.Add(subject);
            ctx.SaveChanges();
            return subject;
        }

        public Mark AddMark(Mark markToAdd, int studentId)
        {
            using var ctx = new StudentsDBContext();
            if (!ctx.Students.Any(s => s.Id == studentId) && (ctx.Subjects.Any(sb => sb.Id == markToAdd.SubjectId)))
            {
                throw new InvalidIdException("Student not found!!!");
            }

            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);
            var mark = new Mark
            {
                Value = markToAdd.Value,
                Date = markToAdd.Date,
                SubjectId = markToAdd.SubjectId,
            };

            student.Marks.Add(mark);
            ctx.SaveChanges();
            return mark;        
        }
        public List<Mark> AllMarksByStudent(int studentId)
        {
            using var ctx = new StudentsDBContext();
            if (!ctx.Students.Any(s => s.Id == studentId))
            {
                throw new InvalidIdException("Student not found!!!");
            }
            var student = ctx.Students.Include(s=>s.Marks).FirstOrDefault(s=>s.Id == studentId);
            return student.Marks;

        }

        public List<Mark> AllStudentMarksBySubject(int studentId,int subjectId)
        {
            using var ctx = new StudentsDBContext();
            if (!ctx.Students.Any(s => s.Id == studentId) && (ctx.Subjects.Any(sb => sb.Id == subjectId)))
            {
                throw new InvalidIdException("Student not found!!!");
            }
            var student = ctx.Students.Include(s => s.Marks).FirstOrDefault(s => s.Id == studentId);
            return student.Marks.Where(m=>m.SubjectId== subjectId).ToList();

        }

        public double GetAverage(int studentId, int subjectId)
        {
            using var ctx = new StudentsDBContext();
            if (!ctx.Students.Any(s => s.Id == studentId) && (ctx.Subjects.Any(sb => sb.Id == subjectId)))
            {
                throw new InvalidIdException("Student not found!!!");
            }
            var student = ctx.Students.Include(s => s.Marks).FirstOrDefault(s => s.Id == studentId);
            var average = student.Marks.Where(mr=>mr.SubjectId==subjectId).Select(v => v.Value).Average();
            return average;

        }
        public IEnumerable<Student>GetAllStudentsByAverage()
        {
            using var ctx = new StudentsDBContext();
            return ctx.Students.Include(m=>m.Marks).OrderBy(s=>s.Marks.Average(m=>m.Value)).ToList();
        }
    }
}
