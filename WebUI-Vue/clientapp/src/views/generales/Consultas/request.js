import Axios from "../../../config/axios";

export const GradosAcademicosGetAll = () =>
  Axios.get("/GradosAcademicos/GetAllConsulta");

export const GetCertificacion = () =>
  Axios.get("/Certificacion/GetCertificacionConsulta");

export const GetEspecialidad = () =>
  Axios.get("/Especialidad/GetEspecialidadConsulta");

export const GetNivelIdioma = () =>
  Axios.get("/NivelIdioma/GetNivelIdiomaConsulta");

export const GetBaseDatos = () => Axios.get("/BaseDatos/GetBaseDatosConsulta");

export const GetLenguajeProgramacion = () =>
  Axios.get("/LenguajeProgramacion/GetLenguajeProgramacionConsulta");

export const GetAmbienteDesarrollo = () =>
  Axios.get("/AmbienteDesarrollo/GetAmbienteDesarrolloConsulta");

export const GetTecnologia = () =>
  Axios.get("/Tecnologia/GetTecnologiaConsulta");

export const GetErpCore = () => Axios.get("/ErpCore/GetErpCoreConsulta");

export const GetCalidad = () => Axios.get("/Calidad/GetCalidadConsulta");

export const GetInfraestructura = () =>
  Axios.get("/Infraestructura/GetInfraestructuraConsulta");

export const GetBusConsulta = () => Axios.get("/Bus/GetBusConsulta");

export const GetBPMConsulta = () => Axios.get("/BPM/GetBPMConsulta");

export const GetCantMes = () =>
  Promise.resolve([
    {
      value: "6",
      label: "6",
      key: 6
    },
    {
      value: "12",
      label: "12",
      key: 12
    },
    {
      value: "18",
      label: "18",
      key: 18
    },
    {
      value: "24",
      label: "24",
      key: 24
    },
    {
      value: "30",
      label: "30",
      key: 30
    },
    {
      value: "36",
      label: "36",
      key: 36
    },
    {
      value: "42",
      label: "42",
      key: 42
    },
    {
      value: "+48",
      label: "+48",
      key: 48
    }
  ]);
export const GetColaboradoresConsulta = query =>
  Axios.get("/Colaborador/GetColaboradoresConsulta", {
    params: { query }
  });

export const GetColaboradoresCV = (query, tipoCV) =>
  Axios.get("/Colaborador/GetColaboradoresCV", {
    params: { query: query, tipoCV: tipoCV }
  });

export const GetTiposCV = () =>
  Promise.resolve([
    {
      value: "1",
      label: "Novacomp - Espanol",
      key: 1
    },
    {
      value: "2",
      label: "Innovative - Espanol",
      key: 2
    },
    {
      value: "3",
      label: "Innovative - Ingles",
      key: 3
    },
    {
      value: "4",
      label: "ITQS - Espanol",
      key: 4
    },
    {
      value: "5",
      label: "ITQS - Ingles",
      key: 5
    },
    {
      value: "6",
      label: "NovaShore",
      key: 6
    },
    {
      value: "7",
      label: "NovaTesting - Espanol",
      key: 7
    },
    {
      value: "8",
      label: "NovaTesting - Ingles",
      key: 8
    }
  ]);

// serializa un objeto
const objToString = (obj, ndeep) => {
  switch (typeof obj) {
    case "string":
      return '"' + obj + '"';
    case "function":
      return obj.name || obj.toString();
    case "object":
      var indent = Array(ndeep || 1).join("\t"),
        isArray = Array.isArray(obj);
      return (
        "{["[+isArray] +
        Object.keys(obj)
          .map(function(key) {
            return (
              "\n\t" +
              indent +
              (isArray ? "" : key + ": ") +
              objToString(obj[key], (ndeep || 1) + 1)
            );
          })
          .join(",") +
        "\n" +
        indent +
        "}]"[+isArray]
      ).replace(/[\s\t\n]+(?=(?:[^\'"]*[\'"][^\'"]*[\'"])*[^\'"]*$)/g, "");
    default:
      return obj.toString();
  }
};

// crea una copia de una colección de objetos, excluyendo las llaves indicadas
// en el argumento 'keys'
const omitKeys = (obj, keys) => {
  var list = [];
  obj.forEach(element => {
    var dup = {};
    for (var key in element.attributes) {
      if (keys.indexOf(key) == -1) {
        dup[key] = element.attributes[key];
      }
    }
    list.push(dup);
  });
  return list;
};

// guarda en el SESSIONSTORAGE la lista de modelos del Visual Search
export const saveJsonState = (models, key) => {
  const storage = window.sessionStorage;

  // cada modelo tiene los atributos 'category', 'value', 'key' y 'app'
  // por lo que se excluye el atributo 'app'
  storage.setItem(key, objToString(omitKeys(models, ["app"])));
};

export const saveState = (value, key) => {
  const storage = window.sessionStorage;
  storage.setItem(key, value);
};

export const getState = key => {
  const storage = window.sessionStorage;
  return storage.getItem(key);
};
