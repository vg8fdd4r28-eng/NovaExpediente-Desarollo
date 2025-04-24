import Axios from "../../config/axios";

export const GetSolicitudesPorPerfil = () =>
  Axios.get("/MasterData/GetSolicitudesPorPerfil");

//buzon
export const GetSolicitudesBuzon = (id, tipo) =>
  Axios.get("/MasterData/GetSolicitudesBuzon", {
    params: { idUsuario: id, tipo: tipo }
  });

export const GuardarSolicitud = values =>
  Axios.post("/MasterData/Upsert", {
    ...values
  });

export const EnviarSolicitud = values =>
  Axios.post("/MasterData/EnviarSolicitud", {
    ...values
  });
export const EliminarSolicitud = id => Axios.delete("/MasterData/Delete/" + id);

export const GetTiposSolicitudes = () =>
  Axios.get("/TipoSolicitud/GetTiposSolicitudes");

//datos personales
export const GetAllColaboradores = () => Axios.get("/Colaborador/GetAll");
export const GetColaboradoresPerfil = id =>
  Axios.get("/MasterData/GetColaboradoresPorPerfil", {
    params: { idColaborador: id }
  });

  
  export const GetColaboradoresAllPorPerfil = id =>
  Axios.get("/MasterData/GetColaboradoresAllPorPerfil", {
    params: { idColaborador: id }
  });

export const GetAllProvincias = () => Axios.get("/Provincia/GetAll");
export const GetAllCantones = () => Axios.get("/Canton/GetAll");
export const GetAllPaises = () => Axios.get("/Pais/GetAll");

export const GetColaboradorDetail = id =>
  Axios.get("/Colaborador/GetDetail", {
    params: { id }
  });

export const GetProyectosByColaborador = id =>
  Axios.get("/ExperienciaProyecto/GetExperienciaProyecto", {
    params: { id }
  });

export const GetAllProyectos = () => Axios.get("/Proyecto/GetProyectos");

export const GetColaboradorDetailResumen = id =>
  Axios.get("/Colaborador/GetDetailResumen", {
    params: { id }
  });

export const GetDetalleSolicitud = id =>
  Axios.get("/MasterData/GetDetalleSolicitud", {
    params: { id }
  });

//Formacion academica
export const GetFormacionAcademicaColaborador = id =>
  Axios.get("/FormacionAcademica/GetByColaborador", {
    params: { id }
  });

export const GetFormacionAcademicaAdjunto = id =>
  Axios.get("/FormacionAcademica/GetFormacionAcademicaAdjunto", {
    params: { id }
  });

export const GetFormacionAcademicaDetalle = id =>
  Axios.get("/FormacionAcademica/GetDetalle", {
    params: { id }
  });

export const GetCentrosEstudio = () =>
  Axios.get("/CentroEstudio/GetCentrosEstudio");

export const GetGradosAcademicos = () => Axios.get("/GradosAcademicos/GetAll");

//Idiomas
export const GetIdiomasColaborador = id =>
  Axios.get("/IdiomaColaborador/GetIdiomasColaborador", {
    params: { id }
  });

export const GetIdiomaColaboradorAdjunto = id =>
  Axios.get("/IdiomaColaborador/GetIdiomaColaboradorAdjunto", {
    params: { id }
  });

export const GetIdiomas = () => Axios.get("/Idiomas/GetAll");

export const GetNivelIdiomaXIdioma = id =>
  Axios.get("/NivelIdioma/GetNivelIdiomaXIdioma", {
    params: { id }
  });

export const GetNivelIdiomaDetalle = id =>
  Axios.get("/NivelIdioma/GetDetalle", {
    params: { id }
  });

//Cursos

export const GetCursos = () => Axios.get("/Curso/GetCursos");

export const GetCursosColaborador = id =>
  Axios.get("/CursoColaborador/GetCursosColaborador", {
    params: { id }
  });

export const GetCursosColaboradorAdjunto = id =>
  Axios.get("/CursoColaborador/GetCursosColaboradorAdjunto", {
    params: { id }
  });

export const GetCursoColaboradorDetalle = id =>
  Axios.get("/CursoColaborador/GetCursoColaboradorDetalle", {
    params: { id }
  });

export const GetClientesEmpresa = id =>
  Axios.get("/Cliente/GetClientesEmpresa", {
    params: { id }
  });

// asignar empresa colaborador

export const GetEmpresaCorporacion = () =>
  Axios.get("/EmpresaCorporacion/GetEmpresaCorporacion");

// asignar dm colaborador

export const GetColaboradoresDM = () =>
  Axios.get("/Colaborador/GetColaboradoresDM");

export const GetColaboradoresDMAsignar = id =>
  Axios.get("/Colaborador/GetColaboradoresDMAsignar", {
    params: { id }
  });

// asignar puesto colaborador

export const GetPuestos = () => Axios.get("/Puesto/GetAll");

//asignar cliente-ubicacion colaborador

export const GetClientes = () => Axios.get("/Cliente/GetClientes");

export const GetClientesReferencias = () =>
  Axios.get("/Cliente/GetClientesReferencias");

// export const ValidaClienteDependencias = () =>
// Axios.get("/Cliente/ValidaClienteDependencias");

export const ValidaClienteDependencias = values =>
  Axios.post("/Cliente/ValidaClienteDependencias", {
    ...values
  });

// clientes

export const GetSectores = () => Axios.get("/Sector/GetAll");

export const GetClienteDetalle = id =>
  Axios.get("/Cliente/GetClienteDetalle", {
    params: { id }
  });

export const GetReferenciasXCliente = id =>
  Axios.get("/Referencia/GetReferenciasXCliente", {
    params: { id }
  });

// certificaciones

export const GetCertificaciones = () =>
  Axios.get("/Certificacion/GetCertificacion");

export const GetProveedoresXCertificacion = id =>
  Axios.get("/CertificacionProveedor/GetProveedoresXCertificacion", {
    params: { id }
  });

export const GetCertificacionesColaborador = id =>
  Axios.get("/CertificacionColaborador/GetCertificacionesColaborador", {
    params: { id }
  });

export const GetCertificacionesColaboradorAdjunto = id =>
  Axios.get("/CertificacionColaborador/GetCertificacionesColaboradorAdjunto", {
    params: { id }
  });

export const GetCertificacionColaboradorDetalle = id =>
  Axios.get("/CertificacionColaborador/GetCertificacionColaboradorDetalle", {
    params: { id }
  });

// proyectos

export const GetProyectos = () => Axios.get("/Proyecto/GetProyectos");

export const GetReferencias = () => Axios.get("/Referencia/GetReferencias");

export const GetProyectoDetalle = id =>
  Axios.get("/Proyecto/GetProyectoDetalle", {
    params: { id }
  });

// catalogos

export const GetAreasOrganizativas = () =>
  Axios.get("/AreaOrganizativa/GetAreasOrganizativas");

export const GetAreaOrganizativaDetalle = id =>
  Axios.get("/AreaOrganizativa/GetAreaOrganizativaDetalle", {
    params: { id }
  });

export const GetAmbienteDesarrollo = () =>
  Axios.get("/AmbienteDesarrollo/GetAmbienteDesarrollo");

export const GetAmbienteDesarrolloDetalle = id =>
  Axios.get("/AmbienteDesarrollo/GetAmbienteDesarrolloDetalle", {
    params: { id }
  });

export const GetBaseDatos = () => Axios.get("/BaseDatos/GetBaseDatos");

export const GetExperienciaActual = datos =>
  Axios.post("/MasterData/GetExperienciaActual", {
    ...datos
  });

export const GetBaseDatosDetalle = id =>
  Axios.get("/BaseDatos/GetBaseDatosDetalle", {
    params: { id }
  });

export const GetBus = () => Axios.get("/Bus/GetBus");

export const GetBusDetalle = id =>
  Axios.get("/Bus/GetBusDetalle", {
    params: { id }
  });

export const GetCentrosEstudioDetalle = id =>
  Axios.get("/CentroEstudio/GetCentrosEstudioDetalle", {
    params: { id }
  });

export const GetCertificacionDetalle = id =>
  Axios.get("/Certificacion/GetCertificacionDetalle", {
    params: { id }
  });

export const GetEmpresaCorporacionDetalle = id =>
  Axios.get("/EmpresaCorporacion/GetEmpresaCorporacionDetalle", {
    params: { id }
  });

export const GetErpCore = () => Axios.get("/ErpCore/GetErpCore");

export const GetErpCoreDetalle = id =>
  Axios.get("/ErpCore/GetErpCoreDetalle", {
    params: { id }
  });

export const GetCalidad = () => Axios.get("/Calidad/GetCalidad");

export const GetCalidadDetalle = id =>
  Axios.get("/Calidad/GetCalidadDetalle", {
    params: { id }
  });

export const GetIdiomasDetalle = id =>
  Axios.get("/Idiomas/GetDetalle", {
    params: { id }
  });

export const GetInfraestructura = () =>
  Axios.get("/Infraestructura/GetInfraestructura");

export const GetInfraestructuraDetalle = id =>
  Axios.get("/Infraestructura/GetInfraestructuraDetalle", {
    params: { id }
  });

export const GetLenguajeProgramacion = () =>
  Axios.get("/LenguajeProgramacion/GetLenguajeProgramacion");

export const GetLenguajeProgramacionDetalle = id =>
  Axios.get("/LenguajeProgramacion/GetLenguajeProgramacionDetalle", {
    params: { id }
  });

export const GetBPMS = () => Axios.get("/BPM/GetBPMS");
export const GetEspecialidad = () => Axios.get("/Especialidad/GetEspecialidad");

export const GetBPMSDetalle = id =>
  Axios.get("/BPM/GetBPMSDetalle", {
    params: { id }
  });

export const GetProveedorCertificacion = () =>
  Axios.get("/ProveedorCertificacion/GetAll");

export const GetProveedorCertificacionesLista = () =>
  Axios.get("/ProveedorCertificacion/GetProveedorCertificacionesLista");

export const GetProveedores = () => Axios.get("/ProveedorCertificacion/GetAll");

export const GetProveedorCertificacionDetalle = id =>
  Axios.get("/ProveedorCertificacion/GetDetalle", {
    params: { id }
  });

export const GetPuesto = () => Axios.get("/Puesto/GetAll");

export const GetPuestoDetalle = id =>
  Axios.get("/Puesto/GetDetalle", {
    params: { id }
  });

export const GetSectorDetalle = id =>
  Axios.get("/Sector/GetDetalle", {
    params: { id }
  });

export const GetTecnologia = () => Axios.get("/Tecnologia/GetTecnologia");

export const GetTecnologiaDetalle = id =>
  Axios.get("/Tecnologia/GetTecnologiaDetalle", {
    params: { id }
  });

export const GetTipoCore = () => Axios.get("/TipoCore/GetAll");

export const GetPhotoByIdentityCard = identificacion =>
  Axios.get("/Colaborador/GetPhotoByIdentityCard", {
    params: { identificacion }
  });

export const GetExperienciaProyecto = id =>
  Axios.get("/ExperienciaProyecto/GetExperienciaProyecto", {
    params: { id }
  });

export const GetExperienciaProyectoDetalle = id =>
  Axios.get("/ExperienciaProyecto/GetExperienciaProyectoDetalle", {
    params: { id }
  });

export const GetListaHerramientas = () =>
  Axios.get("/ExperienciaProyecto/GetListaHerramientas");

export const GetProyectosCliente = (nombreCliente, idCliente) =>
  Axios.get("/ExperienciaProyecto/GetProyectosCliente", {
    params: { nombreCliente, idCliente }
  });

export const ValidarProyecto = values =>
  Axios.post("/ExperienciaProyecto/ValidarProyecto", {
    ...values
  });

export const GetPhotoColaboradorById = id =>
  Axios.get("/Colaborador/GetPhotoById", {
    params: { id }
  });

export const GetCertificacionProveedores = () =>
  Axios.get("/Certificacion/GetCertificacionProveedores");

//Validaciones Catalogos

export const ValidaAmbienteDependencias = values =>
  Axios.post("/AmbienteDesarrollo/ValidaAmbienteDependencias", {
    ...values
  });

export const ValidaAreaOrganizativaDependencias = values =>
  Axios.post("/AreaOrganizativa/ValidaAreaOrganizativaDependencias", {
    ...values
  });

export const ValidaBaseDatosDependencias = values =>
  Axios.post("/BaseDatos/ValidaBaseDatosDependencias", {
    ...values
  });

export const ValidaBPMDependencias = values =>
  Axios.post("/BPM/ValidaBPMDependencias", {
    ...values
  });

export const ValidaBusDependencias = values =>
  Axios.post("/Bus/ValidaBusDependencias", {
    ...values
  });

export const ValidaCentroEstudioDependencias = values =>
  Axios.post("/CentroEstudio/ValidaCentroEstudioDependencias", {
    ...values
  });

export const ValidaCertificacionDependencias = values =>
  Axios.post("/Certificacion/ValidaCertificacionDependencias", {
    ...values
  });

export const ValidaEmpresaDependencias = values =>
  Axios.post("/EmpresaCorporacion/ValidaEmpresaDependencias", {
    ...values
  });

export const ValidaERPDependencias = values =>
  Axios.post("/ErpCore/ValidaERPDependencias", {
    ...values
  });

export const ValidaCalidadDependencias = values =>
  Axios.post("/Calidad/ValidaCalidadDependencias", {
    ...values
  });

export const ValidaIdiomaDependencias = values =>
  Axios.post("/Idiomas/ValidaIdiomaDependencias", {
    ...values
  });

export const ValidaInfraestructuraDependencias = values =>
  Axios.post("/Infraestructura/ValidaInfraestructuraDependencias", {
    ...values
  });

export const ValidaLenguajeDependencias = values =>
  Axios.post("/LenguajeProgramacion/ValidaLenguajeDependencias", {
    ...values
  });

export const ValidaProveedorDependencias = values =>
  Axios.post("/ProveedorCertificacion/ValidaProveedorDependencias", {
    ...values
  });

export const ValidaPuestoDependencias = values =>
  Axios.post("/Puesto/ValidaPuestoDependencias", {
    ...values
  });

export const ValidaSectorDependencias = values =>
  Axios.post("/Sector/ValidaSectorDependencias", {
    ...values
  });

export const ValidaTecnologiaDependencias = values =>
  Axios.post("/Tecnologia/ValidaTecnologiaDependencias", {
    ...values
  });

export const ValidaTipoCoreDependencias = values =>
  Axios.post("/TipoCore/ValidaTipoCoreDependencias", {
    ...values
  });

export const ValidaEspecialidadDependencias = values =>
  Axios.post("/Especialidad/ValidaEspecialidadDependencias", {
    ...values
  });

export const ValidaCertificacionProveedorDependencias = values =>
  Axios.post(
    "/CertificacionProveedor/ValidaCertificacionProveedorDependencias",
    {
      ...values
    }
  );

export const ValidaProveedorCertificacionDependencias = values =>
  Axios.post(
    "/CertificacionProveedor/ValidaProveedorCertificacionDependencias",
    {
      ...values
    }
  );

export const CambiarResponsable = values =>
  Axios.post("/MasterData/CambiarResponsable", {
    ...values
  });

export const GetResponsablePorPerfil = (
  id,
  idSolicitud,
  idTipoSolicitud,
  estadoSolicitud
) =>
  Axios.get("/MasterData/GetResponsablePorPerfil", {
    params: {
      idColaborador: id,
      IdColaboradorSolicitud: idSolicitud,
      idTipoSolicitud: idTipoSolicitud,
      estadoSolicitud: estadoSolicitud
    }
  });

export const GetVacacionesNomina = IdNomina =>
  Axios.get("/Nomina/GetVacacionesNomina", {
    params: { IdNomina }
  });

export const GetColaboradorVacaciones = id =>
  Axios.get("/Colaborador/GetColaboradorVacaciones", {
    params: { id }
  });

export const GetCalculaCantidadDiasListQuery = (id, fechaInicio, fechaFin) =>
  Axios.get("/Vacaciones/GetCalculaCantidadDiasListQuery", {
    params: { idColaborador: id, FechaInicio: fechaInicio, FechaFin: fechaFin }
  });

export const GetVacunaList = () => Axios.get("/Vacuna/GetVacunaList");

export const GetVacunacionColaboradorDetail = idColaborador =>
  Axios.get("/VacunacionColaborador/GetVacunacionColaboradorDetail", {
    params: { idColaborador }
  });

export const GetCloud = () => Axios.get("/Cloud/GetCloud");

export const ValidaCloudDependencias = values =>
  Axios.post("/Cloud/ValidaCloudDependencias", {
    ...values
  });

export const EnvioCorreo = values =>
  Axios.post("/EnvioCorreo/EnvioCorreo", {
    ...values
  });

  export const UploadImageTest = (data) => {
    const formData = new FormData();
    formData.append("data", data);
    const config = {
      headers: {
        "content-type": "multipart/form-data"
      }
    };
  console.log(data);
    return Axios.post("/CloudImages/UploadImage", formData, config);
  };

  export const AnularSolicitud = values =>
  Axios.post("/MasterData/AnularSolicitud", {
    ...values
  });