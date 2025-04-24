import Axios from "../../config/axios";

//categoria de Noticias
export const Categories = () =>
  Axios.get("NoticiaCategoria/GetNoticiasCategorias");
export const DeleteCategory = id =>
  Axios.delete("/NoticiaCategoria/Delete/" + id);
export const SaveCategory = async values => {
  Axios.post("/NoticiaCategoria/Upsert", { ...values });
};


//categoria de Convenios

export const GetConveniosCategorias = () =>
  Axios.get("ConvenioCategoria/GetConveniosCategorias");
export const UpsertConveniosCategoriasCommand = async values => {
  Axios.post("/ConvenioCategoria/UpsertConveniosCategoriasCommand", {
    ...values
  });
};
export const DeleteConveniosCategorias = id =>
  Axios.delete("/ConvenioCategoria/Delete/" + id);

//Noticias
export const News = () => Axios.get("Noticia/GetNoticias");
export const Languages = () => Axios.get("Idiomas/GetAll");

export const NewDetail = idNoticia =>
  Axios.get("Noticia/GetDetalleNoticia", {
    params: { idNoticia: idNoticia }
  });

export const UpsertNoticias = values =>
  Axios.post("Noticia/UpsertNoticias", {
    ...values
  });

export const UploadImageNoticia = (data, IdNoticia, IdUsuarioCrea) => {
  const formData = new FormData();
  formData.append("data", data);
  formData.append("IdNoticia", IdNoticia);
  formData.append("IdUsuarioCrea", IdUsuarioCrea);
  const config = {
    headers: {
      "content-type": "multipart/form-data"
    }
  };
  // console.log(data);
  return Axios.post("/CloudImages/UploadImageNoticias", formData, config);
};
export const DeleteNoticia = id => Axios.delete("/Noticia/Delete/" + id);

export const DeleteImagenNoticia = (idNoticia, idImagen) => Axios.delete('/Noticia/DeleteImagesNoticias', {
  params: {
    idNoticia: idNoticia,
    idImagen: idImagen
  }
});

//Convenios
export const UploadImageConvenio = (data, IdConvenio, IdUsuarioCrea) => {
  const formData = new FormData();
  formData.append("data", data);
  formData.append("IdConvenio", IdConvenio);
  formData.append("IdUsuarioCrea", IdUsuarioCrea);
  const config = {
    headers: {
      "content-type": "multipart/form-data"
    }
  };
  // console.log(data);
  return Axios.post("/CloudImages/UploadImageConvenios", formData, config);
};

export const GetDetalleConvenio = IdConvenio =>
  Axios.get("Convenio/GetDetalleConvenio", {
    params: { IdConvenio: IdConvenio }
  });

export const UpsertConvenios = values =>
  Axios.post("Convenio/UpsertConvenios", {
    ...values
  });

export const GetConvenios = () => Axios.get("Convenio/GetConvenios");

export const DeleteConvenio = id => Axios.delete("/Convenio/DeleteConvenio/" + id);

export const DeleteImagesConvenios = (IdConvenio, idImagen) => Axios.delete('/Convenio/DeleteImagesConvenios', {
  params: {
    IdConvenio: IdConvenio,
    idImagen: idImagen
  }
});

//Actividades

export const GetListaActividades = () => Axios.get("Actividad/GetListaActividades");

export const DeleteActividad = id => Axios.delete("/Actividad/DeleteActividad/" + id);

export const GetDetalleActividad = IdActividad =>
  Axios.get("Actividad/GetDetalleActividad", {
    params: { IdActividad: IdActividad }
  });

export const UpsertActividades = values =>
  Axios.post("Actividad/UpsertActividades", {
    ...values
  });

export const DeleteImagesActividades = (IdActividad, idImagen) => Axios.delete('/Actividad/DeleteImagesActividades', {
  params: {
    IdActividad: IdActividad,
    idImagen: idImagen
  }
});

export const UploadImageActividades = (data, IdActividad, IdUsuarioCrea) => {
  const formData = new FormData();
  formData.append("data", data);
  formData.append("IdActividad", IdActividad);
  formData.append("IdUsuarioCrea", IdUsuarioCrea);
  const config = {
    headers: {
      "content-type": "multipart/form-data"
    }
  };
  // console.log(data);
  return Axios.post("/CloudImages/UploadImageActividades", formData, config);
};

// Sorteos

export const GetListaSorteos = () => Axios.get("/Sorteo/GetListaSorteos");

export const DeleteSorteo = (idSorteo) => Axios.delete(`/Sorteo/Delete/${idSorteo}`);

export const GetDetalleSorteo = idSorteo =>
  Axios.get("/Sorteo/GetDetalleSorteo", {
    params: { idSorteo: idSorteo }
  });

export const UpsertSorteo = values =>
  Axios.post("Sorteo/UpsertSorteo", {
    ...values
  });

export const GetColaboradorDetailResumen = id =>
  Axios.get("/Colaborador/GetDetailResumen", {
    params: { id }
  });

export const ColaboradorGetAll = () => Axios.get("/Colaborador/GetAll");

export const DeleteImagenSorteo = (idSorteo, idImagen) => Axios.delete('/Sorteo/DeleteImagesSorteo', {
  params: {
    idSorteo: idSorteo,
    idImagen: idImagen
  }
});

export const UploadImageSorteo = (data, IdSorteo, IdUsuarioCrea) => {
  const formData = new FormData();
  formData.append("data", data);
  formData.append("IdSorteo", IdSorteo);
  formData.append("IdUsuarioCrea", IdUsuarioCrea);
  const config = {
    headers: {
      "content-type": "multipart/form-data"
    }
  };
  return Axios.post("/CloudImages/UploadImageSorteo", formData, config);
};