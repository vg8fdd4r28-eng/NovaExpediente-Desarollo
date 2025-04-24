<template>
  <CChartBar :datasets="defaultDatasets" :labels="bases" />
</template>

<script>
import { CChartBar } from "@coreui/vue-chartjs";
import { getProp } from "../../utils";
import { GetColaboradoresPorBaseDeDatos } from "../widgets/request";

export default {
  name: "CChartBarNovaBaseDatos",
  components: { CChartBar },
  data() {
    return {
      bases: [],
      basesCant: []
    };
  },
  async mounted() {
    await this.init();
  },
  methods: {
    async init() {
      try {
        const {
          data: { colaboradores }
        } = await GetColaboradoresPorBaseDeDatos();

        this.bases = colaboradores.map(c => getProp("baseDatos", c));
        this.basesCant = colaboradores.map(c =>
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
          label: "Base de datos",
          backgroundColor: "#00aced",
          data: this.basesCant
        }
      ];
    }
  }
};
</script>
