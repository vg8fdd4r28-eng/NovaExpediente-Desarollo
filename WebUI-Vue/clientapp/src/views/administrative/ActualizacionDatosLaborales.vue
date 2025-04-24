<!-- Plantilla de Datos Laborales -->
<template>
  <div>
    <FormulateForm
      v-model="colaborador"
      @submit="submit()"
      @failed-validation="errorValidacion"
      :invalid-message="this.invalidMessage"
      autocomplete="nope"
    >
      <!-- Datos Laborales -->
      <CCard style="border-radius: 16px !important">
        <CCardHeader
          style="
            border-radius: 16px !important;
            border-bottom: transparent !important;
            background-color: #ffffff;
          "
        >
          <strong>Actualización de un colaborador</strong>
          <CBadge color="primary" class="ml-2" shape="pill">
            <span style="margin-top: 0.2rem; margin-bottom: 0.2rem">
              Datos Laborales
            </span>
          </CBadge>
        </CCardHeader>
        <CCardBody>
          <resumen-colaborador
            :colaborador.sync="colaborador"
            estado-solicitud="RE"
            :searchable="true"
            :editableIdentificacion="true"
            :habilitar-controles="true"
            :visible="true"
            @displayModal="displayModal"
          >
          </resumen-colaborador>
        </CCardBody>
      </CCard>

      <!-- Datos administrativos -->
      <CCard style="border-radius: 16px !important">
        <CCardHeader
          style="
            border-radius: 16px !important;
            border-bottom: transparent !important;
            background-color: #ffffff;
          "
        >
          <strong>Datos administrativos</strong>
        </CCardHeader>
        <CCardBody>
          <!-- empresa y area -->
          <CRow>
            <CCol sm="6" class="">
              <FormulateInput
                type="email"
                name="correo"
                label="Correo"
                placeholder="Ingrese el correo de la empresa"
                help="correo@crnova.com"
                validation="required|email"
                validation-name="Correo"
                :input-class="['form-control']"
                :help-class="['hint']"
              />
            </CCol>
            <CCol sm="6" class="">
              <FormulateInput
                type="number"
                name="idNomina"
                label="Id de nómina"
                placeholder="Ingrese el id de nómina"
                validation="number"
                input-class="form-control"
                validation-name="Id de nómina"
              />
            </CCol>
          </CRow>

          <CRow>
            <CCol sm="6" class="">
              <label>Es servicios profesionales</label>
              <FormulateInput
                type="myInputSwitch"
                name="esservicioprofesional"
                :input-class="['form-control', 'readonly']"
                validation-name="Servicios profesionales"
                class="hint"
              />
            </CCol>
            <CCol sm="6" class="">
              <label>Solicita vacaciones</label>
              <FormulateInput
                type="myInputSwitch"
                name="solicitaVacaciones"
                :input-class="['form-control', 'readonly']"
                validation-name="Servicios profesionales"
                class="hint"
              />
            </CCol>
          </CRow>
          <CRow>
            <CCol sm="6" class="">
              <FormulateInput
                type="number"
                name="vacacionesdiasanuales"
                label="Cantidad de vacaciones por año"
                placeholder="Ingrese la cantidad de vacaciones por año"
                validation="number"
                input-class="form-control"
                validation-name="Cantidad de vacaciones por año"
              />
            </CCol>
            <CCol sm="6" class="">
              <FormulateInput
                type="number"
                name="saldoVacaciones"
                label="Saldo de vacaciones"
                placeholder="Ingrese el saldo de vacaciones"
                validation="required|number|min:0"
                input-class="form-control"
                validation-name="Saldo de vacaciones"
              />
            </CCol>
          </CRow>

          <CRow>
            <CCol sm="6" class="">
              <FormulateInput
                name="idareaorganizativa"
                :options="areasList"
                type="select"
                label="Área organizativa"
                placeholder="Seleccione un área organizativa"
                validation="required|not:0"
                validation-name="Área organizativa"
                :validation-messages="{
                  required: 'Seleccione un Área organizativa.',
                  not: 'Debe de seleccionar una opción válida.',
                }"
                :input-class="['form-control']"
              />
            </CCol>
            <CCol sm="6" class="">
              <FormulateInput
                type="date"
                name="fechaingreso"
                label="Fecha de ingreso"
                :validation="'required|before:' + today"
                :validation-messages="{
                  before: 'La fecha de ingreso debe ser menor a la actual.',
                }"
                input-class="form-control"
                :help-class="['hint']"
              />
            </CCol>
          </CRow>
        </CCardBody>
      </CCard>

      <FormulateErrors />
      <CRow style="margin-bottom: 80px">
        <CCol sm="12" class="mt-4 mb-3">
          <!-- Guardar -->
          <CButton
            class="pull-right btn btn-info libro-marca-celeste onHoverDark"
            type="submit"
          >
            <CIcon name="cil-save" />
            Guardar
          </CButton>
        </CCol>
      </CRow>
    </FormulateForm>
    <Dialog
      :visible.sync="showModal"
      :style="{ width: '70vw' }"
      :maximizable="true"
      :modal="true"
    >
      <template #header>
        <h5>{{ tituloModal }}</h5>
      </template>

      <pick-list
        @selected="selected"
        :nombreTipoLista="nombreTipoLista"
        :columnas="columnasPickList"
        :dataContenido="dataPickList"
      ></pick-list>

      <template #footer> </template>
    </Dialog>

    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
    <comment-dialogue ref="commentDialogue"></comment-dialogue>
  </div>
</template>

<script>
import Vue from "vue";
// Métodos de llamados a API
import {
  GetColaboradoresPerfil,
  DatosLaborales,
  GetColaboradorDetail,
  GetAreasOrganizativas,
} from "./request";

// Componentes
import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "../masterdata/ResumenColaborador";

// métodos comunes
import common from "../masterdata/common.js";
import { aplicarFormatoFecha } from "../../utils";
import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";

// Definición del componente DatosPersonales
export default {
  name: "ActualizacionDatosLaborales",
  mixins: [common],
  components: {
    "pick-list": PickList,
    "resumen-colaborador": ResumenColaborador,
    "confirm-dialogue": ConfirmDialogue,
    "comment-dialogue": CommentDialogue,
  },

  data() {
    return {
      colaborador: {
        // values de colaborador
        idcolaborador: 0,
        identificacion: null,
        nombre: null,
        primerapellido: null,
        segundoapellido: null,
        foto: null,
        idNomina: null,
        correo: "",
        esservicioprofesional: "No",
        solicitaVacaciones: "No",
        saldoVacaciones: 0,
        vacacionesdiasanuales: 0,
        idareaorganizativa: null,
        fechaingreso: null,
      },
      comentarios: [],
      showModal: false, // controla la presentación del modal
      nombreTipoLista: "", // referencia el tipo de contenido de un pick
      tituloModal: "", // título para la ventana modal dependiendo del tipo de contenido del pick
      columnasPickList: [], // lista de columnas a mostrar en el componente tabla del pick
      dataPickList: [], // datos para el componente tabla del pick

      TipoListaPick: "",
      mensajeError: common.MENSAJE_ERROR,
      visorEnable: false,
      puestoList: [],
      especialidadList: [],
      empresaList: [],
      areasList: [],
      dmList: [],
      clienteList: [],
      areasList: [],
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...

    this.disableAutoComplete();
    await this.cargarAreasOrganizativas();
  },

  methods: {
    // showModalVisor(show) {
    //   this.visorEnable = show;
    // },
    /**
     * Invoca al API que devuelve la lista de areas organizativas
     */
    async cargarAreasOrganizativas() {
      const {
        data: { areaOrganizativas },
      } = await GetAreasOrganizativas();
      this.areasList = areaOrganizativas.map(
        ({ idareaorganizativa, nombre }) => ({
          label: nombre,
          value: idareaorganizativa,
        })
      );
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        if (fields[key].type !== "group") {
          fieldNames += `-  ${fields[key].label}\r\n`;
        } else {
          switch (fields[key].name) {
            case "afiliaciones":
              fieldNames += "-  Sección de otras afiliaciones\r\n";
              break;

            default:
              break;
          }
        }
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    /**
     * Atiende el evento "failed-validation" del FormulateForm
     * El método muestra un mensaje temporal indicando que el
     * formulario presenta errores de validación
     *
     * @data contiene la lista de validaciones que no se cumplen
     *  */

    errorValidacion(data) {
      Vue.$toast.warning(common.MENSAJE_ERROR, {
        // optional options Object
      });
    },

    /**
     * Obtiene los datos del formulario para el submit
     */
    async submit(formValues) {
      try {
        // if (!this.visorEnable) {
        let resultadoValidacion = this.validacionesSolicitud();
        if (resultadoValidacion.esValido) {
          const res = await this.Confirmacion_Generico(
            "Desea actualizar el colaborador",
            "Actualizar colaborador"
          );
          var result = "";

          this.colaborador.identificacion = this.colaborador.identificacion
            .replace(" ", "")
            .trim();

          // this.colaborador.fechaingreso = aplicarFormatoFecha(
          //   this.colaborador.fechaingreso,
          //   "yyyy-MM-dd"
          // );

          if (res) {
            await DatosLaborales({
              data: JSON.stringify({ ...this.colaborador }),
            })
              .then((response) => {
                console.log(response, "response");
                this.cleanForm();
                Vue.$toast.success(
                  "El colaborador fue actualizado exitosamente",
                  {}
                );
              })
              .catch((error) => {
                console.log(error, "error");
                if (error.response.data.message.length > 0) {
                  Vue.$toast.error(error.response.data.message[0], {});
                } else {
                  Vue.$toast.error("No se pudo actualizar el colaborador", {});
                }
              });
          }
        } else {
          Vue.$toast.warning(
            "La solicitud presenta las siguientes inconsistencias: <br/>" +
              resultadoValidacion.mensaje,
            {}
          );
        }
        // }
      } catch (error) {
        console.log(error);
      }
    },
    validacionesSolicitud() {
      let resultado = { esValido: true };

      // if (!this.colaborador.nombre || this.colaborador.nombre.trim() == "") {
      //   resultado = {
      //     esValido: false,
      //     mensaje: "Debe indicar el nombre."
      //   };
      // }

      if (
        !this.colaborador.identificacion ||
        this.colaborador.identificacion.trim() == ""
      ) {
        resultado = {
          esValido: false,
          mensaje: "Debe indicar la identificación.",
        };
      }

      if (!this.colaborador.correo.toLowerCase().includes("@crnova.com")) {
        resultado = {
          esValido: false,
          mensaje:
            "El correo de la empresa debe estar bajo el dominio crnova.com.",
        };
      }

      return resultado;
    },

    /**
     * Método invocado cuando un usuario selecciona un registro en un Pick
     */
    async selected(item) {
      let loader = this.showLoader();
      switch (this.TipoListaPick) {
        case "Colaboradores":
          this.showModal = false;
          await this.cargarDetalleColaborador(item.id);
          break;

        default:
          break;
      }
      this.hideLoader(loader);
    },

    async cargarDetalleColaborador(id) {
      const {
        data: { colaborador },
      } = await GetColaboradorDetail(id);

      this.colaborador = {
        idcolaborador: colaborador.idcolaborador,
        identificacion: colaborador.identificacion,
        nombre: colaborador.nombre,
        primerapellido: colaborador.primerapellido,
        segundoapellido: colaborador.segundoapellido,
        foto: colaborador.foto,
        idNomina: colaborador.idnomina == null ? 0 : colaborador.idnomina,
        correo: colaborador.correo,
        esservicioprofesional:
          colaborador.esservicioprofesional == true ? "Si" : "No",
        solicitaVacaciones:
          colaborador.solicitavacaciones == true ? "Si" : "No",
        vacacionesdiasanuales: colaborador.vacacionesdiasanuales,
        idareaorganizativa: colaborador.idareaorganizativa,
        saldoVacaciones: colaborador.saldovacaciones,
        fechaingreso: aplicarFormatoFecha(
          colaborador.fechaingreso,
          "yyyy-MM-dd"
        ),
      };
    },

    /**
     * Muestra la ventana modal para un pick, según el tipo correspondiente se invoca al API para
     * obtener los datos que se deben mostrar en el componente tabla del pick
     *
     * @tipo corresponde al tipo de pick a mostrar
     */
    async displayModal(tipo) {
      this.TipoListaPick = tipo;
      this.clean();

      switch (tipo) {
        case "Colaboradores":
          // aqui estarian los request para cargar el picklist segun se requiera
          const {
            data: { colaboradores },
          } = await GetColaboradoresPerfil(this.$root.infoColaboradorActual.id);
          this.dataPickList = colaboradores.map(
            ({ idcolaborador, nombreCompleto, identificacion }) => ({
              id: idcolaborador,
              nombre: nombreCompleto,
              identificacion: identificacion,
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombre", header: "Nombre" },
          ];

          this.showModal = true;
          this.nombreTipoLista = "colaborador";
          this.tituloModal = "Lista de colaboradores";
          break;

        default:
          break;
      }
    },

    onUpload() {
      //this.$toast.add({severity: 'info', summary: 'Success', detail: 'File Uploaded', life: 3000});
    },
    clean() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
    },
    cleanForm() {
      this.colaborador = {
        identificacion: "",
        nombre: "",
        primerapellido: "",
        segundoapellido: "",
        foto: "",
        correo: "",
        idNomina: null,
      };
    },
  },
  computed: {
    today() {
      return new Date();
    },
  },
  watch: {
    // colaborador: function(newValue, oldValue) {
    //   if (!this.cargandoDatos) {
    //     this.colaborador.estado = "M";
    //   }
    // }
    //   solicitud: {
    // 	get: function() {
    // 	},
    // 	set: function(newValue) {
    // 	}
    // }
  },
};
</script>

<style>
.required label::after {
  content: " *";
  color: red;
}

.form-control:disabled,
.form-control[readonly] {
  background-color: #e7e7e7 !important;
  color: #767676 !important;
}

.text-divider {
  margin: 2em 0;
  line-height: 0;
  font-size: 0.875rem;
}

.text-divider span {
  background-color: #9c9c9c !important;
  color: #ffffff;
  padding: 0.5em 0.5em 0.5em 0.5em;
}

.text-divider:before {
  content: " ";
  display: block;
  border-top: 1px dotted rgb(187, 187, 187);
  border-bottom: 1px solid #f7f7f7;
}

.avatar-upload {
  position: relative;
  max-width: 205px;
  /* margin: 50px auto; */
}

.avatar-upload .avatar-edit {
  position: absolute;
  right: 12px;
  z-index: 1;
  top: 10px;
}

.avatar-upload .avatar-edit input {
  display: none;
}

.avatar-upload .avatar-edit input + label {
  display: inline-block;
  width: 34px;
  height: 34px;
  margin-bottom: 0;
  border-radius: 100%;
  background: #ffffff;
  border: 1px solid transparent;
  box-shadow: 0px 2px 4px 0px rgba(0, 0, 0, 0.12);
  cursor: pointer;
  font-weight: normal;
  transition: all 0.2s ease-in-out;
}

.avatar-upload .avatar-edit input + label:hover {
  background: #f1f1f1;
  border-color: #d6d6d6;
}

.avatar-upload .avatar-edit input + label:after {
  content: "\e908";
  font-family: "primeicons";
  color: #757575;
  position: absolute;
  top: 10px;
  left: 0;
  right: 0;
  text-align: center;
  margin: auto;
}

.avatar-upload .avatar-preview {
  width: 192px;
  height: 192px;
  position: relative;
  border-radius: 100%;
  border: 6px solid #f8f8f8;
  box-shadow: 0px 2px 4px 0px rgba(0, 0, 0, 0.1);
}

.avatar-upload .avatar-preview > div {
  width: 100%;
  height: 100%;
  border-radius: 100%;
  background-size: cover;
  background-repeat: no-repeat;
  background-position: center;
}

.p-button {
  background-color: #29abe2;
}

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

.libro-marca-celeste .disabled {
  background-color: #29abe2 !important;
}

.breadcrumb-item + .font-xl.breadcrumb-item::before {
  color: rgb(140, 195, 38);
  content: ">>";
  padding: 0px 10px;
}

.table-striped tbody tr:nth-of-type(odd) {
  background-color: rgba(173, 216, 230, 0.5) !important;
}

.page-item.active .page-link {
  background-color: #29abe2;
  border-color: #29abe2;
}

.page-link {
  color: #29abe2;
}

.btn-link {
  color: #002e6e;
}

.pagination {
  justify-content: flex-end !important;
}

.modal-header {
  background-color: #002e6e !important;
}

.modal-primary .modal-content {
  border-color: #002e6e !important;
  width: 600px;
}

/* Configuración de estilos para tabla PRIMEVUE */
/* INICIO */

/* Se modifica el tamaño de la letra a 14px */
.p-component {
  font-size: 0.875rem !important;
}

.p-dropdown-label.p-inputtext {
  font-size: 0.875rem !important;
}

/* Se oculta la flecha del dropdown */
.dropdown-toggle::after {
  display: none;
}

.dropleft .dropdown-toggle::before {
  display: none;
}

/* tamaño de letra el componente de paginación */
.p-paginator-icon {
  font-size: 0.875rem !important;
}

.hint {
  font-size: 0.705rem !important;
  margin-top: 1%;
}

/* Responsive: en visualización normal se ocultan las etiquetas asociadas al título de cada columna */
.p-datatable-picklist .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-picklist .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}

.p-datatable-picklist .p-datatable-tbody > tr > td {
  overflow-wrap: break-word;
}

.p-datatable .p-datatable-header {
  background-color: unset;
  border: unset;
}

/* configuración de los íconos de ordenamiento */
.p-datatable .p-sortable-column .p-sortable-column-icon {
  color: darkgray;
  font-size: 0.75rem;
}

.p-datatable .p-sortable-column.p-highlight .p-sortable-column-icon {
  color: white;
}

/* configuración del "striped" y fila seleccionada de una tabla */
.p-datatable.p-datatable-striped .p-datatable-tbody > tr:nth-child(even) {
  background-color: rgba(173, 216, 230, 0.5);
}

.p-datatable .p-datatable-tbody > tr.p-highlight {
  background: rgba(12, 123, 190, 0.5);
  /* color: #495057; */
}

.p-datatable.p-datatable-striped
  .p-datatable-tbody
  > tr:nth-child(even).p-highlight {
  background: rgba(12, 123, 190, 0.5);
  /* color: #495057; */
}

.p-dialog-mask {
  z-index: 1049 !important;
}

.btn-opciones {
  display: none;
}

.formulate-form-errors {
  background-color: red;
  color: white;
  white-space: pre-wrap;
}

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de
    etiqueta y valor asociado */
@media screen and (max-width: 40em) {
  .p-datatable.p-datatable-picklist .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-picklist .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-picklist .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-picklist .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-picklist:last-child {
    border-bottom: 1px solid var(--surface-d);
  }

  .dropdown-toggle::after {
    display: inline-block;
  }

  .dropleft .dropdown-toggle::before {
    display: inline-block;
  }

  .dropleft {
    display: none;
  }

  .pi-ellipsis-v {
    display: none;
  }

  .btn-opciones {
    display: flex;
  }

  .p-paginator-bottom {
    padding: 0px;
  }

  .ajustarTamano {
    width: -webkit-fill-available;
  }

  .alinearDerecha {
    text-align: right !important;
  }

  .alinearIzquierda {
    text-align: left !important;
  }

  .alinearCentro {
    text-align: center !important;
  }
}
/* FIN */
</style>
