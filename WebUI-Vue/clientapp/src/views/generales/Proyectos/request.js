import Axios from "../../../config/axios";


export const GetDetail = id =>
  Axios.get("/Colaborador/GetDetail", {
    params: { id }
  });



export const GetProyectos = () => Axios.get("/Proyecto/GetProyectos");