using System.Collections.Generic;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaAdjunto
{
    public class FormacionAcademicaAdjuntoListVm
    {
        public IList<FormacionAcademicaAdjuntoDto> FormacionesAcademicas { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
