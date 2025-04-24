<template>
  <div>
    <WidgetsResumenNova />

    <CCardGroup columns class="card-columns cols-2">
      <CCard>
        <CCardHeader>
          <!-- <h4 id="traffic" class="card-title mb-0">Resumen de especialistas</h4> -->
          <h4 id="traffic" class="card-title mb-0">
            Solicitudes de cambio en tránsito
          </h4>
          <div class="small text-muted">{{ getMonth() }}</div>
        </CCardHeader>
        <CCardBody>
          <!-- <CChartPieNovaEspecialistas
            :dataEspecialidades="especialidades"
            :dataEspecialidadesCant="especialidadesCant"
          ></CChartPieNovaEspecialistas> -->
          <!-- <CChartDonutAntiguedadSolicitudes ref="chartAntiguedad"
            :series="donutAntiguedadSolicitudes.series"
            :labels="donutAntiguedadSolicitudes.labels"
            :colors="donutAntiguedadSolicitudes.colors"
          ></CChartDonutAntiguedadSolicitudes> -->
          <TabView>
            <!-- Antiguedad de solicitudes de cambio  -->
            <TabPanel header="Antigüedad">
              <div>
                <apexchart
                  type="donut"
                  ref="chartAntiguedad"
                  :options="antiguedadSolicitudesOptions"
                  :series="antiguedadSolicitudes"
                ></apexchart>
              </div>
            </TabPanel>
            <TabPanel header="Estado">
              <div>
                <apexchart
                  type="pie"
                  ref="chartEstado"
                  :options="estadoSolicitudesOptions"
                  :series="estadoSolicitudes"
                ></apexchart>
              </div>
            </TabPanel>
          </TabView>
        </CCardBody>
        <!-- <CCardFooter>          
          <CRow v-for="row in 2" :key="row" class="text-center">
            <CCol
              v-for="col in 5"
              :key="col"
              md
              sm="12"
              class="mb-sm-2 mb-0"
            >
              <div class="text-muted especialidad">{{ especialidadesList[(row - 1) * 5 + col - 1].ramaespecialidad }}</div>
              <strong>{{ especialidadesList[(row - 1) * 5 + col - 1].cantidad }}</strong>
              <CProgress
                class="progress-xs mt-2"
                :precision="1"
                color="success"
                :value="especialidadesList[(row - 1) * 5 + col - 1 ].cantidad"
              />
            </CCol>
          </CRow>
        </CCardFooter> -->
      </CCard>
      <!-- <CCard>
        <CCardHeader>
          <h4 id="traffic" class="card-title mb-0">
            Colaboradores por base de datos
          </h4>
          <div class="small text-muted">{{getMonth()}}</div>
        </CCardHeader>
        <CCardBody><CChartBarNovaBaseDatos /></CCardBody>
      </CCard> -->
      <CCard>
        <CCardHeader>
          <h4 id="traffic" class="card-title mb-0">
            Actualización de expedientes
          </h4>
          <div class="small text-muted">{{ getMonth() }}</div>
        </CCardHeader>
        <CCardBody>
          <!-- <CChartPieNovaSolicitudes             
            :dataSolicitudes="enTransito"
            :dataSolicitudesCant="enTransitoCant">
          </CChartPieNovaSolicitudes>  -->
          <!-- <div>
            <apexchart type="pie"  ref="chartEstado" :options="estadoSolicitudesOptions" :series="estadoSolicitudes"></apexchart>
          </div> -->
          <div>
            <apexchart
              type="radialBar"
              ref="chartExpedientes"
              :options="antiguedadExpedientesOptions"
              :series="antiguedadExpedientes"
            ></apexchart>
          </div>
        </CCardBody>
      </CCard>
    </CCardGroup>

    <CRow>
      <CCol md="12">
        <CCard>
          <CCardHeader>
            <CRow>
              <CCol col="6">
                <h4 id="traffic" class="card-title mb-0">Uso del sistema</h4>
                <div class="small text-muted">{{ getMonth() }}</div>
              </CCol>
              <CCol col="6">
                <div class="float-right">
                  <label>Período:&nbsp;</label>
                  <date-range-picker
                    opens="left"
                    :startDate="startDate"
                    :endDate="endDate"
                    :ranges="ranges"
                    :locale-data="locale"
                    @update="actualizarRango"
                    v-model="dateRange"
                  >
                  </date-range-picker>
                </div>
              </CCol>
            </CRow>
            <!-- <div class="flex">
              <litepie-datepicker
                v-model="dateValue"
              ></litepie-datepicker>
            </div> -->
          </CCardHeader>
          <CCardBody>
            <div id="chart">
              <apexchart
                type="area"
                ref="chartUsoApp"
                height="350"
                :options="chartOptions"
                :series="series"
              ></apexchart>
            </div>
          </CCardBody>
        </CCard>
      </CCol>
    </CRow>
    <CRow>
      <CCol md="12">
        <CCard>
          <CCardHeader>
            <h4>Colaboradores por Líder</h4>
            <div class="small text-muted">{{ getMonth() }}</div>
          </CCardHeader>
          <CCardBody>
            <CDataTable
              class="mb-0 table-outline"
              hover
              :items="colaboradesXDM"
              :fields="tableFields"
              head-color="light"
              no-sorting
            >
              <td slot="avatar" class="text-center" slot-scope="{ item }">
                <div class="c-avatar">
                  <!-- <img :src="item.avatar.url" class="c-avatar-img" alt="" />
                  <span
                    class="c-avatar-status"
                    :class="`bg-${item.avatar.status || 'secondary'}`"
                  ></span>-->
                  <!-- <CImg
                    :src="item.avatar.url"
                    block
                    class="rounded-circle border border-light shadow"
                    style="width:100%;height:100%"
                  /> -->
                  <avatar
                    :size="36"
                    color="white"
                    :username="item.user.name"
                  ></avatar>
                </div>
              </td>
              <td slot="user" slot-scope="{ item }">
                <div>{{ item.user.name }}</div>
                <div class="small text-muted">
                  | Antigüedad : {{ item.user.registered }}
                </div>
                <div class="small text-muted">
                  <span
                    
                    @click="
                      VerDetalleColaboradores(item.user.id, item.user.name)
                    "
                  >
                    | Ver actualización del expediente
                    <i class="pi pi-search small text-muted searchIcon" />
                  </span>
                </div>
              </td>
              <td slot="stats" slot-scope="{ item }" width="200px">
                <div width="300px" height="300px" style="cursor:pointer">
                  <apexchart
                    type="donut"
                    height="100%"
                    :options="radialOptions"
                    :series="item.stats.value"
                     @click="
                      VerDetalleColaboradores(item.user.id, item.user.name)
                    "
                  ></apexchart>
                  
                </div>
              </td>
              <td slot="cv" slot-scope="{ item }" width="200px" class="text-center">
                <div width="300px" height="300px" style="cursor:pointer">
                  <apexchart
                    type="donut"
                    height="100%"
                    :options="radialOptions"
                    :series="item.statsCV.value"
                     @click="
                      VerDetalleColaboradores(item.user.id, item.user.name)
                    "
                  ></apexchart>
                  
                </div>
              </td>
              <!--   <td slot="country" slot-scope="{ item }" class="text-center">
                <CIcon :name="item.country.flag" height="25" />
              </td>-->
              <td slot="usage" slot-scope="{ item }">
                <div class="clearfix">
                  <div class="float-left">
                    <strong>{{ item.usage.value }}%</strong>
                  </div>
                  <div class="float-right">
                    <small class="text-muted">{{ item.usage.period }}</small>
                  </div>
                </div>
                <CProgress
                  class="progress-xs"
                  v-model="item.usage.value"
                  :color="color(item.usage.value)"
                />
              </td>        
              <!--  <td slot="payment" slot-scope="{ item }" class="text-center">
                <CIcon :name="item.payment.icon" height="25" />
              </td>-->
              <!--<td slot="activity" slot-scope="{ item }">
                <div class="small text-muted">Cantidad</div>
                <strong>{{ item.activity }}</strong>
              </td>-->
            </CDataTable>
          </CCardBody>
        </CCard>
      </CCol>
    </CRow>

    <!-- Popup para ver el archivo del detalle -->
    <Dialog
      :visible.sync="showDetalleColaboradores"
      :style="{ width: '65vw' }"
      :modal="true"
      :maximizable="true"
    >
      <template #header>
        <h5>
          {{ nombreDMSeleccionadoComputed }} - Estado de actualización de
          expedientes
        </h5>
      </template>
      <CRow class="pt-2">
        <CCol sm="12" lg="12" class="pull-right">
          <DataTable
            ref="dt"
            :value="listaDetalleColaboradores"
            :paginator="true"
            class="p-datatable-consultas p-datatable-striped p-datatable-sm"
            :rows="10"
            dataKey="identificacion"
            :rowHover="true"
            :filters.sync="filtersDetalle"
            :loading="loadingDetalle"
            paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
            :rowsPerPageOptions="[5, 10, 25, 50]"
            currentPageReportTemplate="Mostrando del {first} al {last} de {totalRecords} colaboradores"
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
                <CCol md="3" sm="12" class="mb-2 pull-right">
                  <div style="text-align: right">
                    <CButton
                      block
                      color="primary"
                      @click="exportar()"
                      class="mr-1 btn pull-right btn btn-info libro-marca-celeste onHoverDark btn-primary"
                    >
                      <i class="pi pi-download"></i>&nbsp;Exportar</CButton
                    >
                  </div>
                </CCol>
              </CRow>
            </template>
            <template #empty>
              No se encontraron colaboradores.
            </template>
            <template #loading>
              Cargando la información de colaboradores. Por favor espere.
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
                <!-- <span class="p-column-title">Identificación</span> -->
                {{ slotProps.data["identificacion"] }}
              </template>
            </Column>

            <Column
              field="nombreCompleto"
              header="Nombre del colaborador"
              sortField="nombreCompleto"
              filterField="nombreCompleto"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:22%;"
            >
              <template #body="slotProps">
                <!-- <span class="p-column-title">Nombre del colaborador</span> -->
                {{ slotProps.data.nombreCompleto }}
              </template>
            </Column>

            <Column
              field="tipoSolicitud"
              header="Tipo de actualización"
              sortField="tipoSolicitud"
              filterField="tipoSolicitud"
              filterMatchMode="contains"
              :sortable="true"
              headerStyle="width:22%;"
            >
              <template #body="slotProps">
                {{ slotProps.data.tipoSolicitud }}
              </template>
            </Column>

            <Column
              header="Última modificación"
              :sortable="true"
              sortField="ultimaFecha"
              filterField="ultimaFecha"
              filterMatchMode="contains"
              headerStyle="width:10%;"
            >
              <template #body="slotProps">
                {{ formatearFecha(slotProps.data.ultimaFecha) }}
                <div class="progress">
                  <div
                    class="progress-bar"
                    v-bind:class="meterClass(slotProps.data.status)"
                    role="progressbar"
                    style="width: 100%"
                    aria-valuenow="100"
                    aria-valuemin="0"
                    aria-valuemax="100"
                  >
                    {{ getMeterLabel(slotProps.data.status) }}
                  </div>
                </div>
              </template>
            </Column>
          </DataTable>
        </CCol>
      </CRow>

      <template #footer>
        <CButton
          color="primary"
          @click="showDetalleColaboradores = false"
          class="mr-1  btn btn-info libro-marca-celeste onHoverDark"
        >
          <span class="pi pi-times"></span> <span>Cerrar</span>
        </CButton>
      </template>
    </Dialog>
  </div>
</template>

<script>
import MainChartExample from "./charts/MainChartExample";
import WidgetsDropdown from "./widgets/WidgetsDropdown";
import WidgetsBrand from "./widgets/WidgetsBrand";
import WidgetsResumenNova from "./widgets/WidgetsResumenNova";
import CChartPieNovaEspecialistas from "./charts/CChartPieNovaEspecialistas";
import CChartBarNovaBaseDatos from "./charts/CChartBarNovaBaseDatos";
import CChartPieNovaSolicitudes from "./charts/CChartPieNovaSolicitudes";
import CChartDonutAntiguedadSolicitudes from "./charts/CChartDonutAntiguedadSolicitudes";

import {
  GetColaboradoresPorEspecialidad,
  GetColaboradoresPorDM,
  GetPhotoColaboradorById,
  GetSolicitudesEnTransito,
  GetAntiguedadSolicitudes,
  GetAntiguedadExpedientes,
  GetUsoAplicacion,
  GetColaboradoresAsignados
} from "./widgets/request";
import { getProp } from "../utils";
import Avatar from "vue-avatar";

import DateRangePicker from "vue2-daterange-picker";
import moment from "moment";
import "vue2-daterange-picker/dist/vue2-daterange-picker.css";
import XLSX from "sheetjs-style";
import Vue from "vue";

import { aplicarFormatoFecha } from "../utils";

export default {
  name: "Dashboard",
  components: {
    MainChartExample,
    WidgetsDropdown,
    WidgetsBrand,
    WidgetsResumenNova,
    CChartPieNovaEspecialistas,
    CChartBarNovaBaseDatos,
    CChartPieNovaSolicitudes,
    CChartDonutAntiguedadSolicitudes,
    Avatar,
    DateRangePicker
  },
  data() {
    return {
      selected: "Month",
      colaboradesXDM: [],
      tableItems: [
        {
          avatar: { url: "img/avatars/hleiva.jpg", status: "success" },
          user: {
            name: "Harold Leiva",
            new: false,
            registered: "1 Enero, 2015"
          },
          country: { name: "USA", flag: "cif-us" },
          usage: { value: 70, period: "92 colaboradores" },
          payment: { name: "Mastercard", icon: "cib-cc-mastercard" },
          activity: "12"
        },
        {
          avatar: { url: "img/avatars/lmorales.jpg", status: "danger" },
          user: {
            name: "Laura Morales",
            new: true,
            registered: "1 Enero, 2015"
          },
          country: { name: "Brazil", flag: "cif-br" },
          usage: { value: 22, period: "9 colaboradores" },
          payment: { name: "Visa", icon: "cib-cc-visa" },
          activity: "2"
        },
        {
          avatar: { url: "img/avatars/rmidence.jpg", status: "warning" },
          user: {
            name: "Rebeca Midence",
            new: true,
            registered: "1 Enero, 2015"
          },
          country: { name: "India", flag: "cif-in" },
          usage: { value: 45, period: "16 colaboradores" },
          payment: { name: "Stripe", icon: "cib-stripe" },
          activity: "3"
        },
        {
          avatar: { url: "img/avatars/jcromero.jpg", status: "" },
          user: {
            name: "Juan Carlos Romero",
            new: false,
            registered: "1 Enero, 2015"
          },
          country: { name: "France", flag: "cif-fr" },
          usage: { value: 58, period: "18 colaboradores" },
          payment: { name: "PayPal", icon: "cib-paypal" },
          activity: "4"
        },
        {
          avatar: { url: "img/avatars/mrodriguez.jpg", status: "success" },
          user: {
            name: "Marlon Rodríguez",
            new: false,
            registered: "1 Enero, 2015"
          },
          country: { name: "Spain", flag: "cif-es" },
          usage: { value: 22, period: "7 colaboradores" },
          payment: { name: "Google Wallet", icon: "cib-google-pay" },
          activity: "1"
        },
        {
          avatar: { url: "img/avatars/aremedios.jpg", status: "danger" },
          user: {
            name: "Alexander Remedios",
            new: true,
            registered: "1 Enero, 2015"
          },
          country: { name: "Poland", flag: "cif-pl" },
          usage: { value: 43, period: "3 colaboradores" },
          payment: { name: "Amex", icon: "cib-cc-amex" },
          activity: "2"
        }
      ],
      tableFields: [
        { key: "avatar", label: "", _classes: "text-center" },
        { key: "user", label: "Nombre del líder" },
        { key: "cv", label: "Actualización Currículum",  _classes: "text-center" },
        { key: "stats", label: "Actualización Expediente" },
        // { key: 'country', _classes: 'text-center' },
        { key: "usage", label: "% Colaboradores" }
        // { key: 'payment', label: 'Payment method', _classes: 'text-center' },
        // { key: "activity", label: "Proyectos" }
      ],
      especialidadesList: [],
      especialidades: [],
      especialidadesCant: [],
      enTransitoList: [],
      enTransito: [],
      enTransitoCant: [],
      antiguedadSolicitudes: [0],
      antiguedadSolicitudesOptions: {
        chart: {
          type: "donut",
          id: "chartAntiguedad",
          redrawOnWindowResize: false,
          toolbar: {
            show: true,
            offsetX: 0,
            offsetY: 0,
            tools: {
              download: true,
              selection: true,
              zoom: true,
              zoomin: true,
              zoomout: true,
              pan: true,
              reset: true | '<img src="/static/icons/reset.png" width="20">',
              customIcons: []
            },
            export: {
              csv: {
                filename: undefined,
                columnDelimiter: ",",
                headerCategory: "tipo",
                headerValue: "cantidad",
                dateFormatter(timestamp) {
                  return new Date(timestamp).toDateString();
                }
              },
              svg: {
                filename: undefined
              },
              png: {
                filename: undefined
              }
            },
            autoSelected: "zoom"
          }
        },
        legend: {
          position: "bottom"
        },
        plotOptions: {
          pie: {
            donut: {
              labels: {
                show: true,
                name: {
                  show: true,
                  fontSize: "22px",
                  fontFamily: "Rubik",
                  color: "#dfsda",
                  offsetY: -10
                },
                value: {
                  show: true,
                  fontSize: "16px",
                  fontFamily: "Helvetica, Arial, sans-serif",
                  color: undefined,
                  offsetY: 16,
                  formatter: function(val) {
                    return val;
                  }
                },
                total: {
                  show: true,
                  label: "Total",
                  color: "#373d3f",
                  formatter: function(w) {
                    return w.globals.seriesTotals.reduce((a, b) => {
                      return a + b;
                    }, 0);
                  }
                }
              }
            }
          }
        },
        fill: {
          type: "gradient"
        },
        title: {
          text: "Antigüedad de las solicitudes en tránsito",
          align: "left",
          margin: 10,
          offsetX: 0,
          offsetY: 0,
          floating: false,
          style: {
            fontSize: "14px",
            fontWeight: "bold",
            fontFamily: undefined,
            color: "#263238"
          }
        },
        // responsive: [{
        //   breakpoint: 480,
        //   options: {
        //     chart: {
        //       width: 200
        //     },
        //     legend: {
        //       position: 'bottom'
        //     },
        //     plotOptions: {
        //       pie: {
        //         startAngle: -90,
        //         endAngle: 270
        //       }
        //     },
        //     fill: {
        //       type: 'gradient',
        //     },
        //   }
        // }],
        noData: {
          text: "Loading..."
        }
      },
      estadoSolicitudesOptions: {
        chart: {
          width: 380,
          type: "pie",
          redrawOnWindowResize: false,
          toolbar: {
            show: true,
            offsetX: 0,
            offsetY: 0,
            tools: {
              download: true,
              selection: true,
              zoom: true,
              zoomin: true,
              zoomout: true,
              pan: true,
              reset: true | '<img src="/static/icons/reset.png" width="20">',
              customIcons: []
            },
            export: {
              csv: {
                filename: undefined,
                columnDelimiter: ",",
                headerCategory: "tipo",
                headerValue: "cantidad",
                dateFormatter(timestamp) {
                  return new Date(timestamp).toDateString();
                }
              },
              svg: {
                filename: undefined
              },
              png: {
                filename: undefined
              }
            },
            autoSelected: "zoom"
          }
        },
        labels: [],
        // theme: {
        //   monochrome: {
        //     enabled: true
        //   }
        // },
        legend: {
          position: "bottom"
        },
        fill: {
          type: "gradient"
        },
        title: {
          text: "Estado de las solicitudes en tránsito",
          align: "left",
          margin: 10,
          offsetX: 0,
          offsetY: 0,
          floating: false,
          style: {
            fontSize: "14px",
            fontWeight: "bold",
            fontFamily: undefined,
            color: "#263238"
          }
        }
        // responsive: [{
        //   breakpoint: 480,
        //   options: {
        //     chart: {
        //       width: 200
        //     },
        //     legend: {
        //       position: 'bottom'
        //     }
        //   }
        // }]
        // chart: {
        //   type: 'pie',
        //   id:"chartEstado",
        //   // toolbar: {
        //   //           show: true,
        //   //           offsetX: 0,
        //   //           offsetY: 0,
        //   //           tools: {
        //   //             download: true,
        //   //             selection: true,
        //   //             zoom: true,
        //   //             zoomin: true,
        //   //             zoomout: true,
        //   //             pan: true,
        //   //             reset: true | '<img src="/static/icons/reset.png" width="20">',
        //   //             customIcons: []
        //   //           },
        //   //           export: {
        //   //             csv: {
        //   //               filename: undefined,
        //   //               columnDelimiter: ',',
        //   //               headerCategory: 'clasificacion',
        //   //               headerValue: 'cantidad',
        //   //               dateFormatter(timestamp) {
        //   //                 return new Date(timestamp).toDateString()
        //   //               }
        //   //             },
        //   //             svg: {
        //   //               filename: undefined,
        //   //             },
        //   //             png: {
        //   //               filename: undefined,
        //   //             }
        //   //           },
        //   //           autoSelected: 'zoom'
        //   //         },
        // },
        // theme: {
        //   monochrome: {
        //     enabled: true
        //   }
        // },
        // legend: {
        //     position: 'bottom'
        //   },
        // title: {
        //         text: "Estado de las solicitudes en tránsito",
        //         align: 'left',
        //         margin: 10,
        //         offsetX: 0,
        //         offsetY: 0,
        //         floating: false,
        //         style: {
        //           fontSize:  '14px',
        //           fontWeight:  'bold',
        //           fontFamily:  undefined,
        //           color:  '#263238'
        //         },
        //     },
        // responsive: [{
        //   breakpoint: 480,
        //   options: {
        //     chart: {
        //       width: 200
        //     },
        //     legend: {
        //       position: 'bottom'
        //     },
        //     plotOptions: {
        //       pie: {
        //         startAngle: -90,
        //         endAngle: 270
        //       }
        //     },
        //     fill: {
        //       type: 'gradient',
        //     },
        //   }
        // }]
      },

      estadoSolicitudes: [0],
      antiguedadExpedientes: [0],
      antiguedadExpedientesOptions: {
        chart: {
          type: "radialBar",
          id: "chartExpediente",
          redrawOnWindowResize: false,
          toolbar: {
            show: true,
            offsetX: 0,
            offsetY: 0,
            tools: {
              download: true,
              selection: true,
              zoom: true,
              zoomin: true,
              zoomout: true,
              pan: true,
              reset: true | '<img src="/static/icons/reset.png" width="20">',
              customIcons: []
            },
            export: {
              csv: {
                filename: undefined,
                columnDelimiter: ",",
                headerCategory: "tipo",
                headerValue: "cantidad",
                dateFormatter(timestamp) {
                  return new Date(timestamp).toDateString();
                }
              },
              svg: {
                filename: undefined
              },
              png: {
                filename: undefined
              }
            },
            autoSelected: "zoom"
          },
          events: {
            click: function(event, chartContext, config) {
              
              // The last parameter config contains additional information like `seriesIndex` and `dataPointIndex` for cartesian charts
            }
          }
        },
        legend: {
          position: "bottom"
        },
        plotOptions: {
          radialBar: {
            dataLabels: {
              show: true,
              name: {
                show: true,
                fontSize: "22px",
                fontFamily: "Rubik",
                color: "#dfsda",
                offsetY: -10
              },
              value: {
                show: true,
                fontSize: "16px",
                fontFamily: "Helvetica, Arial, sans-serif",
                color: undefined,
                offsetY: 16,
                formatter: function(val) {
                  return val + " %";
                }
              },
              total: {
                show: true,
                label: "Total",
                color: "#373d3f",
                formatter: function(w) {
                  return w.globals.seriesTotals.reduce((a, b) => {
                    var total = Math.round(a + b);
                    return total;
                  }, 0);
                }
              }
            }
          }
        },
        fill: {
          type: "gradient"
        },
        title: {
          text: "Rango porcentual de expedientes actualizados",
          align: "left",
          margin: 10,
          offsetX: 0,
          offsetY: 0,
          floating: false,
          style: {
            fontSize: "14px",
            fontWeight: "bold",
            fontFamily: undefined,
            color: "#263238"
          }
        }
        // responsive: [{
        //   breakpoint: 480,
        //   options: {
        //     chart: {
        //       width: 200
        //     },
        //     legend: {
        //       position: 'bottom'
        //     },
        //     plotOptions: {
        //       pie: {
        //         startAngle: -90,
        //         endAngle: 270
        //       }
        //     },
        //     fill: {
        //       type: 'gradient',
        //     },
        //   }
        // }]
      },
      donutAntiguedadSolicitudes: {
        series: [],
        labels: [],
        colors: []
      },
      radialOptions: {
        // chart: {
        //   height: 100,
        //   width: 100,
        //   type: 'donut',
        // },
        // plotOptions: {
        //   radialBar: {
        //     dataLabels: {
        //       name: {
        //         fontSize: '16px',
        //       },
        //       value: {
        //         fontSize: '16px',
        //       },
        //       // total: {
        //       //   show: true,
        //       //   label: 'Total',
        //       //   formatter: function (w) {
        //       //     // By default this function returns the average of all series. The below is just an example to show the use of custom formatter function
        //       //     return 100
        //       //   }
        //       //}
        //     }
        //   }
        // },
        // labels: ['<= 30 días', '> 30  <= 60 días', '> 60 días'],
        chart: {
          type: "donut",
          events: {
            click: function(event, chartContext, config) {
              
              // The last parameter config contains additional information like `seriesIndex` and `dataPointIndex` for cartesian charts
            }
          }
        },
        legend: { show: false },
        dataLabels: {
          enabled: true,
          textAnchor: "start",
          style: {
            fontSize: "10px",
            fontFamily: "Helvetica, Arial, sans-serif",
            fontWeight: "300",
            colors: ["#000000", "#000000", "#000000"]
          },
          offset: 300,
          dropShadow: {
            enabled: false,
            top: 1,
            left: 1,
            blur: 1,
            color: "#000",
            opacity: 0.45
          }
        },
        labels: ["<= 60 días", "> 60  <= 90 días", "> 90 días"],
        colors: ["#2eb85c", "#F9B115", "#FF0000"],
        plotOptions: {
          pie: {
            startAngle: -90,
            endAngle: 90,
            offsetY: 10
          }
        },
        grid: {
          padding: {
            bottom: -80
          }
        },
        /*responsive: [
          {
            breakpoint: 480,
            options: {
              chart: {
                width: 200
              },
              legend: {
                position: "bottom"
              }
            }
          }
        ]*/
        },

      startDate: new Date(),
      endDate: new Date(),
      locale: {
        direction: "ltr", //direction of text
        format: "dd-mm-yyyy", //fomart of the dates displayed
        separator: " al ", //separator between the two ranges
        applyLabel: "Aceptar",
        cancelLabel: "Cancelar",
        weekLabel: "W",
        customRangeLabel: "Custom Range",
        daysOfWeek: ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb"],
        monthNames: [
          "Ene",
          "Feb",
          "Mar",
          "Abr",
          "May",
          "Jun",
          "Jul",
          "Ago",
          "Sep",
          "Oct",
          "Nov",
          "Dic"
        ],
        // daysOfWeek: moment.weekdaysMin(), //array of days - see moment documenations for details
        // monthNames: moment.monthsShort(), //array of month names - see moment documenations for details
        firstDay: 1 //ISO first day of week - see moment documenations for details
      },
      dateRange: {
        startDate: new Date(new Date().setDate(new Date().getDate() - 6)), //new Date(),
        endDate: new Date()
      },
      ranges: {
        //default value for ranges object (if you set this to false ranges will no be rendered)
        Hoy: [this.hoy, this.hoy],
        Ayer: [new Date().getDate() - 1, new Date().getDate() - 1],
        "Este mes": [moment().startOf("month"), moment().endOf("month")],
        "Este año": [moment().startOf("year"), moment().endOf("year")],
        "Última semana": [
          moment()
            .subtract(1, "week")
            .startOf("week"),
          moment()
            .subtract(1, "week")
            .endOf("week")
        ],
        "Mes anterior": [
          moment()
            .subtract(1, "month")
            .startOf("month"),
          moment()
            .subtract(1, "month")
            .endOf("month")
        ]
      },
      series: [
        {
          data: []
        }
      ],
      chartOptions: {
        chart: {
          id: "area-datetime",
          type: "area",
          height: 350,
          zoom: {
            autoScaleYaxis: true
          }
        },
        annotations: {
          yaxis: [
            {
              y: 15,
              borderColor: "#999",
              label: {
                show: true,
                text: "Mínimo esperado",
                style: {
                  color: "#fff",
                  background: "#00E396"
                }
              }
            }
          ],
          xaxis: [
            {
              x: new Date().getTime(),
              borderColor: "#999",
              yAxisIndex: 0,
              label: {
                show: true,
                text: "Hoy",
                style: {
                  color: "#fff",
                  background: "#775DD0"
                }
              }
            }
          ]
        },
        dataLabels: {
          enabled: false
        },
        markers: {
          size: 0,
          style: "hollow"
        },
        xaxis: {
          type: "datetime",
          min: new Date(2021, 0, 1).getTime(),
          tickAmount: 6
        },
        tooltip: {
          x: {
            format: "dd-MM-yyyy"
          }
        },
        fill: {
          type: "gradient",
          gradient: {
            shadeIntensity: 1,
            opacityFrom: 0.7,
            opacityTo: 0.9,
            stops: [0, 100]
          }
        }
      },
      nombreDMSeleccionado: "",
      showDetalleColaboradores: false,
      listaDetalleColaboradores: [],
      loading: true,
      filters: {},
      loadingDetalle: true,
      filtersDetalle: {}
    };
  },
  async mounted() {
    // await this.GetAntiguedadSolicitudes();
    await this.GetAntiguedadExpedientes();
    await this.GetSolicitudesEnTransito();
    await this.GetColaboradoresPorEspecialidad();
    await this.GetColaboradoresPorDM();

    await this.GetUsoAplicacion();
    this.initRanges();
    //await this.CargarPhotos();
  },
  async created() {
    //because we dont call async methods within a view
    // await this.GetColaboradoresPorDM();
    await this.GetAntiguedadSolicitudes();
  },
  methods: {
    formatearFecha(fecha) {
      //return moment(fecha).format("L");
      return aplicarFormatoFecha(fecha, "dd-MM-yyyy");
    },
    color(value) {
      let $color;
      if (value <= 25) {
        $color = "info";
      } else if (value > 25 && value <= 50) {
        $color = "success";
      } else if (value > 50 && value <= 75) {
        $color = "warning";
      } else if (value > 75 && value <= 100) {
        $color = "danger";
      }
      return $color;
    },
    async GetColaboradoresPorEspecialidad() {
      try {
        const {
          data: { colaboradores }
        } = await GetColaboradoresPorEspecialidad();

        this.especialidades = colaboradores.map(c =>
          getProp("ramaespecialidad", c)
        );

        this.especialidadesCant = [
          {
            backgroundColor: [
              "#3b5998",
              "#f9b115",
              "#2eb85c",
              "#DD1B16",
              "#00aced"
            ],
            data: colaboradores.map(c => getProp("cantidad", c))
          }
        ];

        this.especialidadesList = colaboradores;
      } catch (error) {
        console.log(error);
      }
    },
    async GetSolicitudesEnTransito() {
      try {
        const {
          data: { solicitudes }
        } = await GetSolicitudesEnTransito();

        this.enTransito = solicitudes.map(c => getProp("clasificacion", c));

        this.enTransitoCant = [
          {
            backgroundColor: [
              "#3b5998",
              "#f9b115",
              "#2eb85c",
              "#DD1B16",
              "#00aced"
            ],
            data: solicitudes.map(c => getProp("cantidad", c))
          }
        ];

        this.estadoSolicitudes = solicitudes.map(c => getProp("cantidad", c));

        this.$refs.chartEstado.updateSeries([
          {
            data: this.estadoSolicitudes
          }
        ]);

        this.$refs.chartEstado.updateOptions({
          labels: solicitudes.map(c => getProp("clasificacion", c))
        });

        this.enTransitoList = solicitudes;
      } catch (error) {
        console.log(error);
      }
    },
    async GetAntiguedadSolicitudes() {
      try {
        const {
          data: { tipos }
        } = await GetAntiguedadSolicitudes();

        this.antiguedadSolicitudesOptions.labels = tipos.map(c =>
          getProp("tipo", c)
        );

        this.antiguedadSolicitudesOptions.colors = tipos.map(c =>
          getProp("color", c)
        );

        this.antiguedadSolicitudes = tipos.map(c => getProp("cantidad", c));

        this.donutAntiguedadSolicitudes.series = tipos.map(c =>
          getProp("cantidad", c)
        );

        this.donutAntiguedadSolicitudes.labels = tipos.map(c =>
          getProp("tipo", c)
        );

        this.donutAntiguedadSolicitudes.colors = tipos.map(c =>
          getProp("color", c)
        );

        this.$refs.chartAntiguedad.updateOptions({
          labels: tipos.map(c => getProp("tipo", c)),
          colors: tipos.map(c => getProp("color", c))
        });
      } catch (error) {
        console.log(error);
      }
    },
    async GetUsoAplicacion() {
      try {
        let rango = {
          fechaInicio: new Date(
            this.dateRange.startDate.getFullYear(),
            this.dateRange.startDate.getMonth(),
            this.dateRange.startDate.getDate()
          ),
          fechaFin: new Date(
            this.dateRange.endDate.getFullYear(),
            this.dateRange.endDate.getMonth(),
            this.dateRange.endDate.getDate()
          )
        };

        const {
          data: { series }
        } = await GetUsoAplicacion(rango);

        this.series.data = series;
        this.$refs.chartUsoApp.updateSeries([
          {
            data: series
          }
        ]);
      } catch (error) {
        console.log(error);
      }
    },
    async GetAntiguedadExpedientes() {
      try {
        const {
          data: { tipos }
        } = await GetAntiguedadExpedientes();

        this.antiguedadExpedientesOptions.labels = tipos.map(c =>
          getProp("tipo", c)
        );

        this.antiguedadExpedientesOptions.colors = tipos.map(c =>
          getProp("color", c)
        );

        this.antiguedadExpedientes = tipos.map(c => getProp("cantidad", c));

        this.$refs.chartExpedientes.updateOptions({
          labels: tipos.map(c => getProp("tipo", c)),
          colors: tipos.map(c => getProp("color", c))
        });
      } catch (error) {
        console.log(error);
      }
    },
    async GetColaboradoresPorDM() {
      try {
        const {
          data: { colaboradores }
        } = await GetColaboradoresPorDM();

        var arrCant = colaboradores.map(c => getProp("cantidad", c));

        var cantSum = arrCant.reduce((a, b) => a + b, 0);

        this.colaboradesXDM = colaboradores.map(
          ({
            nombre,
            cantidad,
            idDeliveryManager,
            fechaIngreso,
            idColaborador,
            actualizacionExpediente,
            actualizacionCurriculum
          }) => ({
            avatar: {
              // url: this.GetPhotoById(idColaborador)
              url: ""
              //  status: "success"
            },
            user: {
              name: nombre,
              id: idDeliveryManager,
              idColaborador: idColaborador,
              //new: true,
              registered: fechaIngreso
            },
            usage: {
              value: Math.abs(((cantidad / cantSum) * 100).toFixed(2)),
              period: `${cantidad} colaboradores`
            },
            stats: {
              value: actualizacionExpediente
            },
            statsCV: {
             value: actualizacionCurriculum 
            }
          })
        );
      } catch (error) {
        console.log(error);
      }
    },
    async GetPhotoById(id) {
      try {
        const {
          data: { fotoColaborador }
        } = await GetPhotoColaboradorById(id);

        return fotoColaborador.foto;
      } catch (error) {
        console.log(error);
        return ""; //pedir imagen uiux
      }
    },
    async CargarPhotos() {
      const promiseArray = new Array();
      this.colaboradesXDM.forEach(
        //  async   element =>
        //       (element.avatar.url =await  this.GetPhotoById(element.user.idColaborador))
        //   );

        async element =>
          promiseArray.push(this.GetPhotoById(element.user.idColaborador))
      );

      const colaboradesXDM = this.colaboradesXDM;
      await Promise.all(promiseArray)
        .then(data => {
          data.forEach((element, index) => {
            colaboradesXDM[index].avatar.url = element;
          });
        })
        .catch(err => {
          console.log(err);
        });
    },
    getMonth() {
      var d = new Date();
      var n = d.getMonth();
      var months = new Array(
        "Enero",
        "Febrero",
        "Marzo",
        "Abril",
        "Mayo",
        "Junio",
        "Julio",
        "Agosto",
        "Setiembre",
        "Octubre",
        "Noviembre",
        "Diciembre"
      );
      return months[n] + " " + d.getFullYear().toString();
    },
    initRanges() {
      let hoy = new Date();
      let ayer = new Date();
      let y = hoy.getFullYear();
      let m = hoy.getMonth();
      let s = new Date();

      s.setDate(s.getDate() - 6);
      ayer.setDate(hoy.getDate() - 1);

      this.ranges = {
        //default value for ranges object (if you set this to false ranges will no be rendered)
        Hoy: [hoy, hoy],
        Ayer: [ayer, ayer],
        "Este mes": [new Date(y, m, 1), new Date(y, m + 1, 0)],
        "Este año": [
          new Date(hoy.getFullYear(), 0, 1),
          new Date(hoy.getFullYear(), 11, 31)
        ],
        "Últimos 7 días": [s, hoy],
        "Mes anterior": [new Date(y, m - 1, 1), new Date(y, m, 0)]
      };
    },
    async actualizarRango(rango) {
      await this.GetUsoAplicacion();
    },
    async VerDetalleColaboradores(idDM, nombreDM) {
      this.listaDetalleColaboradores= [];
      this.nombreDMSeleccionado = nombreDM;
      this.showDetalleColaboradores = true;
      try {
        const {
          data: { colaboradores }
        } = await GetColaboradoresAsignados(idDM);
        this.listaDetalleColaboradores = colaboradores;
        this.loadingDetalle = false;
      } catch (error) {
        console.log(error);
        this.loadingDetalle = false;
      }
    },

    meterClass(rango) {
      rango = rango.substring(0, 1);
      return {
        "bg-success": rango == 1,
        "bg-warning": rango == 2,
        "bg-danger": rango == 3
      };
    },
    getMeterLabel(rango) {
      let label = "";
      rango = rango.substring(0, 1);
      switch (rango) {
        case "1":
          label = "<= 60 días";
          break;
        case "2":
          label = "> 60 y <= 90 días";
          break;
        case "3":
          label = "> 90 días";
          break;

        default:
          break;
      }
      return label;
    },
    getStatus(rango) {
      let label = "";
      rango = rango.substring(0, 1);
      switch (rango) {
        case "1":
          label = "Verde (<= 60 días)";
          break;
        case "2":
          label = "Amarillo (> 60 y <= 90 días)";
          break;
        case "3":
          label = "Rojo (> 90 días)";
          break;

        default:
          break;
      }
      return label;
    },
    async exportar() {
      var data = this.listaDetalleColaboradores.map(
        ({
          identificacion,
          nombreCompleto,
          ultimaFecha,
          tipoSolicitud,
          status
        }) => ({
          Identificacion: identificacion,
          Nombre_Completo: nombreCompleto,
          Ultima_Fecha: moment(ultimaFecha).format("YYYY/MM/DD"),
          Tipo_Solicitud: tipoSolicitud,
          Estado: this.getStatus(status)
        })
      );

      const wb = XLSX.utils.book_new();
      const ws = XLSX.utils.json_to_sheet(data);
      XLSX.utils.book_append_sheet(wb, ws, "EstadoActualExpedientes.csv");
      XLSX.writeFile(wb, `Expedientes_${this.getDateTime()}.csv`);
      Vue.$toast.success("Generación exitosa del archivo.");
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
    }
  },
  computed: {
    nombreDMSeleccionadoComputed() {
      if (this.nombreDMSeleccionado) {
        return this.nombreDMSeleccionado;
      } else {
        return "delivery manager";
      }
    }
  }
};
</script>
<style>
.c-avatar {
  width: 64px;
  height: 64px;
}

.especialidad {
  text-overflow: ellipsis;
  overflow: hidden;
  white-space: nowrap;
}
.calendars {
  flex-wrap: unset !important;
}

.searchIcon {
  color: #3b5998 !important;
  font-size: 90%;
  font-weight: bold;
  cursor: pointer;
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
</style>
