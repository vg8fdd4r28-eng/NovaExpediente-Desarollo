import Axios from "../../../config/axios";

export const GetCalidad = () => Axios.get("/Calidad/GetCalidad");