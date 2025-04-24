using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesPorPerfil;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresAsignadosDM;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresConsulta;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresCV;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaborador;
using NovaExpediente.Application.MasterData.Queries.GetDetalleSolicitud;
using NovaExpediente.Common;
using NovaExpediente.Domain.Common;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Persistence
{
    public class NovaExpedienteDbContext : DbContext, INovaExpedienteDbContext
    {
        #region Fields
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        #endregion

        #region Constructors
        public NovaExpedienteDbContext(DbContextOptions<NovaExpedienteDbContext> options)
            : base(options)
        {
        }

        public NovaExpedienteDbContext(
            DbContextOptions<NovaExpedienteDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }
        #endregion

        #region DBSets
        public DbSet<GradoAcademico> GradosAcademicos { get; set; }
        public DbSet<Clasificador> Clasificadores { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<FormacionAcademica> FormacionesAcademicas { get; set; }
        public DbSet<EspecialidadConsultor> EspecialidadConsultors { get; set; }
        public DbSet<IdiomaColaborador> IdiomaColaboradors { get; set; }
        public DbSet<CertificacionColaborador> CertificacionColaboradors { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoColaborador> CursoColaboradors { get; set; }
        public DbSet<CursoImpartir> CursoImpartirs { get; set; }
        public DbSet<HabilidadColaborador> HabilidadColaboradors { get; set; }
        public DbSet<ExperienciaBaseDatos> ExperienciasBaseDatos { get; set; }
        public DbSet<ExperienciaLenguaje> ExperienciaLenguajes { get; set; }
        public DbSet<ExperienciaAmbienteDesarrollo> ExperienciaAmbienteDesarrollos { get; set; }
        public DbSet<ExperienciaTecnologia> ExperienciaTecnologias { get; set; }
        public DbSet<ExperienciaERP> ExperienciaERPs { get; set; }
        public DbSet<ExperienciaCalidad> ExperienciaCalidads { get; set; }
        public DbSet<ExperienciaInfraestructura> ExperienciaInfraestructuras { get; set; }
        public DbSet<ExperienciaBus> ExperienciaBuss { get; set; }
        public DbSet<ExperienciaBPM> ExperienciaBPMs { get; set; }

        public DbSet<ProveedorCertificacion> ProveedorCertificacions { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<ProyectoAmbiente> ProyectoAmbientes { get; set; }
        public DbSet<ProyectoBaseDatos> ProyectoBaseDatoss { get; set; }
        public DbSet<ProyectoLenguaje> ProyectoLenguajes { get; set; }
        public DbSet<ProyectoTecnologia> ProyectoTecnologias { get; set; }
        public DbSet<ProyectoClasificador> ProyectoClasificadors { get; set; }
        public DbSet<ColaboradorProyecto> ColaboradorProyectos { get; set; }
        public DbSet<EmpresaCorporacion> EmpresaCorporacions { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<ExperienciaProyecto> ExperienciaProyectos { get; set; }
        public DbSet<ExperienciaProyectoContacto> ExperienciaProyectoContactos { get; set; }
        public DbSet<TecnologiaProyecto> TecnologiaProyectos { get; set; }
        public DbSet<HerramientaProyecto> HerramientaProyectos { get; set; }



        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<TipoCore> TipoCores { get; set; }
        public DbSet<Canton> Cantones { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<NivelIdioma> NivelIdiomas { get; set; }
        public DbSet<Certificacion> Certificacions { get; set; }
        public DbSet<HabilidadBlanda> HabilidadBlandas { get; set; }
        public DbSet<NivelConocimiento> NivelConocimientos { get; set; }
        public DbSet<BaseDatos> BasesDatos { get; set; }
        public DbSet<LenguajeProgramacion> LenguajeProgramacions { get; set; }
        public DbSet<AmbienteDesarrollo> AmbienteDesarrollos { get; set; }
        public DbSet<Tecnologia> Tecnologias { get; set; }
        public DbSet<ErpCore> ErpCores { get; set; }
        public DbSet<Calidad> Calidads { get; set; }
        public DbSet<Infraestructura> Infraestructuras { get; set; }
        public DbSet<CentroEstudio> CentroEstudios { get; set; }
        public DbSet<Bus> Buss { get; set; }
        public DbSet<BPM> BPMs { get; set; }
        public DbSet<AreaOrganizativa> AreaOrganizativas { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Cliente> Clientess { get; set; }
        public DbSet<CertificacionProveedor> CertificacionProveedors { get; set; }
        public DbSet<Proyecto> Proyectoss { get; set; }
        public DbSet<Referencia> Referenciass { get; set; }

        public DbSet<TipoSolicitud> TipoSolicitud { get; set; }
        public DbSet<SolicitudMasterData> SolicitudMasterData { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<TipoSolicitudPerfil> TipoSolicitudPerfil { get; set; }
        public DbSet<TransicionTipoSolicitud> TransicionTipoSolicitud { get; set; }
        public DbSet<SolicitudComentario> SolicitudComentario { get; set; }

        public DbSet<AfiliacionColaborador> AfiliacionColaborador { get; set; }

        public DbSet<Domain.Entities.Parametros> Parametross { get; set; }
        public DbSet<Domain.Entities.Bitacora> Bitacoras { get; set; }
        public DbSet<Domain.Entities.Vacacion> Vacacions { get; set; }
        public DbSet<Domain.Entities.Incapacidad> Incapacidads { get; set; }
        public DbSet<Domain.Entities.CargaIncapacidades> CargaIncapacidadess { get; set; }
        public DbSet<Domain.Entities.CurriculumVitae> CurriculumsVitaes { get; set; }
        public DbSet<Domain.Entities.CargaVacaciones> CargaVacacioness { get; set; }
        public DbSet<Domain.Entities.Permiso> Permisos { get; set; }
        public DbSet<Domain.Entities.TipoPermisoGoce> TipoPermisoGoces { get; set; }
        public DbSet<Feriado> Feriados { get; set; }
        public DbSet<Vacuna> Vacunas { get; set; }
        public DbSet<VacunasColaborador> VacunasColaboradores { get; set; }
        public DbSet<VacunacionColaborador> VacunacionColaboradores { get; set; }
        public DbSet<Cloud> Cloudss { get; set; }
        public DbSet<ExperienciaCloud> ExperienciaClouds { get; set; }
        public DbSet<NoticiaCategoria> NoticiaCategoria { get; set; }
        public DbSet<ConvenioCategoria> ConvenioCategorias { get; set; }


        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Convenio> Convenios { get; set; }
        public DbSet<Sorteo> Sorteos { get; set; }
        public DbSet<SorteoColaborador> SorteosColaboradores { get; set; }
        public DbSet<Actividad> Actividads { get; set; }
        public DbSet<CatalogoValor> CatalogoValores { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<EventoSeccion> EventoSecciones { get; set; }
        public DbSet<ImagenEvento> ImagenEventos { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }


        public DbSet<UnoAUnoCategoria> UnoAUnoCategorias { get; set; }
        public DbSet<UnoAUnoEstadoColaborador> UnoAUnoEstadosColaborador { get; set; }
        public DbSet<UnoAUnoEstado> UnoAUnoEstados { get; set; }
        public DbSet<UnoAUno> UnoAUnos { get; set; }
        public DbSet<UnoAUnoDetalle> UnoAUnoDetalles { get; set; }

        #endregion




        #region Overrided Methods
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.IDUSUARIOCREA = entry.Entity.IDUSUARIOCREA == 0 ? _currentUserService.UserId : entry.Entity.IDUSUARIOCREA;
                        entry.Entity.FECHACREACION = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.IDUSUARIOMODIFICA = entry.Entity.IDUSUARIOMODIFICA == 0 ? _currentUserService.UserId : entry.Entity.IDUSUARIOMODIFICA;
                        entry.Entity.FECHAMODIFICACION = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NovaExpedienteDbContext).Assembly);
        }
        #endregion

        #region Custom Methods

        public async Task<List<ColaboradoresConsultaDto>> ConsultaCriterios(string query)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                var queryParam = new SqlParameter("datosCriterios", query);
                List<ColaboradoresConsultaDto> lista = new List<ColaboradoresConsultaDto>();

                command.CommandText = "NE.USP_CONSULTA";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(queryParam);
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    ColaboradoresConsultaDto colaborador = new ColaboradoresConsultaDto()
                    {
                        IDCOLABORADOR = dataReader.GetInt32("idcolaborador"),
                        NOMBRECOMPLETO = dataReader.GetString("nombrecompleto"),
                        IDENTIFICACION = dataReader.GetString("identificacion"),
                        DELIVERYMANAGER = dataReader.GetString("deliverymanager"),
                        RANKING = dataReader.GetDecimal("ranking")
                    };
                    lista.Add(colaborador);
                }
                dataReader.Close();
                this.Database.CloseConnection();

                return lista;
            }
        }

        public async Task<ColaboradoresCVDto> ConsultaCV(string query)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                var queryParam = new SqlParameter("criterios", query);
                // List<ColaboradoresCVDto> lista = new List<ColaboradoresCVDto>();
                ColaboradoresCVDto cv = new ColaboradoresCVDto();

                command.CommandText = "NE.USP_Genera_Json_CV";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(queryParam);
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {


                    cv.JsonResult += dataReader.GetString(0);


                }



                dataReader.Close();
                this.Database.CloseConnection();

                return cv;
            }
        }


        public async Task<List<int>> SolicitudesTramitadas(int idUsuario)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                var queryParam = new SqlParameter("IdUsuario", idUsuario);
                List<int> lista = new List<int>();

                command.CommandText = "NE.USP_SOLICITUDES_TRAMITADAS";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(queryParam);
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    lista.Add(dataReader.GetInt32(0));
                }

                dataReader.Close();
                this.Database.CloseConnection();

                return lista;
            }
        }

        public async Task<List<int>> SolicitudesAdministrativasTramitadas(int idUsuario)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                var queryParam = new SqlParameter("IdUsuario", idUsuario);
                List<int> lista = new List<int>();

                command.CommandText = "NE.USP_SOLICITUDES_TRAMITADAS_ADMIN";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(queryParam);
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    lista.Add(dataReader.GetInt32(0));
                }

                dataReader.Close();
                this.Database.CloseConnection();

                return lista;
            }
        }
        public async Task PersistirSolicitud(int idTipoSolicitud, int idSolicitud)
        {
            // se obtiene el procedimiento almacenado a ejecutar segun el tipo de solicitud
            var tipoSolicitud = this.TipoSolicitud
                                    .Where(p => p.IDTIPOSOLICITUD == idTipoSolicitud)
                                    .First();

            string spName = $"NE.{tipoSolicitud.NOMBREPROCEDIMIENTO}";


            if (!spName.Equals(string.Empty))
            {
                try
                {
                    // se ejecuta el procedimiento almacenado
                    using var command = this.Database.GetDbConnection().CreateCommand();
                    var queryParam = new SqlParameter("idSolicitud", idSolicitud);

                    command.CommandTimeout = 0;
                    command.CommandText = spName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(queryParam);
                    this.Database.OpenConnection();

                    await command.ExecuteNonQueryAsync();

                    this.Database.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    // en caso de error se asocia la excepción a la solicitud
                    SolicitudMasterData solicitud = await this.SolicitudMasterData.FindAsync(idSolicitud);
                    solicitud.RESULTADOACTUALIZA = ex.ToString();
                    solicitud.ESTADOSOLICITUD = "PE";
                    await this.SaveChangesAsync();
                }
            }
        }

        public async Task PersistirLog(string logType, string logCategory, string data, int? id)
        {

            string spName = $"NE.USP_PERSISTIRLOGGING";

            // se ejecuta el procedimiento almacenado
            using var command = this.Database.GetDbConnection().CreateCommand();

            command.CommandTimeout = 0;
            command.CommandText = spName;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("logType", logType));
            command.Parameters.Add(new SqlParameter("logCategory", logCategory));
            command.Parameters.Add(new SqlParameter("data", data));
            command.Parameters.Add(new SqlParameter("id", id));
            this.Database.OpenConnection();

            await command.ExecuteNonQueryAsync();

            this.Database.CloseConnection();
        }

        public async Task<Dictionary<string, object>> ObtenerDatosVacaciones(int idSolicitud)
        {
            string spName = $"NE.USP_OBTENERDATOSVACACIONES";
            Dictionary<string, object> resultado = new Dictionary<string, object>();
            // se ejecuta el procedimiento almacenado
            using var command = this.Database.GetDbConnection().CreateCommand();

            command.CommandTimeout = 0;
            command.CommandText = spName;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("idSolicitud", idSolicitud));
            this.Database.OpenConnection();

            var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                resultado = new Dictionary<string, object>() {
                    { "IDNOMINA", reader.GetInt32("IDNOMINA") },
                    { "FECHAINICIO", reader.GetDateTime("FECHAINICIO") },
                    { "FECHAFINAL", reader.GetDateTime("FECHAFINAL") },
                    { "CANTIDADDIAS", reader.GetInt32("CANTIDADDIAS") }
                };
                break;
            }
            reader.Close();
            this.Database.CloseConnection();

            return resultado;
        }
        public async Task PersistirBitacora(Bitacora bitacora)
        {
            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_PERSISTIR_BITACORA";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@correo", bitacora.CORREO));
                command.Parameters.Add(new SqlParameter("@nombreusuario", bitacora.NOMBREUSUARIO));
                command.Parameters.Add(new SqlParameter("@opcion", bitacora.OPCION));
                command.Parameters.Add(new SqlParameter("@nombreopcion", bitacora.NOMBREOPCION));
                command.Parameters.Add(new SqlParameter("@parametros", bitacora.PARAMETROS));
                command.Parameters.Add(new SqlParameter("@fechavisita", bitacora.FECHAVISITA));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();
            }
            catch (System.Exception ex)
            {
            }
        }
        public async Task<Dictionary<long, int>> UsoAplicacion(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {

                Dictionary<long, int> resultado = new Dictionary<long, int>();

                command.CommandText = "NE.USP_USO_APLICACION";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("fechaInicio", fechaInicio));
                command.Parameters.Add(new SqlParameter("fechaFin", fechaFin));
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado.Add(dataReader.GetInt64(0), dataReader.GetInt32(1));
                }

                dataReader.Close();
                this.Database.CloseConnection();

                return resultado;
            }
        }
        public async Task<Dictionary<int, Dictionary<string, int>>> ActualizacionExpedientePorDM()
        {
            Dictionary<int, Dictionary<string, int>> diccionario = new Dictionary<int, Dictionary<string, int>>();
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                List<int> lista = new List<int>();

                command.CommandText = "NE.USP_ACTUALIZACION_EXPEDIENTE_DM";
                command.CommandType = CommandType.StoredProcedure;
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    if (diccionario.ContainsKey(dataReader.GetInt32(0)))
                    {
                        ((Dictionary<string, int>)diccionario[dataReader.GetInt32(0)])[dataReader.GetString(1)] = dataReader.GetInt32(2);
                    }
                    else
                    {
                        diccionario.Add(dataReader.GetInt32(0), new Dictionary<string, int>() {
                            { "1-Verde", 0},
                            { "2-Amarillo", 0},
                            { "3-Rojo", 0},
                        });
                        ((Dictionary<string, int>)diccionario[dataReader.GetInt32(0)])[dataReader.GetString(1)] = dataReader.GetInt32(2);
                    }
                }

                dataReader.Close();
                this.Database.CloseConnection();

                return diccionario;
            }
        }

        public async Task<Dictionary<int, Dictionary<string, int>>> ActualizacionCurriculumPorDM()
        {
            // Inicializar el diccionario que almacenará el resultado
            Dictionary<int, Dictionary<string, int>> diccionario = new Dictionary<int, Dictionary<string, int>>();
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                List<int> lista = new List<int>();

                command.CommandText = "NE.USP_ACTUALIZACION_CURRICULUM_VITAE_DM";
                command.CommandType = CommandType.StoredProcedure;

                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                // Leer los datos retornados por el procedimiento
                while (dataReader.Read())
                {
                    // Obtener el ID del Delivery Manager (índice 0)
                    int idDM = dataReader.GetInt32(0);
                    // Obtener el estado del CV ("1-Verde", "2-Amarillo", "3-Rojo") (índice 1)
                    string estado = dataReader.GetString(1);
                    // Obtener la cantidad de colaboradores en ese estado (índice 2)
                    int cantidad = dataReader.GetInt32(2);

                    // Si ya existe un registro para ese Delivery Manager en el diccionario
                    if (diccionario.ContainsKey(idDM))
                    {
                        // Actualizar la cantidad para el estado correspondiente
                        diccionario[idDM][estado] = cantidad;
                    }
                    else
                    {
                        // Si no existe, agregar una nueva entrada para el Delivery Manager con los estados inicializados en 0
                        diccionario.Add(idDM, new Dictionary<string, int>() {
                            { "1-Verde", 0},
                            { "2-Amarillo", 0},
                            { "3-Rojo", 0},
                        });
                        // Asignar la cantidad al estado correspondiente
                        diccionario[idDM][estado] = cantidad;
                    }
                }

                dataReader.Close();
                this.Database.CloseConnection();

                // Retornar el diccionario con los resultados
                return diccionario;
            }
        }

        public async Task<Dictionary<string, decimal>> ActualizacionExpedienteTotal()
        {
            Dictionary<string, decimal> diccionario = new Dictionary<string, decimal>(){
                            { "1-Verde", 0},
                            { "2-Amarillo", 0},
                            { "3-Rojo", 0},
                        };
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "NE.USP_ACTUALIZACION_EXPEDIENTE_TOTAL";
                command.CommandType = CommandType.StoredProcedure;
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    diccionario[dataReader.GetString(0)] = dataReader.GetDecimal(2);
                }

                dataReader.Close();
                this.Database.CloseConnection();

                return diccionario;
            }
        }

        public async Task<Dictionary<string, object>> RegistrarColaborador(string data)
        {
            Dictionary<string, object> resultado = new Dictionary<string, object>()
            {
                { "resultado" , "" },
                { "idColaborador" , -1 }
            };

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_REGISTROCOLABORADOR";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));
                command.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                command.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();
                resultado["idColaborador"] = command.Parameters["@ReturnValue"].Value;

                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado["resultado"] = ex.Message;
            }
            return resultado;
        }


        public async Task<DetalleSolicitudDto> ObtenerDetalleSolicitud(int idSolicitud)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                var queryParam = new SqlParameter("idSolicitud", idSolicitud);
                DetalleSolicitudDto resultado = null;
                List<ComentarioSolicitudDto> comentarios;

                command.CommandText = "NE.USP_DETALLESOLICITUD";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(queryParam);
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = new DetalleSolicitudDto()
                    {
                        IdSolicitud = dataReader.GetInt32("IdSolicitud"),
                        IdTipoSolicitud = dataReader.GetInt32("IdTipoSolicitud"),
                        CodigoEstado = dataReader.GetString("CodigoEstado"),
                        Estado = dataReader.GetString("Estado"),
                        TipoSolicitud = dataReader.GetString("TipoSolicitud"),
                        IdAutor = dataReader.GetInt32("IdAutor"),
                        Autor = dataReader.GetString("Autor"),
                        IdColaborador = dataReader.IsDBNull("IdColaborador") ? null : dataReader.GetInt32("IdColaborador"),
                        NombreColaborador = dataReader.IsDBNull("NombreColaborador") ? null : dataReader.GetString("NombreColaborador"),
                        IdResponsable = dataReader.GetInt32("IdResponsable"),
                        Responsable = dataReader.GetString("Responsable"),
                        FechaCreacion = dataReader.GetDateTime("FechaCreacion"),
                        FechaModificacion = dataReader.IsDBNull("FechaModificacion") ? null : dataReader.GetDateTime("FechaModificacion"),
                        IDUSUARIORESPONSABLE = dataReader.GetInt32("IDUSUARIORESPONSABLE"),
                        DiferenciaDias = dataReader.GetInt32("DiferenciaDias"),
                        Rango = 0,
                        DocSolicitud = dataReader.GetString("DocSolicitud"),
                    };

                }
                dataReader.NextResultAsync();

                comentarios = new List<ComentarioSolicitudDto>();
                while (dataReader.Read())
                {
                    ComentarioSolicitudDto comentario = new ComentarioSolicitudDto()
                    {
                        IdComentarioSolicitud = dataReader.GetInt32("IdComentarioSolicitud"),
                        Responsable = dataReader.GetString("Responsable"),
                        Comentario = dataReader.GetString("Comentario"),
                        Accion = dataReader.GetString("Accion"),
                        Fecha = dataReader.GetDateTime("Fecha").ToString("dd/MM/yyyy HH:mm")
                    };
                    comentarios.Add(comentario);
                }

                dataReader.Close();
                this.Database.CloseConnection();

                resultado.Comentarios = comentarios;
                return resultado;
            }
        }

        public async Task<Dictionary<string, int>> ObtenerConteoSolicitudes()
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                Dictionary<string, int> resultado = null;

                command.CommandText = "NE.USP_CONTEO_SOLICITUDES";
                command.CommandType = CommandType.StoredProcedure;
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = new Dictionary<string, int>()
                    {
                        { "TotalSolicitudes", dataReader.GetInt32("TotalSolicitudes")},
                        { "SolicitudesEnTransito", dataReader.GetInt32("SolicitudesEnTransito")}
                    };
                }

                dataReader.Close();
                this.Database.CloseConnection();

                return resultado;
            }
        }
        public async Task<string> CargarIncapacidad(string data, string nombreArchivo, int cantRegistros, int idUsuarioCrea)
        {
            string res = "";
            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CARGAINCAPACIDAD";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));
                command.Parameters.Add(new SqlParameter("@nombreArchivo", nombreArchivo));
                command.Parameters.Add(new SqlParameter("@cantRegistros", cantRegistros));
                command.Parameters.Add(new SqlParameter("@idUsuarioCrea", idUsuarioCrea));


                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();

                return res;
            }

            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> CargarVacaciones(string data, string nombreArchivo, int cantRegistros, int idUsuarioCrea)
        {
            string res = "";
            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CARGAVACACIONES";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));
                command.Parameters.Add(new SqlParameter("@nombreArchivo", nombreArchivo));
                command.Parameters.Add(new SqlParameter("@cantRegistros", cantRegistros));
                command.Parameters.Add(new SqlParameter("@idUsuarioCrea", idUsuarioCrea));


                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();

                return res;
            }

            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<ColaboradoresAsignadosDMDto>> ConsultaColaboradoresAsignadosDM(int idDM)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {


                var queryParam = new SqlParameter("@idDM", idDM);
                List<ColaboradoresAsignadosDMDto> lista = new List<ColaboradoresAsignadosDMDto>();

                command.CommandText = "NE.USP_DM_CONSULTORES_ACTUALIZACION";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(queryParam);
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    ColaboradoresAsignadosDMDto colaborador = new ColaboradoresAsignadosDMDto()
                    {

                        IDENTIFICACION = dataReader.GetString("identificacion"),
                        NombreCompleto = dataReader.GetString("NOMBRE") + " " + dataReader.GetString("PRIMERAPELLIDO") + " " + dataReader.GetString("SEGUNDOAPELLIDO"),
                        UltimaFecha = dataReader.GetDateTime("Fecha de última actuaización"),
                        Status = dataReader.GetString("ESTADO"),
                        TipoSolicitud = dataReader.GetString("TipoSolicitud")
                    };
                    lista.Add(colaborador);
                }
                dataReader.Close();
                this.Database.CloseConnection();

                return lista;


            }
        }

        public async Task<int> CambiarResponsable(int idSolicitud, int idNuevoResponsable)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {

                command.CommandText = "NE.USP_MASTER_CAMBIAR_RESPONSABLE";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                command.Parameters.Add(new SqlParameter("@idNuevoResponsable", idNuevoResponsable));

                this.Database.OpenConnection();

                var registrosModificados = await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();

                if (registrosModificados != 1)
                {
                    throw new Exception("No se pudo modificar el responsable de la solicitud.");
                }

                return idSolicitud;
            }
        }



        public async Task<int> GetVacacionesAprobadas(int idColaborador)
        {
            int cantidadDias = 0;
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {

                command.CommandText = "NE.USP_GET_VACACIONES_APROBADAS";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@idColaborador", idColaborador));

                this.Database.OpenConnection();

                // var res = await command.ExecuteReaderAsync();


                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {



                    if (dataReader.GetInt32(0) != null)
                    {
                        cantidadDias += dataReader.GetInt32(0);
                    }

                }



                this.Database.CloseConnection();

                return cantidadDias;
            }
        }


        public async Task<List<ColaboradorVacacionesPorPerfilDto>> GetColaboradoresNomina()
        {

            List<ColaboradorVacacionesPorPerfilDto> lista = new List<ColaboradorVacacionesPorPerfilDto>();

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {

                command.CommandText = "NE.USP_GET_COLABORADORES_NOMINA";
                command.CommandType = CommandType.StoredProcedure;

                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {

                    ColaboradorVacacionesPorPerfilDto colaborador = new ColaboradorVacacionesPorPerfilDto()
                    {

                        IDCOLABORADOR = dataReader.GetInt32("IDCOLABORADOR"),
                        IDNOMINA = dataReader.GetInt32("IDNOMINA")
                    };
                    lista.Add(colaborador);

                }



                this.Database.CloseConnection();

                return lista;
            }
        }


        public async Task<string> ActualizaSaldoVacacionesServiciosProfesionales()
        {
            string res = "";

            // se ejecuta el procedimiento almacenado
            using var command = this.Database.GetDbConnection().CreateCommand();

            command.CommandTimeout = 0;
            command.CommandText = "NE.USP_ACTUALIZA_VACACIONES";
            command.CommandType = CommandType.StoredProcedure;

            this.Database.OpenConnection();

            await command.ExecuteNonQueryAsync();

            this.Database.CloseConnection();

            return res;
        }

        public async Task<string> ActualizaSaldoVacacionesNomina(Dictionary<int, decimal> saldos)
        {
            string res = "";

            // se ejecuta el procedimiento almacenado
            using var command = this.Database.GetDbConnection().CreateCommand();

            command.CommandTimeout = 0;
            command.CommandText = "UPDATE NE.COLABORADOR SET SALDOVACACIONESACTUAL = @saldo WHERE IDCOLABORADOR = @idcolaborador";
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("idcolaborador", null));
            command.Parameters.Add(new SqlParameter("saldo", null));

            this.Database.OpenConnection();

            foreach (var key in saldos.Keys)
            {
                command.Parameters["idcolaborador"].Value = key;
                command.Parameters["saldo"].Value = saldos[key];
                await command.ExecuteNonQueryAsync();
            }


            this.Database.CloseConnection();

            return res;
        }
        public async Task<string> ReasignarDMPorCliente(int idCliente, int idDM, int idDMOld)
        {
            string res = "";

            // se ejecuta el procedimiento almacenado
            using var command = this.Database.GetDbConnection().CreateCommand();

            command.CommandTimeout = 0;
            command.CommandText = "NE.USP_REASIGNAR_DM_POR_CLIENTE";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@idCliente", idCliente));
            command.Parameters.Add(new SqlParameter("@idDM", idDM));
            command.Parameters.Add(new SqlParameter("@idDMOld", idDMOld));

            this.Database.OpenConnection();

            await command.ExecuteNonQueryAsync();

            this.Database.CloseConnection();

            return res;
        }




        public async Task<List<IdiomaColaboradorDto>> ConsultaIdiomasColaboradores(string datos)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {


                var queryParam = new SqlParameter("@datos", datos);
                List<IdiomaColaboradorDto> lista = new List<IdiomaColaboradorDto>();

                command.CommandText = "NE.USP_VALIDANIVELIDIOMAEXISTENTE";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(queryParam);
                this.Database.OpenConnection();

                var dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    IdiomaColaboradorDto idiomaColaborador = new IdiomaColaboradorDto()
                    {

                        IDIDIOMA = dataReader.GetInt32("IDIDIOMA"),
                        IDNIVELIDIOMA = dataReader.GetInt32("IDNIVELIDIOMA"),
                        IDCOLABORADOR = dataReader.GetInt32("IDCOLABORADOR")
                    };
                    lista.Add(idiomaColaborador);
                }
                dataReader.Close();
                this.Database.CloseConnection();

                return lista;


            }
        }


        public async Task<string> ConsultaNoticiasAPP(bool esPublica, int idIdioma)
        {
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {

                string res = "";

                command.CommandText = "NE.USP_CONSULTANOTICIASAPP";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@esPublica", esPublica));
                command.Parameters.Add(new SqlParameter("@idIdioma", idIdioma));

                this.Database.OpenConnection();

                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();


                while (dataReader.Read())
                {
                    res = dataReader.GetString(0); //The 0 stands for "the 0'th column", so the first column of the result.
                                                   // Do something with this rows string, for example to put them in to a list

                }


                dataReader.Close();
                this.Database.CloseConnection();

                return res.Replace(@"\", "");


            }
        }

        public async Task<string> IngresarDenunciaAPP(string data)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_INGRESO_DENUNCIAS";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> ConsultaConveniosAPP(int idIdioma, int idCategoria)
        {
            string resultado = "";

            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CONSULTA_CONVENIOS_APP";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@idIdioma", idIdioma));
                command.Parameters.Add(new SqlParameter("@idCategoria", idCategoria));

                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetString(0); // The 0 stands for "the 0'th column", so the first column of the result
                }                                        // Do something with this rows string, for example to put them in to a list

                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> ConsultaConvenioDetalleAPP(int Id)
        {
            string resultado = "";

            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CONSULTA_CONVENIO_DETALLE_APP";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", Id));

                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetString(0); // The 0 stands for "the 0'th column", so the first column of the result
                }                                        // Do something with this rows string, for example to put them in to a list

                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> ConsultaActividadesMesAPP(int mes, int anho, int idIdioma)
        {
            string resultado = "";

            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CONSULTA_ACTIVADES_APP_X_MES";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@mes", mes));
                command.Parameters.Add(new SqlParameter("@anho", anho));
                command.Parameters.Add(new SqlParameter("@idIdioma", idIdioma));


                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetString(0);
                }

                dataReader.Close();
                this.Database.CloseConnection();

            }
            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> ConsultaActividadesDiaAPP(DateTime fecha, int idIdioma)
        {
            string resultado = "";

            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CONSULTA_ACTIVIDADES_APP_X_DIA";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@fecha", fecha));
                command.Parameters.Add(new SqlParameter("@idIdioma", idIdioma));



                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetString(0);
                }

                dataReader.Close();
                this.Database.CloseConnection();

            }
            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> ActualizaDatosPersonalesAPP(string data)
        {
            string result = "";

            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_ACTUALIZA_DATOS_PERSONALES_APP";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@data", data));

                this.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        public async Task<string> ActualizaBitacoraVacaciones(bool esInsert, int idNomina, DateTime fechaInicio, DateTime fechaFin,
                                                             int cantidadDias, int idSolicitud, string mensaje, string estado)
        {
            string resultado = "";

            try
            {

                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_ACTUALIZABITACORAVACACIONES";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@esInsert", esInsert));
                command.Parameters.Add(new SqlParameter("@idNomina", idNomina));
                command.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                command.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                command.Parameters.Add(new SqlParameter("@cantidadDias", cantidadDias));
                command.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                command.Parameters.Add(new SqlParameter("@mensaje", mensaje));
                command.Parameters.Add(new SqlParameter("@estado", estado));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();


                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> InactivarCategoriaNoticias(int IdCategoria)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_INACTIVA_CATEGORIA_NOTICIA";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", IdCategoria));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        public async Task<string> InactivarCategoriaConvenios(int IdCategoria)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_INACTIVA_CATEGORIA_CONVENIO";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", IdCategoria));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> UpsertNoticias(string data)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_UPSERT_NOTICIAS";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));

                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetInt32(0).ToString();
                }

                dataReader.Close();
                this.Database.CloseConnection();


            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }


        public async Task<string> CargaImagenNoticias(string url, int idNoticia, int IdUsuarioCrea)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CARGA_IMAGEN_NOTICIA";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@URL", url));
                command.Parameters.Add(new SqlParameter("@idNoticia", idNoticia));
                command.Parameters.Add(new SqlParameter("@IdUsuarioCrea", IdUsuarioCrea));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();

            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> CargaImagenConvenios(string url, int idConvenio, int IdUsuarioCrea)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CARGA_IMAGEN_CONVENIO";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@URL", url));
                command.Parameters.Add(new SqlParameter("@idConvenio", idConvenio));
                command.Parameters.Add(new SqlParameter("@IdUsuarioCrea", IdUsuarioCrea));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();

            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> ConsultaDetalleNoticia(int idNoticia)
        {
            string result = "";
            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CONSULTADETALLENOTICIA";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@idNoticia", idNoticia));
                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result = result.Replace(@"\", "");
            return result;
        }

        public async Task<string> InactivarNoticia(int IdNoticia)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_INACTIVA_NOTICIA";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", IdNoticia));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        public async Task<string> InactivarConvenio(int IdConvenio)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_INACTIVA_CONVENIO";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", IdConvenio));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        public async Task<string> UpsertConvenios(string data)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_UPSERT_CONVENIOS";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));

                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetInt32(0).ToString();
                }

                dataReader.Close();
                this.Database.CloseConnection();


            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        public async Task<string> ConsultaDetalleConvenio(int IdConvenio)
        {
            string result = "";
            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CONSULTA_DETALLE_CONVENIO";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@IdConvenio", IdConvenio));
                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result = result.Replace(@"\", "");
            return result;
        }

        public async Task<string> EliminarNoticiaImagen(int IdNoticia, int IdImagen)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_ELIMINA_IMAGEN_NOTICIA";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@IdNoticia", IdNoticia));
                command.Parameters.Add(new SqlParameter("@IdImagen", IdImagen));

                this.Database.OpenConnection();

                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    resultado = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> EliminarConvenioImagen(int IdConvenio, int IdImagen)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_ELIMINA_IMAGEN_CONVENIO";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@IdConvenio", IdConvenio));
                command.Parameters.Add(new SqlParameter("@IdImagen", IdImagen));

                this.Database.OpenConnection();

                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    resultado = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }

            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }


        public async Task<string> InactivaSorteo(int IdSorteo)
        {
            string result = "";
            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_INACTIVA_SORTEO";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@idSorteo", IdSorteo));
                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public async Task<string> ConsultaDetalleActividad(int IdActividad)
        {
            string result = "";
            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CONSULTA_DETALLE_ACTIVIDAD";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@IdActividad", IdActividad));
                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result = result.Replace(@"\", "");
            return result;
        }

        public async Task<string> InactivarActividad(int IdActividad)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_INACTIVA_ACTIVIDAD";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", IdActividad));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> EliminarActividadImagen(int IdActividad, int IdImagen)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_ELIMINA_IMAGEN_ACTIVIDAD";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@IdActividad", IdActividad));
                command.Parameters.Add(new SqlParameter("@IdImagen", IdImagen));

                this.Database.OpenConnection();

                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    resultado = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }

            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        public async Task<string> UpsertActividades(string data)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_UPSERT_ACTIVIDADES";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));

                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetInt32(0).ToString();
                }

                dataReader.Close();
                this.Database.CloseConnection();


            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> CargaImagenActividades(string url, int IdActividad, int IdUsuarioCrea)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CARGA_IMAGEN_ACTIVIDAD";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@URL", url));
                command.Parameters.Add(new SqlParameter("@IdActividad", IdActividad));
                command.Parameters.Add(new SqlParameter("@IdUsuarioCrea", IdUsuarioCrea));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();

            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        public async Task<string> ConsultaDetalleSorteo(int idSorteo)
        {
            string result = "";
            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CONSULTADETALLESORTEO";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@idSorteo", idSorteo));
                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result = result.Replace(@"\", "");
            return result;
        }

        public async Task<string> UpsertSorteo(string data)
        {
            string result = "";
            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_UPSERT_SORTEO";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@data", data));
                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetInt32(0).ToString();
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }
        public async Task<string> EliminarSorteoImagen(int IdSorteo, int IdImagen)
        {
            string resultado = "";
            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_ELIMINA_IMAGEN_SORTEO";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@IdSorteo", IdSorteo));
                command.Parameters.Add(new SqlParameter("@IdImagen", IdImagen));
                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    resultado = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }

            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        public async Task<string> CargaImagenSorteos(string url, int IdSorteo, int IdUsuarioCrea)
        {
            string result = "";
            try
            {
                using
                var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CARGA_IMAGEN_SORTEO";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@URL", url));
                command.Parameters.Add(new SqlParameter("@IdSorteo", IdSorteo));
                command.Parameters.Add(new SqlParameter("@IdUsuarioCrea", IdUsuarioCrea));
                Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
                Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public async Task<string> ConsultaDetalleSorteoAPP()
        {
            string result = "";
            try
            {
                using
                var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "[NE].[USP_CONSULTADETALLESORTEOAPP]";
                command.CommandType = CommandType.StoredProcedure;
                Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result = result.Replace(@"\", "");
            return result;
        }

        public async Task<string> DatosLaborales(string data)
        {
            string result = "";
            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_DATOSLABORALES";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@datos", data));
                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;



        }


        public async Task<string> ConsultaBitacoraVacaciones()
        {
            string result = "";
            try
            {
                using
                var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "[NE].[USP_CONSULTABITACORAVACACIONES]";
                command.CommandType = CommandType.StoredProcedure;
                Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result = result.Replace(@"\", "");
            return result;

        }

        public async Task<string> ReporteCertificacionesColaborador()
        {
            string result = "";
            try
            {
                using
                var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "[NE].[USP_REPORTE_CERTIFICACIONES]";
                command.CommandType = CommandType.StoredProcedure;
                Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result = result.Replace(@"\", "");
            return result;

        }



        public async Task<string> ReporteCursosColaborador()
        {
            string result = "";
            try
            {
                using
                var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "[NE].[USP_REPORTE_CURSOS]";
                command.CommandType = CommandType.StoredProcedure;
                Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result = result.Replace(@"\", "");
            return result;

        }




        public async Task<string> ReporteFormacionColaborador()
        {
            string result = "";
            try
            {
                using
                var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 0;
                command.CommandText = "[NE].[USP_REPORTE_FORMACION]";
                command.CommandType = CommandType.StoredProcedure;
                Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    result = dataReader.GetString(0);
                }
                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result = result.Replace(@"\", "");
            return result;

        }

        public async Task<string> ConsultaDetalleUnoaUno(int IdUnoAUno)
        {
            string resultado = "";

            try
            {
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_CONSULTA_DETALLE_UNOAUNO";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@IdUnoAUno", IdUnoAUno));

                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetString(0); // The 0 stands for "the 0'th column", so the first column of the result
                }                                        // Do something with this rows string, for example to put them in to a list

                dataReader.Close();
                this.Database.CloseConnection();
            }
            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> UpsertUnosaUnos(string data)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_UPSERT_UNOSAUNOS";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));

                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetInt32(0).ToString();
                }

                dataReader.Close();
                this.Database.CloseConnection();

            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> UpsertUnosaUnosDetalles(string data)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_UPSERT_UNOSAUNOSDETALLES";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));

                this.Database.OpenConnection();
                System.Data.Common.DbDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {
                    resultado = dataReader.GetInt32(0).ToString();
                }

                dataReader.Close();
                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<string> InactivarDetalleUnoaUno(int IdUnoAUnoDetalle)
        {
            string resultado = "";

            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_INACTIVA_UNOAUNODETALLE";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", IdUnoAUnoDetalle));

                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();
            }

            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public async Task<int> CargarDetalleCV(string data, int idColaborador, int idUsuarioCrea)
        {
            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_INSERT_CURRICULUMVITAE";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));
                command.Parameters.Add(new SqlParameter("@idColaborador", idColaborador));
                command.Parameters.Add(new SqlParameter("@idUsuarioCrea", idUsuarioCrea));

                this.Database.OpenConnection();

                // Ejecutar el comando y obtener el ID del registro insertado
                var result = await command.ExecuteScalarAsync();

                this.Database.CloseConnection();

                // Convertir el resultado a entero y devolverlo
                return Convert.ToInt32(result);
            }
            catch (System.Exception ex)
            {
                // En caso de error, devolver -1
                return -1;
            }
        }



        public async Task<string> ActualizarDetalleCV(string data, int idColaborador, int idUsuarioCrea, int idCurriculum)
        {
            string res = "";
            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_UPDATE_CURRICULUMVITAE";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@datos", data));
                command.Parameters.Add(new SqlParameter("@idColaborador", idColaborador));
                command.Parameters.Add(new SqlParameter("@idUsuarioCrea", idUsuarioCrea));
                command.Parameters.Add(new SqlParameter("@idCurriculum", idCurriculum));


                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();

                return res;
            }

            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> ActualizarEstadoBitacora(int id, string estado, string mensaje, int idUsuarioModifica, string uuid)
        {
            string res = "";
            try
            {
                // se ejecuta el procedimiento almacenado
                using var command = this.Database.GetDbConnection().CreateCommand();

                command.CommandTimeout = 0;
                command.CommandText = "NE.USP_ACTUALIZAR_ESTADO_BITACORA";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@estado", estado));
                command.Parameters.Add(new SqlParameter("@mensaje", mensaje));
                command.Parameters.Add(new SqlParameter("@idUsuarioModifica", idUsuarioModifica));
                command.Parameters.Add(new SqlParameter("@uuid", uuid));
                this.Database.OpenConnection();

                await command.ExecuteNonQueryAsync();

                this.Database.CloseConnection();

                return res;
            }

            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }


        #endregion

    }
}
