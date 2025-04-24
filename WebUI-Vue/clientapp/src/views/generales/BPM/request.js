import Axios from "../../../config/axios";

export const GetBPMS = () => Axios.get("/BPM/GetBPMS");