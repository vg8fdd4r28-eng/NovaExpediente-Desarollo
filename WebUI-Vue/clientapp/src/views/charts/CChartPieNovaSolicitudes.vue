<template>
  <CChartPie :datasets="dataSolicitudesCant" :labels="dataSolicitudes" />
</template>

<script>
import { CChartPie } from "@coreui/vue-chartjs";
import { getProp } from "../../utils";

export default {
  name: "CChartPieNovaSolicitudes",
  components: { CChartPie },
  props: {
    dataSolicitudes: { type: Array, default: function () {
    return []
  } },
    dataSolicitudesCant: { type: Array, default: function () {
    return []
  } }
  },
  data() {
    return {
      enTransito: [],
      enTransitoCant: []
    };
  },
  async mounted() {
  //  await this.init();
  },
  methods: {
    async init() {
      try {

        this.enTransito = this.dataSolicitudes.map(c =>
          getProp("clasificacion", c)
        );
        this.enTransitoCant  = this.dataSolicitudes.map(c =>
          getProp("cantidad", c)
        );
      } catch (error) {
        console.log(error);
      }
    }
  },
  computed: {
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
  }
};
</script>
