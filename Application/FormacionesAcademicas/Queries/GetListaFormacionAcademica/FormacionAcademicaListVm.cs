using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademica
{
    public class FormacionAcademicaListVm
    {
        public IList<FormacionAcademicaDto> FormacionesAcademicas { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
