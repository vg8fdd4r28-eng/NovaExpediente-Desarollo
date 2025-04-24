<template>
  <div class="row pt-1">
    <div class="col-lg-12">
      <FormulateInput
        name="experiencias"
        type="group"
        :repeatable="true"
        mode="plain"
      >
        <template #addmore="{addMore}">
          <div class="row pt-1 mb-2 mt-1">
            <CButton
              type="button"
              class="btn btn-info ml-1  libro-marca-celeste onHoverDark"
              data-toggle="tooltip"
              data-placement="top"
              title="Agregar experiencia"
              @click="agregarNuevaExperiencia(addMore)"
              v-if="puedePintar"
              v-tooltip.top="'Permite actualizar sus meses de experiencia en una tecnología'"
              ><li class="pi pi-plus"></li>
              Agregar experiencia
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
            <div class="col-11 mt-1">
              <div class="row">
                <div class="col-lg-3">
                  <FormulateInput
                    class="mb-3"
                    name="idTipoExperiencia"
                    :options="TiposExperiencia()"
                    type="select"
                    label="Tipo de tecnología"
                    placeholder="Seleccione un tipo tecnología"
                    validation="required|not:0"
                    :validation-messages="{
                      required: 'Seleccione un tipo tecnología.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    @change="onChangeTipoExperiencia(index, $event)"
                    :disabled="!puedePintar"
                  />
                </div>
                <div class="col-lg-5">
                  <FormulateInput
                    name="idExperiencia"
                    :options="experiencias[index] ? experiencias[index].opcionesExperiencia : null"
                    type="select"
                    label="Nombre específico"
                    placeholder="Seleccione una opción"
                    validation="required|not:0"
                    :validation-messages="{
                      required: 'Seleccione una opción.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    :disabled="!puedePintar"
                    @change="onChangeExperiencia(index, $event)"
                  />
                </div>
                <div class="col-lg-2">
                  <FormulateInput
                    type="number"
                    name="cantMesesXExperiencia"
                    label="Meses"
                    placeholder="Cantidad"
                    validation="required|min:1|max:99999|validarMeses"
                    input-class="form-control"
                    :readonly="!puedePintar"
                    validation-name="Meses de experiencia"
                    :validation-rules="{validarMeses}"
                    :validation-messages="{
                      validarMeses: 'Cantidad inválida'
                    }"
                    @change="actualizarEstadoModificar(index)"
                  />
                </div>
                 <div class="col-lg-2 align-self-center">
                  <span class="pi pi-info-circle text-muted" style="width: 1.2rem;text-align: center; color:black !important;font-size:.8rem;" 
                        data-toggle="tooltip" title="" data-original-title="(870) 288-4149" 
                        v-tooltip.top="'Experiencia actual'"></span>
                  <span class="text-muted small">{{experiencias[index] && experiencias[index].meses ? experiencias[index].meses + " meses" : "Indefinida"}}</span>
                  <br>
                  <span  class="pi pi-calendar text-muted" style="width: 1.2rem;text-align: center; color:black !important;font-size:.8rem;"  
                       data-toggle="tooltip" data-original-title="" title=""
                        v-tooltip.top="'Última actualización'"></span>
                  <span class="text-muted small text-truncate">{{experiencias[index] ? formatearFecha(experiencias[index].ultimaActualizacion) : "Indefinida"}}</span>
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
                  title="Eliminar experiencia"
                  v-if="!pintaEstado(index) && puedePintar"
                  @click="removerExperiencia(index, removeItem)"
                  ><li class="pi pi-trash"></li>
                </CButton>
                <CButton
                  type="button"
                  class="btn btn-success onHoverDark"
                  @click="undoExperiencia(index)"
                  data-toggle="tooltip"
                  data-placement="top"
                  title="Recuperar experiencia eliminada"
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
import {
  GetAmbienteDesarrollo,
  GetBaseDatos,
  GetBus,
  GetErpCore,
  GetCalidad,
  GetInfraestructura,
  GetLenguajeProgramacion,
  GetBPMS,
  GetTecnologia,
  GetExperienciaActual,
  GetCloud
} from "../../masterdata/request.js";
import { aplicarFormatoFecha } from "../../../utils";

const requestsTiposExperiencias = [
  {
    id: "1",
    responseKey: "basesDatos",
    value: "idbasedatos",
    label: "nombre",
    cb: GetBaseDatos
  },
  {
    id: "2",
    responseKey: "lenguajesProgramacion",
    value: "idlenguajeprogramacion",
    label: "nombre",
    cb: GetLenguajeProgramacion
  },
  {
    id: "3",
    responseKey: "ambientesDesarrollo",
    value: "idambientedesarrollo",
    label: "nombre",
    cb: GetAmbienteDesarrollo
  },
  {
    id: "4",
    responseKey: "tecnologias",
    value: "idtecnologia",
    label: "nombre",
    cb: GetTecnologia
  },
  {
    id: "5",
    responseKey: "erpCores",
    value: "iderpcore",
    label: "nombre",
    cb: GetErpCore
  },
  {
    id: "6",
    responseKey: "calidads",
    value: "idcalidad",
    label: "nombre",
    cb: GetCalidad
  },
  {
    id: "7",
    responseKey: "infraestructuras",
    value: "idinfraestructura",
    label: "nombre",
    cb: GetInfraestructura
  },
  {
    id: "8",
    responseKey: "buses",
    value: "idBus",
    label: "nombre",
    cb: GetBus
  },
  {
    id: "9",
    responseKey: "bpMs",
    value: "idBPM",
    label: "nombre",
    cb: GetBPMS
  },
  {
    id: "10",
    responseKey: "clouds",
    value: "idcloud",
    label: "nombre",
    cb: GetCloud
  }
];

// métodos comunes
import common from "../../masterdata/common.js";

import ConfirmDialogue from "./PopUpModal/ConfirmDialogue.vue";


export default {
  name: "RepeatableGroup",
  components: {
    "confirm-dialogue": ConfirmDialogue
  },
   mixins: [common],
  props: {
    experiencias: { type: Array, required: true},
    habilitarControles: { type: Boolean, default: false },
    idColaborador:{ type: Number, default: 0 }
  },
  data() {
    return {
      //lines: [],
      blockRemoval: true,
      cloneLines: []
    };
  },
  methods: {
    async confirmarBorrado() {
      // se configura el popup de confirmación de borrado
      // y se queda a la espera de la indicación del usuario
      const ok = await this.$refs.confirmDialogue.show({
        title: "Eliminar experiencia",
        message: "Está seguro que desea eliminar la experiencia registrada?",
        okButton: "Eliminar",
        cancelButton: "Cancelar"
      });

      if (ok) {
        return true;
      } else {
        return false;
      }
    },
    async removerExperiencia(index, cb) {
      if (
        this.experiencias.length > 0 &&
        this.experiencias[index].estado == "N" &&
        (await this.confirmarBorrado())
      ) {
        cb();
      } else if (
        (this.experiencias[index].estado == "S" ||
          this.experiencias[index].estado == "M") &&
        (await this.confirmarBorrado())
      ) {
        this.$emit("remove", index);
      }
      return false;
    },
    undoExperiencia(index) {
      this.$emit("undo", index);
    },
    async onChangeTipoExperiencia(index, $event) {
      const { responseKey, value, label, cb } = requestsTiposExperiencias.find(
        ({ id }) => id === $event.target.value
      );

      const { data } = await cb();
      //asigna de manera dinamica el contenido de la lista segun el tipo de solicitud
      this.$set(
        this.experiencias[index],
        "opcionesExperiencia",
        data[responseKey].map(d => ({
          label: d[label],
          value: d[value]
        }))
      );
      this.actualizarEstadoModificar(index);
      
    },
    async onChangeExperiencia(index, $event) {
      //this.experiencias[index].meses = $event.target.value + " meses";
      // const { responseKey, value, label, cb } = requestsTiposExperiencias.find(
      //   ({ id }) => id === $event.target.value
      // );
        GetExperienciaActual({
          idColaborador: this.idColaborador,
          idCatalogo: this.experiencias[index].idTipoExperiencia,
          idInstanciaCatalogo: $event.target.value
        })
        .then(response => {
          this.experiencias[index].meses = response.data.meses; // ? response.data.meses + " meses" : "Indefinido";
          this.experiencias[index].ultimaActualizacion = response.data.ultimaActualizacion;
          this.actualizarEstadoModificar(index);
        })
        .catch(response => {
          console.log("Error al obtener meses de experiencia")
        });

      
      // //asigna de manera dinamica el contenido de la lista segun el tipo de solicitud
      // this.$set(
      //   this.experiencias[index],
      //   "opcionesExperiencia",
      //   data[responseKey].map(d => ({
      //     label: d[label],
      //     value: d[value]
      //   }))
      // );
    },
    agregarNuevaExperiencia(cb) {
      
      cb();
      this.experiencias[this.experiencias.length - 1].estado = "N";
      this.experiencias[this.experiencias.length - 1].meses = null;
      this.experiencias[this.experiencias.length - 1].ultimaActualizacion = null;
      this.$emit("add");
    },
    actualizarEstadoModificar(index){
      this.$emit("modify", index);
    },
    formatearFecha(fecha){
      return fecha ? aplicarFormatoFecha(fecha,"dd-MM-yyyy")  : "Indefinida"
    },
    validarMeses: function(context) {
        const { value, getGroupValues } = context;
        var ToDate = new Date();
        var valoresExperiencia = getGroupValues();
        var mesesDiferencia = 0;
        var fechaDefault = new Date(2021, 0, 1);

        if (valoresExperiencia.ultimaActualizacion) {
          var fromDate = new Date(valoresExperiencia.ultimaActualizacion);
          mesesDiferencia = (ToDate.getFullYear() - fromDate.getFullYear()) * 12 + 
                            (ToDate.getMonth() - fromDate.getMonth());
          // ToDate.getMonth() - fromDate.getMonth() + 
          //                                           (12 * (ToDate.getFullYear() - 
          //                                                 fromDate.getFullYear()));
        }
        else{
          mesesDiferencia = 500; // cuando no existe una actualización previa 
                            //(ToDate.getFullYear() - fechaDefault.getFullYear()) * 12 + 
                            //(ToDate.getMonth() - fechaDefault.getMonth());
          // ToDate.getMonth() - fechaDefault.getMonth() + 
          //                                 (12 * (ToDate.getFullYear() - fechaDefault.getFullYear()));
        }
        
        if (value > mesesDiferencia) {
          return false;
        }

        return true;
    },
    submit(values) {
     // console.log(values);
    },
      pintaEstado(index) {
      if (this.experiencias[index] && this.experiencias[index].estado) {
        return this.experiencias[index].estado === "E";
      }
      return false;
    },
      pintaEstadoBadge(index) {
      return this.experiencias[index] && this.experiencias[index].estado != "S";
    },
    pintaEstadoBadgeMsg(index) {
      let estado = "";

      switch (this.experiencias[index] && this.experiencias[index].estado) {
        case "E":
          estado = "Eliminada";
          break;
        case "N":
          if (this.experiencias[index].ultimaActualizacion) {
            estado = "Modificada";
          } else {
            estado = "Nueva";
          }
          
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

      switch (this.experiencias[index] && this.experiencias[index].estado) {
        case "E":
          estado = "danger";
          break;
        case "N":
          if (this.experiencias[index].ultimaActualizacion) {
            estado = "warning";
          } else {
            estado = "success";
          }
          
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
    
  },
  updated() {
    this.cloneLines = this.lines.map(a => Object.assign({}, a));
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
