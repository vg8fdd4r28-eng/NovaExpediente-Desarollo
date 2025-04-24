import Axios from "../../../config/axios";

export const GetCursos = () => Axios.get("/Curso/GetCursos");