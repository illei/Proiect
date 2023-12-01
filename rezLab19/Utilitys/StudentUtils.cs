using Data.Models;
using rezLab19.Dtos;

namespace rezLab19.Utilitys
{
    public static class StudentUtils
    {
        public static StudentToGetDto ToDto(this Student student)
        {
            if (student == null)
            {
                return null;
            }
            return new StudentToGetDto { Name = student.Name, Age = student.Age, Id = student.Id };

        }
        public static Student ToEntity(this StudentToCreateDto student)
        {
            if (student == null) { return null; }
            return new Student
            {
                Name = student.Name,
                Age = student.Age
            };
        }

        public static Student ToEntity(this StudentToUpdateDto student)
        {
            if (student == null) { return null; }
            return new Student
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age
            };
        }

        public static Address ToEntity(this AddressToUpdateDto addressToUpdate)
        {
            if (addressToUpdate == null) { return null; }
            return new Address
            {
                City = addressToUpdate.City,
                Street = addressToUpdate.Street,
                Nr = addressToUpdate.Nr,
            };
        }

        //Subject!!

        public static SubjectToCreateDto ToDto(this Subject subject)
        {
            if (subject == null) { return null; }
            return new SubjectToCreateDto
            {
                Name = subject.Name,
               
            };
        }

        public static Subject ToEntity(this SubjectToCreateDto subjectToCreate)
        {
            if (subjectToCreate==null)
            {
                return null;
            }
            return new Subject
            {
                Name = subjectToCreate.Name
            };
        }

        //Mark

        public static MarkToAddDto ToDto(this Mark mark)
        {
            if (mark == null) { return null; }
            return new MarkToAddDto
            {
                Value = mark.Value,
                Date= mark.Date,
                SubjectId= mark.SubjectId,

            };
        }

        public static Mark ToEntity(this MarkToAddDto markToAdd)
        {
            if (markToAdd == null)
            {
                return null;
            }
            return new Mark
            {
                Value= markToAdd.Value,
                Date= markToAdd.Date,   
                SubjectId= markToAdd.SubjectId,
            };
        }


    }
}
