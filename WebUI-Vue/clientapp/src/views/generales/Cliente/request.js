import Axios from "../../../config/axios";

export const GetClientes = () => Axios.get("/Cliente/GetClientes");

export const GetClientesReferencias = () => Axios.get("/Cliente/GetClientesReferencias");