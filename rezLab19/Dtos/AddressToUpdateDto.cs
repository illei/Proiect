using System.ComponentModel.DataAnnotations;

namespace rezLab19.Dtos
{ /// <summary>
  /// Addres taht will be used for update 
  /// </summary>
    public class AddressToUpdateDto
    {
        /// <summary>
        /// City Name
        /// </summary>
        /// 
        [Required(AllowEmptyStrings =false,ErrorMessage ="City is mandatory")]
        public string City { get; set; }
        /// <summary>
        /// Strret Name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Street is mandatory")]
        public string Street { get; set; }
        /// <summary>
        /// House/App Nr
        /// </summary>
        [Range(1,int.MaxValue)]
        public int Nr { get; set; }
    }
}
