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
        <strong> Catálogos</strong>
        <small> Datos Personales </small>
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
          :searchable="false"
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
      </CCardBody>
    </CCard>

    <CRow>
      <CCol sm="4">
        <FormulateInput
          v-model="solicitud.idCatalogoSolicitud"
          :options="listaCatalogos"
          type="select"
          label="Catálogo"
          placeholder="Seleccione un catálogo"
          validation="required|not:0"
          validation-name="Catálogo"
          :validation-messages="{
            required: 'Seleccione un catálogo',
            not: 'Debe de seleccionar una opción válida...'
          }"
          :input-class="['form-control']"
          :disabled="!puedePintar"
        />
      </CCol>
    </CRow>
    <CRow class="mt-1">
      <CCol md="9" sm="12" class="mb-2">
        <CButton
          class="mt-1 mr-1 btn btn-info mt-1 pull-left libro-marca-celeste onHoverDark "
          @click="Agregar()"
          v-if="this.puedePintar && this.solicitud.idCatalogoSolicitud != null"
          v-tooltip.top="
            'Permite agregar ' +
              `un` +
              (generoFemeninoTipoListaComputed == true ? 'a ' : '') +
              ' nuev' +
              (generoFemeninoTipoListaComputed == true ? 'a ' : 'o ') +
              tipocatalogonombreComputed +
              ''
          "
        >
          <li class="pi pi-plus ml-1"></li>
          {{ `Agregar ` + this.tipocatalogonombreComputed }}
        </CButton>
        <CButton
          class="mt-1  btn btn-info pull-left libro-marca-celeste onHoverDark "
          @click="displayModal('Catalogos')"
          v-if="this.puedePintar && this.solicitud.idCatalogoSolicitud != null"
          v-tooltip.top="
            'Permite seleccionar ' +
              `un` +
              (generoFemeninoTipoListaComputed == true ? 'a ' : '') +
              ' ' +
              tipocatalogonombreComputed +
              ' existente para aplicar cambios'
          "
        >
          <li class="pi pi-copy ml-1"></li>

          {{ `Incluir ` + this.tipocatalogonombreComputed }}
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
                :value="solicitud.dataCatalogoList"
                :paginator="true"
                class="p-datatable-catalogo p-datatable-striped p-datatable-sm"
                :rows="5"
                dataKey="id"
                :selection.sync="catalogoSeleccionado"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} de registros"
                sortField="nombre"
                :sortOrder="1"
                stateKey="dt-state-catalogo"
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

                <!-- catalogo  -->
                <Column
                  :sortable="true"
                  header="Nombre"
                  sortField="nombre"
                  filterField="nombre"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Nombre</span>
                    <a
                      href=""
                      style="cursor: hand;"
                      @click.prevent="Editar(slotProps.data)"
                    >
                      {{ slotProps.data.nombre }}</a
                    >
                  </template>
                </Column>

                <!-- descripcion  -->
                <Column
                  header="Descripción"
                  :sortable="true"
                  sortField="descripcion"
                  filterField="descripcion"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Descripción</span>
                    {{ slotProps.data.descripcion }}
                  </template>
                </Column>

                <!-- Estado  -->
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

                <!-- Acciones -->
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
                        :title="
                          'Excluir ' +
                            tipocatalogonombreComputed +
                            ' de la lista'
                        "
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
                        :title="
                          'Eliminar ' +
                            tipocatalogonombreComputed +
                            ' de la lista'
                        "
                      ></a>
                    </div>
                    <div v-else>
                      <a
                        v-if="puedePintar"
                        class="pi pi-refresh ml-1 iconUndo"
                        href=""
                        style="cursor: hand;"
                        @click.prevent="Undo(slotProps.data)"
                        :title="
                          'Recuperar ' +
                            tipocatalogonombreComputed +
                            ' eliminada'
                        "
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
            v-model="catalogoNuevoEditar"
            @submit="submit"
            @failed-validation="errorValidacion"
            :invalid-message="this.invalidMessage"
          >
            <div class="mt-3">
              <TabView>
                <!-- generales  -->
                <TabPanel header="Datos generales">
                  <FormulateErrors />
                  <CRow>
                    <CCol sm="12">
                      <FormulateInput
                        name="nombre"
                        label="Nombre"
                        validation="required|max:100,length"
                        input-class="form-control"
                        validation-name="Nombre"
                        :readonly="!this.puedePintar || eliminada"
                      />
                    </CCol>
                    <CCol sm="12">
                      <FormulateInput
                        type="textarea"
                        name="descripcion"
                        label="Descripción"
                        validation="max:8000,length"
                        validation-name="Descripción"
                        :input-class="['form-control']"
                        :help="
                          `Tiene disponible un máximo de ${8000 -
                            descripcionComputed} carácteres.`
                        "
                        rows="2"
                        :help-class="['hint']"
                        :readonly="!this.puedePintar || eliminada"
                      />
                    </CCol>
                  </CRow>

                  <!-- ERP  -->

                  <CRow
                    v-if="
                      this.validaTipoCatalogo(
                        solicitud.idCatalogoSolicitud,
                        'ERPCORE'
                      )
                    "
                  >
                    <CCol sm="6">
                      <FormulateInput
                        name="idTipoCore"
                        :options="tipoCores"
                        type="select"
                        label="Tipo de core"
                        placeholder="Seleccione un tipo de core"
                        validation="required|not:0"
                        validation-name="Tipo de core"
                        :validation-messages="{
                          required: 'Seleccione un Tipo de core.',
                          not: 'Debe de seleccionar una opción válida.'
                        }"
                        :input-class="['form-control']"
                        :disabled="!this.puedePintar || eliminada"
                      />
                    </CCol>

                    <CCol sm="6">
                      <FormulateInput
                        name="tecnologia"
                        label="Tecnología"
                        validation="max:100,length"
                        input-class="form-control"
                        validation-name="Tecnología"
                        :readonly="!this.puedePintar || eliminada"
                      />
                    </CCol>
                  </CRow>
                  <CRow
                    v-if="
                      this.validaTipoCatalogo(
                        solicitud.idCatalogoSolicitud,
                        'ERPCORE'
                      )
                    "
                  >
                    <CCol sm="6">
                      <label>
                        <span>Es OpenSource?</span>
                      </label>
                      <FormulateInput
                        type="myInputSwitch"
                        name="esopensource"
                        :input-class="['form-control', 'readonly']"
                        validation-name="OpenSource"
                        class="hint"
                        :disabled="!puedePintar"
                      />
                    </CCol>
                  </CRow>
                </TabPanel>

                <!-- Proveedores por certificacion -->
                <TabPanel
                  header="Proveedores de certificación"
                  v-if="
                    this.validaTipoCatalogo(
                      solicitud.idCatalogoSolicitud,
                      'CERTIFICACIONES'
                    )
                  "
                >
                  <!-- Proveedores  -->
                  <CRow>
                    <CCol sm="12">
                      <agrupacion-proveedores
                        :proveedores="proveedoresCertificacionesComputed"
                        :proveedoresDDL="proveedoresList || []"
                        :habilitar-controles="
                          this.habilitarControlesSolicitud(
                            this.datosSolicitud.estadoSolicitud,
                            this.origen
                          )
                        "
                        :idColaborador="colaborador.idcolaborador"
                        :idcatalogo="idCatalogoComputed"
                        @undo="undoProveedor"
                        @modify="modificarProveedor"
                        @add="agregarProveedor"
                        @remove="eliminarProveedor"
                      >
                      </agrupacion-proveedores>
                    </CCol>
                  </CRow>
                </TabPanel>

                <!--Certificaciones  por proveedor -->
                <TabPanel
                  header="Certificaciones de proveedor"
                  v-if="
                    this.validaTipoCatalogo(
                      solicitud.idCatalogoSolicitud,
                      'PROVEEDORCERTIFICACION'
                    )
                  "
                >
                  <!-- Certificaciones  -->
                  <CRow>
                    <CCol sm="12">
                      <agrupacion-certificaciones
                        :certificaciones="this.certificacionesProveedorComputed"
                        :certificacionesDDL="certificacionesList || []"
                        :habilitar-controles="
                          this.habilitarControlesSolicitud(
                            this.datosSolicitud.estadoSolicitud,
                            this.origen
                          )
                        "
                        :idColaborador="this.colaborador.idcolaborador"
                        :idcatalogo="idCatalogoComputed"
                        @undo="undoCertificacion"
                        @modify="modificarCertificacion"
                        @add="agregarCertificacion"
                        @remove="eliminarCertificacion"
                      >
                      </agrupacion-certificaciones>
                    </CCol>
                  </CRow>
                </TabPanel>
              </TabView>

              <!-- confirmacion  -->
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
        :generoTipoLista="generoTipoLista"
      ></pick-list>

      <template #footer> </template>
    </Dialog>

    <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>
    <comment-dialogue ref="commentDialogue"></comment-dialogue>
  </div>
</template>

<script>
import {
  GetColaboradoresPerfil,
  GetAreasOrganizativas,
  GetAmbienteDesarrollo,
  GetBaseDatos,
  GetBus,
  GetCentrosEstudio,
  GetCertificacionProveedores,
  GetEmpresaCorporacion,
  GetErpCore,
  GetCalidad,
  GetIdiomas,
  GetInfraestructura,
  GetLenguajeProgramacion,
  GetBPMS,
  GetProveedorCertificacionesLista,
  GetPuesto,
  GetSectores,
  GetTecnologia,
  GetTipoCore,
  GetColaboradorDetailResumen,
  GetDetalleSolicitud,
  GetEspecialidad,
  GetProveedores,
  GetCertificaciones,
  GetCloud,
  //Validaciones
  ValidaAmbienteDependencias,
  ValidaAreaOrganizativaDependencias,
  ValidaBaseDatosDependencias,
  ValidaBPMDependencias,
  ValidaBusDependencias,
  ValidaCentroEstudioDependencias,
  ValidaCertificacionDependencias,
  ValidaEmpresaDependencias,
  ValidaERPDependencias,
  ValidaCalidadDependencias,
  ValidaIdiomaDependencias,
  ValidaInfraestructuraDependencias,
  ValidaLenguajeDependencias,
  ValidaProveedorDependencias,
  ValidaPuestoDependencias,
  ValidaSectorDependencias,
  ValidaTecnologiaDependencias,
  ValidaTipoCoreDependencias,
  ValidaEspecialidadDependencias,
  ValidaCloudDependencias
} from "./request";
import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "./ResumenColaborador";
import BotoneraMasterData from "./BotoneraMasterData.vue";
import Vue from "vue";

// métodos comunes
import common from "./common.js";
import _ from "lodash";
import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
import Comments from "../generales/Componentes/Comments/Comments.vue";

import RepeatableProveedores from "./Componentes/RepeatableProveedores.vue";
import RepeatableCertificaciones from "./Componentes/RepeatableCertificaciones.vue";

export default {
  name: "Catalogos",
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
    "agrupacion-proveedores": RepeatableProveedores,
    "agrupacion-certificaciones": RepeatableCertificaciones,
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
      catalogoNuevoEditar: {
        idCatalogo: null,
        nombre: "",
        descripcion: "",
        proveedores: []
      },
      solicitud: {
        estado: "S",
        dataCatalogoList: [],
        idCatalogoSolicitud: null
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
      tipoCatalogo: "",
      showModalAgregar: false,
      showModalConfirmacion: false,
      catalogoEliminar: "",
      tituloModalEdicion: "",
      TipoListaPick: "",
      catalogoSeleccionado: null,
      filters: {},
      loading: false,
      cargandoDatos: false,
      IdSolicitudJson: null, // id de la solicitud para cargar el json de una solicitud
      showModalVisorAdjuntos: false,
      esEdicion: false,
      visorEnable: false,
      tituloModal: "",
      srcDocumento: "",
      listaCatalogos: [],
      tipoCores: [],
      tipocatalogonombre: "",
      generoTipoLista: "M",
      proveedoresList: [],
      certificacionesList: []
    };
  },
  async mounted() {
    this.IdSolicitudJson = this.idSolicitud;
    this.listaCatalogo();
    await this.cargarProveedores();
    await this.cargarCertificaciones();

    if (this.IdSolicitudJson) {
      this.cargarSolicitud();
    } else {
      //carga el usuario en session
      this.TipoListaPick = "Colaboradores";
      this.selected(this.$root.infoColaboradorActual);
    }
    await this.cargarTipoCores();
  },
  methods: {
    invalidMessage(fields) {
      //  console.log(fields);
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
            dataCatalogoList: colaborador.dataCatalogoList,
            idCatalogoSolicitud: colaborador.idCatalogoSolicitud
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
              idcatalogo: idComentarioSolicitud,
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
    /**
     * Se posterga la actualización del indicador de carga hasta que se haya "renderizado" la vista
     */
    terminaCargaDatos() {
      this.$nextTick(() => {
        this.cargandoDatos = false;
      });
    },

    Close() {
      this.showModalConfirmacion = false;
    },

    async Eliminar(item) {
      this.catalogoSeleccionado = item;
      this.catalogoEliminar = this.catalogoSeleccionado.nombre;

      if (await this.ValidaDependenciasEliminar()) {
        const res = await this.Eliminar_Generico(
          this.tipocatalogonombreComputed,
          this.catalogoEliminar,
          this.generoTipoLista
        );

        const foundIndex = this.solicitud.dataCatalogoList.findIndex(
          x => x.id == item.id
        );
        if (res) {
          if (item.estado === "N") {
            this.solicitud.dataCatalogoList = this.solicitud.dataCatalogoList.filter(
              x => x.id !== item.id
            );
            this.solicitud.dataCatalogoList = this.addIdentityToArrayObjects(
              this.solicitud.dataCatalogoList
            );
            Vue.$toast.success("El catálogo fue eliminado exitosamente.", {});
          } else {
            this.solicitud.dataCatalogoList[foundIndex].estado = "E";

            Vue.$toast.success("El catálogo eliminado exitosamente.", {});
            this.solicitud.estado = "M";
          }
        }
      }
    },

    async ValidaDependenciasEliminar() {
      var id = this.catalogoSeleccionado.idcatalogo;

      let result = "";
      switch (this.solicitud.idCatalogoSolicitud) {
        case "1":
          result = await ValidaAreaOrganizativaDependencias({
            id
          });
          break;

        case "2":
          result = await ValidaAmbienteDependencias({
            id
          });
          break;

        case "3":
          result = await ValidaBaseDatosDependencias({
            id
          });

          break;

        case "4":
          result = await ValidaBusDependencias({
            id
          });
          break;

        case "5":
          result = await ValidaCentroEstudioDependencias({
            id
          });
          break;

        case "6":
          result = await ValidaCertificacionDependencias({
            id
          });
          break;

        case "7":
          result = await ValidaEmpresaDependencias({
            id
          });
          break;

        case "8":
          result = await ValidaERPDependencias({
            id
          });
          break;

        case "9":
          result = await ValidaCalidadDependencias({
            id
          });
          break;

        case "10":
          result = await ValidaIdiomaDependencias({
            id
          });
          break;

        case "11":
          result = await ValidaInfraestructuraDependencias({
            id
          });
          break;

        case "12":
          result = await ValidaLenguajeDependencias({
            id
          });
          break;

        case "13":
          result = await ValidaBPMDependencias({
            id
          });
          break;

        case "14":
          result = await ValidaProveedorDependencias({
            id
          });
          break;

        case "15":
          result = await ValidaPuestoDependencias({
            id
          });
          break;

        case "16":
          result = await ValidaSectorDependencias({
            id
          });
          break;

        case "17":
          result = await ValidaTecnologiaDependencias({
            id
          });
          break;

        case "18":
          result = await ValidaTipoCoreDependencias({
            id
          });
          break;
        case "19":
          result = await ValidaEspecialidadDependencias({
            id
          });
          break;
        case "20":
          result = await ValidaCloudDependencias({
            id
          });
          break;

        default:
          break;
      }

      if (result.data != "") {
        Vue.$toast.error(result.data, {});
        return false;
      } else {
        return true;
      }
    },

    async Excluir(item) {
      this.catalogoSeleccionado = item;

      this.catalogoEliminar = this.catalogoSeleccionado.nombre;

      const res = await this.Excluir_Generico(
        this.tipocatalogonombreComputed,
        this.catalogoEliminar,
        this.generoTipoLista
      );

      if (res) {
        this.solicitud.dataCatalogoList = this.solicitud.dataCatalogoList.filter(
          x => x.id !== item.id
        );

        this.solicitud.dataCatalogoList = this.addIdentityToArrayObjects(
          this.solicitud.dataCatalogoList
        );

        Vue.$toast.success("El catálogo fue excluido exitosamente.", {});
      }
    },
    Undo(item) {
      const foundIndex = this.solicitud.dataCatalogoList.findIndex(
        x => x.id == item.id
      );

      if (
        this.solicitud.dataCatalogoList[foundIndex].hashCode ==
        this.generaHashCode(
          this.solicitud.idCatalogoSolicitud,
          this.catalogoSeleccionado
        )
      ) {
        this.solicitud.dataCatalogoList[foundIndex].estado = "S";
      } else {
        this.solicitud.dataCatalogoList[foundIndex].estado = "M";
      }
    },

    generaHashCode(idCatalogoSolicitud, data) {
      let result = "";
      switch (idCatalogoSolicitud) {
        case "8":
          result = this.hashCode(
            data.nombre + data.descripcion + data.esopensource + data.tecnologia
          );
          break;
        case "6":
          result = this.hashCode(
            data.nombre + data.descripcion + JSON.stringify(data.proveedores)
            // JSON.stringify(
            //   data.proveedores.map(obj => ({
            //     idproveedorcertificacion: obj.idproveedorcertificacion.toString(),
            //   }))
            // )
          );
          break;
        case "14":
          result = this.hashCode(
            data.nombre +
              data.descripcion +
              JSON.stringify(data.certificaciones)
          );
          break;

        default:
          result = this.hashCode(data.nombre + data.descripcion);
          break;
      }

      return result;
    },

    Agregar() {
      if (
        this.solicitud.idCatalogoSolicitud == null ||
        this.solicitud.idCatalogoSolicitud == "0"
      ) {
        Vue.$toast.warning("Debe de seleccionar un tipo de catálogo", {});
      } else {
        this.tituloModalEdicion = "Agregar " + this.tipocatalogonombreComputed;
        this.cleanForm();
        this.showModalAgregar = true;
        this.esEdicion = false;
        this.catalogoSeleccionado = null;
        this.catalogoSeleccionado = null;
      }
    },

    async Editar(item) {
      this.tituloModalEdicion = "Modificar " + this.tipocatalogonombreComputed;
      this.catalogoSeleccionado = item;
      this.showModalAgregar = true;
      this.esEdicion = true;
      this.cargarEditar(item);
    },

    cargarEditar(item) {
      switch (this.solicitud.idCatalogoSolicitud) {
        case "8":
          this.catalogoNuevoEditar = {
            id: item.id,
            idcatalogo: item.idcatalogo,
            nombre: item.nombre,
            descripcion: item.descripcion,
            estado: item.estado,
            idTipoCore: item.idtipocore,
            esopensource: item.esopensource,
            tecnologia: item.tecnologia
          };
          break;
        case "6":
          this.catalogoNuevoEditar = {
            id: item.id,
            idcatalogo: item.idcatalogo,
            nombre: item.nombre,
            descripcion: item.descripcion,
            estado: item.estado,
            proveedores: item.proveedores
          };
          break;
        case "14":
          this.catalogoNuevoEditar = {
            id: item.id,
            idcatalogo: item.idcatalogo,
            nombre: item.nombre,
            descripcion: item.descripcion,
            estado: item.estado,
            certificaciones: item.certificaciones
          };
          break;

        default:
          this.catalogoNuevoEditar = {
            id: item.id,
            idcatalogo: item.idcatalogo,
            nombre: item.nombre,
            descripcion: item.descripcion,
            estado: item.estado
          };
          break;
      }
    },

    async submit(formValues) {
      try {
        try {
          let resultadoValidacion = this.validacionesSolicitud();

          if (resultadoValidacion.esValido) {
            // actualiza la lista interna de cursos por colaborador, previamente validada
            if (this.actualizarLista()) {
              //  this.$formulate.reset("cursoNuevoEditar");
              this.showModalAgregar = false;
            }
          } else {
            Vue.$toast.warning(resultadoValidacion.mensaje, {});
          }
        } catch (error) {
          console.log(error);
        }
      } catch (error) {
        console.log(error);
      }
    },

    actualizarLista() {
      try {
        if (this.esEdicion) {
          return this.editarListaCatalogos();
        } else {
          return this.agregarListaCatalogos();
        }
      } catch (error) {
        console.log(error);
        return false;
      }
    },
    editarListaCatalogos() {
      const foundIndex = this.solicitud.dataCatalogoList.findIndex(
        x => x.id == this.catalogoNuevoEditar.id
      );

      let cambios = true;

      if (
        this.solicitud.dataCatalogoList[foundIndex].hashCode ==
        this.generaHashCode(
          this.solicitud.idCatalogoSolicitud,
          this.catalogoNuevoEditar
        )
      ) {
        cambios = false;
        Vue.$toast.warning("No se realizaron cambios.", {});
      }

      if (this.solicitud.dataCatalogoList[foundIndex].estado != "N") {
        if (cambios) {
          if (this.aplicaCambiosEditar(foundIndex)) {
            this.solicitud.dataCatalogoList[foundIndex].estado = "M";
            this.solicitud.estado = "M";
          }
        } else {
          this.solicitud.dataCatalogoList[foundIndex].estado = "S";
        }
      } else {
        this.aplicaCambiosEditar(foundIndex);
      }
      return true;
    },
    aplicaCambiosEditar(foundIndex) {
      switch (this.solicitud.idCatalogoSolicitud) {
        case "8":
          this.solicitud.dataCatalogoList[
            foundIndex
          ].nombre = this.catalogoNuevoEditar.nombre;

          this.solicitud.dataCatalogoList[
            foundIndex
          ].descripcion = this.catalogoNuevoEditar.descripcion;

          this.solicitud.dataCatalogoList[
            foundIndex
          ].esopensource = this.catalogoNuevoEditar.esopensource;

          this.solicitud.dataCatalogoList[
            foundIndex
          ].tecnologia = this.catalogoNuevoEditar.tecnologia;
          break;
        case "6":
          if (
            this.validaDuplicadosId(
              this.catalogoNuevoEditar.proveedores,
              "idproveedorcertificacion"
            )
          ) {
            Vue.$toast.warning("Existen proveedores duplicados.", {});
            return false;
          } else {
            this.solicitud.dataCatalogoList[
              foundIndex
            ].nombre = this.catalogoNuevoEditar.nombre;

            this.solicitud.dataCatalogoList[
              foundIndex
            ].descripcion = this.catalogoNuevoEditar.descripcion;

            this.solicitud.dataCatalogoList[
              foundIndex
            ].proveedores = this.catalogoNuevoEditar.proveedores;
          }

          break;
        case "14":
          if (
            this.validaDuplicadosId(
              this.catalogoNuevoEditar.certificaciones,
              "idcertificacion"
            )
          ) {
            Vue.$toast.warning("Existen certificaciones duplicadas.", {});
            return false;
          } else {
            this.solicitud.dataCatalogoList[
              foundIndex
            ].nombre = this.catalogoNuevoEditar.nombre;

            this.solicitud.dataCatalogoList[
              foundIndex
            ].descripcion = this.catalogoNuevoEditar.descripcion;

            this.solicitud.dataCatalogoList[
              foundIndex
            ].certificaciones = this.catalogoNuevoEditar.certificaciones;
          }
          break;

        default:
          this.solicitud.dataCatalogoList[
            foundIndex
          ].nombre = this.catalogoNuevoEditar.nombre;

          this.solicitud.dataCatalogoList[
            foundIndex
          ].descripcion = this.catalogoNuevoEditar.descripcion;
          break;
      }

      return true;
    },

    agregarListaCatalogos() {
      try {
        if (this.agregarNuevo()) {
          this.solicitud.dataCatalogoList = this.addIdentityToArrayObjects(
            this.solicitud.dataCatalogoList
          );
          this.solicitud.estado = "M";
          return true;
        }
        return false;
      } catch (error) {
        console.log(error);
        return false;
      }
    },

    agregarNuevo() {
      switch (this.solicitud.idCatalogoSolicitud) {
        case "8":
          this.solicitud.dataCatalogoList.push({
            // idcolaborador: this.colaborador.idcolaborador,
            nombre: this.catalogoNuevoEditar.nombre,
            descripcion: this.catalogoNuevoEditar.descripcion,
            estado: "N",
            idtipocore: this.catalogoNuevoEditar.idTipoCore,
            esopensource: this.catalogoNuevoEditar.esopensource,
            tecnologia: this.catalogoNuevoEditar.tecnologia
          });
          break;
        case "6":
          if (
            this.validaDuplicadosId(
              this.catalogoNuevoEditar.proveedores,
              "idproveedorcertificacion"
            )
          ) {
            Vue.$toast.warning("Existen proveedores duplicados.", {});
            return false;
          } else {
            this.solicitud.dataCatalogoList.push({
              // idcolaborador: this.colaborador.idcolaborador,
              nombre: this.catalogoNuevoEditar.nombre,
              descripcion: this.catalogoNuevoEditar.descripcion,
              estado: "N",
              proveedores: this.catalogoNuevoEditar.proveedores
            });
          }
          break;
        case "14":
          if (
            this.validaDuplicadosId(
              this.catalogoNuevoEditar.certificaciones,
              "idcertificacion"
            )
          ) {
            Vue.$toast.warning("Existen proveedores duplicados.", {});
            return false;
          } else {
            this.solicitud.dataCatalogoList.push({
              nombre: this.catalogoNuevoEditar.nombre,
              descripcion: this.catalogoNuevoEditar.descripcion,
              estado: "N",
              certificaciones: this.catalogoNuevoEditar.certificaciones
            });
          }
          break;

        default:
          this.solicitud.dataCatalogoList.push({
            // idcolaborador: this.colaborador.idcolaborador,
            nombre: this.catalogoNuevoEditar.nombre,
            descripcion: this.catalogoNuevoEditar.descripcion,
            estado: "N"
          });
          break;
      }
      return true;
    },

    async selected(item) {
      switch (this.TipoListaPick) {
        case "Colaboradores":
          this.showModal = false;

          await this.cargarDetalleColaborador(item.id);

          break;

        case "Catalogos":
          this.showModal = false;
          this.cargarCatalogoPickList(item);
          break;

        default:
          break;
      }
    },

    cargarCatalogoPickList(catalogo) {
      this.cargandoDatos = true;

      if (!this.existeCatalogo(catalogo)) {
        // tipo erp contemplar hash erp

        this.solicitud.dataCatalogoList.push({
          ...catalogo,
          estado: "S",
          hashCode: this.generaHashCode(
            this.solicitud.idCatalogoSolicitud,
            catalogo
          )
        });

        this.solicitud.dataCatalogoList = this.addIdentityToArrayObjects(
          this.solicitud.dataCatalogoList
        );

        this.solicitud.estado = "M";
        this.terminaCargaDatos();
      } else {
        Vue.$toast.warning("El catálogo seleccionado ya está en la lista.");
      }
    },
    existeCatalogo(item) {
      const list = this.solicitud.dataCatalogoList.filter(
        x => x.id === item.id
      );
      const list2 = this.solicitud.dataCatalogoList.filter(
        x => x.idcatalogo === item.idcatalogo
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
              foto
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

        case "Catalogos":
          if (!this.solicitud.idCatalogoSolicitud) {
            //    this.solicitud.dataCatalogoList = [];
            Vue.$toast.warning("Debe de seleccionar un tipo de catálogo.");
          } else {
            let data = await this.displayListaCatalogo(
              this.solicitud.idCatalogoSolicitud
            );

            if (data.length == 0) {
              this.solicitud.dataCatalogoList = [];
              Vue.$toast.warning("No existen catálogos ingresados.");
            } else {
              this.dataPickList = data;
            }

            this.columnasPickList = [
              { field: "id", header: "Id" },
              { field: "nombre", header: "Nombre" }
            ];

            this.showModal = true;
          }
          break;

        default:
          break;
      }
    },

    async displayListaCatalogo(tipo) {
      this.generoTipoLista = "M";
      this.tipoCatalogo = tipo;

      let data = [];

      switch (tipo) {
        case "1":
          const {
            data: { areaOrganizativas }
          } = await GetAreasOrganizativas();
          data = areaOrganizativas.map(
            ({ idareaorganizativa, nombre, descripcion }) => ({
              id: idareaorganizativa,
              idcatalogo: idareaorganizativa,
              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion
            })
          );

          this.nombreTipoLista = "Área organizativa";
          this.tituloModal = "Lista de áreas organizativas";

          break;
        case "2":
          const {
            data: { ambientesDesarrollo }
          } = await GetAmbienteDesarrollo();
          data = ambientesDesarrollo.map(
            ({ idambientedesarrollo, nombre, descripcion }) => ({
              id: idambientedesarrollo,
              idcatalogo: idambientedesarrollo,
              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion
            })
          );

          this.nombreTipoLista = "Ambiente de desarrollo";
          this.tituloModal = "Lista de ambientes de desarrollo";

          break;
        case "3":
          const {
            data: { basesDatos }
          } = await GetBaseDatos();
          data = basesDatos.map(({ idbasedatos, nombre, descripcion }) => ({
            id: idbasedatos,
            idcatalogo: idbasedatos,
            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));
          this.generoTipoLista = "F";
          this.nombreTipoLista = "Base de datos";
          this.tituloModal = "Lista de bases de datos";

          break;
        case "4":
          const {
            data: { buses }
          } = await GetBus();
          data = buses.map(({ idBus, nombre, descripcion }) => ({
            id: idBus,
            idcatalogo: idBus,
            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));

          this.nombreTipoLista = "Bus y SOA";
          this.tituloModal = "Lista de Buses y SOA";

          break;
        case "5":
          const {
            data: { centrosEstudios }
          } = await GetCentrosEstudio();
          data = centrosEstudios.map(
            ({ idCentroEstudio, nombre, descripcion }) => ({
              id: idCentroEstudio,
              idcatalogo: idCentroEstudio,

              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion
            })
          );

          this.nombreTipoLista = "Centro de estudio";
          this.tituloModal = "Lista de centros de estudio";

          break;
        case "6":
          const {
            data: { certificacionesProveedores }
          } = await GetCertificacionProveedores();
          data = certificacionesProveedores.map(
            ({ idcertificacion, nombre, descripcion, proveedoresList }) => ({
              id: idcertificacion,
              idcatalogo: idcertificacion,
              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion,
              proveedores:
                proveedoresList == null
                  ? []
                  : proveedoresList.map(obj2 => ({
                      estado: "S",
                      idproveedorcertificacion: obj2.idproveedorcertificacion.toString()
                    }))
            })
          );

          this.generoTipoLista = "F";
          this.nombreTipoLista = "Certificación";
          this.tituloModal = "Lista de certificaciones";

          break;
        case "7":
          const {
            data: { empresaCorporacions }
          } = await GetEmpresaCorporacion();
          data = empresaCorporacions.map(
            ({ idempresacorporacion, nombre, descripcion }) => ({
              id: idempresacorporacion,
              idcatalogo: idempresacorporacion,
              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion
            })
          );
          this.generoTipoLista = "F";
          this.nombreTipoLista = "Empresa corporación";
          this.tituloModal = "Lista de empresas";

          break;
        case "8":
          const {
            data: { erpCores }
          } = await GetErpCore();
          data = erpCores.map(
            ({
              iderpcore,
              idtipocore,
              nombre,
              descripcion,
              esopensource,
              tecnologia
            }) => ({
              id: iderpcore,
              idcatalogo: iderpcore,
              idtipocore: idtipocore,
              nombre: nombre,
              esopensource: esopensource == true ? "Si" : "No",
              tecnologia: tecnologia,
              descripcion: descripcion == null ? "" : descripcion
            })
          );

          this.nombreTipoLista = "ERP y Core";
          this.tituloModal = "Lista de ERPs y Core";

          break;
        case "9":
          const {
            data: { calidads }
          } = await GetCalidad();
          data = calidads.map(({ idcalidad, nombre, descripcion }) => ({
            id: idcalidad,
            idcatalogo: idcalidad,
            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));
          this.generoTipoLista = "F";
          this.nombreTipoLista = "Tecnología de calidad";
          this.tituloModal = "Lista de Tecnologías de calidad";

          break;
        case "10":
          const {
            data: { idiomas }
          } = await GetIdiomas();
          data = idiomas.map(({ idIdioma, nombre, descripcion }) => ({
            id: idIdioma,
            idcatalogo: idIdioma,
            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));

          this.nombreTipoLista = "Idioma";
          this.tituloModal = "Lista de idiomas";

          break;
        case "11":
          const {
            data: { infraestructuras }
          } = await GetInfraestructura();
          data = infraestructuras.map(
            ({ idinfraestructura, nombre, descripcion }) => ({
              id: idinfraestructura,
              idcatalogo: idinfraestructura,
              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion
            })
          );
          this.generoTipoLista = "F";
          this.nombreTipoLista = "Infraestructura";
          this.tituloModal = "Lista de infraestructuras";

          break;
        case "12":
          const {
            data: { lenguajesProgramacion }
          } = await GetLenguajeProgramacion();
          data = lenguajesProgramacion.map(
            ({ idlenguajeprogramacion, nombre, descripcion }) => ({
              id: idlenguajeprogramacion,
              idcatalogo: idlenguajeprogramacion,
              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion
            })
          );

          this.nombreTipoLista = "Lenguaje de programación";
          this.tituloModal = "Lista de lenguajes de programación";

          break;
        case "13":
          const {
            data: { bpMs }
          } = await GetBPMS();
          data = bpMs.map(({ idBPM, nombre, descripcion }) => ({
            id: idBPM,
            idcatalogo: idBPM,
            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));
          this.generoTipoLista = "F";
          this.nombreTipoLista = "BPM y herramienta de ofimática";
          this.tituloModal = "Lista de BPM y herramientas de ofimática";

          break;
        case "14":
          const {
            data: { proveedorCertificaciones }
          } = await GetProveedorCertificacionesLista();
          data = proveedorCertificaciones.map(
            ({
              idProveedorCertificacion,
              nombre,
              descripcion,
              certificacionesList
            }) => ({
              id: idProveedorCertificacion,
              idcatalogo: idProveedorCertificacion,
              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion,
              certificaciones:
                certificacionesList == null
                  ? []
                  : certificacionesList.map(obj2 => ({
                      estado: "S",
                      idcertificacion: obj2.idcertificacion.toString()
                    }))
            })
          );

          this.nombreTipoLista = "Proveedor certificación";
          this.tituloModal = "Lista de proveedores de certificación";

          break;
        case "15":
          const {
            data: { puesto }
          } = await GetPuesto();
          data = puesto.map(({ idPuesto, nombre, descripcion }) => ({
            id: idPuesto,
            idcatalogo: idPuesto,
            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));

          this.nombreTipoLista = "Puesto";
          this.tituloModal = "Lista de puestos";

          break;
        case "16":
          const {
            data: { sectores }
          } = await GetSectores();
          data = sectores.map(({ idSector, nombre, descripcion }) => ({
            id: idSector,
            idcatalogo: idSector,

            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));

          this.nombreTipoLista = "Sector";
          this.tituloModal = "Lista de sectores";

          break;
        case "17":
          const {
            data: { tecnologias }
          } = await GetTecnologia();
          data = tecnologias.map(({ idtecnologia, nombre, descripcion }) => ({
            id: idtecnologia,
            idcatalogo: idtecnologia,

            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));
          this.generoTipoLista = "F";
          this.nombreTipoLista = "Tecnología";
          this.tituloModal = "Lista de tecnologías";

          break;
        case "18":
          const {
            data: { tiposCores }
          } = await GetTipoCore();
          data = tiposCores.map(({ idTipoCore, nombre, descripcion }) => ({
            id: idTipoCore,
            idcatalogo: idTipoCore,

            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));

          this.nombreTipoLista = "Tipo core";
          this.tituloModal = "Lista de tipos de core";

          break;
        case "19":
          const {
            data: { especialidades }
          } = await GetEspecialidad();
          data = especialidades.map(
            ({ idespecialidad, nombre, descripcion }) => ({
              id: idespecialidad,
              idcatalogo: idespecialidad,

              nombre: nombre,
              descripcion: descripcion == null ? "" : descripcion
            })
          );
          this.generoTipoLista = "F";
          this.nombreTipoLista = "Especialidad";
          this.tituloModal = "Lista de especialidades";

          break;
        case "20":
          const {
            data: { clouds }
          } = await GetCloud();
          data = clouds.map(({ idcloud, nombre, descripcion }) => ({
            id: idcloud,
            idcatalogo: idcloud,

            nombre: nombre,
            descripcion: descripcion == null ? "" : descripcion
          }));
          this.generoTipoLista = "F";
          this.nombreTipoLista = "Cloud";
          this.tituloModal = "Lista de clouds";

          break;

        default:
          data = [];
          Vue.$toast.warning("En construcción.");
          break;
      }
      return data;
    },

    clean() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
      this.filters = {};
    },
    cleanForm() {
      switch (this.solicitud.idCatalogoSolicitud) {
        case "8":
          this.catalogoNuevoEditar = {
            idcatalogo: null,
            nombre: null,
            descripcion: null,
            estado: "S",
            esopensource: "No",
            tecnologia: "",
            idTipoCore: null
          };
          break;
        case "6":
          this.catalogoNuevoEditar = {
            idcatalogo: null,
            nombre: null,
            descripcion: null,
            estado: "S",
            proveedores: []
          };
          break;
        case "14":
          this.catalogoNuevoEditar = {
            idcatalogo: null,
            nombre: null,
            descripcion: null,
            estado: "S",
            certificaciones: []
          };
          break;

        default:
          this.catalogoNuevoEditar = {
            idcatalogo: null,
            nombre: null,
            descripcion: null,
            estado: "S"
          };
          break;
      }
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
    listaCatalogo() {
      this.listaCatalogos = [
        //   { value: 0, label: "Seleccione un catálogo" },
        { value: 2, label: "Ambientes de desarrollo" },
        { value: 1, label: "Áreas organizativas" },
        { value: 3, label: "Bases de datos" },
        { value: 4, label: "Buses y SOA" },
        { value: 13, label: "BPM y herramientas de ofimática" },
        { value: 5, label: "Centros de estudio " },
        { value: 6, label: "Certificaciones" },
        { value: 20, label: "Cloud" },
        { value: 7, label: "Empresas de la corporación" },
        { value: 8, label: "ERPs y Core" },
        { value: 19, label: "Especialidades" },
        { value: 10, label: "Idiomas" },
        { value: 11, label: "Infraestructura" },
        { value: 12, label: "Lenguajes de programación" },
        { value: 14, label: "Proveedores de certificaciones" },
        { value: 15, label: "Puestos" },
        { value: 16, label: "Sectores" },
        { value: 9, label: "Tecnologías de calidad" },
        { value: 17, label: "Tecnologías " },
        { value: 18, label: "Tipos de core" }
      ];

      return this.listaCatalogos;
    },
    async cargarTipoCores() {
      const {
        data: { tiposCores }
      } = await GetTipoCore();
      this.tipoCores = tiposCores.map(({ idTipoCore, nombre }) => ({
        label: nombre,
        value: idTipoCore
      }));
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
          if (this.solicitud.dataCatalogoList.length == 0) {
            mensaje = "Por favor incluir al menos un registro a la lista.";
          } else {
            mensaje =
              "Por favor excluir los catálogos que no tienen modificaciones.";
          }

          Vue.$toast.warning(mensaje, {});
        } else {
          //   let resultadoValidacion = this.validacionesSolicitud();
          //   if (resultadoValidacion.esValido) {
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
          // } else {
          //   Vue.$toast.warning(
          //     "La solicitud presenta las siguientes inconsistencias: <br/>" +
          //       resultadoValidacion.mensaje,
          //     {}
          //   );
          // }
        }
      } catch (error) {
        console.log(error);
      }
    },
    validacionesSolicitud() {
      let resultado = { esValido: true };

      // if (
      //   !this.catalogoNuevoEditar.archivoTitulo ||
      //   this.catalogoNuevoEditar.archivoTitulo == ""
      // ) {
      //   resultado = {
      //     esValido: false,
      //     mensaje: "Debe agregar el certificado de la formación académica"
      //   };
      // }

      switch (this.solicitud.idCatalogoSolicitud) {
        case "8":
          break;
        case "6":
          break;
        case "14":
          break;

        default:
          break;
      }

      return resultado;
    },
    solicitudSinModificaciones() {
      const foundIndex = this.solicitud.dataCatalogoList.findIndex(
        x => x.estado == "S"
      );

      if (foundIndex >= 0 || this.solicitud.dataCatalogoList.length == 0) {
        return true;
      }

      return false;
    },
    validaTipoCatalogo(idCatalogoSolicitud, tipo) {
      let result = false;
      switch (tipo) {
        case "ERPCORE":
          result = idCatalogoSolicitud == "8";
          break;
        case "CERTIFICACIONES":
          result = idCatalogoSolicitud == "6";
          break;
        case "PROVEEDORCERTIFICACION":
          result = idCatalogoSolicitud == "14";
          break;

        default:
          break;
      }

      return result;
    },

    /**
     * Métodos para administrar la lista de proveedores en una solicitud de certificaciones
     */
    undoProveedor(index) {
      const foundIndex = this.solicitud.dataCatalogoList.findIndex(
        x => x.id == this.catalogoNuevoEditar.id
      );
      // if (
      //   this.solicitud.dataCatalogoList[foundIndex].hashCode ==
      //   this.generaHashCode(
      //     this.solicitud.idCatalogoSolicitud,
      //     this.catalogoNuevoEditar
      //   )
      // ) {
      //   this.solicitud.dataCatalogoList[foundIndex].proveedores[index].estado =
      //     "S";
      // } else {
      //   this.solicitud.dataCatalogoList[foundIndex].proveedores[index].estado =
      //     "M";
      // }
      this.solicitud.dataCatalogoList[foundIndex].proveedores[index].estado =
        "S";
    },
    agregarProveedor() {
      if (this.solicitud.dataCatalogoList.length > 0) {
        const foundIndex = this.solicitud.dataCatalogoList.findIndex(
          x => x.id == this.catalogoNuevoEditar.id
        );

        if (foundIndex > -1) {
          this.solicitud.dataCatalogoList[foundIndex].proveedores[
            this.solicitud.dataCatalogoList[foundIndex].proveedores.length - 1
          ].estado = "N";
        } else {
          this.catalogoNuevoEditar.proveedores[
            this.catalogoNuevoEditar.proveedores.length - 1
          ].estado = "N";
        }
      } else {
        this.catalogoNuevoEditar.proveedores[
          this.catalogoNuevoEditar.proveedores.length - 1
        ].estado = "N";
      }
    },
    modificarProveedor(index) {
      if (this.solicitud.dataCatalogoList.length > 0) {
        const foundIndex = this.solicitud.dataCatalogoList.findIndex(
          x => x.id == this.catalogoNuevoEditar.id
        );
        if (foundIndex > -1) {
          this.solicitud.dataCatalogoList[foundIndex].proveedores[
            index
          ].estado = "N";
        }
      }
    },
    eliminarProveedor(index) {
      const foundIndex = this.solicitud.dataCatalogoList.findIndex(
        x => x.id == this.catalogoNuevoEditar.id
      );

      // this.solicitud.dataCatalogoList[foundIndex].proveedores[index].estado =
      //   "E";

      if (
        this.solicitud.dataCatalogoList[foundIndex].proveedores[index]
          .estado === "N"
      ) {
        this.solicitud.dataCatalogoList[
          foundIndex
        ].proveedores = this.solicitud.dataCatalogoList[
          foundIndex
        ].proveedores.filter(
          x =>
            x.idproveedorcertificacion !==
            this.solicitud.dataCatalogoList[foundIndex].proveedores[index]
              .idproveedorcertificacion
        );

        Vue.$toast.success("El proveedor fue eliminado exitosamente.", {});
      } else {
        this.solicitud.dataCatalogoList[foundIndex].proveedores[index].estado =
          "E";

        Vue.$toast.success("El proveedor eliminado exitosamente.", {});
        this.solicitud.estado = "M";
      }
    },

    /**
     * Métodos para administrar la lista de certificaciones en una solicitud de proveedores de certificacion
     */
    undoCertificacion(index) {
      const foundIndex = this.solicitud.dataCatalogoList.findIndex(
        x => x.id == this.catalogoNuevoEditar.id
      );

      this.solicitud.dataCatalogoList[foundIndex].certificaciones[
        index
      ].estado = "S";
    },
    agregarCertificacion() {
      if (this.solicitud.dataCatalogoList.length > 0) {
        const foundIndex = this.solicitud.dataCatalogoList.findIndex(
          x => x.id == this.catalogoNuevoEditar.id
        );

        if (foundIndex > -1) {
          this.solicitud.dataCatalogoList[foundIndex].certificaciones[
            this.solicitud.dataCatalogoList[foundIndex].certificaciones.length -
              1
          ].estado = "N";
        } else {
          this.catalogoNuevoEditar.certificaciones[
            this.catalogoNuevoEditar.certificaciones.length - 1
          ].estado = "N";
        }
      } else {
        this.catalogoNuevoEditar.certificaciones[
          this.catalogoNuevoEditar.certificaciones.length - 1
        ].estado = "N";
      }
    },
    modificarCertificacion(index) {
      if (this.solicitud.dataCatalogoList.length > 0) {
        const foundIndex = this.solicitud.dataCatalogoList.findIndex(
          x => x.id == this.catalogoNuevoEditar.id
        );
        if (foundIndex > -1) {
          this.solicitud.dataCatalogoList[foundIndex].certificaciones[
            index
          ].estado = "N";
        }
      }
    },
    eliminarCertificacion(index) {
      const foundIndex = this.solicitud.dataCatalogoList.findIndex(
        x => x.id == this.catalogoNuevoEditar.id
      );

      if (
        this.solicitud.dataCatalogoList[foundIndex].certificaciones[index]
          .estado === "N"
      ) {
        this.solicitud.dataCatalogoList[
          foundIndex
        ].certificaciones = this.solicitud.dataCatalogoList[
          foundIndex
        ].certificaciones.filter(
          x =>
            x.idcertificacion !==
            this.solicitud.dataCatalogoList[foundIndex].certificaciones[index]
              .idcertificacion
        );

        Vue.$toast.success("La certificación fue eliminada exitosamente.", {});
      } else {
        this.solicitud.dataCatalogoList[foundIndex].certificaciones[
          index
        ].estado = "E";

        Vue.$toast.success("La certificación fue eliminada exitosamente.", {});

        this.solicitud.estado = "M";
      }
    },

    async cargarProveedores() {
      const {
        data: { proveedorCertificaciones }
      } = await GetProveedores();
      this.proveedoresList = proveedorCertificaciones.map(
        ({ idProveedorCertificacion, nombre }) => ({
          label: nombre,
          value: idProveedorCertificacion
        })
      );
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

    validaDuplicadosId(dataList, IdName) {
      return _.uniqBy(dataList, IdName).length !== dataList.length;
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
    catalogoAEliminar() {
      return this.catalogoEliminar;
    },
    puedePintar() {
      return this.habilitarControlesSolicitud(
        this.datosSolicitud.estadoSolicitud,
        this.origen
      );
    },
    pickPuedeBuscarColaborador() {
      return this.datosSolicitud.estadoSolicitud == common.REGISTRADA;
    },
    eliminada() {
      return this.catalogoNuevoEditar.estado === "E";
    },
    tituloModalEdicionComputed() {
      let res = this.tituloModalEdicion;

      if (this.catalogoNuevoEditar.estado === "E") {
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
    descripcionComputed() {
      if (this.catalogoNuevoEditar) {
        if (this.catalogoNuevoEditar.descripcion) {
          return this.catalogoNuevoEditar.descripcion.length;
        } else {
          return 0;
        }
      } else {
        return 0;
      }
    },
    tipocatalogonombreComputed() {
      switch (this.solicitud.idCatalogoSolicitud) {
        case "1":
          this.tipocatalogonombre = "Área organizativa";
          break;
        case "2":
          this.tipocatalogonombre = "Ambiente de desarrollo";
          break;
        case "3":
          this.tipocatalogonombre = "Base de datos";
          break;
        case "4":
          this.tipocatalogonombre = "Buses y SOA";
          break;
        case "5":
          this.tipocatalogonombre = "Centro de estudio";
          break;
        case "6":
          this.tipocatalogonombre = "Certificación";
          break;
        case "7":
          this.tipocatalogonombre = "Empresa corporación";
          break;
        case "8":
          this.tipocatalogonombre = "ERPs y Core";
          break;
        case "9":
          this.tipocatalogonombre = "Tecnología de calidad";
          break;
        case "10":
          this.tipocatalogonombre = "Idioma";
          break;
        case "11":
          this.tipocatalogonombre = "Infraestructura";
          break;
        case "12":
          this.tipocatalogonombre = "Lenguaje de programación";
          break;
        case "13":
          this.tipocatalogonombre = "BPM y herramienta de ofimática";
          break;
        case "14":
          this.tipocatalogonombre = "Proveedor de certificación";
          break;
        case "15":
          this.tipocatalogonombre = "Puesto";
          break;
        case "16":
          this.tipocatalogonombre = "Sector";
          break;
        case "17":
          this.tipocatalogonombre = "Tecnología";
          break;
        case "18":
          this.tipocatalogonombre = "Tipo core";
          break;
        case "19":
          this.tipocatalogonombre = "Especialidad";
        case "20":
          this.tipocatalogonombre = "Cloud";
          break;

        default:
          this.tipocatalogonombre = "Catálogo";
          break;
      }

      return this.tipocatalogonombre.toLowerCase();
    },
    generoFemeninoTipoListaComputed() {
      if (
        this.solicitud.idCatalogoSolicitud == 1 ||
        this.solicitud.idCatalogoSolicitud == 3 ||
        this.solicitud.idCatalogoSolicitud == 6 ||
        this.solicitud.idCatalogoSolicitud == 7 ||
        this.solicitud.idCatalogoSolicitud == 9 ||
        this.solicitud.idCatalogoSolicitud == 11 ||
        this.solicitud.idCatalogoSolicitud == 13 ||
        this.solicitud.idCatalogoSolicitud == 17 ||
        this.solicitud.idCatalogoSolicitud == 19
      ) {
        return true;
      } else {
        return false;
      }
    },
    proveedoresCertificacionesComputed() {
      if (this.catalogoSeleccionado) {
        return this.catalogoSeleccionado.proveedores;
      } else {
        return [];
      }
    },
    certificacionesProveedorComputed() {
      if (this.catalogoSeleccionado) {
        return this.catalogoSeleccionado.certificaciones;
      } else {
        return [];
      }
    },
    idCatalogoComputed() {
      if (this.catalogoSeleccionado) {
        return this.catalogoSeleccionado.idcatalogo;
      } else {
        return 0;
      }
    }
  },
  watch: {
    "solicitud.idCatalogoSolicitud": async function(newValue, oldValue) {
      if (
        !this.cargandoDatos &&
        oldValue != null &&
        this.solicitud.dataCatalogoList.length > 0
      ) {
        const res = await this.Confirmacion_Generico(
          "Los cambios registrados al catálogo de " +
            this.tipocatalogonombreComputed +
            " se perderán. Desea cambiar de tipo de catálogo?"
        );

        if (res) {
          this.cleanForm();
          this.filters = {};
          this.catalogoSeleccionado = null;
          this.solicitud.dataCatalogoList = [];
          this.solicitud.estado = "S";
          this.solicitud.idCatalogoSolicitud = newValue;
        } else {
          this.cargandoDatos = true;
          this.solicitud.idCatalogoSolicitud = oldValue;
          this.terminaCargaDatos();
        }
      }
    }
  }
};
</script>

<style>
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
.p-datatable-catalogo .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-catalogo .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-catalogo .p-datatable-tbody > tr > td {
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
  .p-datatable.p-datatable-catalogo .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-catalogo .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-catalogo .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-catalogo .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-catalogo:last-child {
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
