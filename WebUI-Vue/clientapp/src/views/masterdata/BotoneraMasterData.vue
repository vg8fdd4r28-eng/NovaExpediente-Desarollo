<template>
  <CRow>
    <CCol sm="12" class="mt-4">
      <!-- Guardar -->
      <CButton
        :class="this.obtenerClasesBoton(this.acciones.GUARDAR)"
        v-if="this.puedePintarBoton(this.acciones.GUARDAR)"
        @click="procesarAccion(acciones.GUARDAR)"
        :disabled="isDisabled"
      >
        <CIcon name="cil-save" />
        {{ this.acciones.GUARDAR }}
      </CButton>
      <!-- Enviar -->
      <CButton
        :class="this.obtenerClasesBoton(this.acciones.ENVIAR)"
        v-if="this.puedePintarBoton(this.acciones.ENVIAR)"
        :disabled="isDisabled"
        type="submit"
      >
        <CIcon name="cil-send" />
        {{ this.acciones.ENVIAR }}
      </CButton>
      <!-- Descartar -->
      <CButton
        :class="this.obtenerClasesBoton(this.acciones.DESCARTAR)"
        v-if="this.puedePintarBoton(this.acciones.DESCARTAR)"
        @click="procesarAccion(acciones.DESCARTAR)"
        :disabled="isDisabled"
      >
        <CIcon name="cil-action-undo" />
        {{ this.acciones.DESCARTAR }}
      </CButton>
      <!-- Revisar -->
      <CButton
        :class="this.obtenerClasesBoton(this.acciones.REVISAR)"
        v-if="this.puedePintarBoton(this.acciones.REVISAR)"
        @click="procesarAccion(acciones.REVISAR)"
        :disabled="isDisabled"
      >
        <CIcon name="cil-list" />
        {{ this.acciones.REVISAR }}
      </CButton>
      <!-- Aprobar -->
      <CButton
        :class="this.obtenerClasesBoton(this.acciones.APROBAR)"
        v-if="this.puedePintarBoton(this.acciones.APROBAR)"
        @click="procesarAccion(acciones.APROBAR)"
        :disabled="isDisabled"
      >
        <CIcon name="cil-check" />
        {{ this.acciones.APROBAR }}
      </CButton>
      <!-- Denegar -->
      <CButton
        :class="this.obtenerClasesBoton(this.acciones.DENEGAR)"
        v-if="this.puedePintarBoton(this.acciones.DENEGAR)"
        @click="procesarAccion(acciones.DENEGAR)"
        :disabled="isDisabled"
      >
        <CIcon name="cil-x-circle" />
        {{ this.acciones.DENEGAR }}
      </CButton>
      <!-- Eliminar -->
      <CButton
        :class="this.obtenerClasesBoton(this.acciones.ELIMINAR)"
        v-if="this.puedePintarBoton(this.acciones.ELIMINAR)"
        @click="procesarAccion(acciones.ELIMINAR)"
        :disabled="isDisabled"
      >
        <CIcon name="cil-trash" />
        {{ this.acciones.ELIMINAR }}
      </CButton>
    </CCol>
  </CRow>
</template>

<script>
/**
 * Constantes según las distintas acciones de la botonera
 */
const GUARDAR = "Guardar";
const ENVIAR = "Enviar";
const DESCARTAR = "Descartar";
const APROBAR = "Aprobar";
const DENEGAR = "Denegar";
const ELIMINAR = "Eliminar";
const REVISAR = "Revisar";

/**
 * Constantes para los estados de una solicitud
 */
const REGISTRADA = "RE";
const APROBADA = "AP";
const DENEGADA = "DE";
const DESCARTADA = "DC";
const ENAPROBACION = "EA";
const CORREGIR = "CO";
const ENVALIDACION = "EV";
const PENDIENTEACTUALIZACION = "PE";
const ENPROCESO = "EP";
const ENPROCESOVACACIONES = "EPV";

export default {
  name: "BotoneraMasterData",
  props: {
    estadoSolicitud: { type: String, default: "RE" },
    nombreEventoProcesar: { type: String, default: "" },
    tipo: { type: String, default: "MD" }
  },
  data() {
    return {
      acciones: {
        GUARDAR: GUARDAR,
        ENVIAR: ENVIAR,
        DESCARTAR: DESCARTAR,
        APROBAR: APROBAR,
        DENEGAR: DENEGAR,
        ELIMINAR: ELIMINAR,
        REVISAR: REVISAR
      },
      estados: {
        REGISTRADA: REGISTRADA,
        APROBADA: APROBADA,
        DENEGADA: DENEGADA,
        DESCARTADA: DESCARTADA,
        ENAPROBACION: ENAPROBACION,
        CORREGIR: CORREGIR,
        PENDIENTEACTUALIZACION: PENDIENTEACTUALIZACION,
        ENVALIDACION: ENVALIDACION,
        ENPROCESO: ENPROCESO,
        ENPROCESOVACACIONES: ENPROCESOVACACIONES
      },
      isDisabled:false
    };
  },
  mounted() {
    // se cargarian los request en caso de ser necesario o en created...
  },
  methods: {
    /**
     * Ejecuta una acción según el botón utilizado por el usuario
     */
    procesarAccion(accion) {
      try {
        this.isDisabled=true;
        this.$emit(this.nombreEventoProcesar, accion);

setTimeout(() => {
        this.isDisabled=false;
}, 1000);


      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Determina la apariencia de los botones de acuerdo a la acción
     */
    obtenerClasesBoton(accion) {
      let clasesBoton = ["mr-1", "mt-1", "btn", "pull-right", "onHoverDark"];

      switch (accion) {
        case this.acciones.GUARDAR:
          clasesBoton.push("btn-info");
          break;

        case this.acciones.DESCARTAR:
          clasesBoton.push("btn-warning");
          break;

        case this.acciones.REVISAR:
          clasesBoton.push("btn-warning");
          break;

        case this.acciones.ENVIAR:
          clasesBoton.push("btn-info");
          break;

        case this.acciones.APROBAR:
          clasesBoton.push("btn-success");
          break;

        case this.acciones.DENEGAR:
          clasesBoton.push("btn-danger");
          break;

        case this.acciones.ELIMINAR:
          clasesBoton.push("btn-danger");
          break;

        default:
          break;
      }

      return clasesBoton;
    },

    /**
     * Determina si un botón se puede pintar según el estado de la solicitud
     */
    puedePintarBoton(accion) {
      switch (accion) {
        case this.acciones.GUARDAR:
          return (
            (this.estadoSolicitud == this.estados.REGISTRADA ||
              this.estadoSolicitud == this.estados.CORREGIR) &&
            this.tipo == "MD"
          );
          break;

        case this.acciones.DESCARTAR:
          return this.estadoSolicitud == this.estados.CORREGIR;
          break;

        case this.acciones.ENVIAR:
          return (
            this.estadoSolicitud == this.estados.REGISTRADA ||
            this.estadoSolicitud == this.estados.CORREGIR
          );
          break;

        case this.acciones.APROBAR:
          return (
            this.estadoSolicitud == this.estados.ENAPROBACION ||
            this.estadoSolicitud == this.estados.ENVALIDACION ||
            this.estadoSolicitud == this.estados.ENPROCESO ||
            this.estadoSolicitud == this.estados.ENPROCESOVACACIONES
          );
          break;

        case this.acciones.DENEGAR:
          return (
            this.estadoSolicitud == this.estados.ENAPROBACION ||
            this.estadoSolicitud == this.estados.ENVALIDACION ||
            this.estadoSolicitud == this.estados.ENPROCESO ||
            this.estadoSolicitud == this.estados.ENPROCESOVACACIONES
          );
          break;

        case this.acciones.ELIMINAR:
          return (
            this.estadoSolicitud == this.estados.REGISTRADA && this.tipo == "MD"
          );
          break;

        case this.acciones.REVISAR:
          return (
            this.estadoSolicitud == this.estados.ENVALIDACION ||
            this.estadoSolicitud == this.estados.ENAPROBACION ||
            this.estadoSolicitud == this.estados.ENPROCESOVACACIONES
          );
          break;

        default:
          return true;
          break;
      }

      return clasesBoton;
    }
  }
};
</script>
