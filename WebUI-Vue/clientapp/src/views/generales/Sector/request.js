import Axios from "../../../config/axios";

export const GetAll = () => Axios.get("/Sector/GetAll");