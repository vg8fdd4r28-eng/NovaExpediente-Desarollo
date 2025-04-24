<template>
  <CRow>
    <template v-if="!noCharts">
      <CCol md="3" sm="6">
        <CWidgetBrand
          color="facebook"
          :right-header="hombres"
          right-footer="hombres"
          :left-header="mujeres"
          left-footer="mujeres"
          bodyClass="card-body"
        >
          <CIcon name="cil-people" height="52" class="my-2" />
          <!-- <CChartBarSimple
            class="c-chart-brand"
            background-color="rgba(255,255,255,.1)"
            :data-points="[65, 59, 84, 84, 51, 55, 40]"
            label="Colaboradores"
            labels="years"
          /> -->
        </CWidgetBrand>
      </CCol>
      <CCol md="3" sm="6">
        <CWidgetBrand
          color="twitter"
          :right-header="clientes"
          right-footer="Clientes"
          :left-header="clientesActivos"
          left-footer="Activos"
        >
          <CIcon name="cil-building" height="52" class="my-2" />
          <!-- <CChartLineSimple
            class="c-chart-brand"
            background-color="rgba(255,255,255,.1)"
            :data-points="[1, 13, 9, 17, 34, 41, 38]"
            label="Clientes"
            labels="months"
          /> -->
        </CWidgetBrand>
      </CCol>
      <CCol md="3" sm="6">
        <CWidgetBrand
          color="linkedin"
          :right-header="solicitudes"
          right-footer="Solicitudes"
          :left-header="solicitudesEnTransito"
          left-footer="En tránsito"
        >
          <CIcon name="cil-task" height="52" class="my-2" />
          <!-- <CChartLineSimple
            class="c-chart-brand"
            background-color="rgba(255,255,255,.1)"
            :data-points="[78, 81, 80, 45, 34, 12, 40]"
            label="Proyectos"
            labels="months"
          /> -->
        </CWidgetBrand>
      </CCol>
      <CCol md="3" sm="6">
        <CWidgetBrand
          :right-header="certificacionMicrosoft"
          right-footer="Certif. MS"
          :left-header="certificacionOracle"
          left-footer="Certif. Oracle"
          color="warning"
        >
          <CIcon name="cil-GlobeAlt" height="52" class="my-2" />
          <!-- <CChartLineSimple
            class="c-chart-brand"
            background-color="rgba(255,255,255,.1)"
            :data-points="[35, 23, 56, 22, 97, 23, 64]"
            label="Certificaciones"
            labels="months"
          /> -->
        </CWidgetBrand>
      </CCol>
    </template>
    <template v-else>
      <CCol md="3" sm="6">
        <CWidgetBrand
          color="facebook"
          right-header="89k"
          right-footer="friends"
          left-header="459"
          left-footer="feeds"
        >
          <CIcon name="cib-facebook" height="56" class="my-4" />
        </CWidgetBrand>
      </CCol>
      <CCol md="3" sm="6">
        <CWidgetBrand
          color="twitter"
          right-header="973k"
          right-footer="followers"
          left-header="1.792"
          left-footer="tweets"
        >
          <CIcon name="cib-twitter" height="56" class="my-4" />
        </CWidgetBrand>
      </CCol>
      <CCol md="3" sm="6">
        <CWidgetBrand
          color="linkedin"
          right-header="500+"
          right-footer="contracts"
          left-header="292"
          left-footer="feeds"
        >
          <CIcon name="cib-linkedin" height="56" class="my-4" />
        </CWidgetBrand>
      </CCol>
      <CCol md="3" sm="6">
        <CWidgetBrand
          right-header="12"
          right-footer="events"
          left-header="4"
          left-footer="meetings"
          color="warning"
        >
          <CIcon name="cil-calendar" height="56" class="my-4" />
        </CWidgetBrand>
      </CCol>
    </template>
  </CRow>
</template>

<script>
import { CChartLineSimple, CChartBarSimple } from "../charts/index.js";
import {
  GetColaboradoresPorGenero,
  GetColaboradoresPorTipoDeCertificado,
  GetSolicitudes,
  GetClientes
} from "./request";

export default {
  name: "WidgetsResumenNova",
  data() {
    return {
      hombres: "0",
      mujeres: "0",
      certificacionMicrosoft: "0",
      certificacionOracle: "0",
      solicitudes: "0",
      solicitudesEnTransito: "0",
      clientes: "0",
      clientesActivos: "0"
    };
  },
  async mounted() {
    await this.GetSolicitudes();
    await this.GetClientes();
    await this.GetColaboradoresPorGenero();
    await this.GetColaboradoresPorTipoDeCertificado();
  },
  methods: {
    async GetColaboradoresPorGenero() {
      try {
        const {
          data: { colaboradores }
        } = await GetColaboradoresPorGenero();

        const findHombres = colaboradores.find(
          c => c.genero.toUpperCase() === "M"
        );
        const findMujeres = colaboradores.find(
          c => c.genero.toUpperCase() === "F"
        );
        this.hombres = findHombres ? findHombres.cantidad : 0;
        this.mujeres = findMujeres ? findMujeres.cantidad : 0;
      } catch (error) {
        console.log(error);
      }
    },
    async GetColaboradoresPorTipoDeCertificado() {
      try {
        const {
          data: { certificacioncolaborador }
        } = await GetColaboradoresPorTipoDeCertificado();

        const findMICROSOFT = certificacioncolaborador.find(
          c => c.proveedorCertificacion.toUpperCase() === "MICROSOFT"
        );
        const findORACLE = certificacioncolaborador.find(
          c => c.proveedorCertificacion.toUpperCase() === "ORACLE"
        );
        this.certificacionMicrosoft = findMICROSOFT
          ? findMICROSOFT.cantidad
          : 0;
        this.certificacionOracle = findORACLE ? findORACLE.cantidad : 0;
      } catch (error) {
        console.log(error);
      }
    },
    async GetSolicitudes() {
      try {
        const {
          data: { totalSolicitudes, solicitudesEnTransito }
        } = await GetSolicitudes();

        this.solicitudes = totalSolicitudes.toString();
        this.solicitudesEnTransito = solicitudesEnTransito.toString();
      } catch (error) {
        console.log(error);
      }
    },
    async GetClientes() {
      try {
        try {
        const {
          data: { totalClientes, clientesActivos }
        } = await GetClientes();

        this.clientes = totalClientes.toString();
        this.clientesActivos = clientesActivos.toString();
      } catch (error) {
        console.log(error);
      }
      } catch (error) {
        console.log(error);
      }
    }
  },
  components: {
    CChartLineSimple,
    CChartBarSimple
  },
  props: {
    noCharts: Boolean
  }
};
</script>

<style scoped>
.c-chart-brand {
  position: absolute;
  width: 100%;
  height: 100px;
}

.card-body {
  background-color: red !important;
}
</style>
