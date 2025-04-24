<template>
  <div class="row pt-1">
    <div class="col-lg-12">
      <FormulateInput
        name="proveedores"
        type="group"
        :repeatable="true"
        mode="plain"
        label="Proveedores"
      >
        <template #addmore="{addMore}">
          <div class="row pt-1 mb-2">
            <CButton
              type="button"
              class="btn btn-info ml-1  libro-marca-celeste onHoverDark"
              data-toggle="tooltip"
              data-placement="top"
              title="Agregar proveedor"
              @click="agregarNuevo(addMore)"
              v-if="puedePintar"
              v-tooltip.top="
                'Permite agregar los proveedores de la certificación'
              "
              ><li class="pi pi-plus"></li>
              Agregar proveedor
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
                    name="idproveedorcertificacion"
                    :options="proveedoresDDL"
                    type="select"
                    label="Seleccione un proveedor"
                    validation-name="Proveedor"
                    placeholder="Seleccione una opción"
                    validation="required|not:0"
                    :validation-messages="{
                      required: 'Seleccione una opción.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    :disabled="disableProveedor(index)"
                    @change="onChangeProveedor(index, $event)"
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
                  title="Eliminar proveedor"
                  v-if="!pintaEstado(index) && puedePintar"
                  @click="removerProveedor(index, removeItem)"
                  ><li class="pi pi-trash"></li>
                </CButton>
                <CButton
                  type="button"
                  class="btn btn-success onHoverDark"
                  @click="undoProveedor(index)"
                  data-toggle="tooltip"
                  data-placement="top"
                  title="Recuperar proveedor eliminado"
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
import { ValidaCertificacionProveedorDependencias } from "../request";

import ConfirmDialogue from "../../generales/Componentes/PopUpModal/ConfirmDialogue.vue";

export default {
  name: "RepeatableProveedores",
  components: {
    "confirm-dialogue": ConfirmDialogue
  },
  mixins: [common],
  props: {
    proveedores: { type: Array, required: true },
    proveedoresDDL: { type: Array, required: true },
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
        title: "Eliminar proveedor",
        message: "Está seguro que desea eliminar el proveedor registrado?",
        okButton: "Eliminar",
        cancelButton: "Cancelar"
      });

      if (ok) {
        return true;
      } else {
        return false;
      }
    },
    async removerProveedor(index, cb) {
      if (this.proveedores[index]) {
        if (
          this.proveedores[index].estado == "S" ||
          this.proveedores[index].estado == "M"
        ) {
          //valida dependencias
          if (await this.ValidaDependenciasEliminar( this.proveedores[index].idproveedorcertificacion)) {
             if (
            this.proveedores.length > 0 &&
            this.proveedores[index].estado == "N" &&
            (await this.confirmarBorrado())
          ) {
            cb();
            this.$emit("remove", index);
          } else if (
            (this.proveedores[index].estado == "S" ||
              this.proveedores[index].estado == "M") &&
            (await this.confirmarBorrado())
          ) {
            this.$emit("remove", index);
          }
          }else{
            //alert('here')
          }
        } else {
          if (
            this.proveedores.length > 0 &&
            this.proveedores[index].estado == "N" &&
            (await this.confirmarBorrado())
          ) {
            cb();
            this.$emit("remove", index);
          } else if (
            (this.proveedores[index].estado == "S" ||
              this.proveedores[index].estado == "M") &&
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
    undoProveedor(index) {
      this.$emit("undo", index);
    },

    async onChangeProveedor(index, $event) {
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
      if (this.proveedores[index] && this.proveedores[index].estado) {
        return this.proveedores[index].estado === "E";
      }
      return false;
    },
    pintaEstadoBadge(index) {
      return this.proveedores[index] && this.proveedores[index].estado != "S";
    },
    pintaEstadoBadgeMsg(index) {
      let estado = "";

      switch (this.proveedores[index] && this.proveedores[index].estado) {
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

      switch (this.proveedores[index] && this.proveedores[index].estado) {
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
    disableProveedor(index) {
      if (this.proveedores) {
        if (this.proveedores[index]) {
          return (
            !this.puedePintar ||
            this.proveedores[index].estado === "S" ||
            this.proveedores[index].estado === "E"
          );
        }
      }
      return !this.puedePintar;
    },

    async ValidaDependenciasEliminar(id) {
      
      let result = "";

      result = await ValidaCertificacionProveedorDependencias({
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
    }
  }
};
</script>

<style scoped>
.wrapper-form-borderer {
  border-bottom: 1px solid lightgray;
}
</style>
