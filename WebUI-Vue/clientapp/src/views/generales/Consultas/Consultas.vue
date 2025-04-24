<template>
  <div class="wrapper vld-parent" ref="formContainer">
    <div>
      <CRow>
        <CCol col>
          <CCard>
            <CCardHeader>
              <CIcon name="cil-justify-center" /><strong> Colaboradores</strong>
              <small> Lista de colaboradores</small>
              <div class="card-header-actions">
                <CButton
                  block
                  color="primary"
                  @click="agregar()"
                  class="mr-1"
                  style="display:none"
                >
                  <CIcon name="cil-plus" />&nbsp;Agregar</CButton
                >
              </div>
            </CCardHeader>
            <CCardBody>
              <CRow>
                <CCol lg="12">
                  <DataTable :value="colaboradores" :paginator="true" class="p-datatable-customers p-datatable-striped p-datatable-sm" :rows="10"
                        dataKey="idcolaborador" :rowHover="true" :selection.sync="colaboradorSeleccionado" :filters="filters" :loading="loading"
                        paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown" :rowsPerPageOptions="[5,10,25,50]"
                        currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} registros">
                        <template #header>
                            <div class="table-header">
                                Buscar
                                <span class="p-input-icon-left">
                                    <i class="pi pi-search" />
                                    <InputText v-model="filters['global']" placeholder="escriba un texto" />
                                </span>
                            </div>
                        </template>
                        <template #empty>
                            No se encontraron colaboradores.
                        </template>
                        <template #loading>
                            Cargando la información de colaboradores. Por favor espere.
                        </template>
                        <Column selectionMode="single" headerStyle="width: 3em"></Column> 
                        <Column field="identificacion" header="Identificación" :sortable="true">
                            <template #body="slotProps">
                                <span class="p-column-title">Identificación</span>
                                {{slotProps.data.identificacion}}
                            </template>
                            <!-- <template #filter>
                                <InputText type="text" v-model="filters['name']" class="p-column-filter" placeholder="Search by name"/>
                            </template> -->
                        </Column>
                        <Column header="Nombre" :sortable="true" sortField="nombre" filterField="nombre" filterMatchMode="contains">
                            <template #body="slotProps">
                                <span class="p-column-title">Nombre</span>
                                {{slotProps.data.nombre}}
                            </template>
                        </Column>
                        <Column header="Primer Apellido" :sortable="true" sortField="primerapellido" filterField="primerapellido" filterMatchMode="contains">
                            <template #body="slotProps">
                                <span class="p-column-title">Primer Apellido</span>
                                {{slotProps.data.primerapellido}}
                            </template>
                        </Column>
                        <Column header="Segundo Apellido" :sortable="true" sortField="segundoapellido" filterField="segundoapellido" filterMatchMode="contains">
                            <template #body="slotProps">
                                <span class="p-column-title">Segundo Apellido</span>
                                {{slotProps.data.segundoapellido}}
                            </template>
                        </Column>
                        <Column header="Puesto" :sortable="true" sortField="puesto" filterField="puesto" filterMatchMode="contains">
                            <template #body="slotProps">
                                <span class="p-column-title">Puesto</span>
                                {{slotProps.data.puesto}}
                            </template>
                        </Column>
                        <Column header="Delivery Manager" :sortable="true" sortField="deliveryManager" filterField="deliveryManager" filterMatchMode="contains">
                            <template #body="slotProps">
                                <span class="p-column-title">Delivery Manager</span>
                                {{slotProps.data.deliveryManager}}
                            </template>
                        </Column>
                        <Column header="Celular" :sortable="true" sortField="celular" filterField="celular" filterMatchMode="contains">
                            <template #body="slotProps">
                                <span class="p-column-title">Celular</span>
                                {{slotProps.data.celular}}
                            </template>
                        </Column>
                        <Column header="Acciones" :sortable="false" headerStyle="width:75px;">
                            <template #body="slotProps">
                                <!-- <SplitButton label="" icon="pi pi-ellipsis-v"></SplitButton> -->
                                <!-- <div class="dropleft">
                                  <button class="btn btn-sm dropdown-toggle btn-normal" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="pi pi-ellipsis-v"></i>
                                  </button>
                                  <button class="btn btn-primary dropdown-toggle btn-responsive" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <span class="p-column-title">Acciones</span>
                                  </button>
                                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton" style="">
                                    <a class="dropdown-item" href="#"><i class="pi pi-eye"></i> Ver perfil</a>
                                  </div>
                                </div> -->
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
                                    <CIcon name="cil-magnifying-glass" />&nbsp; Ver
                                    Perfil
                                  </CDropdownItem>
                                  
                                </CDropdown>
                            </template>
                        </Column>
                      
                    </DataTable>
                </CCol>
              </CRow>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </div>
    
  </div>
</template>

<script>
import { GetAll, GetDetail } from "./request";

export default {
  name: "Consultas",
  data() {
    return {
      colaboradorSeleccionado: null,
      colaboradores: null,
      filters: {},
      loading: true
    };
  },
  async mounted() {
    let loader = this.showLoader();
    await this.getLista();
    this.loading = false;
    this.hideLoader(loader);
  },
  methods: {
    rowClicked(item, index) {
      // alert("Fila " + item.nombre);
    },
    async getLista() {
      try {
       // const { data } = await GetAll();
       // this.colaboradores = data.colaboradores;
      } catch (error) {
        console.log(error);
      }
    },
    ver(item) {
      this.$router.push({
        name: "Perfil del Colaborador",
        params: { id: item.idcolaborador }
      });
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
    }
  },
  computed: {
   
  }
};
</script>

<style>
/* Responsive */
.p-datatable-customers .p-datatable-tbody > tr > td .p-column-title {
    display: none;
}

.p-datatable .p-datatable-tbody > tr > td {
    border: 1px solid rgba(175, 189, 216, 1)
}
.p-datatable table {
    width: 99%;
}

.p-component {
  font-size: .875rem !important;
}

.dropdown-toggle::after {
    display: none;
}
.dropleft .dropdown-toggle::before{
    display: none;
}

.btn-responsive{
  display: none;
}
.btn-normal{
  display: inline-block;
}

.p-dropdown-label.p-inputtext{
  font-size: .875rem !important;
}
.p-paginator-icon{
  font-size: .875rem !important;
}
.p-datatable-customers .p-datatable-thead > tr > th {
    background-color: rgb(12, 123, 190) !important;
    color: white !important;
}
.p-datatable-customers .p-datatable-tbody > tr > td {
    overflow-wrap: break-word;
}

.p-datatable .p-sortable-column .p-sortable-column-icon {
  color: darkgray;
  font-size: .75rem;
}
.p-datatable .p-sortable-column.p-highlight .p-sortable-column-icon {
  color: white;
}
.p-datatable .p-datatable-header {
  background-color: unset;
  border: unset;
}
.p-datatable.p-datatable-striped .p-datatable-tbody > tr:nth-child(even) {
  background-color: rgba(173,216,230,.5);
  }

  .p-datatable .p-datatable-tbody > tr.p-highlight {
    background: rgba(12, 123, 190, 0.5);
    /* color: #495057; */
}

.p-datatable.p-datatable-striped .p-datatable-tbody > tr:nth-child(even).p-highlight {
    background: rgba(12, 123, 190, 0.5);
    /* color: #495057; */
}

@media screen and (max-width: 40em) {
    .p-datatable.p-datatable-customers .p-datatable-thead > tr > th{
                display: none !important;
            }

      .p-datatable.p-datatable-customers .p-datatable-tfoot > tr > td  {
                display: none !important;
            } 

      .p-datatable.p-datatable-customers .p-datatable-tbody > tr > td {
                text-align: left;
                display: block;
                width: 100%;
                float: left;
                clear: left;
                border: 0 none;  }

      .p-datatable-customers .p-datatable-tbody > tr > td .p-column-title {
                    padding: .4rem;
                    min-width: 30%;
                    display: inline-block !important;
                    margin: -.4em 1em -.4em -.4rem;
                    font-weight: bold;
                    width: 100%
                } 

      .p-datatable.p-datatable-customers:last-child {
                    border-bottom: 1px solid var(--surface-d);
                }
                
      .dropdown-toggle::after {
          display: inline-block;
      }
      .dropleft .dropdown-toggle::before{
          display: inline-block;
      }

      .pi-ellipsis-v{
                    display: none;
                }
      .btn-responsive{
                display: inline-block !important;
              }
              .btn-normal{
                display: none !important;
              }

}
</style>
