<template>
  <div class="wrapper vld-parent" ref="formContainer">
    <div>
      <CCard style="border-radius: 16px !important; margin-top: 20px">
        <CCardHeader
          style="
            border-radius: 16px !important;
            padding-bottom: 0px;
            border-bottom: transparent !important;
            background-color: #ffffff;
          "
        >
          <strong>Colaboradores</strong>
        </CCardHeader>
        <CCardBody style="padding-top: 0px; padding-bottom: 0px">
          <p>Lista de colaboradores.</p>
        </CCardBody>
      </CCard>

      <CRow>
        <CCol md="6" sm="12" class="mb-2">
          <div class="table-header">
            <span class="p-input-icon-left w-75">
              <i class="pi pi-search" />
              <InputText
                v-model="filters['global']"
                placeholder="Escribe aquí lo que buscas"
                class="w-100"
              />
            </span>
          </div>
        </CCol>
        <CCol md="2" sm="4" class="pull-right mb-2" style="">
          <CButton
            color="primary"
            @click="clearFilters"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark w-100"
          >
            <i class="pi pi-trash"></i>
            Limpiar filtros</CButton
          >
        </CCol>
        <CCol md="2" sm="4" class="pull-right mb-2" style="">
          <CButton
            color="primary"
            @click="toggleFilter"
            ref="btnFilter"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark w-100"
          >
            <i class="pi pi-filter"></i>
            {{ btnFilter.text }}</CButton
          >
        </CCol>
        <CCol md="2" sm="4" class="pull-right mb-2" style="">
          <CButton
            color="primary"
            @click="showModal()"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark w-100"
          >
            <i class="pi pi-download"></i>
            Exportar</CButton
          >
        </CCol>
      </CRow>

      <CRow>
        <CCol col>
          <!-- <CCard style="border-radius: 16px !important; margin-top: 20px;">
            <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
              <div class="card-header-actions">
                <CButton
                  block
                  color="primary"
                  @click="agregar()"
                  class="mr-1"
                  style="display:block"
                >
                  <CIcon name="cil-plus" />&nbsp;Agregar</CButton
                >
              </div>
            </CCardHeader>
            <CCardBody> -->
          <CRow>
            <CCol lg="12">
              <DataTable
                ref="dt"
                :value="colaboradores"
                :paginator="true"
                class="p-datatable-colaboradores p-datatable-striped p-datatable-sm"
                :rows="10"
                dataKey="idcolaborador"
                :rowHover="true"
                :selection.sync="colaboradoresSeleccionados"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} colaboradores"
                sortField="nombreCompleto"
                :sortOrder="1"
                stateStorage="session"
                stateKey="dt-state-colaboradores"
                selectionMode="single"
                :autoLayout="true"
                @update:filters="filterApplied"
                @filter="filterApplied"
              >
                <template #header>
                  <CCollapse :show="collapse">
                    <CCard color="light">
                      <CCardBody
                        style="
                          padding-top: 15px !important;
                          padding-bottom: 0px !important;
                        "
                      >
                        <CRow>
                          <div
                            class="p-field p-col-12 p-md-5"
                            style="padding: 10px 5px 0px 5px !important"
                          >
                            <span class="p-float-label">
                              <MultiSelect
                                v-model="filters['idiomas']"
                                :options="idiomas"
                                optionLabel="nombre"
                                optionValue="valor"
                                class="p-column-filter w-100"
                                display="chip"
                                :filter="true"
                              >
                              </MultiSelect>
                              <label for="dropdown">Idiomas</label>
                            </span>
                          </div>
                          <div
                            class="p-field p-col-12 p-md-7"
                            style="padding: 10px 5px 0px 5px !important"
                          >
                            <span class="p-float-label">
                              <MultiSelect
                                v-model="filters['certificaciones']"
                                :options="certificaciones"
                                optionLabel="nombre"
                                optionValue="valor"
                                class="p-column-filter w-100"
                                display="chip"
                                :filter="true"
                                style="width: 100%"
                              >
                              </MultiSelect>
                              <label for="dropdown">Certificaciones</label>
                            </span>
                          </div>

                          <div
                            class="p-field p-col-12 p-md-7"
                            style="padding: 10px 5px 0px 5px !important"
                          >
                            <!-- <span class="p-float-label"> -->
                            <label for="dropdown"
                              >Estado de los colaboradores</label
                            >
                            <SelectButton
                              v-model="filters['estado']"
                              :options="opcionesEstados"
                              optionLabel="name"
                              dataKey="value"
                              optionValue="value"
                            />
                            <!-- <label for="dropdown">Estado de los colaboradores</label> -->
                            <!-- </span> -->
                          </div>
                        </CRow>
                      </CCardBody>
                    </CCard>
                  </CCollapse>
                  <div>
                    <CBadge v-if="filters.estado" color="success" class="mr-2">
                      {{ filters.estado === "" ? "Todos" : filters.estado }}
                    </CBadge>
                    <CBadge
                      v-if="filters.global"
                      style="background-color: #29abe2"
                      class="mr-2"
                    >
                      {{ filters.global }}
                    </CBadge>
                    <CBadge
                      v-show="filters.idiomas"
                      v-for="item in filters.idiomas"
                      :key="item.idnivelidioma"
                      style="background-color: #29abe2"
                      class="mr-2"
                    >
                      {{ item }}
                    </CBadge>
                    <CBadge
                      v-show="filters.certificaciones"
                      v-for="item in filters.certificaciones"
                      :key="item.idcertificacion"
                      style="background-color: #29abe2"
                      class="mr-2"
                    >
                      {{ item }}
                    </CBadge>
                  </div>
                </template>
                <template #empty> No se encontraron colaboradores. </template>
                <template #loading>
                  Cargando la información de colaboradores. Por favor espere.
                </template>
                <!--   <Column
                      selectionMode="multiple"
                      headerStyle="width: 3em"
                    ></Column>-->
                <Column
                  field="identificacion"
                  header="Identificación"
                  :sortable="true"
                  headerStyle="width:15%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Identificación</span>
                    {{ slotProps.data.identificacion }}
                  </template>
                  <!-- <template #filter>
                                <InputText type="text" v-model="filters['name']" class="p-column-filter" placeholder="Search by name"/>
                            </template> -->
                </Column>
                <Column
                  header="Idiomas"
                  filterField="idiomas"
                  filterMatchMode="custom"
                  :filterFunction="customFilter"
                  headerStyle="display:none;"
                  bodyStyle="display:none;"
                ></Column>
                <Column
                  header="Certificaciones"
                  filterField="certificaciones"
                  filterMatchMode="custom"
                  :filterFunction="customFilter"
                  headerStyle="display:none;"
                  bodyStyle="display:none;"
                >
                </Column>
                <Column
                  header="Nombre completo"
                  :sortable="true"
                  sortField="nombreCompleto"
                  filterField="nombreCompleto"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Nombre completo</span>
                    <a
                      href=""
                      style="cursor: hand"
                      @click.prevent="ver(slotProps.data)"
                      >{{ slotProps.data.nombreCompleto }}</a
                    >
                    <!-- {{ slotProps.data.nombreCompleto }} -->
                  </template>
                </Column>
                <Column
                  header="Especialidad"
                  :sortable="true"
                  sortField="ramaespecialidad"
                  filterField="ramaespecialidad"
                  filterMatchMode="contains"
                  headerStyle="width:15%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Especialidad</span>
                    {{ slotProps.data.ramaespecialidad }}
                  </template>
                </Column>
                 <Column
                  header="Cliente"
                  :sortable="true"
                  sortField="cliente"
                  filterField="cliente"
                  filterMatchMode="contains"
                  headerStyle="width:15%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Cliente</span>
                    {{ slotProps.data.cliente }}
                  </template>
                </Column>
                <Column
                  header="Puesto"
                  :sortable="true"
                  sortField="puesto"
                  filterField="puesto"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Puesto</span>
                    {{ slotProps.data.puesto }}
                  </template>
                </Column>
                <Column
                  header="Líder asignado"
                  :sortable="true"
                  sortField="deliveryManager"
                  filterField="deliveryManager"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Líder asignado</span>
                    {{ slotProps.data.deliveryManager }}
                  </template>
                </Column>
                <Column
                  header="Celular"
                  :sortable="true"
                  sortField="celular"
                  filterField="celular"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Celular</span>
                    {{ slotProps.data.celular }}
                  </template>
                </Column>
                <Column
                  header="Estado"
                  :sortable="true"
                  sortField="estado"
                  filterField="estado"
                  filterMatchMode="contains"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Estado</span>
                    {{ slotProps.data.estado }}
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
                      style="margin-top: -7px"
                      placement="left"
                    >
                      <template #toggler-content>
                        <i class="pi pi-ellipsis-v"></i>
                      </template>

                      <CDropdownItem @click="ver(slotProps.data)">
                        <CIcon name="cil-magnifying-glass" />&nbsp; Ver Perfil
                      </CDropdownItem>
                      <!-- <CDropdownItem @click="showAsociarDM(slotProps.data)">
                            <CIcon name="cil-user" />&nbsp; Asociar DM
                          </CDropdownItem> -->
                      <!-- <CDropdownItem
                            @click="showAsociarEmpresa(slotProps.data)"
                          >
                            <CIcon name="cil-building" />&nbsp; Asociar Empresa
                          </CDropdownItem> -->

                      <CDropdownItem @click="CV(slotProps.data)">
                        <CIcon name="cil-file" />&nbsp; Generar CV
                      </CDropdownItem>
                    </CDropdown>
                    <div class="row btn-opciones">
                      <div class="col">
                        <CButton
                          block
                          color="primary"
                          @click="ver(slotProps.data)"
                        >
                          <CIcon name="cil-magnifying-glass" />&nbsp;Ver perfil
                        </CButton>
                      </div>
                      <!-- <div class="col">
                            <CButton
                              block
                              color="primary"
                              @click="showAsociarDM(slotProps.data)"
                            >
                              <CIcon name="cil-user" />&nbsp;Asociar DM
                            </CButton>
                          </div> -->
                    </div>
                    <!-- <div class="row btn-opciones mt-1">
                          <div class="col">
                            <CButton
                              block
                              color="primary"
                              @click="showAsociarEmpresa(slotProps.data)"
                            >
                              <CIcon name="cil-building" />&nbsp;Asociar Empresa
                            </CButton>
                          </div>
                        </div> -->
                  </template>
                </Column>
              </DataTable>
            </CCol>
          </CRow>
          <!-- </CCardBody>
          </CCard> -->
        </CCol>
      </CRow>
    </div>

    <CModal
      title="Asociar con Delivery Manager"
      :show.sync="asociarDMModal"
      color="primary"
      class="modal-libro"
    >
      <div>
        <CRow class="pt-2">
          <CCol sm="12" md="12">
            <CCol sm="6" md="5">
              <span class="label-title">Colaborador</span>
            </CCol>
            <CCol sm="6" md="7">
              <span
                >{{
                  this.consultorSeleccionado.nombre +
                  ` ` +
                  this.consultorSeleccionado.apellido +
                  ` ` +
                  this.consultorSeleccionado.segundoapellido
                }}
              </span>
            </CCol>
          </CCol>

          <CCol sm="12" class="mt-2">
            <CCol sm="12" lg="12" class="pull-right">
              <CSelect
                label="Delivery Manager"
                :options="deliveryManagers"
                :value.sync="consultorSeleccionado.IdDM"
                add-label-classes="label-title"
              />
            </CCol>
          </CCol>
        </CRow>

        <CRow>
          <CCol sm="12" md="12" class="pull-right">
            <CCol col="6" class="pull-right">
              <CButton
                block
                color="primary"
                @click="asociarDM()"
                class="mr-1 pull-right btn btn-info libro-marca-azuloscuro"
                :disabled="consultorSeleccionado.IdDM === 0"
              >
                Asociar</CButton
              >
            </CCol>

            <CCol col="6" class="pull-right">
              <CButton
                block
                color="primary"
                @click="cancelarAsociarDM()"
                class="mr-1 pull-right btn btn-info libro-marca-celeste"
              >
                Cancelar</CButton
              >
            </CCol>
          </CCol>
        </CRow>
      </div>
      <template #footer>
        <div></div>
      </template>
    </CModal>

    <CModal
      title="Asociar con empresa de corporación"
      :show.sync="asociarEmpresaModal"
      color="primary"
      class="modal-libro"
    >
      <div>
        <CRow class="pt-2">
          <CCol sm="12" md="12">
            <CCol sm="6" md="5">
              <span class="label-title">Colaborador</span>
            </CCol>
            <CCol sm="6" md="7">
              <span
                >{{
                  this.consultorSeleccionado.nombre +
                  ` ` +
                  this.consultorSeleccionado.apellido +
                  ` ` +
                  this.consultorSeleccionado.segundoapellido
                }}
              </span>
            </CCol>
          </CCol>

          <CCol sm="12" class="mt-2">
            <CCol sm="12" lg="12" class="pull-right">
              <CSelect
                label="Empresa"
                :options="empresasCorp"
                :value.sync="empresaSeleccionada.IdEmpresaCorporacion"
                add-label-classes="label-title"
              />
            </CCol>
          </CCol>
        </CRow>

        <CRow>
          <CCol sm="12" md="12" class="pull-right">
            <CCol sm="12" md="6" class="pull-right">
              <CButton
                block
                color="primary"
                @click="asociarEmpresa()"
                class="mr-1 pull-right btn btn-info libro-marca-azuloscuro"
                :disabled="empresaSeleccionada.IdEmpresaCorporacion === 0"
              >
                Asociar</CButton
              >
            </CCol>

            <CCol sm="12" md="6" class="pull-right">
              <CButton
                block
                color="primary"
                @click="cancelarAsociarEmpresa()"
                class="mr-1 pull-right btn btn-info libro-marca-celeste"
              >
                Cancelar</CButton
              >
            </CCol>
          </CCol>
        </CRow>
      </div>
      <template #footer>
        <div></div>
      </template>
    </CModal>

    <CModal
      title="Exportar listado de colaboradores"
      :show.sync="exportarModal"
      color="primary"
      class="modal-libro"
    >
      <div>
        <CRow class="pt-2 mb-4">
          <CCol sm="12" lg="12" class="pull-right">
            <label
              >Por favor indique cuales colaboradores se incluyen en el archivo
              a generar</label
            >
            <CInputRadioGroup
              class="col-sm-12"
              :options="optionsRadio"
              inline
              :checked.sync="syncRadios"
            ></CInputRadioGroup>
          </CCol>
        </CRow>

        <CRow>
          <CCol sm="12" md="12" class="pull-right">
            <CCol col="6" class="pull-right">
              <CButton
                block
                color="primary"
                @click="exportCSV()"
                class="mr-1 pull-right btn btn-info libro-marca-azuloscuro"
              >
                Exportar</CButton
              >
            </CCol>

            <CCol col="6" class="pull-right">
              <CButton
                block
                color="primary"
                @click="cancelarModal()"
                class="mr-1 pull-right btn btn-info libro-marca-celeste"
              >
                Cancelar</CButton
              >
            </CCol>
          </CCol>
        </CRow>
      </div>
      <template #footer>
        <div></div>
      </template>
    </CModal>

    <CModal
      title="Plantilla"
      :show.sync="exportarModalCV"
      color="primary"
      class="modal-libro"
      size="sm"
    >
      <CRow class="pt-2">
        <CCol sm="12" lg="12">
          <CRow class="pt-2 mb-4" v-if="esGrupo">
            <CCol sm="12" lg="12" class="pull-right">
              <span class="label-title"> Colaboradores a considerar:</span>
            </CCol>
            <CCol sm="12" lg="12">
              <CInputRadioGroup
                class="pt-2 ml-3"
                :options="optionsRadioCV"
                inline
                :checked.sync="syncRadiosCV"
              ></CInputRadioGroup>
            </CCol>
          </CRow>
          <CRow class="pt-2">
            <CCol sm="12" lg="12" class="mt-3">
              <span class="label-title">Indique la plantilla a utilizar:</span>
            </CCol>
            <CCol sm="12" lg="12" class="pt-2">
              <CSelect
                class="ml-3"
                :options="tiposCV"
                :value.sync="CVseleccionado"
                add-label-classes="label-title"
              ></CSelect>
            </CCol>
          </CRow>
        </CCol>
      </CRow>
      <template #footer>
        <CRow>
          <CCol col="6">
            <CButton
              block
              color="primary"
              @click="exportCV()"
              class="btn btn-info libro-marca-azuloscuro"
            >
              Generar</CButton
            >
          </CCol>

          <CCol col="6">
            <CButton
              block
              color="primary"
              @click="cancelarModalCV()"
              class="btn btn-info libro-marca-celeste"
            >
              Cancelar</CButton
            >
          </CCol>
        </CRow>
      </template>
    </CModal>
  </div>
</template>

<script>
import Vue from "vue";
import { columnas } from "./constantes";
//import XLSX from "xlsx";
import XLSX from "sheetjs-style";

import {
  GetAll,
  GetDetail,
  GetColaboradoresDM,
  AsociarDMColaboradorCommand,
  GetEmpresaCorporacion,
  AsociarEmpresaColaboradorCommand,
  GetNivelIdioma,
  GetCertificacion,
} from "./request";
import { GetTiposCV, GetColaboradoresCV } from "../Consultas/request";

export default {
  name: "ColaboradorGrid",
  data() {
    return {
      innerCollapse: false,
      collapse: false,
      dt: null,
      btnFilter: {
        text: "Mostrar filtros",
      },
      idiomas: [],
      certificaciones: [],
      colaboradorSeleccionado: null,
      colaboradoresSeleccionados: [],
      colaboradores: null,
      filters: { estado: " Activo" },
      loading: true,
      primaryModal: false,
      items: [],
      deliveryManagers: [],
      empresasCorp: [],
      asociarDMModal: false,
      asociarEmpresaModal: false,
      registroActual: {
        id: null,
        identificacion: "",
        nombre: "",
        primerapellido: "",
        segundoapellido: "",
        fechanacimiento: "",
        genero: "",
        fechaingreso: "",
        direccion: "",
        celular: "",
        telefono: "",
        correo: "",
        correopersonal: "",
        resenaperfil: "",
        numerocpic: "",
        ramaespecialidad: "",
        activo: true,
        estadocivil: "",
        telefonoemergencia: "",
        personallamar: "",
        numerohijos: 0,
      },
      consultorSeleccionado: {
        Id: 0,
        nombre: "",
        apellido: "",
        segundoapellido: "",
        IdDM: 0,
      },
      empresaSeleccionada: {
        Id: 0,
        IdEmpresaCorporacion: 0,
      },
      exportarModal: false,
      optionsRadio: [
        // "Option1",
        { value: "Todos", label: "Todos", props: { checked: true } },
        {
          value: "Filtrados",
          label: "Filtrados",
          props: { disabled: false },
        },
      ],
      syncRadios: "Todos",

      //cv
      exportarModalCV: false,

      optionsRadioCV: [
        // "Option1",
        { value: "Todos", label: "Todos", props: { checked: true } },
        {
          value: "Seleccionados",
          label: "Seleccionados",
          //,props: { desription: "description text" }
        },
      ],
      syncRadiosCV: "Todos",
      tiposCV: [],
      CVseleccionado: 1,
      loader: null,
      esGrupo: true,
      tipoEstado: "Activo",
      //opcionesEstados: ['Activo', 'Inactivo', 'Todos'],
      opcionesEstados: [
        { name: "Activo", value: " Activo" },
        { name: "Inactivo", value: "Inactivo" },
        { name: "Todos", value: "" },
      ],
    };
  },
  async mounted() {
    this.GetTiposCV();
    let loader = this.showLoader();
    this.getLista();
    this.getListaNivelesIdiomas();
    this.getListaCertificaciones();
    this.loading = false;
    this.hideLoader(loader);
  },
  methods: {
    // rowClick(e) { console.log(e);  this.$emit("row-click", e.data.idcolaborador); },
    // rowClicked(e) {
    //   this.$refs.dt.$emit("row-click", e.data.idcolaborador);
    // },
    toggleFilter() {
      this.collapse = !this.collapse;
      this.btnFilter.text = this.collapse
        ? "Ocultar filtros"
        : "Mostrar filtros";
    },
    clearFilters() {
      this.filters = {};
      this.filters.estado = " Activo";
      this.colaboradoresSeleccionados = null;
    },
    filterApplied(e) {},
    async getLista() {
      try {
        const { data } = await GetAll();
        this.colaboradores = data.colaboradores;
      } catch (error) {
        console.log(error);
      }
    },
    async getListaNivelesIdiomas() {
      try {
        const { data } = await GetNivelIdioma();
        this.idiomas = data.nivelIdioma;
        this.idiomas.unshift({ nombre: "Sin idioma", valor: "" });
      } catch (error) {
        console.log(error);
      }
    },
    async getListaCertificaciones() {
      try {
        const { data } = await GetCertificacion();
        this.certificaciones = data.certificaciones;
        this.certificaciones.unshift({
          nombre: "Sin certificación",
          valor: "",
        });
      } catch (error) {
        console.log(error);
      }
    },
    agregar() {
      //this.$router.push({ name: "ClasificadoresForm" });
      this.registroActual = {
        identificacion: "",
        nombre: "",
        primerapellido: "",
        segundoapellido: "",
        fechanacimiento: "",
        genero: "",
        fechaingreso: "",
        direccion: "",
        celular: "",
        telefono: "",
        correo: "",
        correopersonal: "",
        resenaperfil: "",
        numerocpic: "",
        ramaespecialidad: "",
        activo: true,
        estadocivil: "",
        telefonoemergencia: "",
        personallamar: "",
        numerohijos: 0,
      };
      this.primaryModal = true;
    },
    async modificar(item) {
      //this.$router.push({ name: "ClasificadoresForm",params:{"id":item.id} });
      const { data } = await GetDetail(item.idcolaborador);
      this.registroActual = data.colaborador;
      this.primaryModal = true;
    },
    async ver(item, x, event) {
      let loader = this.showLoader();
      this.colaboradorSeleccionado = item;
      setTimeout(
        () =>
          this.$router.push({
            name: "Perfil del Colaborador",
            params: {
              id: item.idcolaborador,
              origen: "Colaborador",
              isConsulta: false,
            },
          }),
        500
      );
    },
    async showAsociarDM(item) {
      this.colaboradorSeleccionado = item;
      this.asociarDMModal = true;
      this.consultorSeleccionado.Id = item.idcolaborador;
      this.consultorSeleccionado.nombre = item.nombre;
      this.consultorSeleccionado.apellido = item.primerapellido;
      this.consultorSeleccionado.segundoapellido = item.segundoapellido;
      this.consultorSeleccionado.IdDM = item.iddeliverymanager;

      const {
        data: { colaboradoresDM },
      } = await GetColaboradoresDM();

      this.deliveryManagers = colaboradoresDM.map(
        ({ idcolaborador, nombreCompleto }) => ({
          value: idcolaborador,
          label: nombreCompleto,
        })
      );
      this.deliveryManagers.push({ value: 0, label: "Seleccione" });
    },
    async asociarDM() {
      try {
        if (this.consultorSeleccionado.IdDM === 0) {
          Vue.$toast.warning("Debe de seleccionar un registro valido.", {
            // optional options Object
          });
          return true;
        } else {
          const { data } = await AsociarDMColaboradorCommand(
            this.consultorSeleccionado
          );

          if (isNaN(data)) {
            Vue.$toast.error("Se produjo un error");
          } else {
            Vue.$toast.success(
              "Se ha almacenado la informacion correctamente."
            );
          }
          this.asociarDMModal = false;

          let loader = this.showLoader();
          await this.getLista();
          this.loading = false;
          this.hideLoader(loader);
        }
      } catch (error) {
        console.log(error);
      }
    },
    cancelarAsociarDM() {
      this.asociarDMModal = false;
    },
    async showAsociarEmpresa(item) {
      this.colaboradorSeleccionado = item;
      this.asociarEmpresaModal = true;
      this.consultorSeleccionado.nombre = item.nombre;
      this.consultorSeleccionado.apellido = item.primerapellido;
      this.consultorSeleccionado.segundoapellido = item.segundoapellido;
      this.empresaSeleccionada.Id = item.idcolaborador;
      this.empresaSeleccionada.IdEmpresaCorporacion = item.idempresacorporacion;

      const {
        data: { empresaCorporacions },
      } = await GetEmpresaCorporacion();

      this.empresasCorp = empresaCorporacions.map(
        ({ idempresacorporacion, nombre }) => ({
          value: idempresacorporacion,
          label: nombre,
        })
      );
      this.empresasCorp.push({ value: 0, label: "Seleccione" });
    },
    async asociarEmpresa() {
      try {
        if (this.empresaSeleccionada.alertIdEmpresaCorporacion === 0) {
          Vue.$toast.warning("Debe de seleccionar un registro valido.");
          return true;
        } else {
          const { data } = await AsociarEmpresaColaboradorCommand(
            this.empresaSeleccionada
          );

          if (isNaN(data)) {
            Vue.$toast.error("Se produjo un error");
          } else {
            Vue.$toast.success(
              "Se ha almacenado la informacion correctamente."
            );
          }
          this.asociarEmpresaModal = false;

          let loader = this.showLoader();
          await this.getLista();
          this.loading = false;
          this.hideLoader(loader);
        }
      } catch (error) {
        console.log(error);
      }
    },
    cancelarAsociarEmpresa() {
      this.asociarEmpresaModal = false;
    },
    async submit(registroActual) {
      try {
        // let loader = this.showLoader();
        //const { data } = await Upsert(registroActual);
        //this.primaryModal = false;
        //this.toast = { show: true, message: "Se almaceno con exito" };
        //this.getLista();
        // this.hideLoader(loader);
      } catch (error) {
        // this.toast = {
        //    show: true,
        //    message: `Se produjo un errror ${error.message}`,
        // };
      }
    },
    showLoader() {
      return this.$loading.show({
        // Optional parameters
        container: this.fullPage ? null : this.$refs.formContainer,
        canCancel: true,
        onCancel: this.onCancel,
      });
    },
    hideLoader(loader) {
      setTimeout(() => {
        loader.hide();
      }, 300);
    },
    customFilter(value, filter) {
      if (
        filter === undefined ||
        filter === null ||
        (typeof filter === "string" && filter.trim() === "") ||
        (typeof filter === "object" && filter.length === 0)
      ) {
        return true;
      }

      if (value === undefined || value === null) {
        return false;
      }

      for (let i = 0; i < filter.length; i++) {
        if (
          (value === "" && filter[i] === "") ||
          (filter[i] !== "" && value.indexOf(filter[i]) !== -1)
        ) {
          return true;
        }
      }
      return false; //value.indexOf(filter) !== -1;
    },
    async exportCSV() {
      //this.$refs.dt.exportCSV(); //funciona el de la tabla pero no permite cambiar el nombre del file

      switch (this.syncRadios) {
        case "Todos":
          //filtrar por activo o inactivo
          var data = [];
          if (this.filters.estado.trim() == "Activo") {
            data = this.colaboradores.filter((x) => x.activo == true);
          } else if (this.filters.estado.trim() == "Inactivo") {
            data = this.colaboradores.filter((x) => x.activo == false);
          } else if (this.filters.estado.trim() == "") {
            data = this.colaboradores;
          }

          data = data.map(
            ({
              identificacion,
              nombreCompleto,
              ramaespecialidad,
              puesto,
              deliveryManager,
              celular,
            }) => ({
              Identificacion: identificacion,
              Nombre_Completo: nombreCompleto,
              Especialidad: ramaespecialidad,
              Puesto: puesto,
              Lider_Asignado: deliveryManager,
              Celular: celular,
            })
          );

          const wb = XLSX.utils.book_new();
          const ws = XLSX.utils.json_to_sheet(data);
          XLSX.utils.book_append_sheet(wb, ws, "Colaboradores.csv");
          XLSX.writeFile(wb, `Colaboradores_${this.getDateTime()}.csv`);
          Vue.$toast.success("Se ha exportado la información correctamente.");
          this.exportarModal = false;

          break;

        case "Filtrados":
          var data = this.$refs.dt.processedData.map(
            ({
              identificacion,
              nombreCompleto,
              ramaespecialidad,
              puesto,
              deliveryManager,
              celular,
            }) => ({
              Identificacion: identificacion,
              Nombre_Completo: nombreCompleto,
              Especialidad: ramaespecialidad,
              Puesto: puesto,
              Lider_Asignado: deliveryManager,
              Celular: celular,
            })
          );

          const wbf = XLSX.utils.book_new();
          const wsf = XLSX.utils.json_to_sheet(data);
          XLSX.utils.book_append_sheet(wbf, wsf, "Colaboradores.csv");
          XLSX.writeFile(wbf, `Colaboradores_${this.getDateTime()}.csv`);
          Vue.$toast.success("Se ha exportado la información correctamente.");
          this.exportarModal = false;
          break;
        case "Seleccionados":
          this.exportSelectedCSV();
          break;

        default:
          Vue.$toast.warning("Debe de seleccionar un tipo de exportación.", {
            // optional options Object
          });
          break;
      }
    },
    async exportSelectedCSV() {
      if (this.colaboradoresSeleccionados.length > 0) {
        var data = this.colaboradoresSeleccionados.map(
          ({
            identificacion,
            nombreCompleto,
            ramaespecialidad,
            puesto,
            deliveryManager,
            celular,
          }) => ({
            Identificacion: identificacion,
            Nombre_Completo: nombreCompleto,
            Especialidad: ramaespecialidad,
            Puesto: puesto,
            Lider_Asignado: deliveryManager,
            Celular: celular,
          })
        );

        const wb = XLSX.utils.book_new();
        const ws = XLSX.utils.json_to_sheet(data);
        XLSX.utils.book_append_sheet(wb, ws, "ColaboradoresSeleccionados");
        XLSX.writeFile(
          wb,
          `ColaboradoresSeleccionados_${this.getDateTime()}.csv`
        );
        Vue.$toast.success("Se ha exportado la información correctamente.");
      } else {
        Vue.$toast.warning("Debe de seleccionar al menos un colaborador.");
      }
    },
    async showModal() {
      this.exportarModal = true;
    },
    async cancelarModal() {
      this.exportarModal = false;
    },
    getDateTime() {
      const today = new Date();
      const date =
        today.getFullYear() +
        "-" +
        (today.getMonth() + 1) +
        "-" +
        today.getDate();
      const time =
        today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
      const dateTime = date + "_" + time;
      return dateTime;
    },

    //cv

    async CV(item, x, event) {
      var marcados = [];
      marcados.push(item);
      this.colaboradoresSeleccionados = marcados;
      //this.colaboradoresSeleccionados = item;
      this.showModalCV(false);
    },
    async exportCV() {
      if (!this.esGrupo) {
        this.exportAllSelectedCV();
      } else {
        switch (this.syncRadiosCV) {
          case "Todos":
            this.exportAllCV();
            break;
          case "Seleccionados":
            this.exportAllSelectedCV();
            break;
          default:
            Vue.$toast.warning("Debe de seleccionar un tipo de exportación.", {
              // optional options Object
            });
            break;
        }
      }
    },
    async exportAllCV() {
      let loader = this.showLoader();
      this.exportarModalCV = false;
      Vue.$toast.info("Espere un momento por favor", { timeOut: 0 });
      if (this.colaboradores.length > 0) {
        var dataId = this.colaboradores.map(({ identificacion }) => ({
          Identificacion: identificacion,
        }));

        const {
          data: { json },
        } = await GetColaboradoresCV(
          JSON.stringify(dataId),
          this.CVseleccionado
        );

        const result = JSON.parse(json);
        if (result.resultado === 1) {
          var buffer = this.base64ToArrayBuffer(result.archivo);
          this.saveByteArray("Colaboradores", buffer);

          Vue.$toast.success("Se ha generado el cv correctamente.");
        } else {
          Vue.$toast.warning("Se ha producido un error en la generación. ");
        }
      } else {
        Vue.$toast.warning("Debe de seleccionar al menos un colaborador.");
      }
      this.hideLoader(loader);
    },
    async exportAllSelectedCV() {
      let loader = this.showLoader();
      this.exportarModalCV = false;
      Vue.$toast.info("Espere un momento por favor", { timeOut: 0 });
      if (this.colaboradoresSeleccionados.length > 0) {
        var dataId = this.colaboradoresSeleccionados.map(
          ({ identificacion }) => ({
            Identificacion: identificacion,
          })
        );
        // try {
        const {
          data: { json },
        } = await GetColaboradoresCV(
          JSON.stringify(dataId),
          this.CVseleccionado
        );
        //   } catch (error) {
        //    this.hideLoader(loader);
        //   Vue.$toast.warning("Se ha producido un error: " + error.mensaje);
        //  }

        const result = JSON.parse(json);
        if (result.resultado === 1) {
          var buffer = this.base64ToArrayBuffer(result.archivo);
          this.saveByteArray("Colaboradores", buffer);
          Vue.$toast.success("Generación exitosa de los documentos.");
        } else {
          Vue.$toast.warning("Se ha producido un error: " + result.mensaje);
        }
      } else {
        Vue.$toast.warning("Debe de seleccionar al menos un colaborador.");
      }
      this.hideLoader(loader);
    },
    async showModalCV(esGrupo) {
      this.esGrupo = esGrupo;
      this.exportarModalCV = true;
    },
    async cancelarModalCV() {
      this.exportarModalCV = false;
    },
    async GetTiposCV() {
      try {
        const data = await GetTiposCV();
        this.tiposCV = data;
      } catch (error) {
        console.log(error);
      }
    },
    base64ToArrayBuffer(base64) {
      var binaryString = window.atob(base64);
      var binaryLen = binaryString.length;
      var bytes = new Uint8Array(binaryLen);
      for (var i = 0; i < binaryLen; i++) {
        var ascii = binaryString.charCodeAt(i);
        bytes[i] = ascii;
      }
      return bytes;
    },
    saveByteArray(reportName, byte) {
      var blob = new Blob([byte], { type: "application/zip" });
      var link = document.createElement("a");
      link.href = window.URL.createObjectURL(blob);
      var fileName = reportName;
      link.download = fileName;
      link.click();
    },
  },
  computed: {
    getColumnas() {
      return columnas;
    },
  },
};
</script>

<style>
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
.p-datatable-colaboradores .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-colaboradores .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-colaboradores .p-datatable-tbody > tr > td {
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
  .p-datatable.p-datatable-colaboradores .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-colaboradores .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-colaboradores .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-colaboradores .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-colaboradores:last-child {
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
