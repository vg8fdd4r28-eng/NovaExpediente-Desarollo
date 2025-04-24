import Axios from "axios";

export default Axios.create({
  baseURL: process.env.VUE_APP_RUTA_RAIZ,
  headers: { "Content-Type": "application/json" }
});

