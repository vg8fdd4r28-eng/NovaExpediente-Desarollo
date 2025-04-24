import Axios from "../../../config/axios";

export const GetAll = () => Axios.get("/ProveedorCertificacion/GetAll");