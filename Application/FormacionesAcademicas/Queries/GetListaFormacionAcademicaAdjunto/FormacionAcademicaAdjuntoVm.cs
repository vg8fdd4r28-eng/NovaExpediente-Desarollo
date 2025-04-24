using System.Collections.Generic;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaAdjunto
{
    public class FormacionAcademicaAdjuntoVm
    {
        public IList<FormacionAcademicaAdjuntoDto> FormacionesAcademicas { get; set; }

        public int Count { get; set; }
    }
}
