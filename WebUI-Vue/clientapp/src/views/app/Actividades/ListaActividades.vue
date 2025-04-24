<template>
  <div>
    <CRow>
      <CCol md="12" sm="12" class="pull-right mb-1">
        <CButton
          color="primary"
          class="pull-right btn btn-info libro-marca-celete onHoverDark ml-1"
          @click="toggleFilters"
        >
          <i class="pi pi-filter"></i>
          {{ btnFilter.text }}
        </CButton>
        <CButton
          color="primary"
          class="pull-right btn btn-info libro-marca-celete onHoverDark ml-1"
          @click="clearFilters"
        >
          <i class="pi pi-trash"></i>
          Limpiar filtros
        </CButton>
      </CCol>
    </CRow>

    <DataTable
      ref="dt"
      :value="actividades"
      :paginator="true"
      class="p-datatable-consultas p-datatable-striped p-datatable-sm"
      :rows="10"
      dataKey="idactividad"
      :rowHover="true"
      :selection.sync="selectedActividad"
      :filters.sync="filters"
      :loading="loading"
      paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
      :rowsPerPageOptions="[5, 10, 25, 50]"
      currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} actividades"
      sortField="idactividad"
      :sortOrder="1"
      stateKey="dt-state-actividades"
      selectionMode="single"
      :autoLayout="true"
      @update:filters="filterApplied"
      @filter="filterApplied"
    >
      <template #header>
        <CCollapse :show="!collapse">
          <CCard color="light">
            <CCardBody
              style="
                padding-top: 15px !important;
                padding-bottom: 0px !important;
              "
            >
         
              <CRow>

                <div
                  class="p-field p-col-12 p-md-4"
                  style="padding: 10px 5px 0 5px !important"
                >
                  <span class="p-float-label">
                    <MultiSelect
                      v-model="filters['idiomas']"
                      :options="Languages"
                      optionLabel="name"
                      optionValue="value"
                      class="p-column-filter w-100"
                      display="chip"
                      :filter="true"
                    />
                    <label for="dropdown">Idioma</label>
                  </span>
                </div>
        
              </CRow>
            </CCardBody>
          </CCard>
        </CCollapse>
        <CRow>
          <CCol md="6" sm="6" class="mb-2">
            <div class="table-header w-100">
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
          <CCol md="6" sm="6" class="mb-2">
            <CButton
              color="primary"
              class="
                pull-right
                btn btn-info
                libro-marca-celete
                onHoverDark
                ml-1
              "
              @click="create"
            >
              Crear Actividad
            </CButton>
          </CCol>
        </CRow>
        <div>
          <CBadge
            v-show="filters.idiomas"
            v-for="item in filters.idiomas"
            :key="item.nombre"
            style="background-color: #29abe2"
            class="mr-2"
            >{{ item }}</CBadge
          >

      
        </div>
      </template>
      <template #empty> No se encontraron actividades. </template>
      <template #loading>
        Cargando la información de actividades. Por favor espere.
      </template>
      <Column
        field="nombre"
        header="Nombre"
        :sortable="true"
        sortField="nombre"
        filterField="nombre"
        filterMatchMode="contains"
        headerStyle="width: 60%"
      >
        <template #body="slotProps">
          <span class="p-column-title">Nombre</span>
          <a
            href=""
            style="cursor: pointer"
            @click.prevent="edit(slotProps.data)"
            >{{ slotProps.data.nombre }}</a
          >
        </template>
      </Column>

      <Column
        field="descripcion"
        header="Descripción"
        sortField="descripcion"
        filterField="descripcion"
        filterMatchMode="contains"
        :sortable="true"
        headerStyle="width: 30%"
      >
        <template #body="slotProps">
          <span class="p-column-title">Descripción</span>
          {{ slotProps.data.descripcion }}
        </template>
      </Column>

     
      <Column
        header="Idioma"
        filterField="idiomas"
        filterMatchMode="custom"
        :filterFunction="customFilter"
        headerStyle="display: none"
        bodyStyle="display: none"
      >
      </Column>
   
      <Column header="Acciones" :sortable="false" headerStyle="width: 10%">
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
            <CDropdownItem @click="view(slotProps.data)">
              <CIcon name="cil-magnifying-glass" />&nbsp; Ver
            </CDropdownItem>
            <CDropdownItem @click="deleteActividad(slotProps.data)">
              <CIcon name="cil-delete" />&nbsp; Eliminar
            </CDropdownItem>
          </CDropdown>
        </template>
      </Column>
    </DataTable>
     <confirm-dialogue ref="confirmDialogue"></confirm-dialogue>

  </div>
</template>

<script>
import Vue from "vue";
import common from "../../masterdata/common.js";
import ConfirmDialogue from "../../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import { GetListaActividades, Languages, DeleteActividad } from "../request";
import moment from "moment";

export default {
  name: "listaActividades",
  mixins: [common],
   components: { ConfirmDialogue },
  emits: ["view-new", "create-new", "edit-new"],
  data() {
    return {
      actividades: [],
      Languages: [],
      selectedActividad: null,
      dt: null,
      showAddModal: false,
      loading: true,
      collapse: false,
      btnFilter: {
        text: "Ocultar Filtros"
      },
      filters: {}
    };
  },
  async mounted() {
    this.loading = false;
    await this.loadTable();
    await this.getLanguages();
  },
  methods: {

    async getLanguages() {
      const {
        data: { idiomas }
      } = await Languages();
      this.Languages = idiomas.map(({ nombre }) => ({
        name: nombre,
        value: nombre
      }));
    },
    invalidMessage(fields) {
      let fieldNames = " Revisar los campos: \r\n";
      for (var key in fields) {
        fieldNames += `-  ${fields[key].label}\r\n`;
      }
      return `${common.MENSAJE_ERROR} ${fieldNames}`;
    },
    validationError() {
      Vue.$toast.warning(common.MENSAJE_ERROR, {});
    },
    async loadTable() {
      const {
        data: { actividades }
      } = await GetListaActividades();

      this.actividades = actividades.map(
        ({
          idactividad,
          ididioma,
          nombre,
          descripcion,
          activo,
          fecha,
          enlace,
          lat,
          lng,
          idiomas
        }) => ({
          idactividad: idactividad,
          ididioma: ididioma,
          nombre: nombre,
          descripcion: descripcion,
          activo: activo,
          fecha: fecha,
          enlace: enlace,
          lat: lat,
          lng: lng,
          idiomas: idiomas
        })
      );
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    filterApplied(_e) {},
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
      return false;
    },
    clearFilters() {
      this.filters = {};
      this.selectedConvenio = null;
    },
    toggleFilters() {
      this.collapse = !this.collapse;
      this.btnFilter.text = this.collapse
        ? "Mostrar Filtros"
        : "Ocultar Filtros";
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    view(item, _x, _event) {
      this.selectedActividad = item;
      this.$emit("view", this.selectedActividad.idactividad);
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    edit(item, _x, _event) {
      this.selectedActividad = item;
      this.$emit("edit", this.selectedActividad.idactividad);
    },
    create() {
      this.$emit("create");
    },
    /*eslint no-unused-vars: ["error", { "argsIgnorePattern": "^_" }]*/
    async deleteActividad(item, _x, _event) {
      const id = item.idactividad;
      const res = await this.Confirmacion_Generico(
        `¿Desea eliminar la actividad?`,
        "Eliminar Actividad"
      );
      if (res) {
        await DeleteActividad(id)
          .then(() => {
            Vue.$toast.success(
              "La actividad fue eliminada exitosamente.",
              {}
            );
          })
          .catch((response) => {
            console.log(response.Error);
            Vue.$toast.error("La actividad no pudo eliminarse.");
          });
        await this.loadTable();
      } else {
        // mostrar error
      }
    },
  },
  computed: {}
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

.p-datatable {
  background-color: white;
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

/* soporte a responsive para que cada fila de la tabla se presente mediante la combinación de etiqueta y valor asociado */

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
</style>
