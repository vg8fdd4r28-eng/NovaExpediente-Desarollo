<template>
  <Dialog
    :header="title"
    :visible.sync="showModal"
    :style="{ width: '30vw' }"
    :modal="true"
    contentStyle="padding-bottom:.5rem;"
    :closable="false"
  >
    <div class="confirmation-content mt-2">
      <CRow>
        <CCol col="2">
          <i
            class="pi pi-exclamation-triangle p-mr-3"
            style="font-size: 2rem"
          />
        </CCol>
        <CCol col="10">
          <span>{{ message }}</span>
        </CCol>
      </CRow>
    </div>
    <template #footer>
      <CButton
        class="mt-1 btn btn-info mt-1 pull-right libro-marca-celeste onHoverDark"
        type="button"
        @click="_cancel"
        v-if="cancelButton != 'NoMostrar'"
      >
        {{ cancelButton }}
      </CButton>
      <CButton
        class="mt-1 btn btn-info mt-1 pull-right libro-marca-celeste onHoverDark"
        type="button"
        @click="_confirm"
      >
        {{ okButton }}
      </CButton>
    </template>
  </Dialog>
</template>

<script>
export default {
  name: "ConfirmDialogue",

  data: () => ({
    // Parameters that change depending on the type of dialogue
    title: undefined,
    message: undefined, // Main text content
    okButton: undefined, // Text for confirm button; leave it empty because we don't know what we're using it for
    cancelButton: "Go Back", // text for cancel button

    // Private variables
    resolvePromise: undefined,
    rejectPromise: undefined,

    showModal: false
  }),

  methods: {
    show(opts = {}) {
      this.title = opts.title;
      this.message = opts.message;
      this.okButton = opts.okButton;
      if (opts.cancelButton) {
        this.cancelButton = opts.cancelButton;
      }
      // Once we set our config, we tell the popup modal to open
      this.showModal = true;
      // Return promise so the caller can get results
      return new Promise((resolve, reject) => {
        this.resolvePromise = resolve;
        this.rejectPromise = reject;
      });
    },

    _confirm() {
      this.showModal = false;
      this.resolvePromise(true);
    },

    _cancel() {
      this.showModal = false;
      this.resolvePromise(false);
      // Or you can throw an error
      // this.rejectPromise(new Error('User cancelled the dialogue'))
    }
  }
};
</script>

<style></style>
