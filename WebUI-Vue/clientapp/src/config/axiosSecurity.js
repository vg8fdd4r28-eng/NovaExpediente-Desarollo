import AxiosSecurity from "axios";

export default AxiosSecurity.create({
  baseURL: process.env.VUE_APP_RUTA_SEGURIDAD,
  headers: { "Content-Type": "application/json" }
});

