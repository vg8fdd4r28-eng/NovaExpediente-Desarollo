import Axios from "../../../config/axios";

export const GetAll = () => Axios.get("/Clasificadores/GetAll");

/*export const GetAll = () =>
  Axios.get("https://localhost:44354/api/Clasificadores/GetAll");

export const Upsert = (values) =>
  Axios.post(
    "https://localhost:44354/api/Clasificadores/Upsert",
    {
      ...values,
    },
    { headers: { "Content-Type": "application/json" } }
  );

  export const Delete = (id) =>
  Axios.delete(
    "https://localhost:44354/api/Clasificadores/Delete/" + id,
    { headers: { "Content-Type": "application/json" } }
  );*/