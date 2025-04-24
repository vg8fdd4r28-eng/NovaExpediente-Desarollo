<template>
  <div class="row pt-1">
    <div class="col-lg-12">
      <FormulateInput
        name="contactos"
        type="group"
        :repeatable="true"
        mode="plain"
      >
        <template #addmore="{addMore}">
          <div class="row mb-3">
            <CButton
              type="button"
              class="btn btn-info ml-1  libro-marca-celeste onHoverDark ml-3"
              data-toggle="tooltip"
              data-placement="top"
              title="Agregar contacto"
              @click="agregarContacto(addMore)"
              v-if="puedePintar"
              ><li class="pi pi-plus"></li>
              Agregar contacto
            </CButton>
          </div>
        </template>
        <template #repeatable="{index, removeItem}">
          <div>
            <div
              class="row"
              :class="{
                'bg-light': pintaEstado(index)
              }"
            >
              <div class="col-11">
                <div class="row">
                  <div class="col-lg-4">
                    <FormulateInput
                      name="contacto"
                      label="Nombre del contacto"
                      placeholder="Ingrese el nombre del contacto"
                      validation="required|max:100,length"
                      validation-name="Nombre del contacto"
                      :input-class="['form-control']"
                      :help-class="['hint']"
                      :readonly="!puedePintar"                    
                      @change="actualizarEstadoModificar(index)"
                    />
                  </div>
                  <div class="col-lg-4">
                    <FormulateInput
                      type="tel"
                      name="telefono"
                      label="Teléfono del contacto"
                      placeholder="Ingrese el número de teléfono"
                      help="Opcional o Formatos: +00 00000000 o +000 00000000"
                      :validation="[
                                      ['optional'],
                                      ['matches', /^\+(?:[0-9] ?){6,14}[0-9]$/]
                                    ]"
                      validation-name="Teléfono"
                      :input-class="['form-control']"
                      :help-class="['hint']"
                      :readonly="!puedePintar"
                      @change="actualizarEstadoModificar(index)"
                    />
                  </div>
                  <div class="col-lg-4">
                    <FormulateInput
                       type="email"
                        name="correo"
                        label="Correo del contacto"
                        placeholder="Ingrese el correo"
                        help="correo@dominio.ext"
                        validation="optional|email|max:100,length"
                        validation-name="Correo"
                        :input-class="['form-control']"
                        :help-class="['hint']"
                        :readonly="!puedePintar"
                      @change="actualizarEstadoModificar(index)"
                    />
                  </div>
                </div>
              </div>
              <div class="col-1 mt-2">
                <div class="block float-right">
                  <CButton
                    type="button"
                    class="btn btn-danger onHoverDark"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Eliminar contacto"
                    v-if="!pintaEstado(index) && puedePintar"
                    @click="removerContacto(index, removeItem)"
                    ><li class="pi pi-trash"></li>
                  </CButton>
                  <CButton
                    type="button"
                    class="btn btn-success onHoverDark"
                    @click="undoContacto(index)"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Recuperar contacto eliminado"
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
            <div>
              <div class="row">
                <div class="col-12"><hr></div>
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
import ConfirmDialogue from "./PopUpModal/ConfirmDialogue.vue";
// métodos comunes
import common from "../../masterdata/common.js";

export default {
  name: "ContactosProyecto",
  components: {
    "confirm-dialogue": ConfirmDialogue
  },
  props: {
    contactos: { type: Array, required: true },
    habilitarControles: { type: Boolean, default: false },
    nombreFile: { type: String, default: "Seleccione un archivo" }
  },
  data() {
    return {
     
    };
  },
  methods: {
     showModalVisor(show) {
      this.visorEnable = show;
    },
    async confirmarBorrado() {
      // se configura el popup de confirmación de borrado
      // y se queda a la espera de la indicación del usuario
      const ok = await this.$refs.confirmDialogue.show({
        title: "Eliminar contacto",
        message: "Está seguro que desea eliminar el contacto?",
        okButton: "Eliminar",
        cancelButton: "Cancelar"
      });

      if (ok) {
        return true;
      } else {
        return false;
      }
    },
    async removerContacto(index, cb) {

      if (
        this.contactos.length > 0 &&
        this.contactos[index].estado == "N" &&
        (await this.confirmarBorrado())
      ) {
        cb();
      } else if (
        (this.contactos[index].estado == "S" ||
          this.contactos[index].estado == "M") &&
        (await this.confirmarBorrado())
      ) {
        this.$emit("remove", index);
      }
      return false;
    },
    undoContacto(index) {
      this.$emit("undo", index);
    },
    agregarContacto(cb) {
      cb();
      this.$emit("add");
    },
    actualizarEstadoModificar(index) {
      this.$emit("modify", index);
    },
    pintaEstado(index) {
      if (this.contactos[index] && this.contactos[index].estado) {
        return this.contactos[index].estado === "E";
      }
      return false;
    }
    ,
    pintaEstadoBadge(index) {
      return this.contactos[index] && this.contactos[index].estado != "S";
    },
    pintaEstadoBadgeMsg(index) {
      let estado = "";

      switch (this.contactos[index] && this.contactos[index].estado) {
        case "E":
          estado = "Eliminado";
          break;
        case "N":
          estado = "Nuevo";
          break;
        case "M":
          estado = "Modificado";
          break;
        default:
          break;
      }

      return estado;
    },
    pintaEstadoColorBadge(index) {
      let estado = "";

      switch (this.contactos[index] && this.contactos[index].estado) {
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
    }
  },
  mounted() {
    //this.addLine();
   // console.log(this.estadoSolicitud);
  },
  updated() {
    // this.cloneLines = this.lines.map(a => Object.assign({}, a));
  },
  submit(values) {
  //  console.log(values);
  },

  // computed is a function // is like a get
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

<style>
.wrapper-form-borderer {
  border-bottom: 1px solid lightgray;
}
</style>
