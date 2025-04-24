<template>
  <CCard>
    <CCardHeader>
      Clasificador
    </CCardHeader>
    <CCardBody>
      <FormulateForm v-model="clasificador" @submit="submit">
        <CRow>
          <CCol sm="12">
            <CCol sm="1" class="pull-right">
              <CButton
                block
                color="primary"
                @click="atras()"
                class="mr-1 pull-right btn btn-info libro-marca"
              >
                Atras</CButton
              >
            </CCol>
          </CCol>
        </CRow>
        <CRow>
          <CCol sm="6">
            <FormulateInput
              name="nombre"
              label="Nombre"
              validation="required|max:100,length"
              input-class="form-control"
            />
          </CCol>
          <CCol sm="6">
            <FormulateInput
              type="number"
              name="orden"
              label="Orden"
              validation="required|number|min:0"
              input-class="form-control"
            />
          </CCol>
        </CRow>
        <CRow>
          <CCol sm="12">
            <FormulateInput
              type="textarea"
              name="descripcion"
              label="Descripcion"
              validation="required"
              input-class="form-control"
            />
          </CCol>
        </CRow>
        <CRow>
          <CCol sm="12">
            <FormulateInput
              type="submit"
              label="Almacenar"
              :input-class="['btn', 'btn-info', 'pull-right']"
            />
          </CCol>
        </CRow>
      </FormulateForm>
    </CCardBody>
  </CCard>
</template>

<script>
import { Upsert } from "./request";

export default {
  name: "ClasificadorForm",
  data() {
    return {
      clasificador: { id: null, nombre: "", orden: 0, descripcion: "" },
    };
  },
  mounted() {
   // console.log(this.$route.params.id);
  },
  methods: {
    async submit(formValues) {
      try {
        const { data } = await Upsert(formValues);
        console.log(data);
      } catch (error) {
        console.log(error);
      }
    },
    atras() {
      this.$router.push({ name: "Clasificadores" });
    },
  },
};
</script>
