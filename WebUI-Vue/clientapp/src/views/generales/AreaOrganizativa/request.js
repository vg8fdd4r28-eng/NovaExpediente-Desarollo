import Axios from "../../../config/axios";

export const GetAreasOrganizativas = () => Axios.get("/AreaOrganizativa/GetAreasOrganizativas");