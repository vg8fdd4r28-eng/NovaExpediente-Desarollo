import Axios from "../config/axios";


export const GetPhoto = email =>
  Axios.get("/Colaborador/GetPhotoByEmail", {
    params: { email }
  });

  export const GetInfoColaborador = email =>
  Axios.get("/Colaborador/GetInfoByEmail", {
    params: { email }
  });

  export const GetContextoSeguridad = () => Axios.get("/CurrentUser/GetSecurityContext");
