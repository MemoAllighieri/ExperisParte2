using FluentValidation;
using Parte2.DTOs;

namespace Parte2.Common
{
    public class ModelValidator : AbstractValidator<ProductoCommand>
    {
        public ModelValidator()
        {
            RuleFor(user => user.Nombre).NotEmpty().WithMessage("No ha indicado el nombre de producto.")
                                        .Length(1, 100).WithMessage("El nombre debe tener una longitud entre 1 y 100 caracteres.");
            RuleFor(user => user.Precio).NotNull().WithMessage("El precio no debe estar vacio.")
                                        .GreaterThan(0).WithMessage("el precio tiene que ser mayor a 0.");
            RuleFor(user => user.Stock).NotNull().WithMessage("El stock no debe estar vacio.")
                                        .GreaterThan(0).WithMessage("el stock tiene que ser mayor a 0.");
        }
    }
}
