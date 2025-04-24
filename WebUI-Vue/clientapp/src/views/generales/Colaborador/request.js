import Axios from "../../../config/axios";

export const GetAll = () => Axios.get("/Colaborador/GetAll");

export const GetDetail = id =>
  Axios.get("/Colaborador/GetDetail", {
    params: { id },
    headers: {
      "Content-Encoding": "gzip"
    }
  });

export const GetColaboradoresDM = id =>
  Axios.get("/Colaborador/GetColaboradoresDM");

export const GetCertificadoCPIC = id =>
  Axios.get("/Colaborador/GetCertificadoCPICById", {
    params: { id }
  });

export const GetCursoColaboradorDocumento = id =>
  Axios.get("/CursoColaborador/GetCursoColaboradorDocumentoById", {
    params: { id }
  });

export const GetCertificacionDocumentoById = id =>
  Axios.get("/CertificacionColaborador/GetCertificacionDocumentoById", {
    params: { id }
  });

export const GetIdiomaColaboradorDocumentoById = id =>
  Axios.get("/IdiomaColaborador/GetIdiomaColaboradorDocumentoById", {
    params: { id }
  });

export const GetFormacionAcademicaDocumentoById = id =>
  Axios.get("/FormacionAcademica/GetFormacionAcademicaDocumentoById", {
    params: { id }
  });

export const GetByColaborador = id =>
  Axios.get("/FormacionAcademica/GetByColaborador", {
    params: { id }
  });

export const GetAfiliacionByColaborador = id =>
  Axios.get("/Afiliacion/GetByColaborador", {
    params: { id }
  });

export const GetCertificadoAfiliacion = id =>
  Axios.get("/Afiliacion/GetCertificadoAfiliacionById", {
    params: { id }
  });
export const GetEspecialidadesConsultor = id =>
  Axios.get("/EspecialidadConsultor/GetEspecialidadesConsultor", {
    params: { id }
  });

export const GetIdiomasColaborador = id =>
  Axios.get("/IdiomaColaborador/GetIdiomasColaborador", {
    params: { id }
  });

export const GetCertificacionesColaborador = id =>
  Axios.get("/CertificacionColaborador/GetCertificacionesColaborador", {
    params: { id }
  });

export const GetCursosColaborador = id =>
  Axios.get("/CursoColaborador/GetCursosColaborador", {
    params: { id }
  });

export const GetCursosImpartir = id =>
  Axios.get("/CursoImpartir/GetCursosImpartir", {
    params: { id }
  });

export const GetHabilidadesColaborador = id =>
  Axios.get("/HabilidadColaborador/GetHabilidadesColaborador", {
    params: { id }
  });

export const GetExperienciasBaseDatos = id =>
  Axios.get("/ExperienciaBaseDatos/GetExperienciasBaseDatos", {
    params: { id }
  });

export const GetExperienciaLenguaje = id =>
  Axios.get("/ExperienciaLenguaje/GetExperienciaLenguaje", {
    params: { id }
  });

export const GetExperienciaAmbienteDesarrollo = id =>
  Axios.get("/ExperienciaAmbienteDesarrollo/GetExperienciaAmbienteDesarrollo", {
    params: { id }
  });

export const GetExperienciaTecnologia = id =>
  Axios.get("/ExperienciaTecnologia/GetExperienciaTecnologia", {
    params: { id }
  });

export const GetExperienciaERP = id =>
  Axios.get("/ExperienciaERP/GetExperienciaERP", {
    params: { id }
  });

export const GetExperienciaCalidad = id =>
  Axios.get("/ExperienciaCalidad/GetExperienciaCalidad", {
    params: { id }
  });

export const GetExperienciaInfraestructura = id =>
  Axios.get("/ExperienciaInfraestructura/GetExperienciaInfraestructura", {
    params: { id }
  });

export const GetExperienciaBPM = id =>
  Axios.get("/ExperienciaBPM/GetExperienciaBPM", {
    params: { id }
  });

export const GetExperienciaBus = id =>
  Axios.get("/ExperienciaBus/GetExperienciaBus", {
    params: { id }
  });

export const GetExperienciaProyecto = id =>
  Axios.get("/ExperienciaProyecto/GetExperienciaProyecto", {
    params: { id }
  });

export const GetTecnologiaProyecto = id =>
  Axios.get("/TecnologiaProyecto/GetTecnologiaProyecto", {
    params: { id }
  });

export const GetHerramientaProyecto = id =>
  Axios.get("/HerramientaProyecto/GetHerramientaProyecto", {
    params: { id }
  });

export const AsociarDMColaboradorCommand = values =>
  Axios.post("/Colaborador/AsociarDMColaboradorCommand", {
    ...values
  });

export const AsociarEmpresaColaboradorCommand = values =>
  Axios.post("/Colaborador/AsociarEmpresaColaboradorCommand", {
    ...values
  });

export const GetEmpresaCorporacion = () =>
  Axios.get("/EmpresaCorporacion/GetEmpresaCorporacion");

export const GetNivelIdioma = () => Axios.get("/NivelIdioma/GetNivelIdioma");

export const GetCertificacion = () =>
  Axios.get("/Certificacion/GetCertificacion");

export const GetVacunacionColaboradorPerfil = idColaborador =>
  Axios.get("/VacunacionColaborador/GetVacunacionColaboradorPerfil", {
    params: { idColaborador }
  });
  export const GetSolicitudesPorPerfil = () =>
  Axios.get("/MasterData/GetSolicitudesPorPerfil");

export const GetVacunacionColaboradorDocumento = idColaborador =>
  Axios.get("/VacunacionColaborador/GetVacunacionColaboradorDocumento", {
    params: { idColaborador }
  });

  export const GetExperienciaCloud = id =>
  Axios.get("/ExperienciaCloud/GetExperienciaCloud", {
    params: { id }
  });
