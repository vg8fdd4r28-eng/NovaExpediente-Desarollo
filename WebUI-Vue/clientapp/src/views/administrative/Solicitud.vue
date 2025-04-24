<template>
  <div>
    <component :is="renderForm" v-bind="currentProperties"> </component>
  </div>
</template>

<script>
import ConstanciaSalario from "./ConstanciaSalario";
import CartaRestriccion from "./CartaRestriccion";
import RenovacionDimex from "./RenovacionDimex";
import ServiciosLaborales from "./ServiciosLaborales";
import SolicitudVacaciones from "./SolicitudVacaciones.vue";
import RegistroIncapacidad from "./RegistroIncapacidad.vue";
import SolicitudPermiso from "./SolicitudPermiso.vue";
import SinDatos from "../generales/Componentes/SinDatos";
import RegistroVacunacion from "./RegistroVacunacion.vue";

export default {
  name: "Solicitud",
  props: {
    origen: { type: String, default: "" }, // origen desde donde se accedio la vista
    idTipoSolicitud: { type: Number, default: 0 }, // id del tipo de la solicitud generada
    tipoSolicitud: { type: String, default: "" }, // nombre del tipo de la solicitud generada
    idSolicitud: { type: Number, default: null }, // id de la solicitud, para obtener el obj Json con la informacion a cargar
    idResponsable: { type: Number, default: null } // id  del responsable de la solicitud
  },
  components: {
    "constancia-salario": ConstanciaSalario,
    "carta-restriccion": CartaRestriccion,
    "renovacion-dimex": RenovacionDimex,
    "servicios-laborales": ServiciosLaborales,
    "solicitud-vacaciones": SolicitudVacaciones,
    "registro-incapacidad": RegistroIncapacidad,
    "solicitud-permiso": SolicitudPermiso,
    "sin-datos": SinDatos,
    "registro-vacunacion": RegistroVacunacion
  },
  data() {
    return {
      Solicitudes: [],
      tiposSolicitudesOptions: [],
      SolicitudSeleccionada: 1
    };
  },
  async mounted() {},
  methods: {},

  computed: {
    renderForm() {
      switch (this.tipoSolicitud) {
        case "Carta de restricción vehicular":
          return "carta-restriccion";
        case "Constancia de Salario":
          return "constancia-salario";
        case "Renovación DIMEX":
          return "renovacion-dimex";
        case "Servicios laborales":
          return "servicios-laborales";
        case "Solicitud de vacaciones":
          return "solicitud-vacaciones";
        case "Registro de incapacidades":
          return "registro-incapacidad";
        case "Solicitud de permiso":
          return "solicitud-permiso";
        case "Registro de vacunación":
          return "registro-vacunacion";
        default:
          return "sin-datos";
      }
    },
    currentProperties: function() {
      // always use kebab-case for passing props to custom components or html
      return {
        origen: this.origen,
        "id-tipo-solicitud": this.idTipoSolicitud,
        "tipo-solicitud": this.tipoSolicitud,
        "id-solicitud": this.idSolicitud,
        "id-responsable": this.idResponsable
      };
    }
  }
};
</script>

<style>
.label-title {
  font-weight: 700;
}
.label-name {
  font-variant: small-caps;
  text-align: center;
}
.libro-marca-azuloscuro {
  background-color: #002e6e !important;
}
.libro-marca-celeste {
  background-color: #29abe2;
}
</style>
