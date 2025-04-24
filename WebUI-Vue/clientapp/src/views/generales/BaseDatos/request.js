import Axios from "../../../config/axios";

export const GetBaseDatos = () => Axios.get("/BaseDatos/GetBaseDatos");