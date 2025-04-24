using FluentValidation;


namespace NovaExpediente.Application.AmbientesDesarrollo.Commands.UpsertAmbientesDesarrollo
{
    class UpsertAmbientesDesarrolloCommandValidator :AbstractValidator<UpsertAmbientesDesarrolloCommand>
    {
        public UpsertAmbientesDesarrolloCommandValidator()
        {

            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Descripcion).MaximumLength(500);
            RuleFor(x => x.Versiones).MaximumLength(500);
        }
    }
}
