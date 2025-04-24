<template>
  <div>
    <!-- <CCard>
      <CCardHeader> Buzón de solicitudes </CCardHeader>
      <CCardBody>
        <CRow>
          <CSelect
            label="Tipo de Solicitud"
            :options="tiposSolicitudesOptions"
            
            add-label-classes="label-title"
            add-input-classes="form-control"
          ></CSelect>
        </CRow> -->
    <component :is="renderForm" v-bind="currentProperties"> </component>
    <!-- </CCardBody>
    </CCard> -->
  </div>
</template>

<script>
import DatosPersonales from "./DatosPersonales";
import IdiomasNivelIdioma from "./IdiomasNivelIdioma";
import FormacionAcademica from "./FormacionAcademica";
import Curso from "./Curso";
import EmpresaColaborador from "./EmpresaColaborador";
import AsignarDm from "./AsignarDm";
import EstadoColaborador from "./EstadoColaborador";
import AsignarPuesto from "./AsignarPuesto";
import Cliente from "./Cliente";
import UbicacionColaborador from "./UbicacionColaborador";
import Catalogos from "./Catalogos";
import Certificaciones from "./Certificaciones";
import Proyectos from "./Proyectos";
import ExperienciaProyectos from "./ExperienciaProyectos";
import ExperienciaProyectosInternos from "./ExperienciaProyectosInternos";
import SinDatos from "../generales/Componentes/SinDatos";
import ColaboradorDM from "./ColaboradorDM.vue";
import AsignarEspecialidad from "./AsignarEspecialidad.vue";

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
    "datos-personales": DatosPersonales,
    "idiomas-nivel-idioma": IdiomasNivelIdioma,
    "formacion-academica": FormacionAcademica,
    curso: Curso,
    "empresa-colaborador": EmpresaColaborador,
    "asignar-dm": AsignarDm,
    "estado-colaborador": EstadoColaborador,
    "asignar-puesto": AsignarPuesto,
    "asignar-especialidad": AsignarEspecialidad,
    cliente: Cliente,
    "ubicacion-colaborador": UbicacionColaborador,
    catalogos: Catalogos,
    certificaciones: Certificaciones,
    proyectos: Proyectos,
    "sin-datos": SinDatos,
    "experiencia-proyectos": ExperienciaProyectos,
    "experiencia-proyectos-internos": ExperienciaProyectosInternos,
    "colaborador-dm": ColaboradorDM
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
        case "Datos Personales":
          return "datos-personales";
        case "Formación Académica":
          return "formacion-academica";
        case "Idiomas (Nivel de idioma)":
          return "idiomas-nivel-idioma";
        case "Cursos":
          return "curso";
        case "Asignar empresa a colaborador":
          return "empresa-colaborador";
        case "Asignar líder a colaborador":
          return "asignar-dm";
        case "Actualizar el estado de un colaborador":
          return "estado-colaborador";
        case "Asignar puesto a colaborador":
          return "asignar-puesto";
        case "Clientes":
          return "cliente";
        case "Ubicación colaborador":
          return "ubicacion-colaborador";
        case "Catálogos":
          return "catalogos";
        case "Certificaciones":
          return "certificaciones";
        case "Proyectos":
          return "proyectos";
        case "Experiencia en Proyectos":
          return "experiencia-proyectos";
        case "Experiencia en tecnologías":
          return "experiencia-proyectos-internos";
        case "Definir colaborador como DM":
          return "colaborador-dm";
        case "Asignar especialidad a colaborador":
          return "asignar-especialidad";

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
