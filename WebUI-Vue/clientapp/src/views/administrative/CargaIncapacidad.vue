<template>
  <div>
    <div class="wrapper vld-parent">


    <CCard style="border-radius: 16px !important; margin-top: 20px;">
      <CCardHeader style="border-radius: 16px !important;  padding-bottom: 0px; border-bottom: transparent !important; background-color: #FFFFFF;">
        <strong>Carga de incapacidades</strong>
      </CCardHeader>
      <CCardBody style="padding-top: 0px; padding-bottom: 0px;">        
        <p>Lista de archivos procesados.</p>
      </CCardBody>
    </CCard> 

    <CRow>
      <CCol md="8" sm="8" class="mb-2">
         <div class="table-header">
           <span class="p-input-icon-left w-75">
            <i class="pi pi-user" />
            <InputText
            v-model="filters['global']"
            placeholder="Escribe aquí lo que buscas"
            class="w-50"
            />
           </span>
          </div>
      </CCol>
      <CCol md="2" sm="2" class="mb-2 pull-right">
        <div style="text-align: right">
         <CButton
          block
          color="primary"
          @click="verArchivo()"
          class="mr-1 btn pull-right btn btn-info libro-marca-celeste-claro onHoverDark btn-primary"
          >
          <CIcon name="cil-file" /> Ver archivo de
          carga</CButton>
          </div>
      </CCol>
       <CCol md="2" sm="2" class="mb-2 pull-right">
        <div style="text-align: right">
         <CButton
          block
          color="primary"
          @click="cargarArchivo()"
          class="mr-1 btn  btn btn-info libro-marca-celeste-claro onHoverDark btn-primary">
          <CIcon name="cil-plus" />&nbsp;Cargar archivo</CButton>
         </div>
       </CCol>                    
    </CRow>
      <CRow>
        <CCol lg="12">
          <!-- <CCard style="border-radius: 16px !important; margin-top: 20px;">
           <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
              
               <div class="card-header-actions">

              </div>
            </CCardHeader>
            <CCardBody>  -->
              <DataTable
                ref="dt"
                :value="incapacidades"
                :paginator="true"
                class="p-datatable-consultas p-datatable-striped p-datatable-sm"
                :rows="10"
                dataKey="idcargaincapacidades"
                :rowHover="true"
                :filters.sync="filters"
                :loading="loading"
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                :rowsPerPageOptions="[5, 10, 25, 50]"
                currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} cargas"
                sortField="nombre"
                :sortOrder="1"
                :autoLayout="true"
                selectionMode="single"
                :selection.sync="incapacidadSeleccionado"
              >
                <template #header>

                </template>
                <template #empty>
                  No se encontraron cargas de incapacidades.
                </template>
                <template #loading>
                  Cargando la información de cargas de incapacidades. Por favor
                  espere.
                </template>

                <Column
                  field="idcargaincapacidades"
                  header="Id"
                  sortField="idcargaincapacidades"
                  filterField="idcargaincapacidades"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Id de carga</span>
                    {{ slotProps.data.idcargaincapacidades }}
                  </template>
                </Column>

                <Column
                  field="fechaCarga"
                  header="Fecha"
                  sortField="fechaCarga"
                  filterField="fechaCarga"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Fecha de carga</span>
                    {{ slotProps.data.fechaCarga }}
                  </template>
                </Column>

                <Column
                  field="nombrearchivo"
                  header="Nombre del archivo"
                  sortField="nombrearchivo"
                  filterField="nombrearchivo"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:24%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Nombre del archivo</span>
                    {{ slotProps.data.nombrearchivo }}
                  </template>
                </Column>

                <Column
                  field="cantidadregistros"
                  sortField="cantidadregistros"
                  header="Registros"
                  filterField="cantidadregistros"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Cantidad de registros</span>
                    {{ slotProps.data.cantidadregistros }}
                  </template>
                </Column>

                <Column
                  field="cantidadingresados"
                  sortField="cantidadingresados"
                  header="Ingresados"
                  filterField="cantidadingresados"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Cantidad ingresados</span>
                    {{ slotProps.data.cantidadingresados }}
                  </template>
                </Column>

                <Column
                  field="resultadocarga"
                  header="Resultado"
                  :sortable="true"
                  sortField="resultadocarga"
                  filterField="resultadocarga"
                  filterMatchMode="contains"
                  headerStyle="width:10%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Resultado</span>
                    {{ slotProps.data.resultadocarga }}
                  </template>
                </Column>

                <Column
                  field="descripcionvalidacion"
                  sortField="descripcionvalidacion"
                  header="Mensaje"
                  filterField="descripcionvalidacion"
                  filterMatchMode="contains"
                  :sortable="true"
                  headerStyle="width:15%;"
                >
                  <template #body="slotProps">
                    <span class="p-column-title">Mensaje de error</span>
                    {{ slotProps.data["descripcionvalidacion"] }}
                  </template>
                </Column>
              </DataTable>
            <!-- </CCardBody>
          </CCard> -->
        </CCol>
      </CRow>
    </div>

    <!-- Popup para ver el archivo del detalle -->
    <Dialog
      :visible.sync="showArchivo"
      :style="{ width: '85vw' }"
      :modal="true"
    >
      <template #header>
        <h5>Detalle del archivo de carga</h5>
      </template>
      <CRow class="pt-2">
        <CCol sm="12" lg="12" class="pull-right">
          <DataTable
            ref="dt"
            :value="listaArchivo"
            :paginator="true"
            class="p-datatable-consultas p-datatable-striped p-datatable-sm"
            :rows="10"
            dataKey="idcargaincapacidades"
            :rowHover="true"
            :filters.sync="filtersDetalle"
            :loading="loadingDetalle"
            paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
            :rowsPerPageOptions="[5, 10, 25, 50]"
            currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} cargas"
            sortField="nombre"
            :sortOrder="1"
            :autoLayout="true"
          >
            <template #header>
              <CRow>
                <CCol md="9" sm="12" class="mb-2">
                  <div class="table-header">
                    Buscar
                    <span class="p-input-icon-left w-75">
                      <i class="pi pi-user" />
                      <InputText
                        v-model="filtersDetalle['global']"
                        placeholder="Escribe aquí lo que buscas"
                        class="w-50"
                      />
                    </span>
                  </div>
                </CCol>
              </CRow>
            </template>
            <template #empty>
              No se encontraron cargas de incapacidades.
            </template>
            <template #loading>
              Cargando la información de cargas de incapacidades. Por favor
              espere.
            </template>

            <Column
              field="identificacion"
              header="Identificación"
              sortField="identificacion"
              filterField="identificacion"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:12%;"
            >
              <template #body="slotProps">
                <span class="p-column-title">Identificación</span>
                {{ slotProps.data["identificacion"] }}
              </template>
            </Column>

            <Column
              field="nombre"
              header="Nombre del colaborador"
              sortField="nombre"
              filterField="nombre"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:20%;"
            >
              <template #body="slotProps">
                <span class="p-column-title">Nombre del colaborador</span>
                {{ slotProps.data["nombre"] }}
              </template>
            </Column>

            <Column
              field="numboleta"
              sortField="numboleta"
              header="Boleta"
              filterField="numboleta"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:10%;"
            >
              <template #body="slotProps">
                <span class="p-column-title">Número de boleta</span>
                {{ slotProps.data["numboleta"] }}
              </template>
            </Column>

            <Column
              field="fechainicio"
              header="Inicio"
              :sortable="true"
              sortField="fechainicio"
              filterField="fechainicio"
              filterMatchMode="contains"
              headerStyle="width:10%;"
            >
              <template #body="slotProps">
                <span class="p-column-title">Fecha de inicio</span>
                {{ slotProps.data["fechainicio"] }}
              </template>
            </Column>
            <Column
              field="fechafin"
              :sortable="true"
              header="Fin"
              sortField="fechafin"
              filterField="fechafin"
              filterMatchMode="contains"
              headerStyle="width:10%;"
            >
              <template #body="slotProps">
                <span class="p-column-title">Fecha de finalización</span>
                {{ slotProps.data["fechafin"] }}
              </template>
            </Column>

            <Column
              field="cantdias"
              sortField="cantdias"
              header="Días"
              filterField="cantdias"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:8%;"
            >
              <template #body="slotProps">
                <span class="p-column-title">Cant. días</span>
                {{ slotProps.data["cantdias"] }}
              </template>
            </Column>

            <Column
              field="tipo"
              sortField="tipo"
              header="Tipo"
              filterField="tipo"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:8%;"
            >
              <template #body="slotProps">
                <span class="p-column-title">Tipo</span>
                {{ slotProps.data["tipo"] }}
              </template>
            </Column>

            <Column
              field="carga"
              sortField="carga"
              header="Carga"
              filterField="carga"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:8%;"
            >
              <!-- <template #body="slotProps">
                <span class="p-column-title">Carga</span>
                {{ slotProps.data["carga"] }}
              </template> -->
              <template #body="slotProps">
                <span class="p-column-title">Estado</span>
                <span :class="pintaEstado(slotProps.data['carga'], true)">
                  {{ slotProps.data["carga"] }}
                </span>
              </template>
            </Column>
            <Column
              field="resultadocarga"
              sortField="resultadocarga"
              header="Mensaje"
              filterField="resultadocarga"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:15%;"
            >
              <template #body="slotProps">
                <span class="p-column-title">Resultado de carga</span>
                {{ slotProps.data["resultadocarga"] }}
              </template>
            </Column>
          </DataTable>
        </CCol>
      </CRow>

      <template #footer>
        <!-- <CButton                            
              color="primary"
              @click="crearSolicitud()"
              class="mr-1  btn btn-info libro-marca-azuloscuro ">
              <span class="pi pi-check"></span> <span>Crear</span>
            </CButton> -->
        <CButton
          color="primary"
          @click="showArchivo = false"
          class="mr-1 mt-2  btn btn-info libro-marca-celeste"
        >
          <span class="pi pi-times"></span> <span>Cancelar</span>
        </CButton>
      </template>
    </Dialog>
  </div>
</template>

<script>
import { GetCargaIncapacidad } from "./request";
import moment from "moment";
import Vue from "vue";
moment.locale("es");

export default {
  name: "CargaIncapacidad",
  data() {
    return {
      incapacidades: [],
      incapacidadSeleccionado: null,
      loading: true,
      filters: {},
      showArchivo: false,
      listaArchivo: [],
      loadingDetalle: true,
      filtersDetalle: {}
    };
  },
  async mounted() {
    // se cargarian los request en caso de ser necesario o en created...

    await this.cargarTabla();
  },
  methods: {
    pintaEstado(estado, style) {
      if (style) {
        switch (estado) {
          case "Incorrecta":
            return "badge rounded-pill bg-warning p-component";
          case "Correcta":
            return "badge rounded-pill bg-success p-component";

          default:
            break;
        }
      } else {
        switch (estado) {
          case 0:
            return "Incorrecta";
          case 1:
            return "Correcta";

          default:
            break;
        }
      }
    },

    formatoIdentificacion(id) {
      if (id) {
        return id.substring(2, id.length);
      } else {
        return "";
      }
    },
    cargarArchivo() {
      this.$router.push({ name: "Carga de incapacidades" });
    },
    async cargarTabla() {
      const {
        data: { cargas }
      } = await GetCargaIncapacidad();
      this.incapacidades = cargas.map(
        ({
          idcargaincapacidades,
          nombrearchivo,
          cantidadregistros,
          contenidoarchivo,
          resultadocarga,
          descripcionvalidacion,
          fechaCarga,
          cantidadingresados
        }) => ({
          idcargaincapacidades: idcargaincapacidades,
          nombrearchivo: nombrearchivo,
          cantidadregistros: cantidadregistros,
          contenidoarchivo: contenidoarchivo,
          resultadocarga:
            resultadocarga == "X"
              ? "Exitoso"
              : resultadocarga == "E"
              ? "Error"
              : "Sin Definir",
          descripcionvalidacion: descripcionvalidacion,
          fechaCarga: moment(fechaCarga).format("L"),
          cantidadingresados: cantidadingresados
        })
      );
      this.loading = false;
    },

    setTipo(tipo) {
      if (tipo == "E") {
        return "Enfermedad";
      }
      if (tipo == "M") {
        return "Maternidad";
      } else {
        return "Sin Definir";
      }
    },
    verArchivo() {
      var lista;
      if (this.incapacidadSeleccionado) {
        lista = JSON.parse(this.incapacidadSeleccionado.contenidoarchivo);
        const $this = this;
        const options = lista.map(function(row) {
          return {
            //    identificacion: $this.formatoIdentificacion(row["IDENTIFICACION"]),
            identificacion: row["IDENTIFICACION"],
            nombre: row["NOMBRE"],
            numboleta: row["No. BOLETA"],
            fechainicio: row["FECHA INICIO"],
            fechafin: row["FECHA FIN"],
            cantdias: row["DIAS INCAP"],
            tipo: $this.setTipo(row["DESC. INCAPACIDAD"]),
            carga: $this.pintaEstado(row["carga"], false),
            resultadocarga: row["resultadoCarga"]
          };
        });

        this.listaArchivo = options;

        this.showArchivo = true;
        this.loadingDetalle = false;
      } else {
        Vue.$toast.warning("Debe de seleccionar un registro.", {});
      }
    },
    formatearFecha(date) {
      return moment(date).format("L");
    },
    SetTipoIncapacidad(tipo) {
      let res;
      switch (tipo) {
        case "E":
          res = "Enfermedad";
          break;

        case "M":
          res = "Maternidad";

          break;
        default:
          res = "Sin Definir";

          break;
      }

      return res;
    }
  },
  computed: {}
};
</script>

<style>
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

.p-datatable .p-datatable-tbody > tr > td {
    border: 1px solid rgba(175, 189, 216, 1)
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
``
