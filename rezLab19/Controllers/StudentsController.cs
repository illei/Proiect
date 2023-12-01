using Data;
using Data.Exceptions;
using Data.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using rezLab19.Dtos;
using rezLab19.Utilitys;
using System.ComponentModel.DataAnnotations;

namespace rezLab19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        
     /// <summary>
     /// Initialize DB
     /// </summary>
        [HttpPost("seed")]
        public void Seed() => DataAccsesLayerSingleton.Instance.Seed();
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<List<StudentToGetDto>>))]
        public IEnumerable<StudentToGetDto> GetAllStudents()
        {
            var allStudents = DataAccsesLayerSingleton.Instance.GetAllStudents();
            return allStudents.Select(s => s.ToDto()).ToList();
        }

        /// <summary>
        /// Gets student by Id
        /// </summary>
        /// <param name="id"> id of the students</param>
        /// <returns> student data</returns>
         
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(StudentToGetDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public ActionResult< StudentToGetDto> GetStudentById([Range(1,int.MaxValue)]int id)
        {

           
            try
            {
                return Ok(DataAccsesLayerSingleton.Instance.GetStudentById(id).ToDto());
            }
            catch (InvalidIdException ex)
            {

                return NotFound(ex.Message);  
            }   

        }
        
        /// <summary>
        /// Creats a student
        /// </summary>
        /// <param name="studentToCreate">student to create data </param>
        /// <returns> created  student data </returns>
 
        [HttpPost("/CreateStudent")]
        public StudentToGetDto CreateStudent([FromBody] StudentToCreateDto studentToCreate)=>
            DataAccsesLayerSingleton.Instance.CreatStudent(studentToCreate.ToEntity()).ToDto();

        /// <summary>
        /// Updates A Student
        /// </summary>
        /// <param name="studentToUpdate"></param>
        /// <returns> Student data</returns>
       
        [HttpPut("/UpdateStudent")]
        public StudentToGetDto UpdateStudent([FromBody] StudentToUpdateDto studentToUpdate)=>DataAccsesLayerSingleton.Instance.UpdateStudent(studentToUpdate.ToEntity()).ToDto();

        /// <summary>
        /// Modify/Creats address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="id"></param>

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult UpdateAddress([FromBody] AddressToUpdateDto address, [FromRoute] int id)
        {
            if (DataAccsesLayerSingleton.Instance.UpdateStudentAddress(id, address.ToEntity()))
            {
                return Created("succses", null);
            }
            return Ok();
        }
        /// <summary>
        /// dELETS THE  specified  student
        /// </summary>
        /// <param name="id"></param>
      
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent([Range(1, int.MaxValue)]int id)
        {
            
            try
            {
                DataAccsesLayerSingleton.Instance.DeleteStudent(id);
            }
            catch (InvalidIdException ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
           
        }

        //Part 2


        /// <summary>
        /// Creats a Subject
        /// </summary>
        /// <param name="SubjectToCreate">Subject to create data </param>
        /// <returns> created  Subject data </returns>

        [HttpPost("/CreateSubject")]
        public SubjectToCreateDto CreateSubject([FromBody] SubjectToCreateDto subjectToCreate) =>
            DataAccsesLayerSingleton.Instance.CreatSubject(subjectToCreate.ToEntity()).ToDto();
        /// <summary>
        /// Adds a mark to a student
        /// </summary>
        /// <param name="marktoAdd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("AddMark/{id}")]
        public MarkToAddDto AddMark([FromBody] MarkToAddDto marktoAdd, [FromRoute] int id) =>
            DataAccsesLayerSingleton.Instance.AddMark(marktoAdd.ToEntity(),id).ToDto();
        /// <summary>
        /// GetAllStudentMarks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetMarks/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public List<Mark> GetAllStudentMarks([FromRoute] int id) => DataAccsesLayerSingleton.Instance.AllMarksByStudent(id);

        /// <summary>
        /// GetStudentMarksbySubject
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        [HttpGet("GetMarksBySubject/{id}/{subjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]

        
        public List<Mark> GetAllStudentMarksBySubject([FromRoute] int id, [FromRoute] int subjectId) => DataAccsesLayerSingleton.Instance.AllStudentMarksBySubject(id,subjectId);

        /// <summary>
        /// Student Average 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        [HttpGet("GetStudentAvverage/{id}/{subjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public double GetStudentAvverage([FromRoute] int id, [FromRoute]int subjectId)=>DataAccsesLayerSingleton.Instance.GetAverage(id,subjectId);

        /// <summary>
        /// Students order by their Average
        /// </summary>
        /// <returns></returns>
        [HttpGet("StudentsByAverage")]
       
        public IEnumerable<StudentToGetDto> GetAllStudentsByAverage()
        {
            var allStudents = DataAccsesLayerSingleton.Instance.GetAllStudentsByAverage();
            return allStudents.Select(s => s.ToDto()).ToList();
        }
    } 

    





}
