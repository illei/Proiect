using System.ComponentModel.DataAnnotations;

namespace rezLab19.Dtos
{
    public class MarkToAddDto
    {
        [Range(1, 10)]
        public int Value { get; set; }
        public DateTime Date { get; set; }

        public int SubjectId { get; set; }
    }
}
