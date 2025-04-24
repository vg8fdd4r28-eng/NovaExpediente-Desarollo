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
            <strong>Reporte de Cursos</strong>
          </CCardHeader>
          <CCardBody style="padding-top: 0px; padding-bottom: 0px">
            <p>Cursos por Colaborador.</p>
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
          <CCol md="3" sm="4" class="mb-2" style=""> </CCol>
          <CCol md="2" sm="4" class="ml-10 mb-2" style="">
            <CButton
              color="primary"
              @click="showModal()"
              class="pull-rigth btn btn-info libro-marca-celeste onHoverDark w-100"
            >
              <i class="pi pi-download"></i>
              Exportar</CButton
            >
          </CCol>
        </CRow>
  
        <CRow>
          <CCol col>
            <CRow>
              <CCol lg="12">
                <DataTable
                  ref="dt"
                  :value="reporte"
                  :paginator="true"
                  class="p-datatable-colaboradores p-datatable-striped p-datatable-sm"
                  :rows="10"
                  dataKey="identificacion"
                  :rowHover="true"
                  :selection.sync="lineasSeleccionadas"
                  :filters.sync="filters"
                  :loading="loading"
                  paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                  :rowsPerPageOptions="[5, 10, 25, 50]"
                  currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} cursos"
                  :sortOrder="1"
                  stateStorage="session"
                  stateKey="dt-state-reporte-cursos"
                  selectionMode="single"
                  :autoLayout="true"
                  @update:filters="filterApplied"
                  @filter="filterApplied"
                >
                  <template #empty> No se encontraron cursos. </template>
                  <template #loading>
                    Cargando la información de cursos. Por favor espere.
                  </template>
                  <!--   <Column
                        selectionMode="multiple"
                        headerStyle="width: 3em"
                      ></Column>-->
                  <Column
                    field="identificacion"
                    header="Id"
                    :sortable="true"
                   
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Id</span>
                      {{ slotProps.data.identificacion }}
                    </template>
                  </Column>
  
                  <Column
                    header="Nombre"
                    :sortable="true"
                    sortField="nombre"
                    filterField="nombre"
                    filterMatchMode="contains"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Nombre</span>
                      <a style="cursor: hand">{{ slotProps.data.nombre }}</a>
                    </template>
                  </Column>
                  <Column
                    header="Correo"
                    :sortable="true"
                    sortField="correo"
                    filterField="correo"
                    filterMatchMode="contains"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Correo</span>
                      <a style="cursor: hand">{{ slotProps.data.correo }}</a>
                    </template>
                  </Column>
                  <Column
                    header="Genero"
                    :sortable="true"
                    sortField="genero"
                    filterField="genero"
                    filterMatchMode="contains"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Genero</span>
                      <a style="cursor: hand">{{ slotProps.data.genero }}</a>
                    </template>
                  </Column>
                  <Column
                    header="Activo"
                    :sortable="true"
                    sortField="activo"
                    filterField="activo"
                    filterMatchMode="contains"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Activo</span>
                      {{ slotProps.data.activo }}
                    </template>
                  </Column>
                  
                  <Column
                    header="Entidad"
                    :sortable="true"
                    sortField="entidad"
                    filterField="entidad"
                    filterMatchMode="contains"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Entidad</span>
                      {{ slotProps.data.entidad }}
                    </template>
                  </Column>
                  <Column
                    header="Curso"
                    :sortable="true"
                    sortField="curso"
                    filterField="curso"
                    filterMatchMode="contains"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Curso</span>
                      {{ slotProps.data.curso }}
                    </template>
                  </Column>
                  <Column
                    header="Cantidad Horas"
                    :sortable="true"
                    sortField="cantidadHoras"
                    filterField="cantidadHoras"
                    filterMatchMode="contains"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Cantidad Horas</span>
                      {{ slotProps.data.cantidadHoras }}
                    </template>
                  </Column>
                  <Column
                    header="Fecha de curso"
                    :sortable="true"
                    sortField="fechaCurso"
                    filterField="fechaCurso"
                    filterMatchMode="contains"
                    headerStyle="width:10%;"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Fecha de curso</span>
                      {{ slotProps.data.fechaCurso }}
                    </template>
                  </Column>

                  <Column
                    header="Fecha de ingreso"
                    :sortable="true"
                    sortField="fechaIngreso"
                    filterField="fechaIngreso"
                    filterMatchMode="contains"
                    headerStyle="width:10%;"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Fecha de ingreso</span>
                      {{ slotProps.data.fechaIngreso }}
                    </template>
                  </Column>
                  <Column
                    header="Cliente"
                    :sortable="true"
                    sortField="cliente"
                    filterField="cliente"
                    filterMatchMode="contains"
                    headerStyle="width:10%;"
                  >
                    <template #body="slotProps">
                      <span class="p-column-title">Cliente</span>
                      {{ slotProps.data.cliente }}
                    </template>
                  </Column>
                </DataTable>
              </CCol>
            </CRow>
      
          </CCol>
        </CRow>
      </div>
  
      <CModal
        title="Exportar Reporte"
        :show.sync="exportarModal"
        color="primary"
        class="modal-libro"
      >
        <div>
          <CRow class="pt-2 mb-4">
            <CCol sm="12" lg="12" class="pull-right">
              <label
                >Desea exportar el reporte?</label
              >
              <!-- <CInputRadioGroup
                class="col-sm-12"
                :options="optionsRadio"
                inline
                :checked.sync="syncRadios"
              ></CInputRadioGroup> -->
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
    </div>
  </template>
  
  <script>
  import Vue from "vue";
  import { columnas } from "./constantes";
  //import XLSX from "xlsx";
  import XLSX from "sheetjs-style";
  
  import { GetReporteCursosColaborador } from "./request";
  
  export default {
    name: "ReporteCursos",
    data() {
      return {
        dt: null,
        lineasSeleccionadas: [],
        reporte: null,
        filters: {},
        loading: true,
        primaryModal: false,
        items: [],
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
        loader: null
      };
    },
    async mounted() {
      let loader = this.showLoader();
      this.getLista();
  
      this.loading = false;
      this.hideLoader(loader);
    },
    methods: {
      clearFilters() {
        this.filters = {};
        this.bitacorasSeleccionadas = null;
      },
      filterApplied(e) {},
  
      async getLista() {
        try {
          let  {data}  = await GetReporteCursosColaborador();

    this.reporte = data.map(
        ({ ACTIVO, CORREO,IDENTIFICACION,NOMBRE,CURSO,FECHANE,FECHACURSO,NOMBREENTIDAD,GENERO,CLIENTE,CANTIDADHORAS}) => ({
          activo: ACTIVO,
          correo: CORREO,
          identificacion: IDENTIFICACION,
          nombre: NOMBRE,
          curso: CURSO,
          fechaIngreso: FECHANE,
          fechaCurso: FECHACURSO,
          entidad: NOMBREENTIDAD,
          genero: GENERO,
          cliente: CLIENTE,
          cantidadHoras: CANTIDADHORAS
        })
      );
     


        } catch (error) {
          console.log(error);
        }
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
  
            //filtrar por activo o inactivo
            var data = [];
         
              data = this.reporte;


            data = data.map(
              ({
                identificacion,
                nombre,
                correo,
                activo,
                curso,
                fechaIngreso,
                fechaCurso,
                entidad,
                genero,
                cliente, 
                cantidadHoras
              }) => ({
                Identificacion: identificacion,
                Nombre: nombre,
                Correo: correo,
                Activo: activo,
                Genereo: genero,
                Curso: curso,
                Entidad: entidad,
                Fecha_Curso: fechaCurso,
                Fecha_Ingreso: fechaIngreso,
                Cliente:cliente,
                Cantidad_Horas:cantidadHoras

              })
            );
  
            const wb = XLSX.utils.book_new();
            const ws = XLSX.utils.json_to_sheet(data);
            XLSX.utils.book_append_sheet(wb, ws, "CursosColaborador.csv");
            XLSX.writeFile(wb, `ReporteCursosColaborador${this.getDateTime()}.csv`);
            Vue.$toast.success("Se ha exportado la información correctamente.");
            this.exportarModal = false;

          
   
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
  