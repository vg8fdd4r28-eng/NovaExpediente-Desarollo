<template>
  <apexchart type="donut" ref="chartAntiguedad2" :options="antiguedadSolicitudesOptions" :series="datos"></apexchart>
</template>

<script>

import { getProp } from "../../utils";

export default {
  name: "CChartDonutAntiguedadSolicitudes",
  components: {  },
  props: {
    series: { type: Array, 
              default: function () {
                return []
              } 
            },
    labels: { type: Array, 
              default: function () {
                return []
              } 
            },
    colors: { type: Array, 
              default: function () {
                return []
              } 
            }
  },
  data() {
    return {
      isMounted: false,
      antiguedadSolicitudes: this.series,
      xLabels : [],
      xColors : [],
      antiguedadSolicitudesOptions: {
            chart: {
              type: 'donut',
              id:"chartAntiguedad",
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
                            columnDelimiter: ',',
                            headerCategory: 'tipo',
                            headerValue: 'cantidad',
                            dateFormatter(timestamp) {
                              return new Date(timestamp).toDateString()
                            }
                          },
                          svg: {
                            filename: undefined,
                          },
                          png: {
                            filename: undefined,
                          }
                        },
                        autoSelected: 'zoom' 
                      },
            },  
            labels: this.xLabels,   
            colors: this.xColors,       
            legend: {
                position: 'bottom'
              },
            plotOptions: {
              pie: {
                donut: {
                  labels: {
                    show: true,
                    name: {
                      show: true,
                      fontSize: '22px',
                      fontFamily: 'Rubik',
                      color: '#dfsda',
                      offsetY: -10
                    },
                    value: {
                      show: true,
                      fontSize: '16px',
                      fontFamily: 'Helvetica, Arial, sans-serif',
                      color: undefined,
                      offsetY: 16,
                      formatter: function (val) {
                        return val
                      }
                    },
                    total: {
                      show: true,
                      label: 'Total',
                      color: '#373d3f',
                      formatter: function (w) {
                        return w.globals.seriesTotals.reduce((a, b) => {
                          return a + b
                        }, 0)
                      }
                    }
                  }
                }
              }
            },
            fill: {
              type: 'gradient',
            },                  
            title: {
                    text: "Antigüedad de las solicitudes en tránsito",
                    align: 'left',
                    margin: 10,
                    offsetX: 0,
                    offsetY: 0,
                    floating: false,
                    style: {
                      fontSize:  '14px',
                      fontWeight:  'bold',
                      fontFamily:  undefined,
                      color:  '#263238'
                    },
                },
            responsive: [{
              breakpoint: 480,
              options: {
                chart: {
                  width: 200
                },
                legend: {
                  position: 'bottom'
                },
                plotOptions: {
                  pie: {
                    startAngle: -90,
                    endAngle: 270
                  }
                },
                fill: {
                  type: 'gradient',
                },
              }
            }]
          }
    };
  },
  async mounted() {
    this.isMounted = true;
  },
  methods: {
   
  },
  computed: {
    opciones(){
      if(this.isMounted)
        return this.antiguedadSolicitudesOptions;
      else
        return {};
    },
    datos(){
      if(this.isMounted)
        return this.series;
      else
        return [];
      
    },
    defaultDatasets() {
      return [
        {
          backgroundColor: [
            "#3b5998",
            "#f9b115",
            "#2eb85c",
            "#DD1B16",
            "#00aced",
            "#ffec00",
            "#ff7300",
            "#7cd0dd"
          ],
          data: this.dataSolicitudesCant
        }
      ];
    }
  },
  watch: {
    colors : function (valores) {
      if(this.isMounted)
        this.antiguedadSolicitudesOptions = {...this.antiguedadSolicitudesOptions, ...{
            colors: valores    }
            };
    },
    labels : function (valores) {
      if(this.isMounted)      
        this.$refs.chartAntiguedad2.updateOptions({
            labels:  valores
          });
    },
    // series : function (valores) {
    //   console.log(this.series);
    //   console.log(valores);
    //   this.$refs.chartAntiguedad2.updateSeries([{
    //     data: valores,
    //   }], false, true);
    // }
  }
};
</script>
