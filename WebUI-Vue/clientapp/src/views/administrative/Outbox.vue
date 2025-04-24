<template>
  <div class="wrapper vld-parent" ref="formContainer">
    <div>

     <CCard style="border-radius: 16px !important; margin-top: 20px;">
      <CCardHeader style="border-radius: 16px !important;  padding-bottom: 0px; border-bottom: transparent !important; background-color: #FFFFFF;">
        <strong>Mis trámites</strong>
      </CCardHeader>
      <CCardBody style="padding-top: 0px; padding-bottom: 0px;">        
        <p>Lista de certificaciones solicitadas.</p>
      </CCardBody>
    </CCard> 

                      <CRow>
                        <CCol md="8" sm="12" class="mb-1">
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
                        <CCol md="2" sm="4" class="pull-right mb-1" style="">
                          <CButton
                            color="primary"
                            @click="clearFilters"
                            class="pull-right btn btn-info libro-marca-celeste onHoverDark w-100"
                          >
                            <i class="pi pi-trash"></i>
                            Limpiar filtros</CButton
                          >
                        </CCol>
                        <CCol md="2" sm="4" class="pull-right mb-1" style="">
                         <CButton
                          block
                          color="primary"
                          @click="mostrarModalSolicitudes()"
                          class="pull-right btn btn-info libro-marca-celeste onHoverDark w-100"
                          v-if="this.tiposDeSolicitud.length > 0"
                           >
                             <CIcon name="cil-plus" />&nbsp;Crear solicitud</CButton
                           >
                        </CCol>                        
                      </CRow>

      <CRow>
        <CCol col>
          <!-- <CCard style="border-radius: 16px !important; margin-top: 20px;">
           <CCardHeader style="border-radius: 16px !important;  padding-bottom: 0px; border-bottom: transparent !important; background-color: #FFFFFF;">
            </CCardHeader>
            <CCardBody> -->
              <CRow>
                <CCol lg="12">
                  <!-- Tabla con la lista de solicitudes -->
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
                    stateKey="dt-state-tramitadas-admin"
                    :autoLayout="true"
                    @update:filters="filterApplied"
                    @filter="filterApplied"
                  >
                    <!-- Personalización de las secciones de la tabla -->
                    <template #header>
               
                      <div>
                        <CBadge
                          v-if="filters.global"
                           style="background-color:#29abe2"
                          class="mr-2"
                        >
                          {{ filters.global }}
                        </CBadge>
                      </div>
                    </template>
                    <template #empty>
                      No se encontraron solicitudes.
                    </template>
                    <template #loading>
                      Cargando la información de solicitudes. Por favor espere.
                    </template>

                    <!-- Identificador solicitud -->
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

                    <!-- Tipo de solicitud -->
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

                    <!-- Estado de la solicitud -->
                    <Column
                      header="Estado"
                      :sortable="true"
                      sortField="estado"
                      filterField="estado"
                      filterMatchMode="contains"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Estado</span>
                        <i
                          style="font-size:12px;"
                          class="pi pi-circle-on"
                          v-bind:class="
                            claseEstado(slotProps.data.codigoEstado)
                          "
                        ></i>
                        {{ slotProps.data.estado }}
                      </template>
                    </Column>

                    <!-- Solicitante -->
                    <Column
                      header="Solicitante"
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

                    <!-- Responsable -->
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

                    <!-- Solicitada el -->
                    <Column
                      header="Solicitada"
                      :sortable="true"
                      sortField="fechaCreacion"
                      filterField="fechaCreacion"
                      filterMatchMode="contains"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Última modificación</span>
                        {{ formatearFecha(slotProps.data.fechaCreacion) }}
                      </template>
                    </Column>

                    <!-- Atendida el -->
                    <Column
                      header="Atendida"
                      :sortable="true"
                      sortField="fechaModificacion"
                      filterField="fechaModificacion"
                      filterMatchMode="contains"
                    >
                      <template #body="slotProps">
                        <span class="p-column-title">Última modificación</span>
                        {{
                          slotProps.data.codigoEstado == "EP"
                            ? "Pendiente"
                            : formatearFecha(slotProps.data.fechaModificacion)
                        }}
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

    <!-- Popup para seleccionar el tipo de certificación a solicitar -->
    <Dialog
      :visible.sync="mostrarTiposSolicitud"
      :style="{ width: '35vw' }"
      :modal="true"
    >
      <template #header>
        <h5>Solicitud Administrativa</h5>
      </template>
      <CRow class="pt-2">
        <CCol sm="12" lg="12" class="pull-right">
          <CSelect
            label="Seleccione el tipo de solicitud"
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
          class="mr-1  btn btn-info libro-marca-azuloscuro "
        >
          <span class="pi pi-check"></span> <span>Crear</span>
        </CButton>
        <CButton
          color="primary"
          @click="cancelarMostrarTiposSolicitud()"
          class="mr-1  btn btn-info libro-marca-celeste"
        >
          <span class="pi pi-times"></span> <span>Cancelar</span>
        </CButton>
      </template>
    </Dialog>
  </div>
</template>

<script>
// métodos comunes
import common_admin from "./common-admin.js";
import common from "../masterdata/common.js";

// Llamadas AJAX
import {
  GetSolicitudesPorPerfil,
  GetSolicitudesBuzon,
  GetColaboradorDetailResumen
} from "./request";

export default {
  name: "Outbox",
  mixins: [common_admin, common],
  data() {
    return {
      dt: null,
      solicitudes: null,
      filters: {},
      loading: true,
      tiposDeSolicitud: [],
      mostrarTiposSolicitud: false,
      solicitudSeleccionada: 0,
      loader: null
    };
  },
  async mounted() {
    await this.cargarDatos();
    await this.PermiteVacaciones(this.$root.infoColaboradorActual.id);
  },
  methods: {
    clearFilters() {
      this.filters = {};
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
      console.log(datosSolicitud,'srr');
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
        name: "Administrative",
        params: {
          origen: "CertificacionesTramitadas", // origen desde donde se accedio la vista
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
        .getLista(this.$root.infoColaboradorActual.id, "TramitadasAdmin")
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
    claseEstado: function(estado) {
      return {
        "text-success": estado === "AP",
        "text-danger": estado === "DE" || estado === "DC" || estado === "AN",
        "text-warning": !(estado === "AP" || estado === "DE" || estado === "DC")
      };
    },

    async PermiteVacaciones(id) {
      const {
        data: { colaborador }
      } = await GetColaboradorDetailResumen(id);

      if (colaborador) {
        if (!colaborador.solicitavacaciones) {
          this.tiposDeSolicitud = this.tiposDeSolicitud.filter(
            x => x.label != 'Solicitud de vacaciones'
          );
        }
      }
    }
  }
};
</script>

<style>
@import "styles.css";

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
</style>
