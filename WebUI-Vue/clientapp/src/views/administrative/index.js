import PickList from '../generales/Componentes/PickList';
import ResumenColaborador from "../masterdata/ResumenColaborador";
import BotoneraMasterData from "../masterdata/BotoneraMasterData.vue";
import Vue from "vue";

// métodos comunes
import common_admin from "./common-admin.js";
import common from "../masterdata/common.js";

import ConfirmDialogue from "../generales/Componentes/PopUpModal/ConfirmDialogue.vue";
import CommentDialogue from "../generales/Componentes/PopUpModal/CommentDialogue.vue";
import Comments from "../generales/Componentes/Comments/Comments.vue";

export default {
    common_admin,
    common,
    components: {
        PickList,
        ResumenColaborador,
        BotoneraMasterData,
        Vue,
        ConfirmDialogue,
        CommentDialogue,
        Comments
    }
}