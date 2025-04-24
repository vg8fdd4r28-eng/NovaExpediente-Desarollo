using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesPorPerfil;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresAsignadosDM;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresConsulta;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresCV;
using NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaborador;
using NovaExpediente.Application.MasterData.Queries.GetDetalleSolicitud;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface INovaExpedienteDbContext
    {
        #region DBSets
        DbSet<GradoAcademico> GradosAcademicos { get; set; }
        DbSet<CentroEstudio> CentroEstudios { get; set; }
        DbSet<Clasificador> Clasificadores { get; set; }
        DbSet<Colaborador> Colaboradores { get; set; }
        DbSet<FormacionAcademica> FormacionesAcademicas { get; set; }
        DbSet<EspecialidadConsultor> EspecialidadConsultors { get; set; }
        DbSet<Especialidad> Especialidades { get; set; }
        DbSet<IdiomaColaborador> IdiomaColaboradors { get; set; }
        DbSet<CertificacionColaborador> CertificacionColaboradors { get; set; }
        DbSet<Curso> Cursos { get; set; }
        DbSet<CursoColaborador> CursoColaboradors { get; set; }
        DbSet<CursoImpartir> CursoImpartirs { get; set; }
        DbSet<HabilidadBlanda> HabilidadBlandas { get; set; }
        DbSet<HabilidadColaborador> HabilidadColaboradors { get; set; }
        DbSet<ExperienciaBaseDatos> ExperienciasBaseDatos { get; set; }
        DbSet<ExperienciaLenguaje> ExperienciaLenguajes { get; set; }
        DbSet<ExperienciaAmbienteDesarrollo> ExperienciaAmbienteDesarrollos { get; set; }
        DbSet<ExperienciaTecnologia> ExperienciaTecnologias { get; set; }
        DbSet<ExperienciaERP> ExperienciaERPs { get; set; }
        DbSet<ExperienciaCalidad> ExperienciaCalidads { get; set; }
        DbSet<ExperienciaInfraestructura> ExperienciaInfraestructuras { get; set; }
        DbSet<ExperienciaBus> ExperienciaBuss { get; set; }
        DbSet<ExperienciaBPM> ExperienciaBPMs { get; set; }
        DbSet<ProveedorCertificacion> ProveedorCertificacions { get; set; }
        DbSet<Proyecto> Proyectos { get; set; }
        DbSet<ProyectoAmbiente> ProyectoAmbientes { get; set; }
        DbSet<ProyectoBaseDatos> ProyectoBaseDatoss { get; set; }
        DbSet<ProyectoLenguaje> ProyectoLenguajes { get; set; }
        DbSet<ProyectoTecnologia> ProyectoTecnologias { get; set; }
        DbSet<ProyectoClasificador> ProyectoClasificadors { get; set; }
        DbSet<Puesto> Puestos { get; set; }
        DbSet<Sector> Sectors { get; set; }

        DbSet<TipoCore> TipoCores { get; set; }
        DbSet<EmpresaCorporacion> EmpresaCorporacions { get; set; }
        DbSet<Empresa> Empresas { get; set; }

        DbSet<ColaboradorProyecto> ColaboradorProyectos { get; set; }
        DbSet<AreaOrganizativa> AreaOrganizativas { get; set; }
        DbSet<Idioma> Idiomas { get; set; }
        DbSet<NivelIdioma> NivelIdiomas { get; set; }
        DbSet<Certificacion> Certificacions { get; set; }
        DbSet<ExperienciaProyecto> ExperienciaProyectos { get; set; }
        DbSet<ExperienciaProyectoContacto> ExperienciaProyectoContactos { get; set; }
        DbSet<TecnologiaProyecto> TecnologiaProyectos { get; set; }
        DbSet<HerramientaProyecto> HerramientaProyectos { get; set; }
        DbSet<BaseDatos> BasesDatos { get; set; }
        DbSet<LenguajeProgramacion> LenguajeProgramacions { get; set; }
        DbSet<AmbienteDesarrollo> AmbienteDesarrollos { get; set; }
        DbSet<Tecnologia> Tecnologias { get; set; }
        DbSet<ErpCore> ErpCores { get; set; }
        DbSet<Calidad> Calidads { get; set; }
        DbSet<Infraestructura> Infraestructuras { get; set; }
        DbSet<Bus> Buss { get; set; }
        DbSet<BPM> BPMs { get; set; }
        DbSet<Provincia> Provincias { get; set; }
        DbSet<Canton> Cantones { get; set; }
        DbSet<Pais> Paises { get; set; }
        DbSet<Cliente> Clientess { get; set; }
        DbSet<CertificacionProveedor> CertificacionProveedors { get; set; }
        DbSet<Proyecto> Proyectoss { get; set; }

        DbSet<Referencia> Referenciass { get; set; }

        DbSet<TipoSolicitud> TipoSolicitud { get; set; }
        DbSet<TipoSolicitudPerfil> TipoSolicitudPerfil { get; set; }
        DbSet<SolicitudMasterData> SolicitudMasterData { get; set; }
        DbSet<TransicionTipoSolicitud> TransicionTipoSolicitud { get; set; }
        DbSet<SolicitudComentario> SolicitudComentario { get; set; }
        DbSet<Distrito> Distritos { get; set; }
        DbSet<AfiliacionColaborador> AfiliacionColaborador { get; set; }

        DbSet<Domain.Entities.Parametros> Parametross { get; set; }
        DbSet<Domain.Entities.Bitacora> Bitacoras { get; set; }
        DbSet<Domain.Entities.Vacacion> Vacacions { get; set; }
        DbSet<Domain.Entities.Incapacidad> Incapacidads { get; set; }
        DbSet<Domain.Entities.CargaIncapacidades> CargaIncapacidadess { get; set; }

        DbSet<Domain.Entities.CurriculumVitae> CurriculumsVitaes { get; set; }

        DbSet<Domain.Entities.CargaVacaciones> CargaVacacioness { get; set; }

        DbSet<Domain.Entities.Permiso> Permisos { get; set; }
        DbSet<Domain.Entities.TipoPermisoGoce> TipoPermisoGoces { get; set; }
        DbSet<Feriado> Feriados { get; set; }
        DbSet<Vacuna> Vacunas { get; set; }
        DbSet<VacunasColaborador> VacunasColaboradores { get; set; }
        DbSet<VacunacionColaborador> VacunacionColaboradores { get; set; }
        DbSet<Cloud> Cloudss { get; set; }
        DbSet<ExperienciaCloud> ExperienciaClouds { get; set; }
        DbSet<NoticiaCategoria> NoticiaCategoria { get; set; }
        DbSet<ConvenioCategoria> ConvenioCategorias { get; set; }
        DbSet<Noticia> Noticias { get; set; }
        DbSet<Convenio> Convenios { get; set; }
        DbSet<Sorteo> Sorteos { get; set; }
        DbSet<SorteoColaborador> SorteosColaboradores{ get; set; }
        DbSet<Actividad> Actividads { get; set; }
        DbSet<CatalogoValor> CatalogoValores { get; set; }
        DbSet<Evento> Eventos { get; set; }
        DbSet<EventoSeccion> EventoSecciones { get; set; }
        DbSet<ImagenEvento> ImagenEventos { get; set; }
        DbSet<Imagen> Imagenes { get; set; }


        DbSet<UnoAUnoCategoria> UnoAUnoCategorias { get; set; }
        DbSet<UnoAUnoEstadoColaborador> UnoAUnoEstadosColaborador { get; set; }
        DbSet<UnoAUnoEstado> UnoAUnoEstados { get; set; }
        DbSet<UnoAUno> UnoAUnos { get; set; }
        DbSet<UnoAUnoDetalle> UnoAUnoDetalles { get; set; }

        #endregion

        #region SaveChanges
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion

        #region Llamadas ADO.NET
        Task<List<ColaboradoresConsultaDto>> ConsultaCriterios(string query);
        Task<ColaboradoresCVDto> ConsultaCV(string query);
        Task<List<int>> SolicitudesTramitadas(int idUsuario);
        Task<List<int>> SolicitudesAdministrativasTramitadas(int idUsuario);
        Task PersistirSolicitud(int idTipoSolicitud, int idSolicitud);
        Task<Dictionary<int, Dictionary<string, int>>> ActualizacionExpedientePorDM();
        Task<Dictionary<string, decimal>> ActualizacionExpedienteTotal();
        Task PersistirBitacora(Bitacora bitacora);
        Task PersistirLog(string logType, string logCategory, string data, int? id);
        Task<Dictionary<long, int>> UsoAplicacion(DateTime fechaInicio, DateTime fechaFin);
        Task<Dictionary<string, object>> ObtenerDatosVacaciones(int idSolicitud);

        Task<Dictionary<string, object>> RegistrarColaborador(string data);
        Task<string> CargarIncapacidad(string data, string nombreArchivo, int cantRegistros, int idUsuarioCrea);
        Task<string> CargarVacaciones(string data, string nombreArchivo, int cantRegistros, int idUsuarioCrea);

        Task<DetalleSolicitudDto> ObtenerDetalleSolicitud(int idSolicitud);
        Task<Dictionary<string, int>> ObtenerConteoSolicitudes();

        Task<List<ColaboradoresAsignadosDMDto>> ConsultaColaboradoresAsignadosDM(int idDM);

        Task<int> CambiarResponsable(int idSolicitud, int idNuevoResponsable);
        Task<int> GetVacacionesAprobadas(int idColaborador);

        Task<List<ColaboradorVacacionesPorPerfilDto>> GetColaboradoresNomina();

        Task<string> ActualizaSaldoVacacionesServiciosProfesionales();
        Task<string> ActualizaSaldoVacacionesNomina(Dictionary<int, decimal> saldos);

        Task<string> ReasignarDMPorCliente(int idCliente, int idDM, int idDMOld);

        Task<List<IdiomaColaboradorDto>> ConsultaIdiomasColaboradores(string datos);
        Task<string> ConsultaNoticiasAPP(bool EsPublica, int idIdioma);

        Task<string> IngresarDenunciaAPP(string data);

        Task<string> ConsultaConveniosAPP(int idIdioma, int idCategoria);
        Task<string> ConsultaConvenioDetalleAPP(int Id);

        Task<string> ConsultaDetalleNoticia(int idNoticia);

        Task<string> ConsultaActividadesMesAPP(int mes, int anho, int idIdioma);

        Task<string> ConsultaActividadesDiaAPP(DateTime date, int idIdioma);

        Task<string> ActualizaDatosPersonalesAPP(string data);
        Task<string> ActualizaBitacoraVacaciones(bool esInsert, int idNomina, DateTime fechaInicio, DateTime fechaFin,
                                                             int cantidadDias, int idSolicitud, string mensaje, string estado);
        Task<string> InactivarCategoriaNoticias(int IdCategoria);
        Task<string> InactivarCategoriaConvenios(int IdCategoria);
        Task<string> UpsertNoticias(string data);
        Task<string> CargaImagenNoticias(string url, int idNoticia, int IdUsuarioCrea);
        Task<string> CargaImagenConvenios(string url, int idNoticia, int IdUsuarioCrea);
        Task<string> InactivarConvenio(int IdConvenio);
        Task<string> InactivarNoticia(int IdNoticia);
        Task<string> UpsertConvenios(string data);

        Task<string> ConsultaDetalleConvenio(int idConvenio);
        Task<string> EliminarNoticiaImagen(int IdNoticia, int IdImagen);
        Task<string> EliminarConvenioImagen(int IdConvenio, int IdImagen);
        Task<string> InactivaSorteo(int IdSorteo);


        Task<string> ConsultaDetalleActividad(int IdActividad);
        Task<string> InactivarActividad(int IdActividad);
        Task<string> EliminarActividadImagen(int IdActividad, int IdImagen);
        Task<string> UpsertActividades(string data);
        Task<string> CargaImagenActividades(string url, int IdActividad, int IdUsuarioCrea);


        Task<string> ConsultaDetalleSorteo(int IdSorteo);
        Task<string> UpsertSorteo(string data);
        Task<string> EliminarSorteoImagen(int IdNoticia, int IdImagen);
        Task<string> CargaImagenSorteos(string url, int IdSorteo, int IdUsuarioCrea);
        Task<string> ConsultaDetalleSorteoAPP();

        Task<string> DatosLaborales(string data);

        Task<string> ConsultaBitacoraVacaciones();

        Task<string> ReporteCertificacionesColaborador();
        Task<string> ReporteCursosColaborador();
        Task<string> ReporteFormacionColaborador();

        Task<string> ConsultaDetalleUnoaUno(int IdUnoAUno);
        Task<string> UpsertUnosaUnos(string data);
        Task<string> UpsertUnosaUnosDetalles(string data);
        Task<string> InactivarDetalleUnoaUno(int IdUnoAUnoDetalle);
        Task<int> CargarDetalleCV(string data, int idColaborador, int idUsuarioCrea);
        Task<string> ActualizarDetalleCV(string data, int idColaborador, int idUsuarioCrea, int idCurriculum);

        Task<string> ActualizarEstadoBitacora(int id, string estado, string mensaje, int idUsuarioModifica, string uuid);
        Task<Dictionary<int, Dictionary<string, int>>> ActualizacionCurriculumPorDM();
        #endregion
    }
}
