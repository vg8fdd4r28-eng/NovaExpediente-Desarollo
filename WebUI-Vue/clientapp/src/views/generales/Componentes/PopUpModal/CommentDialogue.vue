<template>
    <Dialog
      :header="title"
      :visible.sync="showModal"
      :style="{ width: '30vw' }"
      :modal="true"
      contentStyle="padding-bottom:.5rem;"
    >
      <div class="confirmation-content mt-2">
        <CRow>
            <CCol col="2">
                <i class="pi pi-exclamation-triangle p-mr-3" style="font-size: 2rem" />
            </CCol>
            <CCol col="10">
                <span>{{ message }}</span>
            </CCol>
        </CRow>
        <CRow class="mt-2">
             <CCol col="12">
                <FormulateInput
                    v-model="comment"
                    type="textarea"
                    name="comentario"
                    validation="required"
                    validation-name="Comentario"
                    :input-class="['form-control']"
                    rows="5"
                    ref="comentario"
                    :help-class="['hint']"
                />
            </CCol>
       </CRow>
      </div>
      <template #footer>
        <CButton
          class="btn btn-info pull-right libro-marca-celeste onHoverDark"
          type="button"
          @click="_cancel"
        >
          {{ cancelButton }}
        </CButton>
        <CButton
          class="btn btn-info pull-right libro-marca-celeste onHoverDark"
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
    name: 'CommentDialogue',

    data: () => ({
        // Parameters that change depending on the type of dialogue
        title: undefined,
        message: undefined, // Main text content
        okButton: undefined, // Text for confirm button; leave it empty because we don't know what we're using it for
        cancelButton: 'Go Back', // text for cancel button
        
        comment: "",
        // Private variables
        resolvePromise: undefined,
        rejectPromise: undefined,
        returnValue: undefined,

        showModal: false,
    }),
    methods: {
        show(opts = {}) {
            this.title = opts.title
            this.message = opts.message
            this.okButton = opts.okButton
            if (opts.cancelButton) {
                this.cancelButton = opts.cancelButton
            }

            // Once we set our config, we tell the popup modal to open
            this.showModal = true;            

            // this.$nextTick(() => {
            //     
            //     //ref.$refs.comentario.$el.querySelector('textarea').focus();
            //     let obj = document.getElementsByTagName("textarea");
            //     //this.$el.querySelector('textarea').focus();
            // });
    
            // Return promise so the caller can get results
            return new Promise((resolve, reject) => {
                this.resolvePromise = resolve
                this.rejectPromise = reject
            })
        },

        _confirm() {
            
            if (this.comment.replaceAll(' ','') !== ""){
                this.showModal = false;
                this.returnValue = {result: true, comment: this.comment.trim()};
                this.resolvePromise(this.returnValue)
            }
        },

        _cancel() {
            this.showModal = false;
            this.returnValue = {result: false, comment: undefined};
            this.resolvePromise(this.returnValue);
            // Or you can throw an error
            // this.rejectPromise(new Error('User cancelled the dialogue'))
        },
    },
}
</script>

<style>
.window-title{
    margin-left: -1rem;
    margin-right: -1rem;
    margin-top: -1rem;
    border-radius: 5px 5px 0px 0px;
    background-color: #002e6e;
}
.btns {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}

.ok-btn {
    color: red;
    /* text-decoration: underline; */
    line-height: 2.5rem;
    cursor: pointer;
    
    font-weight: bold;
    font-size: .75rem;
}

.cancel-btn {
    padding: 0.5em 1em;
    background-color: #d5eae7;
    color: #35907f;
    border: 2px solid #0ec5a4;
    border-radius: 5px;
    font-weight: bold;
    font-size: .75rem;
    text-transform: uppercase;
    cursor: pointer;
}
</style>