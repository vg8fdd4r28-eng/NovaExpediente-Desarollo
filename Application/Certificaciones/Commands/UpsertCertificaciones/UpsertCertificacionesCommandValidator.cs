using FluentValidation;

namespace NovaExpediente.Application.Certificaciones.Commands.UpsertCertificaciones
{
    class UpsertCertificacionesCommandValidator : AbstractValidator<UpsertCertificacionesCommand>
    {
        public UpsertCertificacionesCommandValidator()
        {
            RuleFor(x => x.CodigoCertificacion).MaximumLength(100);
            RuleFor(x => x.Nombre).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Descripcion).MaximumLength(500); 
        }

    }
}
