<template>
  <CCard class="mt-2">
    <CCardHeader>
      <span
        >Seleccione {{ generoTipoListaComputed }} {{ nombreTipoLista.toLowerCase() }} de la
        lista</span
      >
    </CCardHeader>
    <CCardBody>
      <CRow>
        <CCol lg="12">
          <DataTable
            ref="dt"
            :value="dataContenido"
            :paginator="true"
            class="p-datatable-picklist p-datatable-striped p-datatable-sm"
            :rows="5"
            dataKey="id"
            :rowHover="true"
            :selection.sync="contenidoSeleccionado"
            :filters.sync="filters"
            :loading="loading"
            paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
            :rowsPerPageOptions="[5, 10, 25, 50]"
            currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} de registros"
            sortField="nombreCompleto"
            :sortOrder="1"
            selectionMode="single"
            :autoLayout="true"
          >
            <template #header>
              <CRow>
                <CCol md="8" sm="12" class="mb-2">
                  <div class="table-header ">
                    Buscar
                    <span class="p-input-icon-left w-75">
                      <i class="pi pi-search" />
                      <InputText
                        v-model="filters['global']"
                        placeholder="escriba un texto"
                        class="w-100"
                      />
                    </span>
                  </div>
                </CCol>
              </CRow>
            </template>
            <template #empty>
              No se encontraron registros.
            </template>
            <template #loading>
              Cargando la información de selección dinámica. Por favor espere.
            </template>
            <Column
              v-for="col of columnas"
              :field="col.field"
              :header="col.header"
              :key="col.field"
              :sortable="true"
              :sortField="col.field"
              :filterField="col.field"
              filterMatchMode="contains"
            ></Column>
          </DataTable>
        </CCol>
      </CRow>
    </CCardBody>
    <CCardFooter>
      <CRow>
        <CCol col="12" class="pull-right">
          <CButton
            color="primary"
            @click="aceptar()"
            class="mr-1 pull-right btn btn-info libro-marca-azuloscuro"
            :disabled="contenidoSeleccionado.length === 0"
          >
            Seleccionar</CButton
          >
        </CCol>
      </CRow>
    </CCardFooter>
  </CCard>
</template>

<script>
//import { Upsert } from "./request";

export default {
  name: "PickList",
  props: {
    nombreTipoLista: { type: String, default: "" },
    urlApi: { type: String, default: "" },
    columnas: { type: Array, default: () => [] }, //columnas y nombre de header
    dataContenido: { type: Array, type: Array, default: () => [] }, //ES6
    generoTipoLista: { type: String, default: "" }
  },
  data() {
    return {
      resultado: null,
      contenidoTabla: [],
      contenidoSeleccionado: [],
      loading: true,
      filters: {}
    };
  },
  mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    this.loading = false;
    this.filters = {};
  },
  methods: {
    async aceptar() {
      try {
        this.$emit("selected", this.contenidoSeleccionado);
        this.filters = {};
      } catch (error) {
        console.log(error);
      }
    }
  },
  computed: {
    generoTipoListaComputed() {
      return this.generoTipoLista == "M" ? "un" : "una";
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

.btn-opciones {
  display: none;
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
}
/* FIN */
</style>
