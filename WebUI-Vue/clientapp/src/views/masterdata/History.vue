<template>
  <div class="wrapper vld-parent" ref="formContainer">
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

    <CCard style="border-radius: 16px !important; margin-top: 20px;">
      <CCardHeader style="border-radius: 16px !important; padding-bottom: 0px; border-bottom: transparent !important; background-color: #FFFFFF;">
        <strong>Historial</strong>
      </CCardHeader>
      <CCardBody style="padding-top: 0px; padding-bottom: 0px;">      
        <p>Solicitudes de cambio que actualizaron mi expediente.</p>
      </CCardBody>
    </CCard> 

                      <CRow>
                        <CCol md="6" sm="12" class="mb-1">
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
                        <CCol md="6" sm="12" class="pull-right mb-1" style="">
                          <CButton
                            color="primary"
                            @click="clearFilters"
                            class="pull-right btn btn-info libro-marca-celeste onHoverDark ml-1"
                          >
                            <i class="pi pi-trash"></i>
                            Limpiar filtros
                          </CButton>                          
                        </CCol>
                      </CRow>

      <CRow>
        <CCol col>
          <!-- <CCard style="border-radius: 16px !important; margin-top: 20px;">
            <CCardHeader style="border-radius: 16px !important; border-bottom: transparent !important; background-color: #FFFFFF;">
            </CCardHeader>
            <CCardBody> -->
              <CRow>
                <CCol lg="12">
                  <DataTable
                    ref="dt"
                    :value="listaSolicitudes"
                    :paginator="true"
                    class="p-datatable-colaboradores p-datatable-striped p-datatable-sm"
                    :rows="10"
                    dataKey="idSolicitud"
                    :rowHover="true"
                    :filters.sync="filters"
                    :loading="loading"
                    paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                    currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} solicitudes"
                    sortField="fechaModificacion"
                    :sortOrder="1"
                    stateStorage="session"
                    stateKey="dt-state-history"
                    :autoLayout="true"
                    @update:filters="filterApplied"
                    @filter="filterApplied"
                  >
                    <template #header>


                    </template>
                    <template #empty>
                      No se encontraron solicitudes.
                    </template>
                    <template #loading>
                      Cargando la información de solicitudes. Por favor espere.
                    </template>
                    <!--   <Column
                      selectionMode="multiple"
                      headerStyle="width: 3em"
                    ></Column>-->
                    <Column
                      field="idSolicitud"
                      header="Id"
                      :sortable="true"
                      headerStyle="width:8%;"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Id</span>
                        {{ slotProps.data.idSolicitud }}
                      </template>
                      <!-- <template #filter>
                                <InputText type="text" v-model="filters['name']" class="p-column-filter" placeholder="Search by name"/>
                            </template> -->
                    </Column>
                    <Column
                      header="Tipo de Solicitud"
                      :sortable="true"
                      sortField="tipoSolicitud"
                      filterField="tipoSolicitud"
                      filterMatchMode="contains"
                      headerStyle="width:15%;"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Tipo de Solicitud</span>

                        <a
                          href=""
                          style="cursor: hand;"
                          @click.prevent="mostrarSolicitud(slotProps.data)"
                          >{{ slotProps.data.tipoSolicitud }}</a
                        >
                      </template>
                    </Column>                    
                    <Column
                      header="Autor"
                      :sortable="true"
                      sortField="autor"
                      filterField="autor"
                      filterMatchMode="contains"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Autor</span>
                        {{ slotProps.data.autor }}
                      </template>
                    </Column>                    
                    <Column
                      header="Aprobador"
                      :v-show="this.$root.userRoles.some(p => p == 'xxx')"
                      :sortable="true"
                      sortField="responsable"
                      filterField="responsable"
                      filterMatchMode="contains"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Responsable</span>
                        {{ slotProps.data.responsable }}
                      </template>
                    </Column>
                    <Column
                      header="Última modificación"
                      :v-show="this.$root.userRoles.some(p => p == 'xxx')"
                      :sortable="true"
                      sortField="fechaModificacion"
                      filterField="fechaModificacion"
                      filterMatchMode="contains"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Última modificación</span>
                        {{ formatearFecha(slotProps.data.fechaModificacion) }}
                        <div class="progress" v-if="showMeter(slotProps.data.codigoEstado, slotProps.data.rango)"> 
                           <!-- <div class="progress-bar" v-bind:class="meterClass(slotProps.data.fechaModificacion)" role="progressbar" style="width: 100%" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100">{{getMeterLabel(slotProps.data.fechaModificacion)}}</div> -->
                          <div class="progress-bar"
                               v-bind:class="meterClass(slotProps.data.rango)" 
                               role="progressbar" 
                               style="width: 100%" 
                               aria-valuenow="100" 
                               aria-valuemin="0" 
                               aria-valuemax="100">{{getMeterLabel(slotProps.data.rango)}}</div>
                        </div>
                       
                      </template>
                    </Column>
                    <!-- <Column
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
                    </Column> -->
                  </DataTable>
                </CCol>
              </CRow>
            <!-- </CCardBody>
          </CCard> -->
        </CCol>
      </CRow>
    </div>

    <!-- <CModal
      title="Tipo de Solicitud de Cambio"
      :show.sync="mostrarTiposSolicitud"
      color="primary"
      class="modal-libro"
    >
      <div>
        <CRow class="pt-2">
          <CCol sm="12" lg="12" class="pull-right">
            <CSelect
              label="Seleccione el tipo de solicitud a registrar"
              :options="tiposDeSolicitud"
              :value.sync="solicitudSeleccionada"
              add-label-classes="label-title"
            />
          </CCol>
        </CRow>

        <CRow>
          <CCol col="6">
            <CButton
              block
              color="primary"
              @click="crearSolicitud()"
              class="mr-1  btn btn-info libro-marca-azuloscuro"
            >
              Crear</CButton
            >
          </CCol>

          <CCol col="6">
            <CButton
              block
              color="primary"
              @click="cancelarMostrarTiposSolicitud()"
              class="mr-1  btn btn-info libro-marca-celeste"
            >
              Cancelar</CButton
            >
          </CCol>
        </CRow>
      </div>
      <template #footer>
        <div></div>
      </template>
    </CModal> -->
    <Dialog :visible.sync="mostrarTiposSolicitud" :style="{width: '35vw'}" :modal="true" >
        <template #header>
          <h5>Tipo de Solicitud de Cambio</h5>
        </template>
         <CRow class="pt-2">
          <CCol sm="12" lg="12" class="pull-right">
            <CSelect
              label="Seleccione el tipo de solicitud a registrar"
              :options="tiposDeSolicitud"
              :value.sync="solicitudSeleccionada"
              add-label-classes="label-title"
            />
          </CCol>
        </CRow>

        <template #footer>
            <CButton                            
              color="primary"
              @click="crearSolicitud()"
              class="mr-1  btn btn-info libro-marca-azuloscuro ">
              <span class="pi pi-check"></span> <span>Crear</span>
            </CButton>
            <CButton              
              color="primary"
              @click="cancelarMostrarTiposSolicitud()"
              class="mr-1  btn btn-info libro-marca-celeste">
              <span class="pi pi-times"></span> <span>Cancelar</span>
            </CButton>
        </template>
    </Dialog>
  </div>
</template>

<script>
import Vue from "vue";
import XLSX from "sheetjs-style";

import {
  GetSolicitudesPorPerfil,
  GetSolicitudesBuzon,
  GetAll,
  GetDetail,
  GetColaboradoresDM,
  AsociarDMColaboradorCommand,
  GetEmpresaCorporacion,
  AsociarEmpresaColaboradorCommand,
  GetNivelIdioma,
  GetCertificacion
} from "./request";
//import { obtenerTiposSolicitudes, GetColaboradoresCV } from "../generales/Consultas/request";
import { aplicarFormatoFecha } from "../../utils";

export default {
  name: "History",
  data() {
    return {
      dt: null,
      btnFilter: {
        text: "Mostrar filtros"
      },
      colaboradorSeleccionado: null,
      colaboradoresSeleccionados: [],
      solicitudes: null,
      filters: {},
      loading: true,
      primaryModal: false,
      items: [],
      tiposDeSolicitud: [],
      empresasCorp: [],
      mostrarTiposSolicitud: false,
      solicitudSeleccionada: 0,
      optionsRadio: [
        // "Option1",
        { value: "Todos", label: "Todos", props: { checked: true } },
        {
          value: "Seleccionados",
          label: "Seleccionados",
          props: { disabled: true }
        }
      ],
      syncRadios: "Todos",

      //cv
      exportarModalCV: false,

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
      esGrupo: true,
      
    };
  },
  async mounted() {
    await this.cargarDatos("History");
  },
  methods: {
    formatearFecha(fecha){
      return aplicarFormatoFecha(fecha, "dd-MM-yyyy")
    },
    clearFilters() {
      this.filters = {};
      this.colaboradoresSeleccionados = null;
    },
    
    filterApplied(e) {},
    /**
     * Retorna una promesa para obtener la lista de solicitudes
     */
    async getLista(id, tipo) {
      try {
        return GetSolicitudesBuzon(id, tipo);
      } catch (error) {
        console.log(error);
      }
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
              isConsulta: false
            }
          }),
        500
      );
    },
    /**
     * Modifica el valor de la variable que indica si el modal de tipos de solicitudes debe mostrarse
     */
    async mostrarModalSolicitudes() {
      this.mostrarTiposSolicitud = true;
    },
    /**
     * Obtiene los datos del tipo de solicitud que el usuario desea crear
     * para luego invocar el método que se encarga de mostrar la vista
     * según el tipo de solicitud
     */
    async crearSolicitud() {
      try {
        let inbox = this;
        // oculta popup
        this.cancelarMostrarTiposSolicitud();

        // busca el tipo de solicitud seleccionada por el usuario
        let indice = this.tiposDeSolicitud.findIndex(function(tipo, index) {
          if (tipo.value == inbox.solicitudSeleccionada) return true;
        });
        // carga el formulario según el tipo de solicitud
        this.cargarSolicitud(
          this.obtenerObjetoCargarSolicitud(
            inbox.tiposDeSolicitud[indice].value,
            inbox.tiposDeSolicitud[indice].label,
            null // solicitud nueva
          )
        );
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Obtiene los datos del tipo de solicitud asociada a la solicitud
     * que el usuario desea abrir para luego invocar el método que se
     * encarga de mostrar la vista según el tipo de solicitud
     *
     * @datosSolicitud datos de la solicitud que seleccionada por el usuario
     */
    mostrarSolicitud(datosSolicitud) {
      // carga el formulario según el tipo de solicitud
      this.cargarSolicitud(
        this.obtenerObjetoCargarSolicitud(
          datosSolicitud.idTipoSolicitud,
          datosSolicitud.tipoSolicitud,
          datosSolicitud.idSolicitud
        )
      );
    },
    /**
     * Redirige al usuario a la vista correspondiente del tipo de solicitud
     */
    cargarSolicitud(solicitud) {
      this.$router.push({
        name: "MasterData",
        params: {
          origen: "History", // origen desde donde se accedio la vista
          idTipoSolicitud: solicitud.idTipoSolicitud, // id del tipo de la solicitud generada
          tipoSolicitud: solicitud.tipoSolicitud, // nombre del tipo de la solicitud generada
          idSolicitud: solicitud.idSolicitud
        }
      });
    },
    /**
     * Genera objeto con los datos de la solicitud que se desea mostrar
     */
    obtenerObjetoCargarSolicitud(idTipoSolicitud, tipoSolicitud, idSolicitud) {
      return {
        idTipoSolicitud: idTipoSolicitud,
        tipoSolicitud: tipoSolicitud,
        idSolicitud: idSolicitud
      };
    },
    cancelarMostrarTiposSolicitud() {
      this.mostrarTiposSolicitud = false;
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
        onCancel: this.onCancel
      });
    },
    hideLoader(loader) {
      setTimeout(() => {
        loader.hide();
      }, 300);
    },
    /**
     * Retorna la promesa asociada a la llamada asíncrona
     * del API que devuelve la lista de tipos de solicitud
     * según el perfil del usuario
     */
    async obtenerTiposSolicitudes() {
      return GetSolicitudesPorPerfil();
    },
    /***
     * Carga lo datos requeridos para la vista
     */
    async cargarDatos(tipo) {
      let history = this;
      let loader = history.showLoader();

      history.loading = true;

      await history
        .getLista(this.$root.infoColaboradorActual.id, tipo)
        // lista de solicitudes
        .then(function(result) {
          history.solicitudes = result.data.solicitudesMasterData;
          history.loading = false;
          history.hideLoader(loader);
        })
        .catch(error => {
      
          console.error("(2) Inside error:", error);
        });
    },
     claseEstado: function (estado) {
      return {
        'text-success': estado === 'AP',
        'text-danger': estado === 'DE' || estado === 'DC',
        'text-warning': ! (estado === 'AP' || estado === 'DE' || estado === 'DC'),
      }
    },
    showMeter: function (estado, rango) {
      return !(estado === 'AP' || estado === 'DE' || estado === 'DC')
             && rango != 0;      
    },
    badgeClass(tipoFiltro){
      return {
        'bg-success': tipoFiltro == "A tiempo",
        'bg-warning': tipoFiltro == "Requiere atención",
        'bg-danger': tipoFiltro == "Vencidas",
      }
    },
    meterClass(rango){ //(fecha){
      return {
        'bg-success': rango == 1,
        'bg-warning': rango == 2,
        'bg-danger': rango == 3,
      }
    },
    getMeterLabel(rango){ 
      let label = "";
      switch (rango) {
        case 1:
          label = "<= 3 días"
          break;
        case 2:
          label = "> 3 y <= 8 días"
          break;
        case 3:
          label = "> 8 días"
          break;
      
        default:
          break;
      }
      return label;
    },
    obtenerDiferenciaDias(fecha){
      var ToDate = new Date();
      var fromDate = new Date(fecha);
      var Difference_In_Time = ToDate.getTime() - fromDate.getTime();
      return Difference_In_Time / (1000 * 3600 * 24);
    },
  },
  computed: {    
    listaSolicitudes(){      
        return this.solicitudes;
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
