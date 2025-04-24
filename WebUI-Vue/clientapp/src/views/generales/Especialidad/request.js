import Axios from "../../../config/axios";

export const GetEspecialidad = () => Axios.get("/Especialidad/GetEspecialidad");