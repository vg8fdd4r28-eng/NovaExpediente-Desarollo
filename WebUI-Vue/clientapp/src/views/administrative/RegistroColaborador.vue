<!-- Plantilla de Datos Personales -->
<template>
  <div>
    <FormulateForm
      v-model="colaborador"
      @submit="submit()"
      @failed-validation="errorValidacion"
      :invalid-message="this.invalidMessage"
      autocomplete="nope"
    >
      <!-- Datos Personales -->
      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong>Registro de un colaborador</strong>
          <CBadge color="primary" class="ml-2" shape="pill">
            <span style="margin-top:.2rem; margin-bottom:.2rem">
              Nuevo colaborador
            </span>
          </CBadge>
        </CCardHeader>
        <CCardBody>
          <resumen-colaborador
            :colaborador.sync="colaborador"
            estado-solicitud="RE"
            :searchable="false"
            :editableIdentificacion="true"
            :habilitar-controles="true"
            :visible="false"
            @displayModal="displayModal"
          >
          </resumen-colaborador>
        </CCardBody>
      </CCard>

      <!-- Datos Nomina y Vacaciones -->
      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong> Datos Nómina y Vacaciones</strong>
        </CCardHeader>
        <CCardBody>
          <!-- Estado civil y cantidad de hijos -->
          <CRow>
            <CCol sm="6" class="required">
              <label>Es por nómina</label>
              <FormulateInput
                type="myInputSwitch"
                name="esPayRoll"
                :input-class="['form-control', 'readonly']"
                validation-name="Servicios profesionales"
                class="hint"
              />
            </CCol>
            <CCol sm="6" class="required">
              <label>Es servicios profesionales</label>
              <FormulateInput
                type="myInputSwitch"
                name="esservicioprofesional"
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
                name="idNomina"
                label="Id de nómina"
                placeholder="Ingrese el id de nómina"
                validation="number"
                input-class="form-control"
                validation-name="Id de nómina"
              />
            </CCol>
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
          </CRow>
          <CRow>
            <CCol sm="6" class="required">
              <label>Solicita vacaciones</label>
              <FormulateInput
                type="myInputSwitch"
                name="solicitaVacaciones"
                :input-class="['form-control', 'readonly']"
                validation-name="Servicios profesionales"
                class="hint"
              />
            </CCol>
            <CCol sm="6" class="required">
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
      <CCol sm="5" class="required">
              <FormulateInput
                name="paisFeriado"
                label="País para obtener los feriados"
                placeholder="Seleccione un país"
                validation="required"
                validation-name="País para feriados"
                :validation-messages="{
                  required: 'Seleccione un país.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
                :disabled="true"
              />
            </CCol>
              <CCol sm="1" class="mt-4">
              <CButton
                class="btn btn-info mt-1 pull-right w-100 libro-marca-celeste onHoverDark"
                @click="displayModal('PaisesFeriados')"
              >
                <li class="pi pi-search"></li>
              </CButton>
            </CCol>
          </CRow>
        </CCardBody>
      </CCard>

      <!-- Datos administrativos -->
      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong>Datos administrativos</strong>
        </CCardHeader>
        <CCardBody>
          <!-- empresa y area -->
          <CRow>
            <CCol sm="6" class="required">
              <FormulateInput
                name="idempresa"
                :options="empresaList"
                type="select"
                label="Empresa"
                placeholder="Seleccione una empresa"
                validation="required|not:0"
                validation-name="empresa"
                :validation-messages="{
                  required: 'Seleccione una empresa.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
              />
            </CCol>
            <CCol sm="6" class="required">
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
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
              />
            </CCol>
          </CRow>

          <!-- puesto y  fecha ingreso -->

          <CRow>
            <CCol sm="6" class="required">
              <FormulateInput
                name="idPuesto"
                :options="puestoList"
                type="select"
                label="Puesto"
                placeholder="Seleccione un puesto"
                validation="required|not:0"
                validation-name="puesto"
                :validation-messages="{
                  required: 'Seleccione un puesto.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
              />
            </CCol>
            <CCol sm="6" class="required">
              <FormulateInput
                type="date"
                name="fechaingreso"
                label="Fecha de ingreso"
                :validation="'required|before:' + today"
                :validation-messages="{
                  before: 'La fecha de ingreso debe ser menor a la actual.'
                }"
                input-class="form-control"
                :help-class="['hint']"
              />
            </CCol>
          </CRow>

          <!-- dm y ubicado en -->

          <CRow>
            <CCol sm="6" class="required">
              <FormulateInput
                name="iddeliverymanager"
                :options="dmList"
                type="select"
                label="Delivery manager"
                placeholder="Seleccione un delivery manager"
                validation="required|not:0"
                validation-name="Delivery manager"
                :validation-messages="{
                  required: 'Seleccione un Delivery manager.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
              />
            </CCol>
            <CCol sm="6" class="required">
              <FormulateInput
                name="idcliente"
                :options="clienteList"
                type="select"
                label="Ubicado en"
                placeholder="Seleccione un cliente"
                validation="required|not:0"
                validation-name="Ubicado en"
                :validation-messages="{
                  required: 'Debe de seleccionar una opción válida.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
              />
            </CCol>
          </CRow>
          <CRow>
            <!--correo / especialidad  -->
            <CCol sm="6" class="required">
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
            <CCol sm="6" class="required">
              <FormulateInput
                name="ramaespecialidad"
                :options="especialidadList"
                type="select"
                label="Especialidad"
                placeholder="Seleccione una especialidad"
                validation="required|not:0"
                validation-name="Especialidad"
                :validation-messages="{
                  required: 'Seleccione una especialidad.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
              />
            </CCol>
          </CRow>
        </CCardBody>
      </CCard>
      <!-- Datos Generales -->
      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong> Datos Generales</strong>
        </CCardHeader>
        <CCardBody>
          <!-- Género y fecha de nacimiento -->
          <CRow>
            <CCol sm="6" class="required">
              <FormulateInput
                name="genero"
                :options="{
                  0: 'Seleccione un género',
                  I: 'Indefinido',
                  M: 'Masculino',
                  F: 'Femenino'
                }"
                type="select"
                placeholder="Seleccione un género"
                label="Género"
                :input-class="['form-control']"
                validation="required|not:0"
                validation-name="Género"
                :validation-messages="{
                  required: 'Seleccione un género.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
              />
            </CCol>
            <CCol sm="6" class="required">
              <FormulateInput
                type="date"
                name="fechanacimiento"
                label="Fecha de nacimiento"
                :validation="'required|before:' + today"
                :validation-messages="{
                  before: 'La fecha de nacimiento debe ser menor a la actual.'
                }"
                input-class="form-control"
                :help-class="['hint']"
              />
            </CCol>
          </CRow>

          <!-- Estado civil y cantidad de hijos -->
          <CRow>
            <CCol sm="6" class="required">
              <FormulateInput
                name="estadoCivil"
                :options="{
                  0: 'Seleccione un estado civil',
                  S: 'Soltero (a)',
                  C: 'Casado (a)',
                  D: 'Divorciado (a)',
                  U: 'Union Libre',
                  V: 'Viudo (a)'
                }"
                type="select"
                placeholder="Seleccione un estado civil"
                label="Estado civil"
                :input-class="['form-control']"
                validation="required|not:0"
                validation-name="Estado civil"
                :validation-messages="{
                  required: 'Seleccione un estado civil.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
              />
            </CCol>
            <CCol sm="6" class="required">
              <FormulateInput
                type="number"
                name="numeroHijos"
                label="Cantidad de hijos"
                placeholder="Ingrese la cantidad de hijos"
                validation="required|number"
                min="0"
                max="30"
                input-class="form-control"
                validation-name="Cantidad de hijos"
              />
            </CCol>
          </CRow>

          <!-- Número de celular y número de teléfono -->
          <CRow>
            <CCol sm="6" class="required">
              <FormulateInput
                type="tel"
                name="celular"
                label="Número de celular"
                placeholder="Ingrese el número de celular"
                help="Formatos: +00 00000000 o +000 00000000"
                :validation="[
                  ['required'],
                  ['matches', /^\+(?:[0-9] ?){6,14}[0-9]$/]
                ]"
                validation-name="Número de celular"
                :input-class="['form-control']"
                :help-class="['hint']"
              />
            </CCol>

            <CCol sm="6">
              <FormulateInput
                type="tel"
                name="telefono"
                label="Número de teléfono"
                placeholder="Ingrese el número de teléfono"
                help="Formatos: +00 00000000 o +000 00000000"
                :validation="[
                  ['optional'],
                  ['matches', /^\+(?:[0-9] ?){6,14}[0-9]$/]
                ]"
                validation-name="Número de teléfono"
                :input-class="['form-control']"
                :help-class="['hint']"
              />
            </CCol>
          </CRow>
        </CCardBody>
      </CCard>

      <!-- Datos de ubicacion -->
      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong> Datos de ubicación</strong>
        </CCardHeader>
        <CCardBody>
          <!-- Correo personal y país de origen -->
          <CRow>
            <CCol sm="6" class="required">
              <FormulateInput
                type="email"
                name="correoPersonal"
                label="Correo personal"
                placeholder="Ingrese su correo personal"
                help="correo@dominio.ext"
                validation="required|email"
                validation-name="Correo personal"
                :input-class="['form-control']"
                :help-class="['hint']"
              />
            </CCol>
            <CCol sm="5" class="required">
              <FormulateInput
                name="paisOrigen"
                label="País de origen"
                placeholder="Seleccione un país de origen"
                validation="required"
                validation-name="País de origen"
                :validation-messages="{
                  required: 'Seleccione un país de origen.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
                :disabled="true"
              />
            </CCol>
            <CCol sm="1" class="mt-4">
              <CButton
                class="btn btn-info mt-1 pull-right w-100 libro-marca-celeste onHoverDark"
                @click="displayModal('PaisesOrigen')"
              >
                <li class="pi pi-search"></li>
              </CButton>
            </CCol>
          </CRow>

          <!-- País de residencia y cantón -->
          <!-- v-if="solicitud.idpaisresidencia != this.codigoPaisCostaRica" en un computed -->
          <CRow>
            <CCol sm="5" class="required">
              <FormulateInput
                name="paisResidencia"
                label="País de residencia"
                placeholder="Seleccione un país de residencia"
                validation="required"
                validation-name="País de residencia"
                :validation-messages="{
                  required: 'Seleccione un país de residencia.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
                readonly="true"
              />
            </CCol>
            <CCol sm="1" class="mt-4">
              <CButton
                class="btn btn-info mt-1 pull-right w-100 libro-marca-celeste onHoverDark"
                @click="displayModal('Paises')"
              >
                <li class="pi pi-search"></li>
              </CButton>
            </CCol>
            <CCol
              sm="5"
              v-show="idPaisResidenciaComputed == this.codigoPaisCostaRica"
            >
              <FormulateInput
                name="canton"
                label="Cantón"
                placeholder="Seleccione un cantón"
                validation="optional"
                validation-name="Cantón"
                :validation-messages="{
                  required: 'Seleccione un cantón.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
                :disabled="true"
              />
            </CCol>
            <CCol
              sm="1"
              class="mt-4"
              v-if="idPaisResidenciaComputed == this.codigoPaisCostaRica"
            >
              <CButton
                class="btn btn-info mt-1 pull-right w-100 libro-marca-celeste onHoverDark"
                @click="displayModal('Cantones')"
              >
                <li class="pi pi-search"></li>
              </CButton>
            </CCol>
            <CCol
              sm="6"
              v-if="idPaisResidenciaComputed != this.codigoPaisCostaRica"
            >
              <FormulateInput
                name="ciudadresidencia"
                label="Ciudad de residencia"
                validation="max:100,length"
                input-class="form-control"
                validation-name="Ciudad de residencia"
                placeholder="Ingrese la ciudad de residencia"
              />
            </CCol>
          </CRow>

          <!-- Código postal -->
          <CRow>
            <CCol sm="6">
              <FormulateInput
                name="codigoPostal"
                label="Código postal"
                validation="max:10,length"
                input-class="form-control"
                validation-name="Código postal"
                placeholder="Ingrese el código postal"
              />
            </CCol>
          </CRow>

          <!-- Dirección -->
          <CRow>
            <CCol sm="12" class="required">
              <FormulateInput
                type="textarea"
                name="direccion"
                label="Dirección exacta"
                validation="required|max:8000,length"
                validation-name="Dirección exacta"
                :validation-messages="{
                  required: 'Ingrese una dirección.'
                }"
                :input-class="['form-control']"
                rows="5"
                :help="helpTextArea"
                :help-class="['hint']"
              />
            </CCol>
          </CRow>
        </CCardBody>
      </CCard>
      <!-- En caso de emergencia -->
      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong> En caso de emergencia</strong>
        </CCardHeader>
        <CCardBody>
          <!-- En caso de emergencia -->
          <CRow>
            <CCol sm="6">
              <FormulateInput
                type="tel"
                name="telefonoemergencia"
                label="Teléfono de emergencia"
                input-class="form-control"
                validation-name="Teléfono de emergencia"
                placeholder="Ingrese el teléfono de emergencia"
                help="Opcional o Formatos: +00 00000000 o +000 00000000"
                :help-class="['hint']"
                :validation="[
                  ['optional'],
                  ['matches', /^\+(?:[0-9] ?){6,14}[0-9]$/]
                ]"
              />
            </CCol>
            <CCol sm="6">
              <FormulateInput
                name="personallamar"
                label="Persona a llamar"
                validation="optional|max:100,length"
                input-class="form-control"
                validation-name="Persona a llamar"
                placeholder="Ingrese la persona a llamar"
              />
            </CCol>
          </CRow>
        </CCardBody>
      </CCard>
      <!-- CPIC -->
      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong> CPIC</strong>
        </CCardHeader>
        <CCardBody>
          <CRow>
            <CCol sm="6">
              <FormulateInput
                type="number"
                name="numerocpic"
                label="Número de colegiado CPIC"
                validation="optional|not:0|min:1|max:9999999999"
                input-class="form-control"
                validation-name="# CPIC"
              />
            </CCol>
            <CCol sm="6">
              <label>Certificado del CPIC</label>
              <FormulateInput
                type="myfileupload"
                name="fotoCpic"
                help="Formatos válidos: png/jpg/gif/pdf"
                input-class="form-control"
                validation-name="mime:image/jpeg,image/png,image/gif,application/pdf"
                class="hint"
                tipo-documento="Documento CPIC"
                estado-solicitud="RE"
                :eliminada="false"
                :visor-padre="false"
                :habilitar-controles="true"
                @showModal="showModalVisor"
              />
            </CCol>
          </CRow>
        </CCardBody>
      </CCard>
      <!-- Otras afilicaciones -->
      <CCard style="border-radius: 16px !important;">
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong> Otras afiliaciones</strong>
        </CCardHeader>
        <CCardBody>
          <agrupacion-afiliaciones
            :afiliaciones="(colaborador && colaborador.afiliaciones) || []"
            estado-solicitud="RE"
            :habilitar-controles="true"
            :esRegistro="true"
            nombreFile="Documento afiliación"
            @undo="undoAfiliacion"
            @modify="modificarAfiliacion"
            @add="agregarAfiliacion"
            @remove="eliminarAfiliacion"
          >
          </agrupacion-afiliaciones>
        </CCardBody>
      </CCard>
      <FormulateErrors />
      <CRow style="margin-bottom: 80px;">
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
    <!-- <CModal
      :title="tituloModal"
      :show.sync="showModal"
      color="primary"
      class="modal-libro"
    >
      <div>
        <pick-list
          @selected="selected"
          :nombreTipoLista="nombreTipoLista"
          :columnas="columnasPickList"
          :dataContenido="dataPickList"
        ></pick-list>
      </div>
      <template #footer>
        <div></div>
      </template>
    </CModal> -->
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
  GetAllCantones,
  GetColaboradoresPerfil,
  GetAllPaises,
  RegistroColaborador,
  GetPuesto,
  GetEspecialidad,
  GetEmpresaCorporacion,
  GetColaboradoresDM,
  GetAreasOrganizativas,
  GetClientes
} from "./request";

// Componentes
import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "../masterdata/ResumenColaborador";
import { aplicarFormatoFecha } from "../../utils";
import RepeatableGroup from "../generales/Componentes/RepeatableGroup.vue";

// métodos comunes
import common from "../masterdata/common.js";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";

// Definición del componente DatosPersonales
export default {
  name: "RegistroColaborador",
  mixins: [common],
  components: {
    "pick-list": PickList,
    "resumen-colaborador": ResumenColaborador,
    "agrupacion-afiliaciones": RepeatableGroup,
    "confirm-dialogue": ConfirmDialogue,
    "comment-dialogue": CommentDialogue
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
        paisOrigen: "",
        paisResidencia: "",
        paisFeriado: "",
        canton: "",
        afiliaciones: [],
        esservicioprofesional: "No",
        esPayRoll: "Si",
        solicitaVacaciones: "Si",
        saldoVacaciones: 0,
        codigoPostal: 0
      },
      comentarios: [],
      showModal: false, // controla la presentación del modal
      nombreTipoLista: "", // referencia el tipo de contenido de un pick
      tituloModal: "", // título para la ventana modal dependiendo del tipo de contenido del pick
      columnasPickList: [], // lista de columnas a mostrar en el componente tabla del pick
      dataPickList: [], // datos para el componente tabla del pick
      cantones: [], // lista de cantones
      paisesOrigen: [], // lista de países
      paisesResidencia: [], // lista de países
      mostrarCuidadResidencia: false, // controla si el campo de ciudad de residencia debe presentarse
      // cuando el país de residencia no es Costa Rica
      TipoListaPick: "",
      codigoPaisCostaRica: 51, // identificador de Costa Rica en la tabla de Paises
      mensajeError: common.MENSAJE_ERROR,
      visorEnable: false,
      puestoList: [],
      especialidadList: [],
      empresaList: [],
      areasList: [],
      dmList: [],
      clienteList: []
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...

    await this.cargarCantones();
    await this.cargarPaises();
    await this.cargarPuestos();
    await this.cargarEspecialidad();

    await this.cargarEmpresa();
    await this.cargarAreasOrganizativas();
    await this.cargarDMs();
    await this.cargarClientes();

    this.colaborador.afiliaciones = [];

    this.disableAutoComplete();
  },

  methods: {
    showModalVisor(show) {
      this.visorEnable = show;
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

    getNombreEspecialidad(id) {
      const name = this.especialidadList.filter(x => x.value == id)[0].label;
      return name;
    },

    /**
     * Obtiene los datos del formulario para el submit
     */
    async submit(formValues) {
      try {
        if (!this.visorEnable) {
          let resultadoValidacion = this.validacionesSolicitud();
          if (resultadoValidacion.esValido) {
            const res = await this.Confirmacion_Generico(
              "Desea registrar el nuevo colaborador",
              "Nuevo colaborador"
            );
            var result = "";

            if (
              this.colaborador.ramaespecialidad &&
              !isNaN(this.colaborador.ramaespecialidad)
            ) {
              this.colaborador.ramaespecialidad = this.getNombreEspecialidad(
                this.colaborador.ramaespecialidad
              );
            }

            this.colaborador.identificacion = this.colaborador.identificacion.replace(" ","").trim();

            if (res) {
              await RegistroColaborador({
                data: JSON.stringify({ ...this.colaborador })
              })
                .then(response => {
                  console.log(response, "response");
                  this.cleanForm();
                  Vue.$toast.success(
                    "El colaborador fue registrado exitosamente",
                    {}
                  );
                })
                .catch(error => {
                  console.log(error, "error");
                  if (error.response.data.message.length > 0) {
                    Vue.$toast.error(error.response.data.message[0], {});
                  } else {
                    Vue.$toast.error("No se pudo registrar el colaborador", {});
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
        }
      } catch (error) {
        console.log(error);
      }
    },
    validacionesSolicitud() {
      let resultado = { esValido: true };

      // si incluyen código CPIC, debe tener un documento asociado y viceversa
      if (
        this.colaborador.numerocpic &&
        this.colaborador.numerocpic.trim() !== "" &&
        (!this.colaborador.fotoCpic || this.colaborador.fotoCpic.trim() === "")
      )
        resultado = {
          esValido: false,
          mensaje: "Debe asociar el documento del CPIC"
        };

      if (
        (!this.colaborador.numerocpic ||
          this.colaborador.numerocpic.trim() === "") &&
        this.colaborador.fotoCpic &&
        this.colaborador.fotoCpic.trim() !== ""
      )
        resultado = {
          esValido: false,
          mensaje: "Debe indicar el número de asociado del CPIC"
        };

      if (this.colaborador.idpaisresidencia != this.codigoPaisCostaRica) {
        if (
          !this.colaborador.ciudadresidencia ||
          this.colaborador.ciudadresidencia.trim() === ""
        ) {
          resultado = {
            esValido: false,
            mensaje: "Debe indicar la ciudad de residencia."
          };
        }
      }

      // if (
      //   !this.colaborador.codigoPostal ||
      //   this.colaborador.codigoPostal.trim() === ""
      // ) {
      //   resultado = {
      //     esValido: false,
      //     mensaje: "Debe indicar el código postal."
      //   };
      // }

      if (this.colaborador.idpaisresidencia == this.codigoPaisCostaRica) {
        if (!this.colaborador.idcanton || this.colaborador.idcanton == 0) {
          resultado = {
            esValido: false,
            mensaje: "Debe indicar el cantón."
          };
        }
      }

      if (!this.colaborador.nombre || this.colaborador.nombre.trim() == "") {
        resultado = {
          esValido: false,
          mensaje: "Debe indicar el nombre."
        };
      }

      if (
        !this.colaborador.ramaespecialidad ||
        this.colaborador.ramaespecialidad.trim() == ""
      ) {
        resultado = {
          esValido: false,
          mensaje: "Debe indicar la especialidad."
        };
      }

      if (
        !this.colaborador.primerapellido ||
        this.colaborador.primerapellido.trim() == ""
      ) {
        resultado = {
          esValido: false,
          mensaje: "Debe indicar el primer apellido."
        };
      }

      if (
        !this.colaborador.segundoapellido ||
        this.colaborador.segundoapellido.trim() == ""
      ) {
        resultado = {
          esValido: false,
          mensaje: "Debe indicar el segundo apellido."
        };
      }

      if (
        !this.colaborador.identificacion ||
        this.colaborador.identificacion.trim() == ""
      ) {
        resultado = {
          esValido: false,
          mensaje: "Debe indicar la identificación."
        };
      }

      // if (!isNaN(this.colaborador.telefonoemergencia)) {
      //   if (this.colaborador.personallamar) {
      //     resultado = {
      //       esValido: false,
      //       mensaje:
      //         "Debe indicar una persona a llamar en caso de emergencia válida."
      //     };
      //   }
      // }

      if (!this.colaborador.correo.toLowerCase().includes("@crnova.com")) {
        resultado = {
          esValido: false,
          mensaje:
            "El correo de la empresa debe estar bajo el dominio crnova.com."
        };
      }

      if (typeof this.colaborador.esPayRoll == typeof null) {
        resultado = {
          esValido: false,
          mensaje: "El identificador es por nómina, es requerido."
        };
      }

      if (typeof this.colaborador.solicitaVacaciones == typeof null) {
        resultado = {
          esValido: false,
          mensaje: "El identificador solicita vacaciones, es requerido."
        };
      }

      if (typeof this.colaborador.esservicioprofesional == typeof null) {
        resultado = {
          esValido: false,
          mensaje: "El identificador es servicios profesionales, es requerido."
        };
      }

      if (this.colaborador.esPayRoll == "Si") {
        if (this.colaborador.idNomina < 1) {
          resultado = {
            esValido: false,
            mensaje: "El id de nómina, es requerido."
          };
        }
      }

      return resultado;
    },
    /**
     * Invoca al API que devuelve la lista de cantones
     */
    async cargarCantones() {
      const {
        data: { cantones }
      } = await GetAllCantones();
      this.cantones = cantones.map(({ idcanton, nombre }) => ({
        label: nombre,
        value: idcanton
      }));
    },

    /**
     * Invoca al API que devuelve la lista de países
     */
    async cargarPaises() {
      const {
        data: { paises }
      } = await GetAllPaises();
      this.paisesOrigen = paises.map(({ idpais, nombre }) => ({
        label: nombre,
        value: idpais
      }));
      this.paisesResidencia = this.paisesOrigen;
    },

    /**
     * Invoca al API que devuelve la lista de puestos
     */
    async cargarPuestos() {
      const {
        data: { puesto }
      } = await GetPuesto();
      this.puestoList = puesto.map(({ idPuesto, nombre }) => ({
        label: nombre,
        value: idPuesto
      }));
    },

    /**
     * Invoca al API que devuelve la lista de especialidades
     */
    async cargarEspecialidad() {
      const {
        data: { especialidades }
      } = await GetEspecialidad();
      this.especialidadList = especialidades.map(
        ({ idespecialidad, nombre }) => ({
          label: nombre,
          value: idespecialidad
        })
      );
    },

    /**
     * Invoca al API que devuelve la lista de empresas
     */
    async cargarEmpresa() {
      const {
        data: { empresaCorporacions }
      } = await GetEmpresaCorporacion();
      this.empresaList = empresaCorporacions.map(
        ({ idempresacorporacion, nombre }) => ({
          label: nombre,
          value: idempresacorporacion
        })
      );
    },

    /**
     * Invoca al API que devuelve la lista de areas organizativas
     */
    async cargarAreasOrganizativas() {
      const {
        data: { areaOrganizativas }
      } = await GetAreasOrganizativas();
      this.areasList = areaOrganizativas.map(
        ({ idareaorganizativa, nombre }) => ({
          label: nombre,
          value: idareaorganizativa
        })
      );
    },

    /**
     * Invoca al API que devuelve la lista de DMS
     */
    async cargarDMs() {
      const {
        data: { colaboradoresDM }
      } = await GetColaboradoresDM();
      this.dmList = colaboradoresDM.map(
        ({ idcolaborador, nombreCompleto }) => ({
          label: nombreCompleto,
          value: idcolaborador
        })
      );
    },

    /**
     * Invoca al API que devuelve la lista de clientes
     */
    async cargarClientes() {
      const {
        data: { clientes }
      } = await GetClientes();
      this.clienteList = clientes.map(({ idcliente, nombre }) => ({
        label: nombre,
        value: idcliente
      }));
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

        case "Paises":
          if (
            this.colaborador.idpaisresidencia === this.codigoPaisCostaRica &&
            item.id !== this.codigoPaisCostaRica
          ) {
            this.colaborador.idcanton = null;
            this.colaborador.canton = "";
          } else if (
            this.colaborador.idpaisresidencia !== this.codigoPaisCostaRica &&
            item.id === this.codigoPaisCostaRica
          ) {
            this.colaborador.ciudadresidencia = "";
          }
          this.colaborador.canton = "";
          this.colaborador.ciudadresidencia = "";
          this.colaborador.paisResidencia = item.nombre;
          this.colaborador.idpaisresidencia = item.id;
          this.showModal = false;
          break;

        case "Cantones":
          this.colaborador.canton = item.nombre;
          this.colaborador.idcanton = item.id;
          this.showModal = false;
          break;

        case "PaisesOrigen":
          this.colaborador.paisOrigen = item.nombre;
          this.colaborador.idpaisorigen = item.id;
          this.showModal = false;
          break;
    case "PaisesFeriados":
          this.colaborador.paisFeriado = item.nombre;
          this.colaborador.idpaisferiado = item.id;
          this.showModal = false;
          break;
        default:
          break;
      }
      this.hideLoader(loader);
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
            data: { colaboradores }
          } = await GetColaboradoresPerfil(this.$root.infoColaboradorActual.id);
          this.dataPickList = colaboradores.map(
            ({ idcolaborador, nombreCompleto, identificacion }) => ({
              id: idcolaborador,
              nombre: nombreCompleto,
              identificacion: identificacion
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombre", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "colaborador";
          this.tituloModal = "Lista de colaboradores";
          break;

        case "Paises":
          const {
            data: { paises }
          } = await GetAllPaises();
          this.dataPickList = paises.map(({ idpais, nombre }) => ({
            id: idpais,
            nombre: nombre
          }));

          this.columnasPickList = [
            // { field: "id", header: "Id" },
            { field: "nombre", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "país";
          this.tituloModal = "Lista de países";
          break;

        case "Cantones":
          const {
            data: { cantones }
          } = await GetAllCantones();
          this.dataPickList = cantones.map(
            ({ idcanton, nombre, provincianombre }) => ({
              id: idcanton,
              nombre: nombre,
              provincianombre: provincianombre
            })
          );

          this.columnasPickList = [
            //{ field: "id", header: "Id" },
            { field: "provincianombre", header: "Provincia" },
            { field: "nombre", header: "Cantón" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "cantón";
          this.tituloModal = "Lista de cantones";
          break;

        case "PaisesOrigen":
          const paisesOrigen = await GetAllPaises();
          this.dataPickList = paisesOrigen.data.paises.map(
            ({ idpais, nombre }) => ({
              id: idpais,
              nombre: nombre
            })
          );

          this.columnasPickList = [
            // { field: "id", header: "Id" },
            { field: "nombre", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "país";
          this.tituloModal = "Lista de países";
          break;

           case "PaisesFeriados":
          const paisesFeriados = await GetAllPaises();
          this.dataPickList = paisesFeriados.data.paises.map(
            ({ idpais, nombre }) => ({
              id: idpais,
              nombre: nombre
            })
          );

          this.columnasPickList = [
            // { field: "id", header: "Id" },
            { field: "nombre", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "país";
          this.tituloModal = "Lista de países";
          break;
        default:
          break;
      }
    },

    /**
     * Modifica la estructura de las afiliaciones de un colaborador, formateando la fecha
     * y agregando el atributo del "estado" a cada una de las afiliaciones
     */
    async getAfiliaciones(afiliaciones) {
      // si el colaborador tiene afiliaciones se formatea la fecha y se agrega el atributo del estado a
      // cada una de las afiliaciones
      if (afiliaciones && afiliaciones != null && afiliaciones.length > 0) {
        afiliaciones.forEach(afiliacion => {
          afiliacion.fechaInicio = aplicarFormatoFecha(
            afiliacion.fechaInicio,
            "yyyy-MM-dd"
          );

          // se agrega el campo ESTADO con un valor "S" (S: Sin modificaciones N: Nuevo E: Eliminado M: Modificado)
          if (!afiliacion.estado)
            this.$set(afiliacion, "estado", common.SOLICITUD_SINMODIFICACIONES);
        });

        return afiliaciones;
      }
      // // sino se regresa una lista con una afiliacion nueva y vacía
      else {
        return afiliaciones;
        //return [this.getAfiliacionNueva()];
      }
    },

    /**
     * Contenido de una nueva afiliación
     */
    getAfiliacionNueva() {
      return {
        idAfiliacionColaborador: null,
        nombre: null,
        descripcion: null,
        fechaInicio: null,
        certificado: null,
        estado: "N"
      };
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
        idPuesto: null,
        correo: "",
        genero: null,
        fechanacimiento: null,
        estadoCivil: 0,
        numeroHijos: 0,
        celular: "",
        telefono: "",
        email: "",
        paisOrigen: null,
        paisFeriado: null,
        idpaisorigen: 0,
        paisResidencia: null,
        idpaisresidencia: 0,
        canton: "",
        idcanton: 0,
        ciudadresidencia: "",
        codigoPostal: 0,
        ramaespecialidad: null,
        direccion: "",
        telefonoemergencia: "",
        personallamar: "",
        numerocpic: "",
        fotoCpic: "",
        afiliaciones: [],
        fechaingreso: null,
        iddeliverymanager: null,
        idareaorganizativa: null,
        idempresa: null,
        idcliente: null,
        esservicioprofesional: "No",
        esPayRoll: "Si",
        solicitaVacaciones: "Si",
        idNomina: null,
        vacacionesdiasanuales: null,
        saldoVacaciones: 0
      };
    },

    undoAfiliacion(index) {
      // this.colaborador.afiliaciones[index].estado = "S";
    },
    agregarAfiliacion() {
      //   this.colaborador.afiliaciones[
      //     this.colaborador.afiliaciones.length - 1
      //   ].estado = "N";
    },
    modificarAfiliacion(index) {
      //   if (this.colaborador.afiliaciones[index].estado === "S") {
      //     this.colaborador.afiliaciones[index].estado = "M";
      //   }
    },
    eliminarAfiliacion(index) {
      //  this.colaborador.afiliaciones[index].estado = "E";
    }
  },
  computed: {
    muestraCuidadResidencia() {},

    helpTextArea() {
      if (this.colaborador.direccion) {
        return `Tiene disponible un máximo de ${8000 -
          this.colaborador.direccion.length} carácteres.`;
      }
      return null;
    },
    idPaisResidenciaComputed() {
      if (this.colaborador) {
        return this.colaborador.idpaisresidencia;
      }
      return null;
    },
    today() {
      return new Date();
    }
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
  }
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
