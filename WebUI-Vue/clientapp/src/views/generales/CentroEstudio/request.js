import Axios from "../../../config/axios";

export const GetCentrosEstudio = () =>
  Axios.get("/CentroEstudio/GetCentrosEstudio");


