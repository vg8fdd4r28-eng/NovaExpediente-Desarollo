import Axios from "../../config/axios";

export const GetColaboradoresPorGenero = () =>
  Axios.get("/DashBoard/GetColaboradoresPorGenero");

export const GetColaboradoresPorTipoDeCertificado = () =>
  Axios.get("/DashBoard/GetColaboradoresPorTipoDeCertificado");

export const GetColaboradoresPorEspecialidad = () =>
  Axios.get("/DashBoard/GetColaboradoresPorEspecialidad");

export const GetColaboradoresPorBaseDeDatos = () =>
  Axios.get("/DashBoard/GetColaboradoresPorBaseDeDatos");

export const GetColaboradoresPorDM = () =>
  Axios.get("/DashBoard/GetColaboradoresPorDM");

export const GetSolicitudesEnTransito = () =>
Axios.get("/DashBoard/GetSolicitudesEnTransito");

export const GetAntiguedadSolicitudes = () =>
Axios.get("/DashBoard/GetAntiguedadSolicitudes");

export const GetAntiguedadExpedientes = () =>
Axios.get("/DashBoard/GetAntiguedadExpediente");

export const GetUsoAplicacion = (rango) =>
Axios.post("/DashBoard/GetUsoAplicacion",
{
  ...rango,
});

export const GetPhotoColaboradorById = id =>
  Axios.get("/Colaborador/GetPhotoById", {
    params: { id }
  });

export const GetSolicitudes = () =>
Axios.get("/DashBoard/GetSolicitudes");

export const GetClientes = () =>
Axios.get("/DashBoard/GetClientes");

export const GetColaboradoresAsignados = id =>
  Axios.get("/Dashboard/GetColaboradoresAsignados", {
    params: { id }
  });