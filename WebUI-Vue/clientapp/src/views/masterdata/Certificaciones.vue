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

    <CCard style="border-radius: 16px !important;">
      <CCardHeader
        style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
      >
        <strong> Certificaciones</strong>
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
        <FormulateForm
          v-model="solicitud"
          @submit="submit()"
          @failed-validation="errorValidacion"
          :invalid-message="this.invalidMessage"
        >
          <FormulateErrors />
          <resumen-colaborador
            :colaborador.sync="colaborador"
            :estado-solicitud="datosSolicitud.estadoSolicitud"
            :searchable="pickPuedeBuscarColaborador"
            :editable="false"
            :habilitar-controles="
              this.habilitarControlesSolicitud(
                this.datosSolicitud.estadoSolicitud,
                this.origen
              )
            "
            @displayModal="displayModal"
          >
          </resumen-colaborador>
        </FormulateForm>
      </CCardBody>
    </CCard>
    <CRow class="mt-1">
      <CCol md="9" sm="12" class="mb-2">
        <CButton
          class="mt-1 mr-1 btn btn-info mt-1 pull-left libro-marca-celeste onHoverDark"
          @click="Agregar()"
          v-if="this.puedePintar && puedePintarAdd"
          v-tooltip.top="'Permite agregar una nueva certificación.'"
        >
          <li class="pi pi-plus ml-1"></li>
          Agregar certificación
        </CButton>
        <CButton
          class="mt-1  btn btn-info pull-left libro-marca-celeste onHoverDark "
          @click="displayModal('Certificaciones')"
          v-if="this.puedePintar"
          v-tooltip.top="
            'Permite seleccionar una certificación ya registrada para aplicar cambios'
          "
        >
          <li class="pi pi-copy ml-1"></li>
          Incluir certificación
        </CButton>
      </CCol>

      <CCol md="3" sm="12" class="mb-2">
        <div class="table-header ">
          <span class="p-input-icon-left pull-right">
            <i class="pi pi-search" />
            <InputText v-model="filters['global']" class="w-100" />
          </span>
        </div>
      </CCol>
    </CRow>

    <!-- <CCard style="border-radius: 16px !important;">
      <CCardHeader
        style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;"
      >
      </CCardHeader>
      <CCardBody> -->
        <FormulateForm name="solicitud" @submit="submitSolicitud">
          <CRow style="margin-bottom: 20px;">
            <CCol lg="12">
              <DataTable
                ref="dt"
                :value="solicitud.certificacionList"
                :paginator="true"
                class="p-datatable-certificacion p-datatable-striped p-datatable-sm"
                :rows="5"
                dataKey="id"
                :selection.sync="certificacionSeleccionada"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} de registros"
                sortField="nombre"
                :sortOrder="1"
                stateKey="dt-state-certificacion"
                selectionMode="single"
                :autoLayout="true"
              >
                <template #header> </template>
                <template #empty>
                  No se encontraron registros.
                </template>
                <template #loading>
                  Cargando la información de selección dinámica. Por favor
                  espere.
                </template>

                <!-- nombre -->
                <Column
                  header="Certificación"
                  :sortable="true"
                  sortField="nombre"
                  filterField="nombre"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Certificación</span>
                    <a
                      href=""
                      style="cursor: hand;"
                      @click.prevent="Editar(slotProps.data)"
                    >
                      {{ slotProps.data.nombre }}</a
                    >
                  </template>
                </Column>

                <!-- descripcion -->
                <Column
                  header="Descripción"
                  :sortable="true"
                  sortField="descripcion"
                  filterField="descripcion"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Descripción</span>
                    {{ slotProps.data.proveedorCertificacion }}
                  </template>
                </Column>

                <!-- estado -->
                <Column
                  header="Estado"
                  :sortable="true"
                  sortField="estado"
                  filterField="estado"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Estado</span>
                    <span :class="pintaEstado(slotProps.data.estado, true)">
                      {{ pintaEstado(slotProps.data.estado, false) }}
                    </span>
                  </template>
                </Column>

                <!-- acciones  -->
                <Column
                  header=""
                  :sortable="false"
                  sortField="accion"
                  filterField="accion"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title"></span>

                    <div
                      v-if="
                        slotProps.data.estado !== 'N' &&
                          slotProps.data.estado !== 'E'
                      "
                      style="display:inline;"
                    >
                      <a
                        v-if="puedePintar"
                        class="pi pi-times-circle ml-1 iconExclude"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Excluir(slotProps.data)"
                        title="Excluir certificación de la lista"
                      ></a>
                    </div>
                    <div
                      v-if="slotProps.data.estado !== 'E'"
                      style="display:inline;"
                    >
                      <a
                        v-if="puedePintar"
                        class="pi pi-trash ml-1 iconTrash"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Eliminar(slotProps.data)"
                        title="Eliminar certificación"
                      ></a>
                    </div>
                    <div v-else>
                      <a
                        v-if="puedePintar"
                        class="pi pi-refresh ml-1 iconUndo"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Undo(slotProps.data)"
                        title="Recuperar certificación eliminada"
                      ></a>
                    </div>
                  </template>
                </Column>
              </DataTable>
            </CCol>
          </CRow>
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
                :comments_wrapper_classes="[
                  'custom-scrollbar',
                  'comments-wrapper'
                ]"
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

        <Dialog
          :visible.sync="showModalAgregar"
          :style="{ width: '70vw' }"
          :maximizable="true"
          :modal="true"
        >
          <template #header>
            <h5>{{ tituloModalEdicionComputed }}</h5>
          </template>

          <FormulateForm
            name="formulario"
            v-model="certificacionNuevoEditar"
            @submit="submit"
            @failed-validation="errorValidacion"
            :invalid-message="this.invalidMessage"
            class="mt-3"
          >
            <div>
              <FormulateErrors />
              <CRow>
                <CCol sm="6">
                  <FormulateInput
                    name="idcertificacion"
                    :options="certificacionesList"
                    type="select"
                    label="Certificación"
                    placeholder="Seleccione una certificación"
                    validation="required|not:0"
                    validation-name="Certificación"
                    :validation-messages="{
                      required: 'Seleccione una certificación.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    @change="onChangeCertificacion()"
                    :disabled="!puedePintar || eliminada"
                  />
                </CCol>
                <CCol sm="6">
                  <FormulateInput
                    name="idcertificacionproveedor"
                    :options="proveedoresList"
                    type="select"
                    label="Proveedor"
                    placeholder="Seleccione un proveedor"
                    validation="required|not:0"
                    validation-name="Proveedor"
                    :validation-messages="{
                      required: 'Seleccione un proveedor.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    :disabled="!puedePintar || eliminada"
                  />
                </CCol>
              </CRow>
              <CRow>
                <CCol sm="6">
                  <FormulateInput
                    type="number"
                    name="anio"
                    label="Año de obtención de la certificación"
                    placeholder="Ingrese el año de obtención"
                    :validation="
                      'required|matches:/[0-9]{4}/|max:4,length|min:4,length|max:' +
                        year +
                        ',value'
                    "
                    validation-name="Año de obtención del título"
                    :input-class="['form-control']"
                    :help-class="['hint']"
                    :readonly="!puedePintar || eliminada"
                  />
                </CCol>

                <CCol sm="6">
                  <FormulateInput
                    type="date"
                    name="vence"
                    label="Fecha de vencimiento"
                    :validation="'optional'"
                    :validation-messages="{
                      after:
                        'La fecha de vencimiento debe ser mayor a la actual.'
                    }"
                    input-class="form-control"
                    validation-name="Fecha vencimiento"
                    :help-class="['hint']"
                    :disabled="!puedePintar || eliminada"
                  />
                </CCol>
              </CRow>
              <CRow>
                <CCol sm="6" class="">
                  <label>Certificado de participación / aprovechamiento</label>

                  <FormulateInput
                    type="myfileupload"
                    name="certificado"
                    :help="
                      puedePintar === true
                        ? 'Formatos válidos: png/jpg/gif/pdf'
                        : ''
                    "
                    input-class="form-control"
                    validation-name="Certificado de participación"
                    class="hint"
                    tipo-documento="Documento de la certificación"
                    :estado-solicitud="datosSolicitud.estadoSolicitud"
                    :eliminada="eliminada"
                    :visor-padre="true"
                    :habilitar-controles="
                      this.habilitarControlesSolicitud(
                        this.datosSolicitud.estadoSolicitud,
                        this.origen
                      )
                    "
                    @showModal="showModalVisor"
                    @showModalPadre="showModalVisorPreview"
                  />
                </CCol>
              </CRow>

              <CRow>
                <CCol sm="12">
                  <FormulateInput
                    type="button"
                    label="Cancelar"
                    style="display:inline;"
                    @click="showModalAgregar = false"
                    :input-class="[
                      'btn',
                      'btn-info',
                      'pull-right',
                      'libro-marca-azuloscuro',
                      'ml-2'
                    ]"
                  />
                  <FormulateInput
                    type="submit"
                    label="Guardar"
                    style="display:inline;"
                    :input-class="[
                      'btn',
                      'btn-info',
                      'pull-right',
                      'libro-marca-azuloscuro'
                    ]"
                    v-if="this.puedePintar && !eliminada"
                  />
                </CCol>
              </CRow>
            </div>
          </FormulateForm>
          <template #footer>
            <div></div>
          </template>
        </Dialog>
      <!-- </CCardBody>
    </CCard> -->

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
    <visor-documentos
      :src="srcDocumento"
      :showModalVisorAdjuntos.sync="showModalVisorAdjuntos"
      @showModalVisor="showModalVisorDocumentos"
    >
    </visor-documentos>
  </div>
</template>

<script>
import {
  GetColaboradoresPerfil,
  GetAllColaboradores,
  GetCertificaciones,
  GetCertificacionesColaboradorAdjunto,
  GetProveedoresXCertificacion,
  GetColaboradorDetailResumen,
  GetDetalleSolicitud
} from "./request";
import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "./ResumenColaborador";
import BotoneraMasterData from "./BotoneraMasterData.vue";
import VisorDocumentos from "../generales/Componentes/VisorDocumentos.vue";

import Vue from "vue";
import _ from "lodash";
import { aplicarFormatoFecha } from "../../utils";
// métodos comunes
import common from "./common.js";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
import Comments from "../generales/Componentes/Comments/Comments.vue";

export default {
  name: "Certificaciones",
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
    "botonera-master-data": BotoneraMasterData,
    "confirm-dialogue": ConfirmDialogue,
    "comment-dialogue": CommentDialogue,
    "visor-documentos": VisorDocumentos,
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
      certificacionNuevoEditar: {
        idcertificacion: null,
        idcertificacionproveedor: null,
        anio: null,
        vence: null,
        certificado: null
      },

      solicitud: {
        estado: "S",
        certificacionList: []
      },
      comentarios: [],
      datosSolicitud: {
        idSolicitudMasterData: null,
        idTipoSolicitud: this.idTipoSolicitud,
        tipoSolicitud: this.tipoSolicitud,
        estadoSolicitud: "RE"
      },
      showModal: false,
      nombreTipoLista: "",
      columnasPickList: [],
      dataPickList: [],
      filters: {},
      loading: false,
      certificacionSeleccionada: [],
      showModalAgregar: false,
      showModalConfirmacion: false,
      certificacionEliminar: "",
      tituloModalEdicion: "",
      TipoListaPick: "",
      proveedoresList: [],
      certificacionesList: [],
      cargandoDatos: false,
      IdSolicitudJson: null,
      showModalVisorAdjuntos: false,
      esEdicion: false,
      visorEnable: false,
      tituloModal: "",
      srcDocumento: ""
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    await this.cargarCertificaciones();

    this.IdSolicitudJson = this.idSolicitud;

    if (this.IdSolicitudJson) {
      await this.cargarSolicitud();
    } else {
      //carga el usuario en session
      this.TipoListaPick = "Colaboradores";
      this.selected(this.$root.infoColaboradorActual);
    }
  },
  methods: {
    showModalVisorPreview(data) {
      this.srcDocumento = data.src;
      this.showModalVisorAdjuntos = data.show;
      this.visorEnable = data.show;
    },
    showModalVisorDocumentos(show) {
      this.visorEnable = show;
      this.showModalVisorAdjuntos = show;
    },
    showModalVisor(show) {
      this.visorEnable = show;
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        fieldNames += `-  ${fields[key].label}\r\n`;
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    errorValidacion(data) {
      Vue.$toast.warning(common.MENSAJE_ERROR, {
        // optional options Object
      });
    },
    obtenerDescripcionEstado(codigoEstadoSolicitud) {
      switch (codigoEstadoSolicitud) {
        case common.REGISTRADA:
          return "Registrada";
        case common.APROBADA:
          return "Aprobada";
        case common.DENEGADA:
          return "Denegada";
        case common.DESCARTADA:
          return "Descartada";
        case common.ENAPROBACION:
          return "En aprobación";
        case common.CORREGIR:
          return "En correcciones";
        case common.ENVALIDACION:
          return "En validación";
        case common.PENDIENTEACTUALIZACION:
          return "Pendiente de actualización";
        default:
          break;
      }
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
            estado: colaborador.estado,
            certificacionList: colaborador.certificacionList
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

      let loader = this.showLoader();

      GetDetalleSolicitud(this.IdSolicitudJson)
        .then(response => {
          // objeto devuelto por el API
          var solicitud = response.data.solicitudMasterData;

          // se inicializan los datos base de la solicitud
          this.datosSolicitud = this.getDatosSolicitud(solicitud);

          this.cargarDetalleColaborador(this.datosSolicitud.idcolaborador);

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

          this.hideLoader(loader);
        })
        .catch(response => {
          console.log(response);
          this.terminaCargaDatos();
          this.hideLoader(loader);
        });
    },

    async onChangeCertificacion() {
      await this.cargarProveedoresCertificacion(
        this.certificacionNuevoEditar.idcertificacion
      );
    },
    Close() {
      this.showModalConfirmacion = false;
    },
    async Eliminar(item) {
      this.certificacionSeleccionada = item;
      this.certificacionEliminar =
        this.certificacionSeleccionada.proveedorCertificacion +
        " - " +
        this.certificacionSeleccionada.nombre;

      const res = await this.Eliminar_Generico(
        "certificación",
        this.certificacionEliminar,
        "M"
      );

      const foundIndex = this.solicitud.certificacionList.findIndex(
        x => x.id == item.id
      );
      if (res) {
        if (item.estado === "N") {
          //  this.solicitud.certificacionList.splice(foundIndex);

          this.solicitud.certificacionList = this.solicitud.certificacionList.filter(
            x => x.id !== item.id
          );

          this.solicitud.certificacionList = this.addIdentityToArrayObjects(
            this.solicitud.certificacionList
          );

          Vue.$toast.success(
            "La certificación fue eliminada exitosamente.",
            {}
          );
        } else {
          this.solicitud.certificacionList[foundIndex].estado = "E";

          Vue.$toast.success(
            "La certificación fue eliminada exitosamente.",
            {}
          );
          this.solicitud.estado = "M";
        }
      }
    },

    async Excluir(item) {
      this.certificacionSeleccionada = item;
      this.certificacionEliminar =
        this.certificacionSeleccionada.proveedorCertificacion +
        " - " +
        this.certificacionSeleccionada.nombre;

      const res = await this.Excluir_Generico(
        "certificación",
        this.certificacionEliminar,
        "M"
      );

      if (res) {
        this.solicitud.certificacionList = this.solicitud.certificacionList.filter(
          x => x.id !== item.id
        );
        this.solicitud.certificacionList = this.addIdentityToArrayObjects(
          this.solicitud.certificacionList
        );
        Vue.$toast.success("La certificación fue excluida exitosamente.", {});
      }
    },

    Undo(item) {
      const foundIndex = this.solicitud.certificacionList.findIndex(
        x => x.id == item.id
      );

      if (
        this.solicitud.certificacionList[foundIndex].hashCode ==
        this.hashCode(
          this.certificacionSeleccionada.idcertificacion.toString() +
            this.certificacionSeleccionada.idcertificacionproveedor.toString() +
            this.certificacionSeleccionada.anio.toString() +
            this.certificacionSeleccionada.vence +
            this.certificacionSeleccionada.certificado
        )
      ) {
        this.solicitud.certificacionList[foundIndex].estado = "S";
      } else {
        this.solicitud.certificacionList[foundIndex].estado = "M";
      }

      // this.solicitud.certificacionList[foundIndex].estado = "S";
      // Vue.$toast.success("El curso fue eliminado exitosamente.", {});
    },
    Agregar() {
      // this.$formulate.resetValidation("formulario");
      this.tituloModalEdicion = "Agregar certificación";
      this.cleanForm();
      this.showModalAgregar = true;
      this.esEdicion = false;
      this.certificacionNuevoEditar = {
        idcertificacion: null,
        idcertificacionproveedor: null,
        anio: null,
        vence: null,
        certificado: null,
        estado: "S"
      };
    },
    async Editar(item) {
      this.tituloModalEdicion = "Modificar certificación";
      this.certificacionSeleccionada = item;
      this.showModalAgregar = true;
      this.esEdicion = true;

      this.cargarProveedoresCertificacion(item.idcertificacion);

      this.certificacionNuevoEditar = {
        idcolaborador: this.colaborador.idcolaborador,
        id: item.id,
        idcertificacion: item.idcertificacion,
        idcertificacionproveedor: item.idcertificacionproveedor,
        idProveedorCertificacion: item.idProveedorCertificacion,
        anio: item.anio,
        vence: item.vence,
        certificado: item.certificado === undefined ? null : item.certificado,
        estado: item.estado
      };
    },
    async onClick() {
      //  await this.cargarFormacion(this.formacionSeleccionada.id);
    },

    obtenerCertificacionNombre(id) {
      let result = "";
      _.forEach(this.certificacionesList, function(value, key) {
        if (value.value == id) {
          result = value.label;
        }
      });
      return result;
    },
    obtenerProveedorNombre(id) {
      let result = "";
      _.forEach(this.proveedoresList, function(value, key) {
        if (value.value == id) {
          result = value.label;
        }
      });

      return result;
    },

    async submit(formValues) {
      try {
        if (!this.showModalVisorAdjuntos) {
          let resultadoValidacion = this.validacionesSolicitud();

          if (resultadoValidacion.esValido) {
            // actualiza la lista interna de certificaciones por colaborador, previamente validada
            if (this.actualizarLista()) {
              this.showModalAgregar = false;
            }
          } else {
            Vue.$toast.warning(resultadoValidacion.mensaje, {});
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    actualizarLista() {
      try {
        if (this.esEdicion) {
          this.editarListaInterna();
        } else {
          this.agregarListaInterna();
        }
        return true;
      } catch (error) {
        console.log(error);
        return false;
      }
    },
    editarListaInterna() {
      const foundIndex = this.solicitud.certificacionList.findIndex(
        x => x.id == this.certificacionNuevoEditar.id
      );

      let cambios = true;

      if (
        this.solicitud.certificacionList[foundIndex].hashCode ==
        this.hashCode(
          this.certificacionNuevoEditar.idcertificacion.toString() +
            this.certificacionNuevoEditar.idcertificacionproveedor.toString() +
            this.certificacionNuevoEditar.anio.toString() +
            this.certificacionNuevoEditar.vence +
            this.certificacionNuevoEditar.certificado
        )
      ) {
        cambios = false;
        Vue.$toast.warning("No se realizaron cambios.", {});
      }

      this.solicitud.certificacionList[
        foundIndex
      ].idcertificacion = this.certificacionNuevoEditar.idcertificacion;

      this.solicitud.certificacionList[
        foundIndex
      ].idcertificacionproveedor = this.certificacionNuevoEditar.idcertificacionproveedor;

      this.solicitud.certificacionList[
        foundIndex
      ].idProveedorCertificacion = this.certificacionNuevoEditar.idProveedorCertificacion;

      this.solicitud.certificacionList[
        foundIndex
      ].anio = this.certificacionNuevoEditar.anio;
      this.solicitud.certificacionList[
        foundIndex
      ].vence = this.certificacionNuevoEditar.vence;

      this.solicitud.certificacionList[
        foundIndex
      ].certificado = this.certificacionNuevoEditar.certificado;

      this.solicitud.certificacionList[
        foundIndex
      ].nombre = this.obtenerCertificacionNombre(
        this.certificacionNuevoEditar.idcertificacion
      );
      this.solicitud.certificacionList[
        foundIndex
      ].proveedorCertificacion = this.obtenerProveedorNombre(
        this.certificacionNuevoEditar.idcertificacionproveedor
      );

      // this.solicitud.certificacionList[
      //   foundIndex
      // ].idProveedorCertificacion = this.certificacionNuevoEditar.idcertificacionproveedor;

      if (this.solicitud.certificacionList[foundIndex].estado != "N") {
        if (cambios) {
          this.solicitud.certificacionList[foundIndex].estado = "M";
        } else {
          this.solicitud.certificacionList[foundIndex].estado = "S";
        }
      }

      if (cambios) {
        this.solicitud.estado = "M";
      }
    },
    agregarListaInterna() {
      this.solicitud.certificacionList.push({
        idcolaborador: this.colaborador.idcolaborador,
        idcertificacion: this.certificacionNuevoEditar.idcertificacion,
        idcertificacionproveedor: this.certificacionNuevoEditar
          .idcertificacionproveedor,
        anio: this.certificacionNuevoEditar.anio,
        vence: this.certificacionNuevoEditar.vence,
        certificado: this.certificacionNuevoEditar.certificado,
        estado: "N",
        // idcertificacioncolaborador: this.getNuevoId(), // se utiliza representativamente para el manejo del id de la tabla, y sus acciones, no es un valor almacenable, este seria un identity en bd
        nombre: this.obtenerCertificacionNombre(
          this.certificacionNuevoEditar.idcertificacion
        ),
        proveedorCertificacion: this.obtenerProveedorNombre(
          this.certificacionNuevoEditar.idcertificacionproveedor
        ),
        idProveedorCertificacion: this.certificacionNuevoEditar
          .idcertificacionproveedor
      });
      this.solicitud.certificacionList = this.addIdentityToArrayObjects(
        this.solicitud.certificacionList
      );
      this.solicitud.estado = "M";
    },

    validacionesSolicitud() {
      let resultado = { esValido: true };

      if (
        !this.certificacionNuevoEditar.certificado ||
        this.certificacionNuevoEditar.certificado == ""
      ) {
        resultado = {
          esValido: false,
          mensaje: "Debe agregar el certificado de la certificación"
        };
      }

      return resultado;
    },

    async selected(item) {
      switch (this.TipoListaPick) {
        case "Colaboradores":
          this.cargarDetalleColaborador(item.id);
          // await this.cargarCertificacionesColaborador(item.id);
          this.solicitud.certificacionList = [];

          this.showModal = false;
          break;

          break;
        case "Certificaciones":
          this.showModal = false;
          this.cargarCertificacionPickList(item);
          break;

        default:
          break;
      }
    },
    cargarCertificacionPickList(certificacion) {
      this.cargandoDatos = true;

      if (!this.existeCertificacion(certificacion)) {
        this.solicitud.certificacionList.push({
          ...certificacion,
          estado: "S",
          hashCode: this.hashCode(
            certificacion.idcertificacion.toString() +
              certificacion.idcertificacionproveedor.toString() +
              certificacion.anio.toString() +
              certificacion.vence +
              certificacion.certificado
          )
        });
        this.solicitud.certificacionList = this.addIdentityToArrayObjects(
          this.solicitud.certificacionList
        );
        this.solicitud.estado = "M";
        this.terminaCargaDatos();
      } else {
        Vue.$toast.warning(
          "La certificación seleccionada ya está en la lista."
        );
      }
    },
    existeCertificacion(item) {
      const list = this.solicitud.certificacionList.filter(
        x => x.idcertificacioncolaborador === item.idcertificacioncolaborador
      );
      const list2 = this.solicitud.certificacionList.filter(
        x => x.id === item.id
      );

      if (list.length > 0 || list2.length > 0) {
        return true;
      } else {
        return false;
      }
    },

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
            ({
              idcolaborador,
              nombreCompleto,
              nombre,
              identificacion,
              primerapellido,
              segundoapellido,
              foto
            }) => ({
              id: idcolaborador,
              nombre: nombre,
              nombreCompleto: nombreCompleto,
              identificacion: identificacion,
              primerapellido: primerapellido,
              segundoapellido: segundoapellido,
              foto: foto
            })
          );

          this.columnasPickList = [
            { field: "identificacion", header: "Id" },
            { field: "nombreCompleto", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "Colaboradores";
          this.tituloModal = "Lista de colaboradores";
          break;

        case "Certificaciones":
          const {
            data: { certificacionesColaborador }
          } = await GetCertificacionesColaboradorAdjunto(
            this.colaborador.idcolaborador
          );

          this.dataPickList = certificacionesColaborador.map(
            ({
              idcertificacioncolaborador,
              idcertificacionproveedor,
              idcertificacion,
              certificacion,
              descripcion,
              proveedorCertificacion,
              anio,
              vence,
              idProveedorCertificacion,
              certificado
            }) => ({
              id: idcertificacioncolaborador,
              idcertificacionproveedor: idProveedorCertificacion,
              idcertificacion: idcertificacion,
              nombre: certificacion,
              proveedorCertificacion: proveedorCertificacion,
              descripcion: descripcion == null ? "" : descripcion,
              anio: anio,
              vence: aplicarFormatoFecha(vence, "yyyy-MM-dd"),
              estado: "S",
              idProveedorCertificacion: idProveedorCertificacion,
              certificado: certificado,
              idcertificacioncolaborador: idcertificacioncolaborador
            })
          );

          this.columnasPickList = [
            { field: "id", header: "Id" },
            { field: "nombre", header: "Nombre" }
          ];

          this.showModal = true;
          this.nombreTipoLista = "Certificación";
          this.tituloModal = "Lista de certificaciones";

          break;

        default:
          break;
      }
    },
    async cargarCertificaciones() {
      const {
        data: { certificaciones }
      } = await GetCertificaciones();
      this.certificacionesList = certificaciones.map(
        ({ idcertificacion, nombre }) => ({
          label: nombre,
          value: idcertificacion
        })
      );
    },
    async cargarCertificacionesColaborador(id) {
      this.filters = {};
      const {
        data: { certificacionesColaborador }
      } = await GetCertificacionesColaboradorAdjunto(id);
      if (certificacionesColaborador.length == 0) {
        this.solicitud.certificacionList = [];
        Vue.$toast.warning("No existen certificaciones ingresadas.");
      } else {
        this.solicitud.certificacionList = certificacionesColaborador.map(
          ({
            idcertificacioncolaborador,
            idcertificacionproveedor,
            idcertificacion,
            certificacion,
            descripcion,
            proveedorCertificacion,
            anio,
            vence,
            idProveedorCertificacion,
            certificado
          }) => ({
            idcertificacioncolaborador: idcertificacioncolaborador,
            idcertificacionproveedor: idProveedorCertificacion,
            idcertificacion: idcertificacion,
            nombre: certificacion,
            proveedorCertificacion: proveedorCertificacion,
            descripcion: descripcion == null ? "" : descripcion,
            anio: anio,
            vence: aplicarFormatoFecha(vence, "yyyy-MM-dd"),
            estado: "S",
            idProveedorCertificacion,
            certificado
          })
        );
      }
    },
    async cargarProveedoresCertificacion(idcertificacion) {
      const {
        data: { certificacionesProveedor }
      } = await GetProveedoresXCertificacion(idcertificacion);
      this.proveedoresList = certificacionesProveedor.map(
        ({ idproveedorcertificacion, proveedorCertificacion }) => ({
          label: proveedorCertificacion,
          value: idproveedorcertificacion
        })
      );
    },
    clean() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
      this.filters = {};
    },
    cleanForm() {
      this.certificacionNuevoEditar.idcertificacion = null;
      this.certificacionNuevoEditar.proveedor = null;
      this.certificacionNuevoEditar.certificacion = null;
      this.certificacionNuevoEditar.anio = null;
      this.certificacionNuevoEditar.vence = null;
    },
    onUpload() {
      //this.$toast.add({severity: 'info', summary: 'Success', detail: 'File Uploaded', life: 3000});
    },
    async cargarDetalleColaborador(id) {
      this.cargandoDatos = true;
      const {
        data: { colaborador }
      } = await GetColaboradorDetailResumen(id);

      this.colaborador = {
        idcolaborador: colaborador.idcolaborador,
        identificacion: colaborador.identificacion,
        nombre: colaborador.nombre,
        primerapellido: colaborador.primerapellido,
        segundoapellido: colaborador.segundoapellido,
        foto: colaborador.foto
      };
      this.terminaCargaDatos();
    },
    pintaEstado(estado, style) {
      if (style) {
        switch (estado) {
          case "S":
            return "";
          case "N":
            return "badge rounded-pill bg-success p-component";
          case "M":
            return "badge rounded-pill bg-warning p-component";
          case "E":
            return "badge rounded-pill bg-danger p-component";

          default:
            break;
        }
      } else {
        switch (estado) {
          case "S":
            return "Sin cambios";
          case "N":
            return "Nuevo";
          case "M":
            return "Modificado";
          case "E":
            return "Eliminado";

          default:
            break;
        }
      }
    },
    /**
     * Se posterga la actualización del indicador de carga hasta que se haya "renderizado" la vista
     */
    terminaCargaDatos() {
      this.$nextTick(() => {
        this.cargandoDatos = false;
      });
    },

    // Metodos de la botonera

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
            ...this.colaborador.idcolaborador,
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
        docSolicitud: JSON.stringify({
          ...this.colaborador.idcolaborador,
          ...this.solicitud
        })
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
    // procesarAccion_Eliminar(accion) {
    //   this.confirmacionEliminarSolicitud = true;
    // },
    async enviarSolicitud(accion, opciones) {
      try {
        let datos = this.obtenerObjetoSolicitudEnviar(accion);
        await this.Enviar(datos, opciones);
      } catch (error) {
        console.log(error);
      }
    },
    async submitSolicitud(formValues) {
      try {
        if (this.solicitud.estado == common.SOLICITUD_SINMODIFICACIONES) {
          Vue.$toast.warning(
            "La solicitud no tiene modificaciones que guardar.",
            {}
          );
        } else if (this.solicitudSinModificaciones()) {
          let mensaje = "";
          if (this.solicitud.certificacionList.length == 0) {
            mensaje = "Por favor incluir al menos un registro a la lista.";
          } else {
            mensaje =
              "Por favor excluir las certificaciones que no tienen modificaciones.";
          }

          Vue.$toast.warning(mensaje, {});
        } else {
          let datos = {
            idSolicitud: this.datosSolicitud.idSolicitudMasterData,
            idTipoSolicitud: this.datosSolicitud.idTipoSolicitud,
            codigoEstado: this.datosSolicitud.estadoSolicitud,
            idAutor: this.$root.infoColaboradorActual.id,
            idResponsable: this.$root.infoColaboradorActual.id,
            idColaborador: this.colaborador.idcolaborador,
            accion: "Enviar",
            docSolicitud: JSON.stringify({
              ...this.colaborador.idcolaborador,
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
        }
      } catch (error) {
        console.log(error);
      }
    },
    validacionesSolicitud() {
      let resultado = { esValido: true };

      if (
        !this.certificacionNuevoEditar.certificado ||
        this.certificacionNuevoEditar.certificado == ""
      ) {
        resultado = {
          esValido: false,
          mensaje: "Debe agregar el certificado de la certificación"
        };
      }

      return resultado;
    },
    solicitudSinModificaciones() {
      const foundIndex = this.solicitud.certificacionList.findIndex(
        x => x.estado == "S"
      );

      if (foundIndex >= 0 || this.solicitud.certificacionList.length == 0) {
        return true;
      }

      return false;
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
    certificacionAEliminar() {
      return this.certificacionEliminar;
    },
    puedePintar() {
      // let pinta = false;
      // if (
      //   this.datosSolicitud.estadoSolicitud == common.REGISTRADA ||
      //   this.datosSolicitud.estadoSolicitud == common.CORREGIR
      // ) {
      //   pinta = true;
      // }

      // return pinta;
      return this.habilitarControlesSolicitud(
        this.datosSolicitud.estadoSolicitud,
        this.origen
      );
    },
    puedePintarAdd() {
      let pinta = false;
      if (this.datosSolicitud.estadoSolicitud == common.REGISTRADA) {
        pinta = true;
      }

      return pinta;
    },
    pickPuedeBuscarColaborador() {
      return this.datosSolicitud.estadoSolicitud == common.REGISTRADA; // && !(this.usuarioActual.Roles.length == 1 && this.usuarioActual.Roles[0] == "COLABORADOR") ;
    },
    eliminada() {
      return this.certificacionNuevoEditar.estado === "E";
    },
    tituloModalEdicionComputed() {
      let res = this.tituloModalEdicion;

      if (this.certificacionNuevoEditar.estado === "E") {
        res = "Ver";
      }
      if (
        this.datosSolicitud.estadoSolicitud != common.REGISTRADA &&
        this.datosSolicitud.estadoSolicitud != common.CORREGIR
      ) {
        res = "Ver";
      }

      return res;
    },
    today() {
      return new Date();
    },
    year() {
      // return new Date().year;

      let d = new Date();
      let n = d.getFullYear();
      return n;
    }
  },
  watch: {
    /**
     * Controla el cambio de alguno de los campos de ubicacion de colaborador, esto permitirá marcar
     * el documento con un estado de modificado
     */
    solicitud: function(newValue, oldValue) {
      if (!this.cargandoDatos) {
        this.solicitud.estado = "M";
      }
    }
  }
};
</script>

<style>
.iconTrash {
  color: #e55353;
}
.iconUndo {
  color: greenyellow;
}
.bg-danger {
  color: white;
}
.bg-info {
  color: white;
}
.bg-warning {
  color: white;
}
.bg-success {
  color: white;
}

.text-divider span {
  background-color: gainsboro;
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
.avatar-upload .avatar-show {
  position: absolute;
  right: 12px;
  z-index: 1;
  top: 10px;
  display: none;
}
.avatar-upload .avatar-show input {
  display: none;
}
.avatar-upload .avatar-show input + label {
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
.avatar-upload .avatar-show input + label:hover {
  background: #f1f1f1;
  border-color: #d6d6d6;
}
.avatar-upload .avatar-show input + label:after {
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

/*todo*/
.button.close {
  color: red !important;
  background-color: red !important;
}
.modal-ku {
  width: 750px !important;
  margin: auto !important;
}
.modal-lg {
  max-width: 80% !important;
}

.text-divider {
  margin: 2em 0;
  line-height: 0;
  font-size: 0.875rem;
}
.text-divider span {
  background-color: gainsboro;
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
.avatar-upload .avatar-show {
  position: absolute;
  right: 12px;
  z-index: 1;
  top: 10px;
  display: none;
}
.avatar-upload .avatar-show input {
  display: none;
}
.avatar-upload .avatar-show input + label {
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
.avatar-upload .avatar-show input + label:hover {
  background: #f1f1f1;
  border-color: #d6d6d6;
}
.avatar-upload .avatar-show input + label:after {
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

/* */

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

.p-button {
  background-color: #29abe2;
}

.onHoverDark:hover {
  color: #fff;
  background-color: #002e6e;
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

/* Responsive: en visualización normal se ocultan las etiquetas asociadas al título de cada columna */
.p-datatable-certificacion .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-certificacion .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-certificacion .p-datatable-tbody > tr > td {
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

.p-datatable .p-datatable-tbody > tr > td {
  border: 1px solid rgba(175, 189, 216, 1);
}
.p-datatable table {
  width: 99%;
  background-color: #f8f8f8;
}

.p-datatable .p-paginator-bottom {
    width: 99%;
}




.btn-opciones {
  display: none;
}

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de
   etiqueta y valor asociado */
@media screen and (max-width: 40em) {
  .p-datatable.p-datatable-certificacion .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-certificacion .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-certificacion .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-certificacion .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-certificacion:last-child {
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
}
/* FIN */
</style>
