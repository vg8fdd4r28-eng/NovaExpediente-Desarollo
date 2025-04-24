<template>
  <div>
    <CCard style="border-radius: 16px !important; margin: 20px 0">
      <CCardHeader
        style="
          border-radius: 16px !important;
        "
      >
        <strong>Cargar Noticias App</strong>
      </CCardHeader>
      <CCardBody
        style="padding-top: 0px; padding-bottom; 0px; margin-bottom: 20px"
      >
        <FormulateForm name="formulario" v-model="data" class="mt-3">
          <div>
            <CRow>
              <CCol sm="6" lg="12" style="margin-bottom: 20px">
                <label>Ingrese el texto de la noticia a enviar</label>
                <FormulateInput type="myRichTextBox" v-model="content" />
              </CCol>
            </CRow>
            <CRow>
              <CCol sm="2" lg="12">
                <div style="width: 20%; height: 100%">
                  <CButton
                    block
                    color="primary"
                    class="
                      btn
                      pull-right
                      btn btn-info
                      libro-marca-celeste-claro
                      onHoverDark
                      btn-primary
                    "
                    @click="submit()"
                    >Enviar</CButton
                  >
                </div>
              </CCol>
            </CRow>
          </div>
        </FormulateForm>
      </CCardBody>
    </CCard>
  </div>
</template>

<script>
export default {
  data() {
    return {
      content: "",
    };
  },
  methods: {
    async submit() {
      this.content = this.content.replace(/"/g, "'");
      console.log(this.content);
    },
  },
};
</script>