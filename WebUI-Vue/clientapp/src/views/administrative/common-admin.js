// Utilitarios
import { aplicarFormatoFecha } from "../../utils";

export default {
    methods: {
        formatearFecha(fecha){
            return aplicarFormatoFecha(fecha, "dd-MM-yyyy HH:mm")
        },
        showLoader() {
            return this.$loading.show({
                // Optional parameters
                container: this.fullPage ? null : this.$refs.formContainer,
                canCancel: true,
                onCancel: this.onCancel
            });
        },
        hideLoader(loader) {
            setTimeout(() => {
                loader.hide();
            }, 300);
        }
    }
};