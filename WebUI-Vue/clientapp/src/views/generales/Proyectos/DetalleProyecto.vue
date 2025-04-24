<template>
  <div>
    <CRow>
      <CCol sm="12">
        <CCol
          md="2"
          sm="12"
          class="pull-right mb-2"
          style="padding-left:5px; padding-right:0px"
        >
          <CButton
            block
            color="primary"
            @click="atras()"
            class="pull-right btn btn-info libro-marca-celeste onHoverDark"
          >
            Regresar a la lista</CButton
          >
        </CCol>
      </CCol>
    </CRow>

    <CRow class="pb-3">
      <CCol sm="12" md="4">
        <CCard accent-color="info" class="h-100">
          <CCardHeader>
            <CIcon
              name="acercaDelColaborador"
              class="text-primary font-weight-bold"
            /><strong> Acerca del colaborador</strong>
            <div class="card-header-actions">
              <CLink
                class="card-header-action btn-minimize"
                @click="isCollapsedDetail1 = !isCollapsedDetail1"
              >
                <CIcon
                  :name="`cil-chevron-${isCollapsedDetail1 ? 'bottom' : 'top'}`"
                />
              </CLink>
            </div>
          </CCardHeader>
          <CCollapse :show="isCollapsedDetail1" :duration="400">
            <CCardBody color="">
              <CRow>
                <CCol sm="12" md="12">
                  <CRow>
                    <CCol
                      sm="12"
                      md="12"
                      class="d-flex justify-content-center "
                    >
                      <CImg
                        :src="this.colaborador.foto"
                        block
                        class="mb-2 rounded-circle border border-light shadow"
                        width="150"
                        height="150"
                      />
                    </CCol>
                  </CRow>
                  <CRow class="form-group">
                    <CCol
                      sm="12"
                      md="12"
                      class="d-flex justify-content-center "
                    >
                      <h5 class="label-title label-name">
                        {{ this.colaborador.nombre }}
                      </h5>
                    </CCol>
                  </CRow>
                  <CRow>
                    <CCol sm="6" md="5">
                      <span class="label-title">Puesto</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.puesto }}</span>
                    </CCol>
                  </CRow>
                  <CRow>
                    <CCol sm="6" md="5">
                      <span class="label-title">Especialidad</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.ramaespecialidad }}</span>
                    </CCol>
                  </CRow>
                  <CRow>
                    <CCol sm="6" md="5">
                      <span class="label-title">CPIC</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.numerocpic }}</span>
                    </CCol>
                  </CRow>
                  <CRow>
                    <CCol sm="6" md="5">
                      <span class="label-title">País origen</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.paisOrigen }}</span>
                    </CCol>
                  </CRow>
                  <CRow>
                    <CCol sm="6" md="5">
                      <span class="label-title">Género</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.genero }}</span>
                    </CCol>
                  </CRow>
                  <CRow>
                    <CCol sm="6" md="5">
                      <span class="label-title">Hijos</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.numerohijos }}</span>
                    </CCol>
                  </CRow>
                  <CRow>
                    <CCol sm="6" md="5">
                      <span class="label-title">Estado</span>
                    </CCol>
                    <CCol sm="6" md="7">
                      <span>{{ this.colaborador.estado }}</span>
                    </CCol>
                  </CRow>
                </CCol>
              </CRow>
            </CCardBody>
          </CCollapse>
        </CCard>
      </CCol>
      <CCol sm="12" md="8">
        <CCard accent-color="info" class="h-100">
          <CCardHeader>
            <CIcon
              name="datosPersonales"
              class="text-primary font-weight-bold"
            /><strong> Datos Personales</strong>
            <div class="card-header-actions">
              <CLink
                class="card-header-action btn-minimize"
                @click="isCollapsedDetail2 = !isCollapsedDetail2"
              >
                <CIcon
                  :name="`cil-chevron-${isCollapsedDetail2 ? 'bottom' : 'top'}`"
                />
              </CLink>
            </div>
          </CCardHeader>

          <CCollapse :show="isCollapsedDetail2" :duration="400">
            <CCardBody color="">
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Identificación</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.identificacion }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Estado civil</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.estadocivil }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Fecha nacimiento</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.fechanacimiento }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Edad</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.edad }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Fecha de ingreso</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.fechaingreso }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Antigüedad</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.antiguedad }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Celular</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.celular }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Teléfono</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.telefono }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">País de residencia</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.paisResidencia }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Ciudad</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.canton }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Dirección exacta</span>
                </CCol>
                <CCol sm="12" md="9">
                  <span>{{ this.colaborador.direccion }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Correo empresa</span>
                </CCol>
                <CCol sm="6" md="9">
                  <span>{{ this.colaborador.correo }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="6" md="3">
                  <span class="label-title">Correo personal</span>
                </CCol>
                <CCol sm="6" md="9">
                  <span>{{ this.colaborador.correopersonal }}</span>
                </CCol>
              </CRow>
              <CRow class="form-group">
                <CCol sm="12" md="12">
                  <span class="label-title">En caso de emergencia</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span class="label-title">Llamar a</span>
                </CCol>
                <CCol sm="6" md="3">
                  <span>{{ this.colaborador.personallamar }}</span>
                </CCol>
                <CCol sm="6" md="2">
                  <span class="label-title">Al teléfono</span>
                </CCol>
                <CCol sm="6" md="4">
                  <span>{{ this.colaborador.telefonoemergencia }}</span>
                </CCol>
              </CRow>
            </CCardBody>
          </CCollapse>
        </CCard>
      </CCol>
    </CRow>
  </div>
</template>

<script>
import { GetDetail } from "./request";

export default {
  name: "DetalleProyecto",
  data() {
    return {
      colaborador: {
        id: null,
        identificacion: "",
        nombre: "",
        primerapellido: "",
        segundoapellido: "",
        fechanacimiento: "",
        edad: 0,
        genero: "",
        fechaingreso: "",
        antiguedad: 0,
        direccion: "",
        celular: "",
        telefono: "",
        correo: "",
        correopersonal: "",
        resenaperfil: "",
        numerocpic: "",
        ramaespecialidad: "",
        activo: true,
        estadocivil: "",
        telefonoemergencia: "",
        personallamar: "",
        numerohijos: 0
      },
      isCollapsedDetail1: true,
      isCollapsedDetail2: true
    };
  },
  async mounted() {
    await this.initData();
  },
    methods: {
    atras() {
        this.$router.push({
        name: "Perfil del Colaborador",
        params: { id: this.$route.params.id }
      });
    },
    async initData() {
      const {
        data: { colaborador }
      } = await GetDetail(this.$route.params.id);

      const {
        estadocivil,
        genero,
        nombre,
        primerapellido,
        segundoapellido
      } = colaborador;
      this.colaborador = {
        ...colaborador,
        estadocivil: this.definirEstadoCivil(estadocivil, genero),
        genero: this.definirGenero(genero),
        nombre: nombre + " " + primerapellido + " " + segundoapellido,
        estado: this.definirEstado(true)
      };
    },
    definirEstadoCivil(valor, genero) {
      const identificador = genero === "M" ? "o" : genero === "F" ? "a" : "@";
      switch (valor) {
        case "S":
          return "Solter" + identificador;
        case "C":
          return "Casad" + identificador;
        case "D":
          return "Divorciad" + identificador;
        default:
          return "Sin Definir";
      }
    },
    definirGenero(valor) {
      switch (valor) {
        case "M":
          return "Masculino";
        case "F":
          return "Femenino";
        case "I":
          return "Indefinido";
        default:
          return "No definido";
      }
    },
    definirEstado(valor) {
      if (valor) {
        return "Activo";
      } else {
        return "Inactivo";
      }
    },

    rowClicked(item, index) {
      // alert("Fila " + item.nombre);
    }
  },
};
</script>
<style scoped></style>
