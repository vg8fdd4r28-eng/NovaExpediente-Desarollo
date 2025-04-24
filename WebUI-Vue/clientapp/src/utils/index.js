import { format } from "date-fns";
import { es } from "date-fns/locale";

export const aplicarFormatoFecha = (f, ft = "Pp") =>
  f ? format(new Date(f), ft) : f;

export const getProp = (key, obj) => {
  if (obj.hasOwnProperty(key)) {
    return obj[key];
  }
  return null;
};
