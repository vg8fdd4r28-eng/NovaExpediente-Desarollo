using FluentValidation;

namespace NovaExpediente.Application.AplicacionMovil.Actividades.Commands.UpsertActividades
{
    public class UpsertActividadesCommandValidator : AbstractValidator<UpsertActividadesCommand>
    {

        public UpsertActividadesCommandValidator()
        {
            RuleFor(x => x.data).NotEmpty();
        }



 

    }
}
