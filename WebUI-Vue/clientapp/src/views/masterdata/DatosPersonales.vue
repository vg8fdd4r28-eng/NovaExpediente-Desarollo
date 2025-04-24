<!-- Plantilla de Datos Personales -->
<template>
  <div>
    <CRow>
      <CCol>
        <CButton
          block
          color="primary"
          @click="atras()"
          class="btn btn-primary btn-lg"
          style="height: 40px; width: 130px;float: right; color: rgba(98, 184, 235, 1); background-color: transparent; border-color: transparent;"
        >
          <i class="pi pi-arrow-circle-left"></i>&nbsp;Regresar</CButton
        >
      </CCol>
    </CRow>
    <!-- Datos Personales -->
    <CCard style="border-radius: 16px !important;">
      <CCardHeader
        style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
      >
        <strong> Datos Personales</strong>
        <small> Solicitud de cambio </small>
        <CBadge
          color="primary"
          class="mr-2 libro-marca-azuloscuro"
          shape="pill"
          v-if="this.datosSolicitud.idSolicitudMasterData"
        >
          <span style="margin-top:.2rem; margin-bottom:.2rem">
            {{ this.datosSolicitud.idSolicitudMasterData }}
          </span>
        </CBadge>
        <CBadge
          :color="this.obtenerColorEstado(this.datosSolicitud.estadoSolicitud)"
          class="mr-2"
          shape="pill"
        >
          <span style="margin-top:.2rem; margin-bottom:.2rem">
            {{
              this.obtenerDescripcionEstado(this.datosSolicitud.estadoSolicitud)
            }}
          </span>
        </CBadge>
      </CCardHeader>
      <CCardBody>
        <resumen-colaborador
          :colaborador.sync="colaborador"
          :estado-solicitud="datosSolicitud.estadoSolicitud"
          :searchable="pickPuedeBuscarColaborador"
          :habilitar-controles="
            this.habilitarControlesSolicitud(
              this.datosSolicitud.estadoSolicitud,
              this.origen
            )
          "
          @displayModal="displayModal"
        >
        </resumen-colaborador>
      </CCardBody>
    </CCard>
    <FormulateForm
      v-model="solicitud"
      @submit="submit()"
      @failed-validation="errorValidacion"
      :invalid-message="this.invalidMessage"
    >
      <FormulateErrors />

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
            <CCol sm="6">
              <FormulateInput
                name="genero"
                :options="{
                  0: 'Seleccione un género',
                  I: 'No me indentifico con ninguno de los anteriores',
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
                :disabled="!this.puedePintar"
              />
            </CCol>
            <CCol sm="6">
              <FormulateInput
                type="date"
                name="fechanacimiento"
                label="Fecha de nacimiento"
                validation="required|after:1900-01-01|before:2200-01-01"
                validation-name="Fecha de nacimiento"
                :validation-messages="{
                  after: 'La fecha de nacimiento debe ser luego de 01/01/1900.',
                  before: 'La fecha de nacimiento debe ser antes de 01/01/2200.'
                }"
                min="1900-01-01"
                max="2200-01-01"
                input-class="form-control"
                :help-class="['hint']"
                :disabled="!this.puedePintar"
              />
            </CCol>
          </CRow>

          <!-- Estado civil y cantidad de hijos -->
          <CRow>
            <CCol sm="6">
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
                :disabled="!this.puedePintar"
              />
            </CCol>
            <CCol sm="6">
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
                :readonly="!this.puedePintar"
              />
            </CCol>
          </CRow>

          <!-- Número de celular y número de teléfono -->
          <CRow>
            <CCol sm="6">
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
                :readonly="!this.puedePintar"
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
                :readonly="!this.puedePintar"
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
            <CCol sm="6">
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
                :readonly="!this.puedePintar"
              />
            </CCol>
            <CCol sm="5">
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
                v-if="this.puedePintar"
              >
                <li class="pi pi-search"></li>
              </CButton>
            </CCol>
          </CRow>

          <!-- País de residencia y cantón -->
          <!-- v-if="solicitud.idpaisresidencia != this.codigoPaisCostaRica" en un computed -->
          <CRow>
            <CCol sm="5">
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
                v-if="this.puedePintar"
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
                :disabled="
                  !this.puedePintar ||
                    (this.puedePintar &&
                      idPaisResidenciaComputed != this.codigoPaisCostaRica)
                "
                v-if="this.puedePintar"
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
                :readonly="!this.puedePintar"
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
                :readonly="!this.puedePintar"
              />
            </CCol>

            <CCol sm="5">
              <FormulateInput
                name="paisFeriado"
                label="País para obtener los feriados"
                placeholder="Seleccione un país"
                validation-name="País para feriados"
                :validation-messages="{
                  required: 'Seleccione un país.',
                  not: 'Debe de seleccionar una opción válida.'
                }"
                :input-class="['form-control']"
                :disabled="true"
                 v-show="esRRHH()"
              />
            </CCol>
            <CCol sm="1" class="mt-4">
              <CButton
                class="btn btn-info mt-1 pull-right w-100 libro-marca-celeste onHoverDark"
                @click="displayModal('PaisesFeriados')"
                v-show="esRRHH()"
              >
                <li class="pi pi-search"></li>
              </CButton>
            </CCol>
          </CRow>

          <!-- Dirección -->
          <CRow>
            <CCol sm="12">
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
                :readonly="!this.puedePintar"
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
                :readonly="!this.puedePintar"
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
                :readonly="!this.puedePintar"
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
                name="numerocpic"
                label="Número de colegiado CPIC"
                validation="optional|max:10,length"
                input-class="form-control"
                validation-name="# CPIC"
                :readonly="!this.puedePintar"
              />
            </CCol>
            <CCol sm="6" v-if="this.puedePintarCpic">
              <label>Certificado del CPIC</label>
              <FormulateInput
                type="myfileupload"
                name="fotoCpic"
                :help="
                  puedePintar === true
                    ? 'Formatos válidos: png/jpg/gif/pdf'
                    : ''
                "
                input-class="form-control"
                validation-name="mime:image/jpeg,image/png,image/gif,application/pdf"
                class="hint"
                tipo-documento="Documento CPIC"
                :estado-solicitud="datosSolicitud.estadoSolicitud"
                :eliminada="false"
                :visor-padre="false"
                :habilitar-controles="
                  this.habilitarControlesSolicitud(
                    this.datosSolicitud.estadoSolicitud,
                    this.origen
                  )
                "
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
            :afiliaciones="(solicitud && solicitud.afiliaciones) || []"
            :estado-solicitud="datosSolicitud.estadoSolicitud"
            :habilitar-controles="
              this.habilitarControlesSolicitud(
                this.datosSolicitud.estadoSolicitud,
                this.origen
              )
            "
            nombreFile="Documento afiliación"
            @undo="undoAfiliacion"
            @modify="modificarAfiliacion"
            @add="agregarAfiliacion"
            @remove="eliminarAfiliacion"
          >
          </agrupacion-afiliaciones>
          <FormulateErrors />
        </CCardBody>
      </CCard>

      <CCard
        v-if="this.comentarios.length > 0"
        style="border-radius: 16px !important;"
      >
        <CCardHeader
          style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
        >
          <strong> Comentarios</strong>
        </CCardHeader>
        <CCardBody>
          <comments
            :comments_wrapper_classes="['custom-scrollbar', 'comments-wrapper']"
            :comments="this.comentarios"
          ></comments>
        </CCardBody>
      </CCard>

      <CRow style="margin-bottom: 80px;">
        <CCol>
          <botonera-master-data
            :estado-solicitud="this.datosSolicitud.estadoSolicitud"
            nombreEventoProcesar="procesarAccion"
            @procesarAccion="procesarAccion"
            v-if="muestraBotonera()"
          >
          </botonera-master-data>
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
  GetAllColaboradores,
  GetColaboradorDetail,
  GetAllPaises,
  GetDetalleSolicitud
} from "./request";

// Componentes
import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "./ResumenColaborador";
import { aplicarFormatoFecha } from "../../utils";
import RepeatableGroup from "../generales/Componentes/RepeatableGroup.vue";
import BotoneraMasterData from "./BotoneraMasterData.vue";

// métodos comunes
import common from "./common.js";
import lodash from "lodash";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
import Comments from "../generales/Componentes/Comments/Comments.vue";

// Definición del componente DatosPersonales
export default {
  name: "DatosPersonales",
  mixins: [common],
  props: {
    origen: { type: String, default: "" }, // origen desde donde se accedio la vista
    idTipoSolicitud: { type: Number, default: 0 }, // id del tipo de la solicitud generada
    tipoSolicitud: { type: String, default: "" }, // nombre del tipo de la solicitud generada
    idSolicitud: { type: Number, default: null }, // id de la solicitud, para obtener el obj Json con la informacion a cargar
    idResponsable: { type: Number, default: null } // id  del responsable de la solicitud
  },
  components: {
    "pick-list": PickList,
    "resumen-colaborador": ResumenColaborador,
    "agrupacion-afiliaciones": RepeatableGroup,
    "botonera-master-data": BotoneraMasterData,
    "confirm-dialogue": ConfirmDialogue,
    "comment-dialogue": CommentDialogue,
    comments: Comments
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
        foto: null
      },
      // values de solicitud
      solicitud: null,
      datosSolicitud: {
        idSolicitudMasterData: null,
        idTipoSolicitud: this.idTipoSolicitud,
        tipoSolicitud: this.tipoSolicitud,
        estadoSolicitud: "RE"
      },
      comentarios: [],
      showModal: false, // controla la presentación del modal
      nombreTipoLista: "", // referencia el tipo de contenido de un pick
      tituloModal: "", // título para la ventana modal dependiendo del tipo de contenido del pick
      columnasPickList: [], // lista de columnas a mostrar en el componente tabla del pick
      dataPickList: [], // datos para el componente tabla del pick
      cantones: [], // lista de cantones
      paisesOrigen: [], // lista de países
      paisesFeriados: [], //lista de paises para tomar los feriados
      paisesResidencia: [], // lista de países
      mostrarCuidadResidencia: false, // controla si el campo de ciudad de residencia debe presentarse
      // cuando el país de residencia no es Costa Rica
      TipoListaPick: "",
      cargandoDatos: false, // indica si se están cargando datos desde la BD para que el watch no modifique otros indicadores
      codigoPaisCostaRica: 51, // identificador de Costa Rica en la tabla de Paises
      IdSolicitudJson: null, // id de la solicitud para cargar el json de una solicitud
      mensajeError: common.MENSAJE_ERROR,
      visorEnable: false
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    await this.cargarCantones();
    await this.cargarPaises();

    this.IdSolicitudJson = this.idSolicitud;

    this.TipoListaPick = "Colaboradores";
    if (this.IdSolicitudJson) {
      let loader = this.showLoader();
      await this.cargarSolicitud();
      this.hideLoader(loader);
    } else {
      this.selected(this.$root.infoColaboradorActual);
    }

    this.disableAutoComplete();
  },

  methods: {
      esRRHH() {
      return this.$root.userRoles.some(p => p == "Expediente.RRHH");
    },
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
    /**
     * Obtiene los datos del formulario para el submit
     */
    async submit(formValues) {
      try {
        if (!this.visorEnable) {
          if (this.solicitud.estado == common.SOLICITUD_SINMODIFICACIONES) {
            Vue.$toast.warning(
              "La solicitud no tiene modificaciones que guardar.",
              {}
            );
          } else {
            let resultadoValidacion = this.validacionesSolicitud();
            if (resultadoValidacion.esValido) {
              let datos = {
                idSolicitud: this.datosSolicitud.idSolicitudMasterData,
                idTipoSolicitud: this.datosSolicitud.idTipoSolicitud,
                codigoEstado: this.datosSolicitud.estadoSolicitud,
                idAutor: this.$root.infoColaboradorActual.id,
                idResponsable: this.$root.infoColaboradorActual.id,
                idColaborador: this.colaborador.idcolaborador,
                accion: "Enviar",
                docSolicitud: JSON.stringify({
                  ...this.colaborador,
                  ...this.solicitud
                })
              };

              let opciones = {
                tipo: "confirmar", // confirmar comentario ninguno
                titulo: "Solicitud de cambio",
                mensajeDialogo: "Está seguro de enviar la solicitud?",
                btnOk: "Si",
                btnCancel: "No",
                mensajeExito: "Solicitud enviada exitosamente.",
                mensajeError:
                  "Se ha presentado un inconveniente al enviar la solicitud de cambio."
              };

              await this.Enviar(datos, opciones);
            } else {
              Vue.$toast.warning(
                "La solicitud presenta las siguientes inconsistencias: <br/>" +
                  resultadoValidacion.mensaje,
                {}
              );
            }
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
        this.solicitud.numerocpic &&
        this.solicitud.numerocpic.trim() !== "" &&
        (!this.solicitud.fotoCpic || this.solicitud.fotoCpic.trim() === "")
      )
        resultado = {
          esValido: false,
          mensaje: "Debe asociar el documento del CPIC"
        };

      if (
        (!this.solicitud.numerocpic ||
          this.solicitud.numerocpic.trim() === "") &&
        this.solicitud.fotoCpic &&
        this.solicitud.fotoCpic.trim() !== ""
      )
        resultado = {
          esValido: false,
          mensaje: "Debe indicar el número de asociado del CPIC"
        };

      if (this.solicitud.idpaisresidencia != this.codigoPaisCostaRica) {
        if (
          !this.solicitud.ciudadresidencia ||
          this.solicitud.ciudadresidencia.trim() === ""
        ) {
          resultado = {
            esValido: false,
            mensaje: "Debe indicar la ciudad de residencia."
          };
        }
      }

      if (this.solicitud.idpaisresidencia == this.codigoPaisCostaRica) {
        if (!this.solicitud.idCanton || this.solicitud.idCanton == 0) {
          resultado = {
            esValido: false,
            mensaje: "Debe indicar el cantón."
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
            this.solicitud.idpaisresidencia === this.codigoPaisCostaRica &&
            item.id !== this.codigoPaisCostaRica
          ) {
            this.solicitud.idCanton = null;
            this.solicitud.canton = "";
          } else if (
            this.solicitud.idpaisresidencia !== this.codigoPaisCostaRica &&
            item.id === this.codigoPaisCostaRica
          ) {
            this.solicitud.ciudadresidencia = "";
          }
          this.solicitud.canton = "";
          this.solicitud.ciudadresidencia = "";
          this.solicitud.paisResidencia = item.nombre;
          this.solicitud.idpaisresidencia = item.id;
          this.showModal = false;
          break;

        case "Cantones":
          this.solicitud.canton = item.nombre;
          this.solicitud.idCanton = item.id;
          this.showModal = false;
          break;

        case "PaisesOrigen":
          this.solicitud.paisOrigen = item.nombre;
          this.solicitud.idpaisorigen = item.id;
          this.showModal = false;
          break;

        case "PaisesFeriados":
          this.solicitud.paisFeriado = item.nombre;
          this.solicitud.idpaisferiado = item.id;
          this.showModal = false;
          break;

        default:
          break;
      }
      this.hideLoader(loader);
    },

    /**
     * Invoca al API que devuelve la información de un colaborador según su ID
     * @id identificador de un colaborador
     */
    async cargarDetalleColaborador(id) {
      this.cargandoDatos = true;

      const {
        data: { colaborador }
      } = await GetColaboradorDetail(id);

      this.colaborador = {
        idcolaborador: colaborador.idcolaborador,
        identificacion: colaborador.identificacion,
        nombre: colaborador.nombre,
        primerapellido: colaborador.primerapellido,
        segundoapellido: colaborador.segundoapellido,
        foto: colaborador.foto
      };

      this.solicitud = {
        idCanton: colaborador.idcanton,
        direccion: colaborador.direccion,
        codigoPostal: colaborador.codigopostal,
        celular: colaborador.celular,
        telefono: colaborador.telefono,
        correoPersonal: colaborador.correopersonal,
        estadoCivil: colaborador.estadocivil,
        numerocpic: colaborador.numerocpic,
        numeroHijos: colaborador.numerohijos,
        fotoCpic: colaborador.certificadocpic,
        archivoColegiatura: null,
        idpaisresidencia: colaborador.idpaisresidencia,
        idpaisorigen: colaborador.idpaisorigen,
        idpaisferiado: colaborador.idpaisferiado,
        ciudadresidencia: colaborador.ciudadresidencia,
        fechanacimiento: aplicarFormatoFecha(
          colaborador.fechanacimiento,
          "yyyy-MM-dd"
        ),
        genero: colaborador.genero,
        telefonoemergencia: colaborador.telefonoemergencia,
        personallamar: colaborador.personallamar,
        paisOrigen: colaborador.paisOrigen,
        canton: colaborador.canton,
        paisResidencia: colaborador.paisResidencia,
        paisFeriado: colaborador.paisFeriado,
        afiliaciones: await this.getAfiliaciones(colaborador.afiliaciones),
        //afiliaciones: [],
        estado: common.SOLICITUD_SINMODIFICACIONES // datos personales sin modificaciones
      };

      // temporal se debe de cargar la lista con las afiliaciones reales
      // if (typeof this.solicitud.afiliaciones == typeof undefined) {
      //   this.solicitud.afiliaciones = [];
      // }

      this.terminaCargaDatos();
    },

    /**
     * Invoca a un método que obtiene el documento asociado a una solicitud así como
     * los datos propios de la solicitud
     */
    async obtenerSolicitud() {
      await this.cargarSolicitud();
    },
    /**
     * Carga el objeto SOLICITUD con el documento JSON que viene de base de datos
     * @documento hilera JSON con los datos de una solicitud
     */
    leerDocumentoSolicitud(documento) {
      try {
        var colaborador = JSON.parse(documento);

        return {
          colaborador: {
            idcolaborador: colaborador.idcolaborador,
            identificacion: colaborador.identificacion,
            nombre: colaborador.nombre,
            primerapellido: colaborador.primerapellido,
            segundoapellido: colaborador.segundoapellido,
            foto: colaborador.foto
          },
          solicitud: {
            idCanton: colaborador.idCanton,
            direccion: colaborador.direccion,
            codigoPostal: colaborador.codigoPostal,
            celular: colaborador.celular,
            telefono: colaborador.telefono,
            correoPersonal: colaborador.correoPersonal,
            estadoCivil: colaborador.estadoCivil,
            numerocpic: colaborador.numerocpic,
            numeroHijos: colaborador.numeroHijos,
            fotoCpic: colaborador.fotoCpic,
            archivoColegiatura: null,
            idpaisresidencia: colaborador.idpaisresidencia,
            idpaisorigen: colaborador.idpaisorigen,
            idpaisferiado: colaborador.idpaisferiado,
            ciudadresidencia: colaborador.ciudadresidencia,
            fechanacimiento: colaborador.fechanacimiento,
            genero: colaborador.genero,
            telefonoemergencia: colaborador.telefonoemergencia,
            personallamar: colaborador.personallamar,
            paisOrigen: colaborador.paisOrigen,
            canton: colaborador.canton,
            paisResidencia: colaborador.paisResidencia,
            paisFeriado: colaborador.paisFeriado,
            estado: colaborador.estado,
            afiliaciones: colaborador.afiliaciones
          }
        };
      } catch (error) {
        console.log("Error al leer el documento JSON de la solicitud");
        console.error(error);
      }
    },
    /**
     * Invoca al API que obtiene los datos de una solicitud
     */
    async cargarSolicitud() {
      this.cargandoDatos = true;

      await GetDetalleSolicitud(this.IdSolicitudJson)
        .then(response => {
          // objeto devuelto por el API
          var solicitud = response.data.solicitudMasterData;

          // se inicializan los datos base de la solicitud
          this.datosSolicitud = this.getDatosSolicitud(solicitud);

          let documento = this.leerDocumentoSolicitud(solicitud.docSolicitud);
          this.solicitud = documento.solicitud;
          this.colaborador = documento.colaborador;

          // comentarios de la solicitud
          this.comentarios = solicitud.comentarios.map(
            ({
              idComentarioSolicitud,
              responsable,
              comentario,
              accion,
              fecha
            }) => ({
              id: idComentarioSolicitud,
              responsable: responsable,
              comentario: comentario,
              accion: accion,
              fecha: fecha
            })
          );

          // se posterga la actualización del indicador de carga hasta que se haya "renderizado" la vista
          this.terminaCargaDatos();
        })
        .catch(response => {
          console.log(response);
          this.terminaCargaDatos();
        });
    },
    /**
     * Se posterga la actualización del indicador de carga hasta que se haya "renderizado" la vista
     */
    terminaCargaDatos() {
      this.$nextTick(() => {
        this.cargandoDatos = false;
      });
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
          } = await GetColaboradoresPerfil(this.$root.infoColaboradorActual.id); //await GetAllColaboradores();
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
    /**
     * Evento para procesar la acción de los botones del flujo de trabajo
     */
    procesarAccion(accion) {
      return this["procesarAccion_" + accion](
        accion,
        this.datosSolicitud.idSolicitudMasterData
      );
    },
    /**
     * Guarda la solicitud en la base de datos, siempre y cuando la misma tenga cambios registrados.
     * Genera el objeto requerido para luego invocar al método común de guardado.
     */
    async procesarAccion_Guardar(accion, idSolicitud) {
      if (this.solicitud.estado == common.SOLICITUD_SINMODIFICACIONES) {
        Vue.$toast.warning(
          "La solicitud no tiene modificaciones que guardar.",
          {}
        );
      } else {
        let datos = {
          idSolicitud: this.datosSolicitud.idSolicitudMasterData,
          idTipoSolicitud: this.datosSolicitud.idTipoSolicitud,
          codigoEstado: this.datosSolicitud.estadoSolicitud,
          idAutor: this.$root.infoColaboradorActual.id,
          idResponsable: this.$root.infoColaboradorActual.id,
          idColaborador: this.colaborador.idcolaborador,
          docSolicitud: JSON.stringify({
            ...this.colaborador,
            ...this.solicitud
          }) //JSON.stringify(this.solicitud)
        };
        this.datosSolicitud.idSolicitudMasterData = await this.Guardar(
          datos,
          this.datosSolicitud.idSolicitudMasterData
        );
      }
    },
    obtenerObjetoSolicitudEnviar(accion) {
      return {
        idSolicitud: this.datosSolicitud.idSolicitudMasterData,
        idTipoSolicitud: this.datosSolicitud.idTipoSolicitud,
        codigoEstado: this.datosSolicitud.estadoSolicitud,
        idAutor: this.$root.infoColaboradorActual.id,
        idResponsable: this.$root.infoColaboradorActual.id,
        idColaborador: this.colaborador.idcolaborador,
        accion: accion,
        docSolicitud: JSON.stringify({ ...this.colaborador, ...this.solicitud })
      };
    },
    async procesarAccion_Revisar(accion) {
      let opciones = {
        tipo: "comentario", // confirmar comentario ninguno,
        titulo: "Solicitud de cambio",
        mensajeDialogo:
          "Registre una observación para el autor de la solicitud",
        btnOk: "Enviar a revisión",
        btnCancel: "Cancelar",
        mensajeExito: "Solicitud enviada a revisión.",
        mensajeError:
          "Se ha presentado un inconveniente al enviar a revisión la solicitud de cambio."
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async procesarAccion_Descartar(accion) {
      let opciones = {
        tipo: "comentario", // confirmar comentario ninguno,
        titulo: "Solicitud de cambio",
        mensajeDialogo:
          "Registre una justificación para descartar la solicitud de cambio",
        btnOk: "Descartar",
        btnCancel: "Cancelar",
        mensajeExito: "Solicitud descartada.",
        mensajeError:
          "Se ha presentado un inconveniente al descartar la solicitud de cambio."
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async procesarAccion_Denegar(accion) {
      let opciones = {
        tipo: "comentario", // confirmar comentario ninguno,
        titulo: "Solicitud de cambio",
        mensajeDialogo:
          "Registre una justificación para denegar la solicitud de cambio",
        btnOk: "Denegar",
        btnCancel: "Cancelar",
        mensajeExito: "Solicitud denegada.",
        mensajeError:
          "Se ha presentado un inconveniente al denegar la solicitud de cambio."
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async procesarAccion_Aprobar(accion) {
      let opciones = {
        tipo: "confirmar", // confirmar comentario ninguno,
        titulo: "Solicitud de cambio",
        mensajeDialogo: "Está seguro de aprobar la solicitud?",
        btnOk: "Si",
        btnCancel: "No",
        mensajeExito: "Solicitud aprobada.",
        mensajeError:
          "Se ha presentado un inconveniente al aprobar la solicitud de cambio."
      };
      await this.enviarSolicitud(accion, opciones);
    },
    async enviarSolicitud(accion, opciones) {
      try {
        let datos = this.obtenerObjetoSolicitudEnviar(accion);
        await this.Enviar(datos, opciones);
      } catch (error) {
        console.log(error);
      }
    },
    // procesarAccion_Eliminar(accion) {
    //   this.confirmacionEliminarSolicitud=true;
    // },
    undoAfiliacion(index) {
      this.solicitud.afiliaciones[index].estado = "S";
    },
    agregarAfiliacion() {
      this.solicitud.afiliaciones[
        this.solicitud.afiliaciones.length - 1
      ].estado = "N";
    },
    modificarAfiliacion(index) {
      if (this.solicitud.afiliaciones[index].estado === "S") {
        this.solicitud.afiliaciones[index].estado = "M";
      }
    },
    eliminarAfiliacion(index) {
      this.solicitud.afiliaciones[index].estado = "E";
    },
    
    muestraBotonera() {
      if (this.origen == "Inbox") {
        if (!this.idResponsable || this.idResponsable == this.$root.infoColaboradorActual.id) {
          return true;
        } else {
          return false;
        }
      } else {
        return false;
      }
    }
  },
  computed: {
    muestraCuidadResidencia() {},
    pickPuedeBuscarColaborador() {
      return this.datosSolicitud.estadoSolicitud == common.REGISTRADA; // && !(this.usuarioActual.Roles.length == 1 && this.usuarioActual.Roles[0] == "COLABORADOR") ;
    },
    pickPuedeBuscar() {
      return (
        this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
        this.datosSolicitud.estadoSolicitud == common.CORREGIR
      );
    },
    puedePintar() {
      return this.habilitarControlesSolicitud(
        this.datosSolicitud.estadoSolicitud,
        this.origen
      );
      // return (
      //   this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
      //   this.datosSolicitud.estadoSolicitud == common.CORREGIR
      // );
    },
    puedePintarCpic() {
      return (
        this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
        this.datosSolicitud.estadoSolicitud == common.CORREGIR ||
        this.datosSolicitud.estadoSolicitud == common.ENVALIDACION ||
        this.datosSolicitud.estadoSolicitud == common.ENAPROBACION ||
        this.datosSolicitud.estadoSolicitud == common.APROBADA
      );
    },
    helpTextArea() {
      if (this.solicitud) {
        return `Tiene disponible un máximo de ${8000 -
          this.solicitud.direccion.length} carácteres.`;
      }
      return null;
    },
    idPaisResidenciaComputed() {
      if (this.solicitud) {
        return this.solicitud.idpaisresidencia;
      }
      return null;
    }
  },
  watch: {
    /**
     * Controla el cambio de alguno de los campos de datos personales, esto permitirá marcar
     * el documento con un estado de modificado
     */
    solicitud: {
      handler(newValue, oldValue) {
        if (!this.cargandoDatos) {
          // concurrencia -- async - await is the best way to use it

          if (oldValue !== null) {
            // console.log(newValue);
            //  console.log(oldValue);
            this.solicitud.estado = "M";
          }

          //
        }
      },
      deep: true
    },
    colaborador: function(newValue, oldValue) {
      if (!this.cargandoDatos) {
        this.solicitud.estado = "M";
      }
    }

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

  .card {
    border-radius: 16px !important;
  }
  .card-header {
    border-radius: 16px !important;
    border-bottom: transparent !important;
  }
}
/* FIN */
</style>
