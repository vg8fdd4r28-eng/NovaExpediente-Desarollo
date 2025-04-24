import Axios from "../../../config/axios";

export const GetErpCore = () => Axios.get("/ErpCore/GetErpCore");