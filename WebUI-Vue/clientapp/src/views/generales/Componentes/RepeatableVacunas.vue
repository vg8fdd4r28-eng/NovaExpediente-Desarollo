<template>
  <div class="row pt-1">
    <div class="col-lg-12">
      <FormulateInput
        name="vacunasColaborador"
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
              title="Agregar vacuna"
              @click="agregarNueva(addMore)"
              v-if="puedePintar"
              ><li class="pi pi-plus"></li>
              Agregar vacuna
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
              <div class="col-10">
                <div class="row">
                  <div class="col-lg-5">
                    <FormulateInput
                      type="select"
                      :options="vacunasDLL"
                      name="idVacuna"
                      label="Vacuna"
                      validation="required|max:100,length"
                      input-class="form-control"
                      validation-name="Nombre de la vacuna"
                      placeholder="Seleccione una vacuna"
                      :readonly="!puedePintar"
                      @change="actualizarEstadoModificar(index)"
                    />
                  </div>
                  <div class="col-lg-5">
                    <FormulateInput
                      name="fecha"
                      label="Fecha de aplicación"
                      :validation="'required|before:' + today"
                      :validation-messages="{
                        before:
                          'La fecha de aplicación debe ser menor a la actual.'
                      }"
                      input-class="form-control"
                      type="date"
                      :disabled="!puedePintar"
                      @change="actualizarEstadoModificar(index)"
                    />
                  </div>
                </div>
              </div>
              <div class="col-2 mt-2">
                <div class="block float-right">
                  <CButton
                    type="button"
                    class="btn btn-danger onHoverDark"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Eliminar vacuna"
                    v-if="!pintaEstado(index) && puedePintar"
                    @click="remover(index, removeItem)"
                    ><li class="pi pi-trash"></li>
                  </CButton>
                  <CButton
                    type="button"
                    class="btn btn-success onHoverDark"
                    @click="undo(index)"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Recuperar vacuna eliminada"
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
                <div class="col-12"><hr /></div>
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
  name: "RepeatableVacunas",
  mixins: [common],
  components: {
    "confirm-dialogue": ConfirmDialogue
  },
  props: {
    vacunasDLL: { type: Array, required: true },
    vacunasColaborador: { type: Array, required: true },
    habilitarControles: { type: Boolean, default: false },
    estadoSolicitud: { type: String, default: "RE" },
    nombreFile: { type: String, default: "Seleccione un archivo" }
  },
  data() {
    return {
      //lines: [],
      blockRemoval: true,
      cloneLines: []
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
        title: "Eliminar vacuna",
        message: "Está seguro que desea eliminar la vacuna?",
        okButton: "Eliminar",
        cancelButton: "Cancelar"
      });

      if (ok) {
        return true;
      } else {
        return false;
      }
    },
    async remover(index, cb) {
      if (
        this.blockRemoval &&
        this.vacunasColaborador[index].estado == "N" &&
        (await this.confirmarBorrado())
      ) {
        cb();
      } else if (
        (this.vacunasColaborador[index].estado == "S" ||
          this.vacunasColaborador[index].estado == "M") &&
        (await this.confirmarBorrado())
      ) {
        this.$emit("remove", index);
      }

      return false;
    },
    undo(index) {
      this.$emit("undo", index);
    },
    agregarNueva(cb) {
      cb();
      this.$emit("add");
    },
    actualizarEstadoModificar(index) {
      this.$emit("modify", index);
    },
    pintaEstado(index) {
      if (this.vacunasColaborador[index] && this.vacunasColaborador[index].estado) {
        return this.vacunasColaborador[index].estado === "E";
      }
      return false;
    },
    pintaEstadoBadge(index) {
      return this.vacunasColaborador[index] && this.vacunasColaborador[index].estado != "S";
    },
    pintaEstadoBadgeMsg(index) {
      let estado = "";

      switch (this.vacunasColaborador[index] && this.vacunasColaborador[index].estado) {
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

      switch (this.vacunasColaborador[index] && this.vacunasColaborador[index].estado) {
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
    // console.log(values);
  },

  // computed is a function // is like a get
  computed: {
    puedePintar() {
      return this.habilitarControles;
    },
    puedePintarCpic() {
      return (
        this.estadoSolicitud == common.REGISTRADA ||
        this.estadoSolicitud == common.CORREGIR ||
        this.estadoSolicitud == common.ENVALIDACION
      );
    },
    helpFileUpload() {
      return (this.estadoSolicitud == common.REGISTRADA ||
        this.estadoSolicitud == common.CORREGIR) === true
        ? "Formatos válidos: png/jpg/gif/pdf"
        : "";
    },
    today() {
      return new Date();
    }
  }
};
</script>

<style>
.wrapper-form-borderer {
  border-bottom: 1px solid lightgray;
}
</style>
