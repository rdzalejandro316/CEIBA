using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoBibliotecario.Domain.Dtos
{
    public class CreateLoanDto
    {
        
        [Required(ErrorMessage = "El 'Isbn' es obligatorio.")]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "No se puede usar Guid")]
        [NonEmptyGuid]
        public Guid Isbn { get; set; }

        [Required(ErrorMessage = "El 'IdentificacionUsuario' es obligatorio.")]
        [StringLength(10, ErrorMessage = "El campo IdentificacionUsuario debe ser menor de 10 caracteres")]
        public string IdentificacionUsuario { get; set; }

        [Required(ErrorMessage = "El 'TipoUsuario' es obligatorio.")]
        [Range(1, 3, ErrorMessage = "El 'TipoUsuario' debe estar entre 1 y 3")]
        public int TipoUsuario { get; set; }
    }

    public class CreateLoanResponseDto
    {
        public Guid Id { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }
    }

    public class GetLoanDto
    {
        [Required(ErrorMessage = "El 'Id' es obligatorio.")]
        public Guid Id { get; set; }
    }

    public class LoanDto
    {
        public Guid Id { get; set; }
        public Guid Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuario { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }
    }

    internal class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value is Guid) && Guid.Empty == (Guid)value)            
            {
                return new ValidationResult("Guid no puede estar vacion");
            }
            return null;
        }
    }

}
