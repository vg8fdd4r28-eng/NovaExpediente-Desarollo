import Axios from "../../../config/axios";

export const GetTipoCore = () => Axios.get("/TipoCore/GetAll");