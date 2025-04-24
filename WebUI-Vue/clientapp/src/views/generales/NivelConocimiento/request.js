import Axios from "../../../config/axios";

export const GetAllIdiomas = () => Axios.get("/Idiomas/GetAll");

export const GetNivelIdioma = () => Axios.get("/NivelIdioma/GetNivelIdioma");

export const GetNivelIdiomaXIdioma = id =>
  Axios.get("/NivelIdioma/GetNivelIdiomaXIdioma", {
    params: { id }
  });