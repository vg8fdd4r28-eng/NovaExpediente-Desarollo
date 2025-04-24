<template>
  <CChartPie :datasets="dataEspecialidadesCant" :labels="dataEspecialidades" />
</template>

<script>
import { CChartPie } from "@coreui/vue-chartjs";
import { getProp } from "../../utils";

export default {
  name: "CChartPieNovaEspecialistas",
  components: { CChartPie },
  props: {
    dataEspecialidades: { type: Array, default: function () {
    return []
  } },
    dataEspecialidadesCant: { type: Array, default: function () {
    return []
  } }
  },
  data() {
    return {
      especialidades: [],
      especialidadesCant: []
    };
  },
  async mounted() {
  //  await this.init();
  },
  methods: {
    async init() {
      try {

        this.especialidades = this.dataEspecialidades.map(c =>
          getProp("ramaespecialidad", c)
        );
        this.especialidadesCant  = this.dataEspecialidades.map(c =>
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
          data: this.dataEspecialidadesCant
        }
      ];
    }
  }
};
</script>
