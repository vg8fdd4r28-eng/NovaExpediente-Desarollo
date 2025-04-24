<template>
  <div>
    <div class="">
      <CCard
        style="border-radius: 16px !important; margin-top: 20px; padding: 6px"
      >
        <strong>Gestión de Currículum Vitae</strong>
      </CCard>

      <div style="background-color: transparent">
        <component
          :is="renderType"
          @edit="edit"
          @create="create"
          @view-list="viewList"
          :form-action="formAction"
          :idCurriculumVitae="idCurriculumVitae"
          :selectedCurriculumVitae="selectedCurriculumVitae"
        >
        </component>
      </div>
    </div>
  </div>
</template>

<script>
import ListaCurriculumVitae from "./ListaCurriculumVitae.vue";
import FormCurriculumVitae from "./FormCurriculumVitae.vue";
import common from "../../masterdata/common.js";

export default {
  name: "CurriculumVitae",
  mixins: [common],
  components: {
    "lista-curriculum-vitae": ListaCurriculumVitae,
    "form-curriculum-vitae": FormCurriculumVitae,
  },
  data() {
    return {
      idCurriculumVitae: null,
      selectedCurriculumVitae: null,
      loading: true,
      renderType: "lista-curriculum-vitae",
      formAction: "",
    };
  },
  async mounted() {
    this.loading = false;
  },
  methods: {
    receiveId(value) {
      this.formAction = "VIEW";
      this.idCurriculumVitae = value;
      if (this.idCurriculumVitae !== null) {
        this.renderType = "form-curriculum-vitae";
      }
    },
    edit(value) {
      this.formAction = "EDIT";
      this.selectedCurriculumVitae = value;
      if (this.selectedCurriculumVitae !== null) {
        this.renderType = "form-curriculum-vitae";
      }
    },
    create() {
      this.formAction = "CREATE";
      this.renderType = "form-curriculum-vitae";
    },
    viewList(value) {
      this.renderType = value;
    },
  },
  computed: {
    /* renderType() {
      return "lista-curriculum-vitae";
    }, */
  },
};
</script>