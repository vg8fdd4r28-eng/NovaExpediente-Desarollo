<template>
  <div>
    <div class="wrapper vld-parent">
      <CRow>
        <CCol lg="12">
          <CCard>
            <CCardHeader>
              <CIcon name="cil-justify-center" /><strong> Infraestructura</strong>
              <small> Lista de infraestructuras</small>
            </CCardHeader>
            <CCardBody>
              <DataTable
                ref="dt"
                :value="infraestructuras"
                :paginator="true"
                class="p-datatable-consultas p-datatable-striped p-datatable-sm"
                :rows="10"
                dataKey="idinfraestructura"
                :rowHover="true"
                :selection.sync="infraestructuraSeleccionado"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} infraestructuras"
                sortField="nombre"
                :sortOrder="1"
                stateKey="dt-state-infraestructuras"
                selectionMode="single"
                :autoLayout="true"
              >
                <template #header>
                  <!--<CRow>
                  <CCol md="3" sm="12" class="mb-2">
                    <div style="text-align: left">
                      <CButton
                        id="btnExportarColaboradoresConsulta"
                        block
                        color="primary"
                        @click="exportCSV($event)"
                        class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark w-100 btn-primary"
                      >
                        <CIcon
                          name="cil-file"
                        />&nbsp;Exportar</CButton
                      >
                    </div>
                  </CCol>
                  <CCol md="3" sm="12" class="mb-2">
                    <div style="text-align: left">
                      <CButton
                        id="btnExportarColaboradoresConsulta"
                        block
                        color="primary"
                        @click="exportSelectedCSV($event)"
                        class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark w-100 btn-primary"
                      >
                        <CIcon name="cil-file" />&nbsp;Exportar
                        seleccionados</CButton
                      >
                    </div>
                  </CCol>
                </CRow>-->
                  <CRow>
                    <CCol md="8" sm="12" class="mb-2">
                      <div class="table-header">
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
                <template #empty> No se encontraron infraestructuras. </template>
                <template #loading>
                  Cargando la información de infraestructuras. Por favor espere.
                </template>
                <Column
                  field="Nombre"
                  header="Nombre"
                  sortField="nombre"
                  filterField="nombre"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:40%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Nombre</span>
                    {{ slotProps.data.nombre }}
                  </template>
                </Column>

                <Column
                  field="descripcion"
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
              </DataTable>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </div>
  </div>
</template>


<script>
import { GetInfraestructura } from "./request";

export default {
  name: "infraestructuras",
  data() {
    return {
      infraestructuras: [],
      infraestructuraSeleccionado: [],
      loading: true,
      filters: {},
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...
    this.loading = false;
    await this.cargarTabla();
  },
  methods: {
    async cargarTabla() {
      const {
        data: { infraestructuras },
      } = await GetInfraestructura();
      this.infraestructuras = infraestructuras.map(({ idinfraestructura, nombre, descripcion }) => ({
        idinfraestructura: idinfraestructura,
        nombre: nombre,
        descripcion: descripcion,
      }));
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
.p-datatable-consultas .p-datatable-tbody > tr > td .p-column-title {
  display: none;
}
/* configuración del encabezado y cuerpo de la tabla */
.p-datatable-consultas .p-datatable-thead > tr > th {
  background-color: rgb(12, 123, 190) !important;
  color: white !important;
}
.p-datatable-consultas .p-datatable-tbody > tr > td {
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
  .p-datatable.p-datatable-consultas .p-datatable-thead > tr > th {
    display: none !important;
  }

  .p-datatable.p-datatable-consultas .p-datatable-tfoot > tr > td {
    display: none !important;
  }

  .p-datatable.p-datatable-consultas .p-datatable-tbody > tr > td {
    text-align: left;
    display: block;
    width: 100%;
    float: left;
    clear: left;
    border: 0 none;
  }

  .p-datatable-consultas .p-datatable-tbody > tr > td .p-column-title {
    padding: 0.4rem;
    min-width: 30%;
    display: inline-block !important;
    margin: -0.4em 1em -0.4em -0.4rem;
    font-weight: bold;
    width: 100%;
  }

  .p-datatable.p-datatable-consultas:last-child {
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