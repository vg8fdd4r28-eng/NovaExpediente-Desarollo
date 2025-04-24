import Axios from "../../config/axios";

//buzon
export const GetSolicitudesBuzon = (
  id,
  tipo,
  fechaInicio = "1900-01-01",
  fechaFin = "1900-01-01"
) =>
  Axios.get("/MasterData/GetSolicitudesBuzon", {
    params: {
      idUsuario: id,
      tipo: tipo,
      categoria: "AD",
      fechaInicio: fechaInicio,
      fechaFin: fechaFin,
    },
  });

export const GetSolicitudesPorPerfil = () =>
  Axios.get("/MasterData/GetSolicitudesPorPerfil", {
    params: { categoria: "AD" },
  });

export const GetDetalleSolicitud = (idSolicitud, estado, correo) =>
  Axios.post("/MasterData/GetSolicitudPorAprobar", {
    idSolicitud: idSolicitud,
    estado: estado,
    correo: correo,
  });

// export const GetVacacionesPorPerfil = id =>
//   Axios.get("/MasterData/GetVacacionesPorPerfil",
//   {
//     params: { idColaborador: id }
//   }
//   );

export const GetVacacionesPorPerfil = (datos) =>
  Axios.get("/MasterData/GetVacacionesPorPerfil", {
    params: {
      idColaborador: datos.idColaborador,
      fechaInicio: datos.fechaInicio,
      fechaFin: datos.fechaFin,
    },
  });

export const CambiarResponsable = (values) =>
  Axios.post("/MasterData/CambiarResponsable", {
    ...values,
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
      estadoSolicitud: estadoSolicitud,
    },
  });

export const RegistroColaborador = (values) =>
  Axios.post("/RegistroColaborador/RegistroColaborador", {
    ...values,
  });

export const DatosLaborales = (values) =>
  Axios.post("/RegistroColaborador/DatosLaborales", {
    ...values,
  });

export const GetAllCantones = () => Axios.get("/Canton/GetAll");

export const GetAllPaises = () => Axios.get("/Pais/GetAll");

export const GetColaboradoresPerfil = (id) =>
  Axios.get("/MasterData/GetColaboradoresPorPerfil", {
    params: { idColaborador: id },
  });

export const GetPuesto = () => Axios.get("/Puesto/GetAll");

export const GetEspecialidad = () => Axios.get("/Especialidad/GetEspecialidad");

export const GetEmpresaCorporacion = () =>
  Axios.get("/EmpresaCorporacion/GetEmpresaCorporacion");

export const GetColaboradoresDM = () =>
  Axios.get("/Colaborador/GetColaboradoresDM");

export const GetAreasOrganizativas = () =>
  Axios.get("/AreaOrganizativa/GetAreasOrganizativas");

export const GetClientes = () => Axios.get("/Cliente/GetClientes");

export const CargaIncapacidades = (data, idColaborador, Json, cantidad) => {
  const formData = new FormData();
  formData.append("data", data);
  formData.append("idColaborador", idColaborador);
  formData.append("Json", Json);
  formData.append("cantidad", cantidad);
  const config = {
    headers: {
      "content-type": "multipart/form-data",
    },
  };

  return Axios.post("/Incapacidad/CargaIncapacidades", formData, config);
};

export const CargarCV = (data, idColaborador, Json) => {
  const formData = new FormData();
  formData.append("data", data);
  formData.append("idColaborador", idColaborador);
  formData.append("Json", Json);

  const config = {
    headers: {
      "content-type": "multipart/form-data",
    },
  };

  return Axios.post("/CurriculumVitae/CargarCV", formData, config);
};

export const GetCurriculumsVitae = idColaborador =>
  Axios.get("/CurriculumVitae/GetCurriculumVitae", {
    params: { idColaborador }
  });

export const GetCargaIncapacidad = () =>
  Axios.get("/Incapacidad/GetCargaIncapacidad");

export const GetCargaVacaciones = () =>
  Axios.get("/Vacaciones/GetCargaVacaciones");

export const CargaVacaciones = (data, idColaborador, Json, cantidad) => {
  const formData = new FormData();
  formData.append("data", data);
  formData.append("idColaborador", idColaborador);
  formData.append("Json", Json);
  formData.append("cantidad", cantidad);
  const config = {
    headers: {
      "content-type": "multipart/form-data",
    },
  };

  return Axios.post("/Vacaciones/CargaVacaciones", formData, config);
};

// export const GetIncapacidadesPorPerfil = id =>
// Axios.get("/Incapacidad/GetIncapacidadesPorPerfil", {
//   params: { idColaborador: id }
// });

export const GetIncapacidadesPorPerfil = (datos) =>
  Axios.get("/Incapacidad/GetIncapacidadesPorPerfil", {
    params: {
      idColaborador: datos.idColaborador,
      fechaInicio: datos.fechaInicio,
      fechaFin: datos.fechaFin,
    },
  });

export const GetPermisosPorPerfil = (datos) =>
  Axios.get("/Permiso/GetPermisosPorPerfil", {
    params: {
      idColaborador: datos.idColaborador,
      fechaInicio: datos.fechaInicio,
      fechaFin: datos.fechaFin,
    },
  });

export const AnularSolicitud = (values) =>
  Axios.post("/MasterData/AnularSolicitud", {
    ...values,
  });

export const GetTipoPermisoGoce = () =>
  Axios.get("/TipoPermisoGoce/GetTipoPermisoGoce");

export const GetColaboradorDetailResumen = (id) =>
  Axios.get("/Colaborador/GetDetailResumen", {
    params: { id },
  });

export const GetColaboradorDetail = (id) =>
  Axios.get("/Colaborador/GetDetail", {
    params: { id },
  });

export const GetVacacionesNomina = (IdNomina) =>
  Axios.get("/Nomina/GetVacacionesNomina", {
    params: { IdNomina },
  });

export const GetColaboradorVacacionesPorPerfil = (id) =>
  Axios.get("/Vacaciones/GetColaboradorVacacionesPorPerfil", {
    params: { id },
  });

export const GetColaboradorVacacionesConsulta = (id) =>
  Axios.get("/Vacaciones/GetColaboradorVacacionesConsulta", {
    params: { id },
  });

// export const GetVacunaList = id =>
// Axios.get("/Vacuna/GetVacunaList", {
//   params: { id }
// });

export const GetVacunaList = () => Axios.get("/Vacuna/GetVacunaList");
export const GetVacunacionColaboradorReporte = () =>
  Axios.get("/VacunacionColaborador/GetVacunacionColaboradorReporte");

export const UploadImageTest = (data) => {
  const formData = new FormData();
  formData.append("data", data);
  const config = {
    headers: {
      "content-type": "multipart/form-data",
    },
  };
  console.log(data);
  return Axios.post("/CloudImages/UploadImage", formData, config);
};

export const SendNotificationPush = (values) =>
  Axios.post("/OneSignal/SendNotification", {
    ...values,
  });

export const GetBitacoraVacaciones = () =>
  Axios.get("/BitacoraVacaciones/GetBitacoraVacaciones");

export const GetReporteCertificacionesColaborador = () =>
  Axios.get("/Reportes/GetReporteCertificacionesColaborador");

export const GetReporteCursosColaborador = () =>
  Axios.get("/Reportes/GetReporteCursosColaborador");

export const GetReporteFormacionColaborador = () =>
  Axios.get("/Reportes/GetReporteFormacionColaborador");

//Metodos para el uno a uno

export const GetUnoaUnos = (IdDeliveryManager) =>
  Axios.get("/UnoAUno/GetUnosAUnos", {
    params: { IdDeliveryManager },
  });

export const GetDetalleUnoAUno = (IdUnoAUno) =>
  Axios.get("/UnoAUno/GetDetalleUnoAUno", {
    params: { IdUnoAUno: IdUnoAUno },
  });

export const GetUnosAUnosEstadosColaboradores = () =>
  Axios.get("/UnoAUnoEstadoColaborador/GetUnosAUnosEstadosColaboradores");

export const UpsertUnosAUnos = (values) =>
  Axios.post("/UnoAUno/UpsertUnosAUnos", {
    ...values,
  });

export const UpsertUnosAUnosDetalles = (values) =>
  Axios.post("/UnoAUno/UpsertUnosAUnosDetalles", {
    ...values,
  });

export const GetUnosAUnosEstados = () =>
  Axios.get("/UnoAUnoEstado/GetUnosAUnosEstados");

export const GetUnosAUnosCategorias = () =>
  Axios.get("/UnoAUnoCategoria/GetUnosAUnosCategorias");

//export const DeleteUnoaUno = id => Axios.delete("/UnoAUno/DeleteDetalleUnoAUno/{id}/" + id);



//Metodos para el chat IA
export const GetListaConversaciones = (userId, currentPage) =>
  Axios.get("/Conversacion/GetListaConversaciones", {
    params: { userId, currentPage },
  });

export const GetListaDetalleConversacion = (conversationId) =>
  Axios.get("/Conversacion/GetListaDetalleConversacion", {
    params: { conversationId },
  });

export const InsertConversaciones = (values) =>
  Axios.post("/Conversacion/InsertConversaciones", {
    ...values,
  });

export const InsertConversacionMensaje = (values) =>
  Axios.post("/Conversacion/InsertConversacionMensaje", {
    ...values,
  });

//Metodos para el chat de Informe IA
export const GetListaConversacionesInforme = (userId, currentPage) =>
  Axios.get("/Informe/GetListaInformes", {
    params: { userId, currentPage },
  });

export const GetListaDetalleConversacionInforme = (conversationId) =>
  Axios.get("/Informe/GetListaDetalleInforme", {
    params: { conversationId },
  });

export const InsertConversacionesInforme = (values) =>
  Axios.post("/Informe/InsertInformes", {
    ...values,
  });

export const InsertConversacionMensajeInforme = (values) =>
  Axios.post("/Informe/InsertInformeMensaje", {
    ...values,
  });
