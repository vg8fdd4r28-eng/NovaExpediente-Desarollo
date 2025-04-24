<template>
  <div>
    <div>
      <CCard class="main--card">
        <strong>Sorteos</strong>
      </CCard>
      <div class="dyn-comp-container">
        <component
          :is="renderType"
          @view-sorteo="receiveId"
          @create-sorteo="createSorteo"
          @edit-sorteo="editSorteo"
          @view-list="viewList"
          :selected-sorteo-id="selectedSorteoId"
          :form-action="formAction"
        />
      </div>
    </div>
  </div>
</template>

<script>
import ListaSorteos from "./ListaSorteos.vue";
import FormSorteos from "./FormSorteos.vue";
export default {
  name: "sorteos",
  components: { ListaSorteos, FormSorteos },
  data() {
    return {
      renderType: "lista-sorteos",
      formAction: "",
      loading: true,
      selectedSorteoId: null,
    };
  },
  async mounted() {
    this.loading = false;
  },
  methods: {
    receiveId(value) {
      this.formAction = "VIEW";
      this.selectedSorteoId = value;
      this.selectedSorteoId !== null
        ? (this.renderType = "form-sorteos")
        : null;
    },
    editSorteo(value) {
      this.selectedSorteoId = value;
      this.formAction = "EDIT";
      this.renderType = "form-sorteos";
    },
    createSorteo() {
      this.formAction = "CREATE";
      this.renderType = "form-sorteos";
    },
    viewList() {
      this.formAction = "";
      this.selectedSorteoId = null;
      this.renderType = "lista-sorteos";
    },
  },
};
</script>

<style scoped>
.main--card {
  border-radius: 16px !important;
  margin-top: 20px;
  padding: 6px;
}
.dyn-comp-container {
  background-color: transparent;
}
</style>