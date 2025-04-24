import Axios from "../../../config/axios";

export const GetDistritos = () => Axios.get("/Distrito/GetAll");