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
      <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
        <CIcon name="cil-justify-center" /><strong> Proyectos</strong>
        <small> Solicitud de cambio </small>
        <CBadge color="success" class="mr-2" shape="pill">
          <!-- {{
            item.idioma + "- Nivel:  " + item.nivelIdioma
          }} -->
          <span style="margin-top:.2rem; margin-bottom:.2rem">
            {{
              this.obtenerDescripcionEstado(this.datosSolicitud.estadoSolicitud)
            }}
          </span>
        </CBadge>
      </CCardHeader>
      <CCardBody>
        <FormulateForm v-model="solicitud" @submit="submit">
          <resumen-colaborador
            :identificacion="this.solicitud.identificacion"
            :nombre="this.solicitud.nombreColaborador"
            :primerapellido="this.solicitud.primerapellido"
            :segundoapellido="this.solicitud.segundoapellido"
            :foto="this.solicitud.foto"
            @displayModal="displayModal"
            :editable="false"
            :searchable="false"
            :visible="false"
          >
          </resumen-colaborador>

          <CRow class="mt-1">
            <CCol md="1" sm="12" class="mb-2">
              <CButton
                class="mt-1 btn btn-info mt-1 pull-left libro-marca-celeste onHoverDark w-100"
                @click="Agregar()"
                v-if="this.puedePintar && puedePintarAdd"
              >
                <li class="pi pi-plus"></li>
              </CButton>
            </CCol>

            <CCol md="11" sm="12" class="mb-2">
              <div class="table-header ">
                <span class="p-input-icon-left pull-right">
                  <i class="pi pi-search" />
                  <InputText v-model="filters['global']" class="w-100" />
                </span>
              </div>
            </CCol>
          </CRow>
 
          <CRow>
            <CCol lg="12">
              <DataTable
                ref="dt"
                :value="solicitud.proyectosList"
                :paginator="true"
                class="p-datatable-proyecto p-datatable-striped p-datatable-sm"
                :rows="5"
                dataKey="idproyecto"
                :selection.sync="proyectoSeleccionado"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} de registros"
                sortField="nombre"
                :sortOrder="1"
                stateKey="dt-state-proyecto"
                selectionMode="single"
                :autoLayout="true"
                @row-select="onClick()"
              >
                <template #header> </template>
                <template #empty>
                  No se encontraron registros.
                </template>
                <template #loading>
                  Cargando la información de selección dinámica. Por favor
                  espere.
                </template>

                <Column
                  header="Proyecto"
                  :sortable="true"
                  sortField="nombre"
                  filterField="nombre"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Proyecto</span>
                    {{ slotProps.data.nombre }}
                  </template>
                </Column>

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

                <Column
                  header="Acciones"
                  :sortable="false"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <CDropdown
                      color="link"
                      size="sm"
                      :caret="false"
                      style="margin-top:-7px"
                      placement="left"
                    >
                      <template #toggler-content>
                        <i class="pi pi-ellipsis-v"></i>
                      </template>

                      <CDropdownItem @click="Editar(slotProps.data)">
                        <li class="pi pi-user-edit" />
                        &nbsp; Modificar
                      </CDropdownItem>
                      <CDropdownItem @click="Eliminar(slotProps.data)">
                        <li class="pi pi-trash" />
                        &nbsp; Eliminar
                      </CDropdownItem>
                      <CDropdownItem @click="Ver(slotProps.data)">
                        <li class="pi pi-eye" />
                        &nbsp; Ver
                      </CDropdownItem>
                    </CDropdown>

                    <div class="row btn-opciones">
                      <div class="col">
                        <CButton
                          block
                          color="primary"
                          @click="Editar(slotProps.data)"
                        >
                          <li class="pi pi-user-edit" />
                          &nbsp;Modificar
                        </CButton>
                      </div>
                    </div>
                    <div class="row btn-opciones">
                      <div class="col">
                        <CButton
                          block
                          color="primary"
                          @click="Eliminar(slotProps.data)"
                        >
                          <li class="pi pi-trash" />
                          &nbsp;Eliminar
                        </CButton>
                      </div>
                    </div>
                    <div class="row btn-opciones">
                      <div class="col">
                        <CButton
                          block
                          color="primary"
                          @click="Ver(slotProps.data)"
                        >
                          <li class="pi pi-eye" />
                          &nbsp;Ver
                        </CButton>
                      </div>
                    </div>
                  </template>
                </Column>
              </DataTable>
            </CCol>
          </CRow>

          <CModal
            :title="tituloModalEdicion"
            :show.sync="showModalAgregar"
            color="primary"
            class="modal-libro"
          >
            <div>
              <CRow>
                <CCol sm="6">
                  <FormulateInput
                    v-model="solicitud.idcliente"
                    :options="clientesList"
                    type="select"
                    label="Cliente"
                    placeholder="Seleccione un cliente"
                    validation="required|not:0"
                    validation-name="Cliente"
                    :validation-messages="{
                      required: 'Seleccione un cliente.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    @change="onChangeCliente()"
                    :disabled="!puedePintar"
                  />
                </CCol>

                <CCol sm="6">
                  <FormulateInput
                    v-model="solicitud.idempresa"
                    :options="empresaList"
                    type="select"
                    label="Empresa"
                    placeholder="Seleccione una empresa"
                    validation="required|not:0"
                    validation-name="Empresa"
                    :validation-messages="{
                      required: 'Seleccione una empresa.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    :disabled="!puedePintar"
                  />
                </CCol>

                <CCol sm="6">
                  <FormulateInput
                    v-model="solicitud.idsector"
                    :options="sectorList"
                    type="select"
                    label="Sector"
                    placeholder="Seleccione un sector"
                    validation="required|not:0"
                    validation-name="Sector"
                    :validation-messages="{
                      required: 'Seleccione un sector.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    :disabled="!puedePintar"
                  />
                </CCol>

                <CCol sm="6">
                  <FormulateInput
                    v-model="solicitud.idreferencia"
                    :options="referenciaList"
                    type="select"
                    label="Referencia"
                    placeholder="Seleccione una referencia"
                    validation="required|not:0"
                    validation-name="Sector"
                    :validation-messages="{
                      required: 'Seleccione una referencia.',
                      not: 'Debe de seleccionar una opción válida.'
                    }"
                    :input-class="['form-control']"
                    :disabled="!puedePintar"
                  />
                </CCol>

                <CCol sm="6">
                  <FormulateInput
                    type="date"
                    name="fechaInicio"
                    label="Fecha de inicio"
                    validation="required|after:1970-01-01|before:2200-01-01"
                    :validation-messages="{
                      after: 'La fecha de inicio debe ser luego de 01/01/1970.',
                      before: 'La fecha de inicio debe ser antes de 01/01/2200.'
                    }"
                    min="1970-01-01"
                    max="2200-01-01"
                    validation-name="Fecha de inicio"
                    input-class="form-control"
                    :help-class="['hint']"
                    :disabled="!puedePintar"
                  />
                </CCol>
                <CCol sm="6">
                  <FormulateInput
                    type="date"
                    name="fechaFinal"
                    label="Fecha de finalización"
                    validation="after:1970-01-01|before:2200-01-01"
                    :validation-messages="{
                      after:
                        'La fecha de finalización debe ser luego de 01/01/1970.',
                      before:
                        'La fecha de finalización debe ser antes de 01/01/2200.'
                    }"
                    min="1970-01-01"
                    max="2200-01-01"
                    validation-name="Fecha de finalización"
                    input-class="form-control"
                    :help-class="['hint']"
                    :disabled="!puedePintar"
                  />
                </CCol>

                <CCol sm="6">
                  <FormulateInput
                    name="nombreProyecto"
                    label="Nombre del proyecto"
                    validation="required|max:100,length"
                    input-class="form-control"
                    validation-name="Nombre del proyecto"
                    :readonly="!puedePintar"
                  />
                </CCol>
                <CCol sm="6">
                  <FormulateInput
                    name="cantidadhoras"
                    type="number"
                    label="Cantidad de horas"
                    validation="required|number"
                    min="0"
                    max="9999999"
                    input-class="form-control"
                    validation-name="Cantidad de horas"
                    :readonly="!puedePintar"
                  />
                </CCol>

                <CCol sm="6">
                  <FormulateInput
                    name="codproyecto"
                    label="Código del proyecto"
                    validation="required|max:10,length"
                    input-class="form-control"
                    validation-name="Código del proyecto"
                    :readonly="!puedePintar"
                  />
                </CCol>

                <CCol sm="6">
                  <FormulateInput
                    name="numerocontrato"
                    label="Número del contrato"
                    validation="required|max:20,length"
                    input-class="form-control"
                    validation-name="Número del contrato"
                    :readonly="!puedePintar"
                  />
                </CCol>

                <CCol sm="12">
                  <label>Activo</label>
                </CCol>
                <CCol sm="12">
                  <FormulateInput
                    type="myInputSwitch"
                    name="activo"
                    input-class="form-control"
                    validation-name="Activo"
                    class="hint"
                    :disabled="!puedePintar"
                  />
                </CCol>

                <CCol sm="12">
                  <FormulateInput
                    type="textarea"
                    :v-model="solicitud.descripcion"
                    name="descripcion"
                    label="Descripción"
                    validation="required|max:8000,length"
                    validation-name="Descripción"
                    :input-class="['form-control']"
                    :help="
                      `Tiene disponible un máximo de ${8000 -
                        solicitud.descripcion.length} carácteres.`
                    "
                    rows="3"
                    :help-class="['hint']"
                    :readonly="!puedePintar"
                  />
                </CCol>

                <CCol sm="12">
                  <FormulateInput
                    type="submit"
                    label="Almacenar"
                    :input-class="['btn', 'btn-info', 'pull-right']"
                    v-if="puedePintar"
                  />
                </CCol>
              </CRow>
            </div>
            <template #footer>
              <div></div>
            </template>
          </CModal>
        </FormulateForm>
      </CCardBody>
    </CCard>

    <CRow style="margin-bottom: 80px;">
      <CCol>
          <botonera-master-data
            :estadoSolicitud="this.datosSolicitud.estadoSolicitud"
            nombreEventoProcesar="procesarAccion"
            @procesarAccion="procesarAccion"
            v-if="muestraBotonera()"
          >
          </botonera-master-data>
      </CCol>
    </CRow> 


    <CModal
      title=""
      :show.sync="showModal"
      color="primary"
      class="modal-libro"
      style="modal-dialog modal-lg"
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
    </CModal>

    <CModal
      title="¿Está seguro que desea eliminar este registro?"
      :show.sync="showModalConfirmacion"
      color="primary"
      class="modal-libro"
      style="modal-dialog modal-lg"
    >
      <div>
        <CRow>
          <CCol sm="12" md="12" class="pull-right mb-3">
            {{ proyectoAEliminar }}
          </CCol>
        </CRow>
      </div>
      <template #footer>
        <div>
          <CRow>
            <CCol sm="12" md="12" class="pull-right">
              <CCol col="6" class="pull-right">
                <CButton
                  block
                  color="primary"
                  class="mr-1 pull-right btn btn-info libro-marca-azuloscuro"
                  @click="Close"
                >
                  Aceptar</CButton
                >
              </CCol>

              <CCol col="6" class="pull-right">
                <CButton
                  block
                  color="primary"
                  class="mr-1 pull-right btn btn-info libro-marca-celeste"
                  @click="Close"
                >
                  Cancelar</CButton
                >
              </CCol>
            </CCol>
          </CRow>
        </div>
      </template>
    </CModal>
  </div>
</template>

<script>
/**
 * Constantes para los estados de una solicitud
 */
const REGISTRADA = "RE";
const APROBADA = "AP";
const DENEGADA = "DE";
const DESCARTADA = "DC";
const ENAPROBACION = "EA";
const CORREGIR = "CO";
const ENVALIDACION = "EV";
const PENDIENTEACTUALIZACION = "PE";

import {
  GetAllColaboradores,
  GetClientes,
  GetProyectos,
  GetProyectoDetalle,
  GetEmpresaCorporacion,
  GetSectores,
  GetReferencias,
  GetReferenciasXCliente,
  GetColaboradorDetail,
  GetDetalleSolicitud
} from "./request";
import PickList from "../generales/Componentes/PickList";
import ResumenColaborador from "./ResumenColaborador";
import Vue from "vue";
import { aplicarFormatoFecha } from "../../utils";
import BotoneraMasterData from "./BotoneraMasterData.vue";

export default {
  name: "Proyectos",
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
    "botonera-master-data": BotoneraMasterData
  },
  data() {
    return {
      solicitud: {
        idcliente: null,
        idempresa: null,
        idsector: null,
        idreferencia: null,
        idColaborador: null,
        nombreColaborador: "",
        nombreProyecto: "",
        descripcion: "",
        fechaInicio: "",
        fechaFinal: "",
        codproyecto: "",
        cantidadhoras: null,
        numerocontrato: "",
        identificacion: null,
        primerapellido: "",
        segundoapellido: "",
        foto: null,
        activo: false,
        estado: "N",
        proyectosList: []
      },
      datosSolicitud: {
        idSolicitudMasterData: null,
        idTipoSolicitud: null,
        tipoSolicitud: null,
        estadoSolicitud: "RE"
      },
      showModal: false,
      nombreTipoLista: "",
      columnasPickList: [],
      dataPickList: [],
      filters: {},
      loading: false,
      proyectoSeleccionado: [],
      showModalAgregar: false,
      showModalConfirmacion: false,
      proyectoEliminar: "",
      tituloModalEdicion: "",
      TipoListaPick: "",
      clientesList: [],
      sectorList: [],
      empresaList: [],
      referenciaList: [],
      optionsActivo: ["Si", "No"],
      cargandoDatos: false,
      IdSolicitudJson: null // id de la solicitud para cargar el json de una solicitud
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    await this.cargarProyectos();
    await this.cargarClientes();
    await this.cargarEmpresas();
    await this.cargarSectores();

    this.IdSolicitudJson = this.idSolicitud;

    if (this.IdSolicitudJson) {
      this.cargarSolicitud();
    }

    //carga el usuario en session
    this.TipoListaPick = "Colaboradores";
    this.selected(this.$root.infoColaboradorActual);
  },
  methods: {
    
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
    },
    obtenerDescripcionEstado(codigoEstadoSolicitud) {
      switch (codigoEstadoSolicitud) {
        case REGISTRADA:
          return "Registrada";
        case APROBADA:
          return "Aprobada";
        case DENEGADA:
          return "Denegada";
        case DESCARTADA:
          return "Descartada";
        case ENAPROBACION:
          return "En aprobación";
        case CORREGIR:
          return "En correcciones";
        case ENVALIDACION:
          return "En validación";
        case PENDIENTEACTUALIZACION:
          return "Pendiente de actualización";
        default:
          break;
      }
    },
    atras() {
      switch (this.$route.params.origen) {
        case "Inbox":
          this.$router.push({
            name: "Mi Buzón"
          });
          return;
        case "Outbox":
          this.$router.push({ name: "Tramitadas" });
          return;
        default:
          return;
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
     * Invoca al API que obtiene los datos de una solicitud
     */
    async cargarSolicitud() {
      this.cargandoDatos = true;

      GetDetalleSolicitud(this.IdSolicitudJson)
        .then(response => {
          var solicitudJson = JSON.parse(
            response.data.solicitudMasterData.docSolicitud
          );
          var solicitud = response.data.solicitudMasterData;

          this.datosSolicitud.idSolicitudMasterData =
            solicitud.idSolicitudMasterData;
          this.datosSolicitud.idTipoSolicitud = solicitud.idTipoSolicitud;
          this.datosSolicitud.tipoSolicitud = solicitud.tipoSolicitud;
          this.datosSolicitud.estadoSolicitud = solicitud.estadoSolicitud;

          this.solicitud = {
            idempresa: solicitudJson.idempresa,
            idsector: solicitudJson.idsector,
            idreferencia: solicitudJson.idreferencia,
            nombreProyecto: solicitudJson.nombreProyecto,
            descripcion: solicitudJson.descripcion,
            fechaInicio: solicitudJson.fechaInicio,
            fechaFinal: solicitudJson.fechaFinal,
            codproyecto: solicitudJson.codproyecto,
            cantidadhoras: solicitudJson.cantidadhoras,
            numerocontrato: solicitudJson.numerocontrato,
            activo: solicitudJson.activo,
            proyectosList: solicitudJson.proyectosList,

            idcliente: solicitudJson.idcliente,
            idColaborador: solicitudJson.idcolaborador,
            identificacion: solicitudJson.identificacion,
            nombreColaborador: solicitudJson.nombre,
            primerapellido: solicitudJson.primerapellido,
            segundoapellido: solicitudJson.segundoapellido,
            foto: solicitudJson.foto,
            estado: solicitudJson.estado
          };

          // se posterga la actualización del indicador de carga hasta que se haya "renderizado" la vista
          this.$nextTick(() => {
            this.cargandoDatos = false;
          });
        })
        .catch(response => {
          this.cargandoDatos = false;
        });
    },
    async onChangeCliente() {
      const {
        data: { referencias }
      } = await GetReferenciasXCliente(this.solicitud.idcliente);
      this.referenciaList = referencias.map(({ idreferencia, nombre }) => ({
        label: nombre,
        value: idreferencia
      }));
    },
    async submit(formValues) {
      try {
        // const { data } = await Upsert(formValues);
        console.log(formValues);
      } catch (error) {
        console.log(error);
      }
    },
    Close() {
      this.showModalConfirmacion = false;
    },
    Eliminar(item) {
      this.proyectoSeleccionado = item;
      this.showModalConfirmacion = true;
      this.proyectoEliminar = this.proyectoSeleccionado.nombre;
    },
    Agregar() {
      this.tituloModalEdicion = "Agregar proyecto";
      this.cleanForm();
      this.showModalAgregar = true;
    },
    async Editar(item) {
      this.tituloModalEdicion = "Modificar proyecto";
      this.proyectoSeleccionado = item;
      this.showModalAgregar = true;
      await this.cargarProyectoDetalle(item.idproyecto);
    },
    async Ver(item) {
      this.tituloModalEdicion = "Ver proyecto";
      this.proyectoSeleccionado = item;
      this.showModalAgregar = true;
      await this.cargarProyectoDetalle(item.idproyecto);
    },
    async onClick() {
      //  await this.cargarFormacion(this.formacionSeleccionada.id);
    },
    selected(item) {
      switch (this.TipoListaPick) {
        case "Colaboradores":
          this.showModal = false;
          this.cargarDetalleColaborador(item.id);

          break;

        default:
          break;
      }
    },

    async cargarDetalleColaborador(id) {
      const {
        data: { colaborador }
      } = await GetColaboradorDetail(id);

      this.solicitud.idColaborador = colaborador.id;
      this.solicitud.identificacion = colaborador.identificacion;
      this.solicitud.nombreColaborador = colaborador.nombre;
      this.solicitud.primerapellido = colaborador.primerapellido;
      this.solicitud.segundoapellido = colaborador.segundoapellido;
    },
    async cargarProyectoDetalle(id) {
      const {
        data: { proyecto }
      } = await GetProyectoDetalle(id);

      this.solicitud.nombreProyecto = proyecto.nombre;
      this.solicitud.descripcion = proyecto.descripcion;
      this.solicitud.fechaInicio = aplicarFormatoFecha(
        proyecto.fechainicio,
        "yyyy-MM-dd"
      );
      this.solicitud.fechaFinal = aplicarFormatoFecha(
        proyecto.fechafinal,
        "yyyy-MM-dd"
      );
      this.solicitud.cantidadhoras = proyecto.cantidadhoras;
      this.solicitud.numerocontrato = proyecto.numerocontrato;
      this.solicitud.codproyecto = proyecto.codproyecto;
      this.solicitud.idcliente = proyecto.idcliente;
      this.solicitud.idsector = proyecto.idsector;
      this.solicitud.idempresa = proyecto.idempresa;
      this.solicitud.idreferencia = proyecto.idreferencia;
      this.solicitud.activo = proyecto.activo == 1 ? "Si" : "No";
    },
    async cargarProyectos() {
      this.filters = {};
      const {
        data: { proyectos }
      } = await GetProyectos();
      if (proyectos.length == 0) {
        this.solicitud.proyectosList = [];
        Vue.$toast.warning("No existen proyectos ingresados.");
      } else {
        this.solicitud.proyectosList = proyectos.map(
          ({
            idproyecto,
            idcliente,
            idsector,
            nombre,
            descripcion,
            activo
          }) => ({
            idproyecto: idproyecto,
            idcliente: idcliente,
            idsector: idsector,
            nombre: nombre,
            activo: activo,
            descripcion: descripcion == null ? "" : descripcion
          })
        );
      }
    },
    async cargarClientes() {
      const {
        data: { clientes }
      } = await GetClientes();
      this.clientesList = clientes.map(({ idcliente, nombre }) => ({
        label: nombre,
        value: idcliente
      }));
    },
    async cargarEmpresas() {
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
    async cargarSectores() {
      const {
        data: { sectores }
      } = await GetSectores();
      this.sectorList = sectores.map(({ idSector, nombre }) => ({
        label: nombre,
        value: idSector
      }));
    },
    async cargarReferencias() {
      const {
        data: { referencias }
      } = await GetReferencias();
      this.referenciaList = referencias.map(({ idreferencia, nombre }) => ({
        label: nombre,
        value: idreferencia
      }));
    },
    async displayModal(tipo) {
      this.TipoListaPick = tipo;
      this.clean();
      switch (tipo) {
        case "Colaboradores":
          // aqui estarian los request para cargar el picklist segun se requiera
          const {
            data: { colaboradores }
          } = await GetAllColaboradores();
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
          break;

        default:
          break;
      }
    },
    clean() {
      this.showModal = false;
      this.nombreTipoLista = "";
      this.dataPickList = null;
      this.filters = {};
    },
    cleanForm() {
      this.solicitud.nombreProyecto = "";
      this.solicitud.descripcion = "";
      this.solicitud.fechaInicio = "";
      this.solicitud.fechaFinal = "";
      this.solicitud.cantidadhoras = "";
      this.solicitud.numerocontrato = "";
      this.solicitud.codproyecto = "";
      this.solicitud.idcliente = null;
      this.solicitud.idsector = null;
      this.solicitud.idempresa = null;
      this.solicitud.idreferencia = null;
    },
    onUpload() {
      //this.$toast.add({severity: 'info', summary: 'Success', detail: 'File Uploaded', life: 3000});
    },
    procesarAccion(accion) {
      alert(accion);
    }
  },
  computed: {
    proyectoAEliminar() {
      return this.proyectoEliminar;
    },
    puedePintar() {
      let pinta = false;
      if (
        this.datosSolicitud.estadoSolicitud == REGISTRADA ||
        this.datosSolicitud.estadoSolicitud == CORREGIR
      ) {
        pinta = true;
      }

      return pinta;
    },
    puedePintarAdd() {
      let pinta = false;
      if (this.datosSolicitud.estadoSolicitud == REGISTRADA) {
        pinta = true;
      }

      return pinta;
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
.p-datatable-proyecto .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-proyecto .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-proyecto .p-datatable-tbody > tr > td {
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
    border: 1px solid rgba(175, 189, 216, 1)
}
.p-datatable table {
    width: 99%;
}


.btn-opciones {
  display: none;
}

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de
   etiqueta y valor asociado */
@media screen and (max-width: 40em) {
  .p-datatable.p-datatable-proyecto .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-proyecto .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-proyecto .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-proyecto .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-proyecto:last-child {
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
