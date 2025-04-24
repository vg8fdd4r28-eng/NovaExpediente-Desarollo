<template>
  <div class="wrapper vld-parent" ref="formContainer">

    <CCard style="border-radius: 16px !important; margin-top: 20px;">
      <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
        <strong>Colaboradores</strong>
      </CCardHeader>
      <CCardBody style="padding-top: 0px; padding-bottom: 0px;">     
        <CRow>
           <CCol>  
            <p> Lista de colaboradores.</p>
          </CCol>
        </CRow> 
    <CRow>
      <CCol lg="10">
        <div id="search_box_container"></div>
        <div id="search_query">&nbsp;</div>
      </CCol>
      <CCol lg="2" class="mb-2">
        <div class="card-header-actions" lg="12">
          <CButton
            id="btnBuscarColaboradoresConsulta"
            block
            color="primary"
            @click="buscarColaboradores()"
            class="mr-1 btn pull-right btn btn-info libro-marca-celeste onHoverDark w-100 btn-primary"
          >
            <CIcon name="cil-magnifying-glass" />&nbsp;Buscar</CButton
          >
        </div>
      </CCol>
    </CRow>        
      </CCardBody>
    </CCard> 

    <CRow>
      <CCol md="8" sm="12">
         <div class="table-header ">
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
          <CCol md="2" sm="12" class="ml-auto">
            <div style="text-align: right">
              <CButton
               id="btnExportarColaboradoresConsulta"
               block
               color="primary"
               @click="showModal()"
               class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark w-100 btn-primary"
               >
              <CIcon name="cil-file" />&nbsp;Exportar</CButton>
              </div>
           </CCol>
           <CCol md="2" sm="12" class="ml-auto">
             <div style="text-align: right">
                <CButton
                 id="btnGenerarCV"
                 block
                 color="primary"
                 @click="showModalCV(true)"
                  class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark w-100 btn-primary">
                  <CIcon name="cil-file" />&nbsp;Generar CV</CButton>
              </div>
           </CCol>
    </CRow>
    <CRow class="mt-1">
      <CCol lg="12">
        <!-- <CCard style="border-radius: 16px !important; margin-top: 20px;">
         <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
          </CCardHeader>
          <CCardBody> -->
            <DataTable
              ref="dt"
              :value="colaboradores"
              :paginator="true"
              class="p-datatable-colaboradores p-datatable-striped p-datatable-sm"
              :rows="10"
              dataKey="idcolaborador"
              :rowHover="true"
              :selection.sync="colaboradorSeleccionado"
              :filters.sync="filters"
              :loading="loading"
              paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
              :rowsPerPageOptions="[5, 10, 25, 50]"
              currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} colaboradores"
              sortField="nombreCompleto"
              :sortOrder="1"
              stateStorage="session"
              stateKey="dt-state-colaboradoresConsulta"
              selectionMode="multiple"
              :autoLayout="true"
              @update:filters="filterApplied"
              @filter="filterApplied"
            >
              <template #header>

              </template>
              <template #empty>
                No se encontraron colaboradores.
              </template>
              <template #loading>
                Cargando la información de colaboradores. Por favor espere.
              </template>
              <Column
                selectionMode="multiple"
                headerStyle="width: 3em"
              ></Column>
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
              </Column>

              <Column
                field="nombrecompleto"
                header="Nombre completo"
                :sortable="true"
                sortField="nombrecompleto"
                filterField="nombrecompleto"
                filterMatchMode="contains"
              >
                <template #body="slotProps">
                  <span class="p-column-title">Nombre completo</span>
                  <a href="" style="cursor: hand;" @click.prevent="ver(slotProps.data)">{{ slotProps.data.nombrecompleto }}</a>
                  <!-- {{ slotProps.data.nombrecompleto }} -->
                </template>
              </Column>
              <Column
                field="deliverymanager"
                header="Líder Asignado"
                :sortable="true"
                sortField="deliverymanager"
                filterField="deliverymanager"
                filterMatchMode="contains"
              >
                <template #body="slotProps">
                  <span class="p-column-title">Líder Asignado</span>
                  {{ slotProps.data.deliverymanager }}
                </template>
              </Column>
              <Column
                field="ranking"
                header="Ranking"
                :sortable="true"
                sortField="ranking"
                filterField="ranking"
                filterMatchMode="contains"
              >
                <template #body="slotProps">
                  <span class="p-column-title">Ranking</span>
                  <ProgressBar
                    :value="slotProps.data.ranking"
                    :showValue="true"
                  />
                </template>
              </Column>
              <Column
                header="Acciones"
                :sortable="false"
                headerStyle="width:75px;"
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

                    <CDropdownItem @click="ver(slotProps.data)">
                      <CIcon name="cil-magnifying-glass" />&nbsp; Ver Perfil
                    </CDropdownItem>
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
                    <div class="col">
                      <CButton
                        block
                        color="primary"
                        @click="CV(slotProps.data)"
                      >
                        <CIcon name="cil-user" />&nbsp;Generar CV
                      </CButton>
                    </div>
                  </div>
                </template>
              </Column>
            </DataTable>
          <!-- </CCardBody>
        </CCard> -->
      </CCol>
    </CRow>

    <CModal
      title="Generar listado de colaboradores"
      :show.sync="exportarModal"
      color="primary"
      class="modal-libro"
    >
      <div>
        <CRow class="pt-2 mb-4">
          <CCol sm="12" lg="12" class="pull-right">
            <span class="label-title"
              >Indique cuales colaboradores se incluyen en el archivo a
              generar:</span
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
            <CCol sm="12" lg="12" class="pt-2 ">
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
              class=" btn btn-info libro-marca-azuloscuro"
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
const createAnimation = (opacity, duration, queue) => (
  { opacity }, { duration, queue }
);

import {} from "@/vendor/jquery-1.11.0.js";
import {} from "@/vendor/jquery.ui.core.js";
import {} from "@/vendor/jquery.ui.widget.js";
import {} from "@/vendor/jquery.ui.position.js";
import {} from "@/vendor/jquery.ui.menu.js";
import {} from "@/vendor/jquery.ui.autocomplete.js";

import {} from "@/vendor/underscore-1.5.2.js";
import {} from "@/vendor/backbone-1.1.0.js";
import {} from "@/lib/js/visualsearch.js";

import {} from "@/lib/js/utils/jquery_extensions.js";
import {} from "@/lib/js/views/search_box.js";
import {} from "@/lib/js/views/search_facet.js";
import {} from "@/lib/js/views/search_input.js";
import {} from "@/lib/js/models/search_facets.js";
import {} from "@/lib/js/models/search_query.js";

import {} from "@/lib/js/utils/backbone_extensions.js";
import {} from "@/lib/js/utils/hotkeys.js";
import {} from "@/lib/js/utils/search_parser.js";
import {} from "@/lib/js/utils/inflector.js";
import {} from "@/lib/js/templates/templates.js";

//import XLSX from "xlsx";
import XLSX from "sheetjs-style";

import "@/lib/css/reset.css";
import "@/lib/css/icons.css";
import "@/lib/css/workspace.css";
import Vue from "vue";
import {
  GradosAcademicosGetAll,
  GetCertificacion,
  GetEspecialidad,
  GetNivelIdioma,
  GetBaseDatos,
  GetLenguajeProgramacion,
  GetAmbienteDesarrollo,
  GetTecnologia,
  GetErpCore,
  GetCalidad,
  GetInfraestructura,
  GetBusConsulta,
  GetBPMConsulta,
  GetCantMes,
  GetColaboradoresConsulta,
  saveState,
  saveJsonState,
  getState,
  GetColaboradoresCV,
  GetTiposCV
} from "./request";

const QUERYKEY = "query";
const QUERYSEARCHKEY = "querySearch";
const QUERYLABELKEY = "queryLabel";

export default {
  name: "Consulta",
  data() {
    return {
      query: `{}`,
      queryLabel: "",
      colaboradorSeleccionado: [],
      colaboradores: [],
      filters: {},
      loading: true,
      // storage, setTimeout Id
      queryHideDelay: 0,
      // will be data for each category
      grado: [],
      especialidad: [],
      certificacion: [],
      idioma: [],
      basedatos: [],
      lenguaje: [],
      ambienteDesarrollo: [],
      tecnologia: [],
      erp: [],
      calidad: [],
      infraestructura: [],
      bus: [],
      bpm: [],
      cantMes: [],
      exportarModal: false,
      exportarModalCV: false,
      optionsRadio: [
        // "Option1",
        { value: "Todos", label: "Todos", props: { checked: true } },
        {
          value: "Seleccionados",
          label: "Seleccionados"
          //,props: { desription: "description text" }
        }
      ],
      syncRadios: "Todos",
      optionsRadioCV: [
        // "Option1",
        { value: "Todos", label: "Todos", props: { checked: true } },
        {
          value: "Seleccionados",
          label: "Seleccionados"
          //,props: { desription: "description text" }
        }
      ],
      syncRadiosCV: "Todos",
      tiposCV: [],
      CVseleccionado: 1,
      loader: null,
      esGrupo: true
    };
  },

  mounted() {
    this.query = getState(QUERYSEARCHKEY);
    this.queryLabel = getState(QUERYLABELKEY);

    this.$refs.typeahead = VS.init({
      container: $("#search_box_container"),
      query: this.queryLabel,
      jsonQuery: getState(QUERYKEY) ? window.eval(getState(QUERYKEY)) : [],
      showFacets: true,
      readOnly: false,
      unquotable: ["text", "account", "filter", "access"],
      placeholder: "Buscar consultores...",
      callbacks: {
        search: this.search,
        valueMatches: this.valueMatches,
        facetMatches: this.facetMatches
      }
    });
    this.loading = false;

    this.GetColaboradoresConsulta();
    this.GetTiposCV();
  },
  methods: {
    // VS Methods
    search(query, searchCollection) {
      const $sq = $("#search_query");
      const serialized = searchCollection.serializeWithKey();
      this.queryLabel = searchCollection.serialize();

      saveState(this.queryLabel, QUERYLABELKEY);
      saveJsonState(searchCollection.models, QUERYKEY);

      $sq.stop().animate(createAnimation(1, 300, false));
      //  $sq.html(
      //   `<span class="raquo">&raquo;</span> Su búsqueda es por: <b>${serialized}</b>`
      //);
      this.query = `{${serialized}}`;

      saveState(this.query, QUERYSEARCHKEY);

      clearTimeout(this.queryHideDelay);
      this.queryHideDelay = setTimeout(() => {
        $sq.animate(createAnimation(0, 1000, false));
      }, 2000);
    },
    valueMatches(category, searchTerm, callback) {
      switch (category) {
        case "Grado":
          callback(this.grado);
          break;
        case "Certificacion":
          callback(this.certificacion);
          break;
        case "Especialidad":
          callback(this.especialidad);
          break;
        case "Idioma":
          callback(this.idioma);
          break;
        case "BaseDatos":
          callback(this.basedatos);
          break;
        case "Lenguaje":
          callback(this.lenguaje);
          break;
        case "AmbienteDesarrollo":
          callback(this.ambienteDesarrollo);
          break;
        case "Tecnologia":
          callback(this.tecnologia);
          break;
        case "ERP":
          callback(this.erp);
          break;
        case "Calidad":
          callback(this.calidad);
          break;

        case "Infraestructura":
          callback(this.infraestructura);
          break;

        case "Bus":
          callback(this.bus);
          break;
        case "BPMYOfimatica":
          callback(this.bpm);
          break;
        case "Experiencia":
          callback(this.cantMes);
          return;
        default:
          callback([]);
          break;
      }
    },
    facetMatches(callback) {
      callback([
        "Grado",
        "Especialidad",
        "Certificacion",
        "Idioma",
        "BaseDatos",
        "AmbienteDesarrollo",
        "Lenguaje",
        "Tecnologia",
        "ERP",
        "Calidad",
        "Infraestructura",
        "Bus",
        "BPMYOfimatica",
        "Experiencia"
      ]);
    },
    async buscarColaboradores() {
      this.GetColaboradoresConsulta();
      this.colaboradorSeleccionado = null;
    },
    async GetColaboradoresConsulta() {
      const {
        data: { colaboradores }
      } = await GetColaboradoresConsulta(this.query);
      this.colaboradores = colaboradores;
      this.clearFilters();
      // Vue.$toast.success("Se ha generado la consulta correctamente.");
    },
    async ver(item, x, event) {
      // this.colaboradorSeleccionado = item;
      setTimeout(
        () =>
          this.$router.push({
            name: "Perfil del Colaborador",
            params: {
              id: item.idcolaborador,
              origen: "Consulta",
              //queryLabel: this.queryLabel,
              // query: this.query,
              isConsulta: true
            }
          }),
        500
      );
    },
    async CV(item, x, event) {
      //   this.loader = this.showLoader();
      //   Vue.$toast.info("Espere un momento por favor", { timeOut: 0 });
      //  this.exportarModalCV = false;
      // if (item) {
      //  const selectedItems = [];
      //  selectedItems.push({ Identificacion: item.identificacion });

      //  const {
      //   data: { json }
      //  } = await GetColaboradoresCV(
      //  JSON.stringify(selectedItems),
      //    this.CVseleccionado
      //   );

      //   const result = JSON.parse(json);
      //  if (result.resultado === 1) {
      //   var buffer = this.base64ToArrayBuffer(result.archivo);
      //  this.saveByteArray("Colaboradores", buffer);

      //   Vue.$toast.success("Se ha generado el cv correctamente.");
      //  } else {
      //    Vue.$toast.warning("Se ha producido un error: " + result.mensaje);
      //   }
      //  } else {
      //    Vue.$toast.warning("Debe de seleccionar al menos un colaborador.");
      // }

      //  this.hideLoader();
      var marcados = [];
      marcados.push(item);
      this.colaboradorSeleccionado = marcados;
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
      this.loader = this.showLoader();
      this.exportarModalCV = false;
      Vue.$toast.info("Espere un momento por favor", { timeOut: 0 });
      if (this.colaboradores.length > 0) {
        var dataId = this.colaboradores.map(({ identificacion }) => ({
          Identificacion: identificacion
        }));

        const {
          data: { json }
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
          Vue.$toast.warning("Se ha producido un error: " + result.mensaje);
        }
      } else {
        Vue.$toast.warning("Debe de seleccionar al menos un colaborador.");
      }
      this.hideLoader();
    },
    async exportAllSelectedCV() {
      this.loader = this.showLoader();
      this.exportarModalCV = false;
      Vue.$toast.info("Espere un momento por favor", { timeOut: 0 });
      if (this.colaboradorSeleccionado.length > 0) {
        var dataId = this.colaboradorSeleccionado.map(({ identificacion }) => ({
          Identificacion: identificacion
        }));

        const {
          data: { json }
        } = await GetColaboradoresCV(
          JSON.stringify(dataId),
          this.CVseleccionado
        );

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
      this.hideLoader();
    },
    async exportCSV() {
      //this.$refs.dt.exportCSV(); //funciona el de la tabla pero no permite cambiar el nombre del file
      switch (this.syncRadios) {
        case "Todos":
          var data = this.colaboradores.map(
            ({ identificacion, nombrecompleto, deliverymanager, ranking }) => ({
              Identificacion: identificacion,
              Nombre_Completo: nombrecompleto,
              Lider_Asignado: deliverymanager,
              Ranking: ranking
            })
          );
          const wb = XLSX.utils.book_new();
          const ws = XLSX.utils.json_to_sheet(data);

          // títulos de columnas
          ws.A1.v = "Identificación";
          ws.B1.v = "Nombre del Colaborador";
          ws.C1.v = "Nombre del Líder Asignado";
          ws.D1.v = "Ranking";

          // ancho de las columnas
          var wscols = [
            { width: 13 },
            { width: 40 },
            { width: 40 },
            { width: 8 }
          ];

          ws["!cols"] = wscols;

          XLSX.utils.book_append_sheet(wb, ws, "Colaboradores");
          XLSX.writeFile(wb, `Colaboradores_${this.getDateTime()}.xlsx`);
          Vue.$toast.success("Se ha exportado la información correctamente.");

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
      if (this.colaboradorSeleccionado.length > 0) {
        var data = this.colaboradorSeleccionado.map(
          ({ identificacion, nombrecompleto, deliverymanager, ranking }) => ({
            Identificacion: identificacion,
            Nombre_Completo: nombrecompleto,
            Lider_Asignado: deliverymanager,
            Ranking: ranking
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
    async showModalCV(esGrupo) {
      this.esGrupo = esGrupo;
      this.exportarModalCV = true;
    },
    async cancelarModalCV() {
      this.exportarModalCV = false;
    },
    async GetTiposCV() {
      const data = await GetTiposCV();
      this.tiposCV = data;
    },
    showLoader() {
      return this.$loading.show({
        // Optional parameters
        container: this.fullPage ? null : this.$refs.formContainer,
        canCancel: false,
        onCancel: this.onCancel
      });
    },
    hideLoader() {
      setTimeout(() => {
        this.loader.hide();
      }, 300);
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
    filterApplied(e) {},
    clearFilters() {
      this.filters = {};
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
    onCancel() {
      this.hideLoader();
    }
  },
  async created() {
    try {
      // call `grado` data
      const {
        data: { gradosAcademicos }
      } = await GradosAcademicosGetAll();
      this.grado = gradosAcademicos;

      // call `certificacion` data
      const {
        data: { certificaciones }
      } = await GetCertificacion();
      this.certificacion = certificaciones;

      // call `especialidad` data
      const {
        data: { especialidades }
      } = await GetEspecialidad();
      this.especialidad = especialidades;

      // call `idioma` data
      const {
        data: { nivelIdioma }
      } = await GetNivelIdioma();
      this.idioma = nivelIdioma;

      // call `basedatos` data
      const {
        data: { basesDatos }
      } = await GetBaseDatos();
      this.basedatos = basesDatos;

      // call `lenguaje` data
      const {
        data: { lenguajesProgramacion }
      } = await GetLenguajeProgramacion();
      this.lenguaje = lenguajesProgramacion;

      // call `ambienteDesarrollo` data
      const {
        data: { ambientesDesarrollo }
      } = await GetAmbienteDesarrollo();
      this.ambienteDesarrollo = ambientesDesarrollo;

      // call `tecnologia` data
      const {
        data: { tecnologias }
      } = await GetTecnologia();
      this.tecnologia = tecnologias;

      // call `erp` data
      const {
        data: { erpCores }
      } = await GetErpCore();
      this.erp = erpCores;

      // call `calidad` data
      const {
        data: { calidads }
      } = await GetCalidad();
      this.calidad = calidads;

      // call `infraestructura` data
      const {
        data: { infraestructuras }
      } = await GetInfraestructura();
      this.infraestructura = infraestructuras;

      // call `bus` data
      const {
        data: { buses }
      } = await GetBusConsulta();
      this.bus = buses;

      // call `bpm` data
      const {
        data: { bpms }
      } = await GetBPMConsulta();
      this.bpm = bpms;

      // call `meses` data
      const data = await GetCantMes();
      this.cantMes = data;
    } catch (error) {
      console.log(error);
    }
  }
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

.btn-opciones {
  display: none;
}
.p-datatable table {
  width: 99%;
  background-color: #f8f8f8;
}

.p-datatable .p-paginator-bottom {
    width: 99%;
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
