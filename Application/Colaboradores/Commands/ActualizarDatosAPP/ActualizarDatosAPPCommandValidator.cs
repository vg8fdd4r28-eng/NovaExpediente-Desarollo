using FluentValidation;
using Newtonsoft.Json;

namespace NovaExpediente.Application.Colaboradores.Commands.ActualizarDatosAPP
{
    public class ActualizarDatosAPPCommandValidator : AbstractValidator<ActualizarDatosAPPCommand>
    {
        public ActualizarDatosAPPCommandValidator()
        {
            RuleFor(x => x.data).NotEmpty();
            RuleFor(x => x).Must(ValidaIdColaboradorNulo).WithMessage("El valor del id del colaborador es nulo");
            RuleFor(x => x).Must(ValidaIdColaborador).WithMessage("El valor del id del colaborador es menor a 0");
            RuleFor(x => x).Must(ValidaCorreoPersonal).WithMessage("El valor del correo personal es vacío o nulo");
            RuleFor(x => x).Must(ValidaFoto).WithMessage("El valor de la foto es vacío o nulo");
            RuleFor(x => x).Must(ValidaCelular).WithMessage("El valor del teléfono celular es vacío o nulo");
            RuleFor(x => x).Must(ValidaTelefono).WithMessage("El valor del teléfono de domicilio es vacío o nulo");
            RuleFor(x => x).Must(ValidaDireccion).WithMessage("El valor de la dirección es vacío o nulo");
            RuleFor(x => x).Must(ValidaTelefonoEmergencia).WithMessage("El valor del teléfono de emergencia es vacío o nulo");
            RuleFor(x => x).Must(ValidaPersonaLlamar).WithMessage("El valor de la persona a llamar es vacío o nulo");
        }
        private bool ValidaIdColaboradorNulo(ActualizarDatosAPPCommand request)
        {
            DocJsonDatosPersonalesAPP doc;
            doc = JsonConvert.DeserializeObject<DocJsonDatosPersonalesAPP>(request.data);
            if (doc.IDCOLABORADOR == null)
                return false;
            return true;
        }
        private bool ValidaIdColaborador(ActualizarDatosAPPCommand request)
        {
            DocJsonDatosPersonalesAPP doc;
            doc = JsonConvert.DeserializeObject<DocJsonDatosPersonalesAPP>(request.data);
            if (doc.IDCOLABORADOR > 0)
                return true;
            return false;
        }
        private bool ValidaCorreoPersonal(ActualizarDatosAPPCommand request)
        {
            DocJsonDatosPersonalesAPP doc;
            doc = JsonConvert.DeserializeObject<DocJsonDatosPersonalesAPP>(request.data);
            if (string.IsNullOrEmpty(doc.CORREOPERSONAL))
                return false;
            return true;
        }
        private bool ValidaFoto(ActualizarDatosAPPCommand request)
        {
            DocJsonDatosPersonalesAPP doc;
            doc = JsonConvert.DeserializeObject<DocJsonDatosPersonalesAPP>(request.data);
            if (string.IsNullOrEmpty(doc.FOTO))
                return false;
            return true;
        }
        private bool ValidaCelular(ActualizarDatosAPPCommand request)
        {
            DocJsonDatosPersonalesAPP doc;
            doc = JsonConvert.DeserializeObject<DocJsonDatosPersonalesAPP>(request.data);
            if (string.IsNullOrEmpty(doc.CELULAR))
                return false;
            return true;
        }
        private bool ValidaTelefono(ActualizarDatosAPPCommand request)
        {
            DocJsonDatosPersonalesAPP doc;
            doc = JsonConvert.DeserializeObject<DocJsonDatosPersonalesAPP>(request.data);
            if (string.IsNullOrEmpty(doc.TELEFONO))
                return false;
            return true;
        }
        private bool ValidaDireccion(ActualizarDatosAPPCommand request)
        {
            DocJsonDatosPersonalesAPP doc;
            doc = JsonConvert.DeserializeObject<DocJsonDatosPersonalesAPP>(request.data);
            if (string.IsNullOrEmpty(doc.DIRECCION))
                return false;
            return true;
        }
        private bool ValidaTelefonoEmergencia(ActualizarDatosAPPCommand request)
        {
            DocJsonDatosPersonalesAPP doc;
            doc = JsonConvert.DeserializeObject<DocJsonDatosPersonalesAPP>(request.data);
            if (string.IsNullOrEmpty(doc.TELEFONOEMERGENCIA))
                return false;
            return true;
        }
        private bool ValidaPersonaLlamar(ActualizarDatosAPPCommand request)
        {
            DocJsonDatosPersonalesAPP doc;
            doc = JsonConvert.DeserializeObject<DocJsonDatosPersonalesAPP>(request.data);
            if (string.IsNullOrEmpty(doc.PERSONALLAMAR))
                return false;
            return true;
        }
    }
    public class DocJsonDatosPersonalesAPP
    {
        public int? IDCOLABORADOR { get; set; }
        public int? IDPAISRESIDENCIA { get; set; }
        public string CORREOPERSONAL { get; set; }
        public string FOTO { get; set; }
        public string CELULAR { get; set; }
        public string TELEFONO { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONOEMERGENCIA { get; set; }
        public string PERSONALLAMAR { get; set; }
    }
}