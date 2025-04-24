import Axios from "../../../config/axios";

export const GetBusConsulta = () => Axios.get("/Bus/GetBusConsulta");