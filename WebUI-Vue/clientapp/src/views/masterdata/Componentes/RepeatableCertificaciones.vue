<template>
  <div class="row pt-1">
    <div class="col-lg-12">
      <FormulateInput
        name="certificaciones"
        type="group"
        :repeatable="true"
        mode="plain"
        label="Certificaciones"
      >
        <template #addmore="{addMore}">
          <div class="row pt-1 mb-2">
            <CButton
              type="button"
              class="btn btn-info ml-1  libro-marca-celeste onHoverDark"
              data-toggle="tooltip"
              data-placement="top"
              title="Agregar certificación"
              @click="agregarNuevo(addMore)"
              v-if="puedePintar"
              v-tooltip.top="
                'Permite agregar las certificaciones del proveedor'
              "
              ><li class="pi pi-plus"></li>
              Agregar certificación
            </CButton>
          </div>
        </template>
        <template #repeatable="{index, removeItem}">
          <div
            class="row wrapper-form-borderer"
            :class="{
              'bg-light': pintaEstado(index)
            }"
          >
            <div class="col-11">
              <div class="row">
                <div class="col-lg-6">
                  <FormulateInput
                    name="idcertificacion"
                    :options="certificacionesDDL"
                    type="select"
                    label="Seleccione una certificación"
                    validation-name="Certificación"
                    placeholder="Seleccione una opción"
                    validation="required|not:0"
                    :validation-messages="{
                      required: 'Seleccione una opción.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    :disabled="disableCertificacion(index)"
                    @change="onChangeCertificacion(index, $event)"
                  />
                </div>
              </div>
            </div>
            <div class="col-1 mt-2 align-self-center">
              <div class="block float-right">
                <CButton
                  type="button"
                  class="btn btn-danger onHoverDark"
                  data-toggle="tooltip"
                  data-placement="top"
                  title="Eliminar certificación"
                  v-if="!pintaEstado(index) && puedePintar"
                  @click="removerCertificacion(index, removeItem)"
                  ><li class="pi pi-trash"></li>
                </CButton>
                <CButton
                  type="button"
                  class="btn btn-success onHoverDark"
                  @click="undoCertificacion(index)"
                  data-toggle="tooltip"
                  data-placement="top"
                  title="Recuperar certificación eliminada"
                  v-if="pintaEstado(index) && puedePintar"
                  ><li class="pi pi-undo"></li>
                </CButton>

                <CBadge
                  :key="index"
                  :color="pintaEstadoColorBadge(index)"
                  class="mr-2"
                  v-if="pintaEstadoBadge(index) && !puedePintar"
                >
                  {{ pintaEstadoBadgeMsg(index) }}</CBadge
                >
              </div>
            </div>
          </div>
        </template>
      </FormulateInput>
    </div>
    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
  </div>
</template>

<script>
// métodos comunes

import common from "../common.js";
import Vue from "vue";
import { ValidaProveedorCertificacionDependencias } from "../request";

import ConfirmDialogue from "../../generales/Componentes/PopUpModal/ConfirmDialogue.vue";

export default {
  name: "RepeatableCertificaciones",
  components: {
    "confirm-dialogue": ConfirmDialogue
  },
  mixins: [common],
  props: {
    certificaciones: { type: Array, required: true },
    certificacionesDDL: { type: Array, required: true },
    habilitarControles: { type: Boolean, default: false },
    idColaborador: { type: Number, default: 0 },
    idcatalogo: { type: Number, default: 0 }
  },
  data() {
    return {
      blockRemoval: true
    };
  },
  mounted() {},
  methods: {
    async confirmarBorrado() {
      // se configura el popup de confirmación de borrado
      // y se queda a la espera de la indicación del usuario
      const ok = await this.$refs.confirmDialogue.show({
        title: "Eliminar certificación",
        message: "Está seguro que desea eliminar la certificación registrada?",
        okButton: "Eliminar",
        cancelButton: "Cancelar"
      });

      if (ok) {
        return true;
      } else {
        return false;
      }
    },
    async removerCertificacion(index, cb) {
      if (this.certificaciones[index]) {
        if (
          this.certificaciones[index].estado == "S" ||
          this.certificaciones[index].estado == "M"
        ) {
           //valida dependencias
          if (await this.ValidaDependenciasEliminar( this.certificaciones[index].idcertificacion)) {
              if (
            this.certificaciones.length > 0 &&
            this.certificaciones[index].estado == "N" &&
            (await this.confirmarBorrado())
          ) {
            cb();
            this.$emit("remove", index);
          } else if (
            (this.certificaciones[index].estado == "S" ||
              this.certificaciones[index].estado == "M") &&
            (await this.confirmarBorrado())
          ) {
            this.$emit("remove", index);
          }
          }else{

          }
        } else {
          if (
            this.certificaciones.length > 0 &&
            this.certificaciones[index].estado == "N" &&
            (await this.confirmarBorrado())
          ) {
            cb();
            this.$emit("remove", index);
          } else if (
            (this.certificaciones[index].estado == "S" ||
              this.certificaciones[index].estado == "M") &&
            (await this.confirmarBorrado())
          ) {
            this.$emit("remove", index);
          }
        }
      } else {
        cb();
        this.$emit("remove", index);
      }

      return false;
    },
    undoCertificacion(index) {
      this.$emit("undo", index);
    },

    async onChangeCertificacion(index, $event) {
      this.actualizarEstadoModificar(index);
    },
    agregarNuevo(cb) {
      cb();
      this.$emit("add");
    },
    actualizarEstadoModificar(index) {
      this.$emit("modify", index);
    },

    submit(values) {
      console.log(values);
    },
    pintaEstado(index) {
      if (this.certificaciones[index] && this.certificaciones[index].estado) {
        return this.certificaciones[index].estado === "E";
      }
      return false;
    },
    pintaEstadoBadge(index) {
      return (
        this.certificaciones[index] && this.certificaciones[index].estado != "S"
      );
    },
    pintaEstadoBadgeMsg(index) {
      let estado = "";

      switch (
        this.certificaciones[index] &&
        this.certificaciones[index].estado
      ) {
        case "E":
          estado = "Eliminada";
          break;
        case "N":
          estado = "Nueva";
          break;
        case "M":
          estado = "Modificada";
          break;
        default:
          break;
      }

      return estado;
    },
    pintaEstadoColorBadge(index) {
      let estado = "";

      switch (
        this.certificaciones[index] &&
        this.certificaciones[index].estado
      ) {
        case "E":
          estado = "danger";
          break;
        case "N":
          estado = "success";
          break;
        case "M":
          estado = "warning";
          break;
        default:
          break;
      }

      return estado;
    },
    disableCertificacion(index) {
      if (this.certificaciones) {
        if (this.certificaciones[index]) {
          return (
            !this.puedePintar ||
            this.certificaciones[index].estado === "S" ||
            this.certificaciones[index].estado === "E"
          );
        }
      }
      return !this.puedePintar;
    },
    async ValidaDependenciasEliminar(id) {
      let result = "";

      result = await ValidaProveedorCertificacionDependencias({
        id: id,
        idCatalogo: this.idcatalogo
      });

      if (result.data != "") {
        Vue.$toast.error(result.data, {});
        return false;
      } else {
        return true;
      }
    }
  },
  computed: {
    puedePintar() {
      return this.habilitarControles;
      // return (
      //   this.estadoSolicitud == common.REGISTRADA ||
      //   this.estadoSolicitud == common.CORREGIR
      // );
    }
  }
};
</script>

<style scoped>
.wrapper-form-borderer {
  border-bottom: 1px solid lightgray;
}
</style>
