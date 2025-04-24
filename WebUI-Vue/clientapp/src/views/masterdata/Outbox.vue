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
      <CCardHeader style="border-radius: 16px !important;  padding-bottom: 0px; border-bottom: transparent !important; background-color: #FFFFFF;">
        <strong>Solicitudes tramitadas</strong>
      </CCardHeader>
      <CCardBody style="padding-top: 0px; padding-bottom: 0px;">        
        <p>Te encuentras en la sección de Solicitudes tramitadas, donde podés revisar el historial de solicitudes, crear una nueva solicitud, 
          revisar el estado, la última modificación y el responsable de revisarte y aprobarte la solicitud que creaste.</p>
      </CCardBody>
    </CCard> 

        <CRow class="mt-1">
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
            <CCol md="6" sm="12" class="pull-right mb-1">
                <CButton
                  block
                  color="primary"
                  style="width: 230px;"
                  @click="mostrarModalSolicitudes()"
                  class="mt-1 mr-1 btn btn-info mt-1 pull-right libro-marca-celeste onHoverDark "
                  v-if="this.tiposDeSolicitud.length > 0"
                >
                  <CIcon name="cil-plus" />&nbsp;Crear solicitud</CButton
                >
            </CCol>
        </CRow>

    <CRow>
                <CCol lg="12">
                  <DataTable 
                    ref="dt"
                    :value="solicitudes"
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
                    sortField="idSolicitud"
                    :sortOrder="-1"
                    stateStorage="session"
                    stateKey="dt-state-tramitadas"
                    :autoLayout="true"
                    @update:filters="filterApplied"
                    @filter="filterApplied"
                   
                  >
                    <template #header>

                      <div>

                        <CBadge
                          v-if="filters.global"
                           style="background-color:#29abe2"
                          class="mr-2"
                        >
                          {{ filters.global }}
                        </CBadge>
                        <CBadge
                          v-show="filters.idiomas"
                          v-for="item in filters.idiomas"
                          :key="item.idnivelidioma"
                           style="background-color:#29abe2"
                          class="mr-2"
                        >
                          {{ item }}
                        </CBadge>
                        <CBadge
                          v-show="filters.certificaciones"
                          v-for="item in filters.certificaciones"
                          :key="item.idcertificacion"
                           style="background-color:#29abe2"
                          class="mr-2"
                        >
                          {{ item }}
                        </CBadge>
                      </div>
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
                      header="Estado"
                      :sortable="true"
                      sortField="estado"
                      filterField="estado"
                      filterMatchMode="contains"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Estado</span>
                        <i style="font-size:12px;" class="pi pi-circle-on" v-bind:class="claseEstado(slotProps.data.codigoEstado)"></i> {{ slotProps.data.estado }}
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
                      header="Colaborador"
                      :sortable="true"
                      sortField="nombreColaborador"
                      filterField="nombreColaborador"
                      filterMatchMode="contains"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Colaborador</span>
                        {{ slotProps.data.nombreColaborador }}
                      </template>
                    </Column>
                    <Column
                      header="Responsable"
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
                        <!-- <br />
                        <meter
                          max="365"
                          min="0"
                          high="30"
                          low="0"
                          :value="30"
                          optimum="7"
                          v-if="mostrarMeter(slotProps.data.codigoEstado)"
                        ></meter> -->
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

    </div>
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
  name: "Outbox",
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
      esGrupo: true
    };
  },
  async mounted() {
    await this.cargarDatos();
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
        // return new Promise(function(resolve, reject) {
        //                       resolve([
        //                                 {
        //                                   idSolicitud : 1,
        //                                   idTipoSolicitud: 1,
        //                                   tipoSolicitud : "Datos Personales",
        //                                   codigoEstado: "RE",
        //                                   estado: "Registrada",
        //                                   idAutor: 3,
        //                                   autor: "Marvin Gutiérrez Navarro",
        //                                   idColaborador: 3,
        //                                   nombreColaborador: "Marvin Gutiérrez Navarro",
        //                                   idResponsable: 3,
        //                                   responsable: "Marvin Gutiérrez Navarro"
        //                                 },
        //                                 {
        //                                   idSolicitud : 2,
        //                                   idTipoSolicitud: 1,
        //                                   tipoSolicitud : "Datos Personales",
        //                                   codigoEstado: "CO",
        //                                   estado: "En corrección",
        //                                   idAutor: 3,
        //                                   autor: "Marvin Gutiérrez Navarro",
        //                                   idColaborador: 656,
        //                                   nombreColaborador: "Allan Rodrigo Vado Loaiza",
        //                                   idResponsable: 3,
        //                                   responsable: "Marvin Gutiérrez Navarro"
        //                                 },
        //                                 {
        //                                   idSolicitud : 3,
        //                                   idTipoSolicitud: 3,
        //                                   tipoSolicitud : "Idiomas (Nivel de idioma)",
        //                                   codigoEstado: "RE",
        //                                   estado: "Registrada",
        //                                   idAutor: 3,
        //                                   autor: "Marvin Gutiérrez Navarro",
        //                                   idColaborador: 667,
        //                                   nombreColaborador: "Egdar Rolando Orozco",
        //                                   idResponsable: 3,
        //                                   responsable: "Marvin Gutiérrez Navarro"
        //                                 },
        //                                 {
        //                                   idSolicitud : 4,
        //                                   idTipoSolicitud: 10,
        //                                   tipoSolicitud : "Ubicación colaborador",
        //                                   codigoEstado: "RE",
        //                                   estado: "Registrada",
        //                                   idAutor: 3,
        //                                   autor: "Marvin Gutiérrez Navarro",
        //                                   idColaborador: 688,
        //                                   nombreColaborador: "Sergio Rivera",
        //                                   idResponsable: 3,
        //                                   responsable: "Marvin Gutiérrez Navarro"
        //                                 }

        //                               ]);
        // });
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
      console.log(datosSolicitud,'srrOUTBOX');
      this.cargarSolicitud(
        this.obtenerObjetoCargarSolicitud(
          datosSolicitud.idTipoSolicitud,
          datosSolicitud.tipoSolicitud,
          datosSolicitud.idSolicitud,
          datosSolicitud.idusuarioresponsable
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
          origen: (solicitud.idSolicitud ? "Outbox" : "Inbox"), // origen desde donde se accedio la vista
          idTipoSolicitud: solicitud.idTipoSolicitud, // id del tipo de la solicitud generada
          tipoSolicitud: solicitud.tipoSolicitud, // nombre del tipo de la solicitud generada
          idSolicitud: solicitud.idSolicitud,// id de la solicitud
          idResponsable:solicitud.idResponsable // id  del responsable de la solicitud
        }
      });
    },
    /**
     * Genera objeto con los datos de la solicitud que se desea mostrar
     */
    obtenerObjetoCargarSolicitud(idTipoSolicitud, tipoSolicitud, idSolicitud,idResponsable) {
      return {
        idTipoSolicitud: idTipoSolicitud,
        tipoSolicitud: tipoSolicitud,
        idSolicitud: idSolicitud,
        idResponsable: idResponsable
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
    async cargarDatos() {
      let inbox = this;
      let loader = inbox.showLoader();

      inbox.loading = true;

      await inbox
        .getLista(this.$root.infoColaboradorActual.id, "Tramitadas")
        // lista de solicitudes
        .then(function(result) {
          inbox.solicitudes = result.data.solicitudesMasterData;
          inbox.loading = false;
          inbox.hideLoader(loader);
          return inbox.obtenerTiposSolicitudes();
        })
        // tipos de solicitudes
        .then(function(result) {
          inbox.tiposDeSolicitud = result.data.tiposDeSolicitud.map(
            ({ idTipoSolicitud, tipoSolicitud }) => ({
              value: idTipoSolicitud,
              label: tipoSolicitud
            })
          );
          if (inbox.tiposDeSolicitud.length > 0)
            inbox.solicitudSeleccionada = inbox.tiposDeSolicitud[0].value;
          else inbox.solicitudSeleccionada = 0;
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
    mostrarMeter(estado){
  
      if (estado != "AP") {
        return true;
      }
      else{
        return false;
      }
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
