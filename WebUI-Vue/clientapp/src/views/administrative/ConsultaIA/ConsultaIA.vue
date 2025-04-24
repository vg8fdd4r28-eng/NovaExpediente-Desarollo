<template>
  <div>
    <CCard
      style="
        border-radius: 16px !important;
        margin-top: 20px;
        margin-bottom: 20px;
      "
    >
      <CCardHeader
        style="
          border-radius: 16px !important;
          border-bottom: transparent !important;
          background-color: #ffffff;
        "
      >
        <strong>Chat de consulta para busqueda de perfiles</strong>
      </CCardHeader>
      <CCardBody
        style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 20px"
      >
        <vue-advanced-chat
          height="520px"
          :styles="JSON.stringify(styles)"
          :room-id="currentRoomId"
          :current-user-id="currentUserId"
          :rooms="JSON.stringify(rooms)"
          :messages="JSON.stringify(messages)"
          :room-actions="JSON.stringify(roomActions)"
          show-emojis="false"
          show-reaction-emojis="false"
          show-files="false"
          show-audio="false"
          :auto_scroll="auto_scroll"
          :theme="theme"
          :messages-loaded="messagesLoaded"
          :rooms-loaded="roomsLoaded"
          :loading-rooms="loadingRooms"
          :text-messages ="JSON.stringify(textmessages)"
          @send-message="sendMessage($event.detail[0])"
          @add-room="addRoom()"
          @fetch-messages="fetchMessages($event.detail[0])"
          @open-file="openFile($event.detail[0])"
        />
      </CCardBody>
    </CCard>
  </div>
</template>
  
  <script>
import Vue from "vue";
import _ from "lodash";
// métodos comunes
import common from "../common-admin";
import {
  GetListaConversaciones,
  GetListaDetalleConversacion,
  InsertConversacionMensaje,
  InsertConversaciones,
} from "../request";

import {
  GetPhoto
} from "../../../containers/request";
import { error } from "jquery";

export default {
  name: "ConsultaIA",
  mixins: [common],
  props: {
    origen: { type: String, default: "" }, // origen desde donde se accedio la vista
    idTipoSolicitud: { type: Number, default: 0 }, // id del tipo de la solicitud generada
    tipoSolicitud: { type: String, default: "" }, // nombre del tipo de la solicitud generada
    idSolicitud: { type: Number, default: null }, // id de la solicitud, para obtener el obj Json con la informacion a cargar
  },
  components: {},
  data() {
    return {
      data: {},
      showModal: false,
      showModalAgregar: false,
      showModalConfirmacion: false,
      showModalVisorAdjuntos: false,
      esEdicion: false,
      visorEnable: false,
      tituloModal: "",
      srcDocumento: "",
      theme:"light",
      value: null,
      options: [],
      search: "",
      currentUserId: this.$root.infoColaboradorActual.correo,
      styles: { 
        icons: 
        { 
          add: '#0C7BBE',
          send: '#0C7BBE',
          sendDisabled: '#3c4b64',
          checkmark: '#3c4b64',
          checkmarkSeen: '#0C7BBE'
        },
        message: {
          background: '#fff',
          color: '#212529',
			    colorStarted: '#9ca6af',
        }
      },
      currentRoomId:"",
      rooms: [],
      messages: [],
      roomActions: [],
      loadingRooms: true,
      roomsLoaded: false,
      messagesLoaded: false,
      avatar: "",
      avatarIA: "../avatarIA2.png",
      idIA: "IA",
      textmessages:{
        ROOMS_EMPTY: 'No conversations',
        ROOM_EMPTY: 'No conversation selected',
        NEW_MESSAGES: 'New messages',
        MESSAGE_DELETED: 'This message has been deleted',
        MESSAGES_EMPTY: 'No messages',
        CONVERSATION_STARTED: 'The conversation started on:',
        TYPE_MESSAGE: 'Type your message',
        SEARCH: 'Search',
        IS_ONLINE: '🤖 AI online',
        LAST_SEEN: 'Last seen: ',
        IS_TYPING: '🤖 is typing...',
        CANCEL_SELECT_MESSAGE: 'Cancel selection'
        /*
        ROOMS_EMPTY: 'No hay conversaciones',
        ROOM_EMPTY: 'No se ha seleccionado ninguna conversación',
        NEW_MESSAGES: 'Nuevos mensajes',
        MESSAGE_DELETED: 'Este mensaje ha sido eliminado',
        MESSAGES_EMPTY: 'No hay mensajes',
        CONVERSATION_STARTED: 'La conversación comenzó el:',
        TYPE_MESSAGE: 'Escribe tu mensaje',
        SEARCH: 'Buscar',
        IS_ONLINE: '🤖 IA en línea',
        LAST_SEEN: 'Última conexión: ',
        IS_TYPING: '🤖 está escribiendo...',
        CANCEL_SELECT_MESSAGE: 'Cancelar selección'
        */
      },
      auto_scroll: {
          send: {
            new: true,
            newAfterScrollUp: true,
          },
          receive: {
            new: true,
            newAfterScrollUp: true,
          },
      }
    };
  },
  async mounted() {
    await this.refreshConversations("");
    await this.getPhoto();
  },
  methods: {
    async demoload() {
      //rooms

      this.rooms = [
        {
          roomId: "1",
          roomName: "Room 1",
          avatar: "assets/Clientes.svg",
          unreadCount: 4,
          index: 3,
          lastMessage: {
            _id: "xyz",
            content: "Last message received",
            senderId: "1234",
            username: "John Doe",
            timestamp: "10:20",
            saved: true,
            distributed: false,
            seen: false,
            new: true,
          },
          users: [
            {
              _id: "1234",
              username: "John Doe",
              avatar: "assets/imgs/doe.png",
              status: {
                state: "online",
                lastChanged: "today, 14:30",
              },
            },
            {
              _id: "4321",
              username: "John Snow",
              avatar: "assets/imgs/snow.png",
              status: {
                state: "offline",
                lastChanged: "14 July, 20:00",
              },
            },
          ],
          typingUsers: [4321],
        },
      ];

      //messages

      this.messages = [
        {
          _id: "7890",
          indexId: 12092,
          content: "Message 1",
          senderId: "1234",
          username: "John Doe",
          avatar: "assets/imgs/doe.png",
          date: "13 November",
          timestamp: "10:20",
          system: false,
          saved: true,
          distributed: true,
          seen: true,
          deleted: false,
          failure: true,
          disableActions: true,
          disableReactions: false,
          files: [
            {
              name: "My File",
              size: 67351,
              type: "png",
              audio: true,
              duration: 14.4,
              url: "https://firebasestorage.googleapis.com/...",
              preview: "data:image/png;base64,iVBORw0KGgoAA...",
              progress: 88,
            },
          ],
          // reactions: {

          // },
          replyMessage: {
            content: "Reply Message",
            senderId: "4321",
            files: [
              {
                name: "My Replied File",
                size: 67351,
                type: "png",
                audio: true,
                duration: 14.4,
                url: "https://firebasestorage.googleapis.com/...",
                preview: "data:image/png;base64,iVBORw0KGgoAA...",
              },
            ],
          },
        },
      ];

      // finally
      this.roomsLoaded = true;
      this.messagesLoaded = true;
      
    },
    // eslint-disable-next-line no-unused-vars
    async submit(formValues) {
      // var res = await SendNotificationPush(this.data);

      Vue.$toast.success("Se ha enviado el mensaje", {});
    },
    generateMessageId() {
      return 'Message-' + Math.random().toString(36).substr(2, 9);
    },
    activeIA(roomId,content){
      const roomIndex = this.rooms.findIndex(room => room.roomId === roomId);
      const roomUserIndex = this.rooms[roomIndex].users.findIndex(user => user._id === "IA");
      this.rooms[roomIndex].users[roomUserIndex].status.state = "online";
      this.rooms[roomIndex].typingUsers = ["IA"];

      if(this.rooms[roomIndex].lastMessage.content == "No message")
      {
        this.rooms[roomIndex].roomName = content.substring(0, 20);
      }
      return roomIndex;
    },
    updateLastMessage(roomId,content){
      const roomIndex = this.rooms.findIndex(room => room.roomId === roomId);
      this.rooms[roomIndex].lastMessage.content = content.substring(0, 20);
    },
    fetchMessages({ room, options,roomId = {} }) {

      this.messagesLoaded = false;
      this.messages = [];
      this.refreshMessages(room.roomId);
     
    },
    async sendMessage({ content, roomId, files, replyMessage}) {

      const messageId = this.generateMessageId();
      //mensaje local
      this.messages.push({
        _id: messageId,
        indexId: this.messages.length > 0 ? this.messages.length + 1 : 0,
        content: content,
        senderId: this.$root.infoColaboradorActual.correo,
        username: this.$root.infoColaboradorActual.name,
        avatar: this.avatar,
        date: new Date().toLocaleDateString(),
        system: false,
        saved: true,
        distributed: false,
        seen: false,
        failure: false,
        disableActions: true,
        disableReactions: true,
      });

      const foundMessage = this.messages.find(message => message._id === messageId);
      
      const roomIndex = this.activeIA(roomId,content);

      await InsertConversacionMensaje({
        conversationId: roomId,
        userId: this.$root.infoColaboradorActual.correo,
        message: content,
      })
      .then(async (response)  => {
        foundMessage.saved = true;
        foundMessage.distributed = true;
        await this.refreshMessages(roomId);
      })
      .finally(() =>{
        setTimeout(() => {
          this.rooms[roomIndex].typingUsers = []; 
        }, 1000);
      })
      .catch((error) => {
        foundMessage.failure = true;
        Vue.$toast.error(error, {});
        console.log(error);
      });

    },
    async addRoom() {

      await InsertConversaciones({
        userId: this.$root.infoColaboradorActual.correo,
      })
      .then((response) => {
        this.refreshConversations(response.data.conversationId);
        Vue.$toast.success("Se ha creado una nueva conversación", {});
      })
      .catch((error) => {
        Vue.$toast.error(error);
      });

    },
    formatTimestamp(date) {
      const now = new Date();
      const dateToCheck = new Date(date);

      const isToday = now.toDateString() === dateToCheck.toDateString();

      if (isToday) {
          return dateToCheck.toLocaleTimeString();
      } else {
          return dateToCheck.toLocaleDateString();
      }
    },
    async refreshConversations(roomId) {
    
      await GetListaConversaciones(this.$root.infoColaboradorActual.correo, 0)
      .then((response) => {
        const conversations = response.data.conversations;
        this.rooms = conversations.map(({ conversationId, startTime, subject,lastMessageTime,lastMessage }) => ({
          roomName: subject,
          roomId: conversationId,
          lastMessage: {
            _id: "xyz",
            content: lastMessage,
            senderId: this.$root.infoColaboradorActual.correo,
            username: this.$root.infoColaboradorActual.nombre,
            timestamp: this.formatTimestamp(lastMessageTime),
            saved: true,
            distributed: true,
            seen: true,
            new: false,
          },
          users: [
            {
              _id: this.$root.infoColaboradorActual.correo,
              username: this.$root.infoColaboradorActual.nombre,
              avatar: this.$root.infoColaboradorActual.avatar,
              status: {
                state: "online",
                lastChanged: Date.now(),
              },
            },
            {
              _id: this.idIA,
              username: this.idIA,
              avatar: this.avatarIA,
              status: {
                state: "offline",
                lastChanged: this.formatTimestamp(lastMessageTime),
              }
            },
          ],
          typingUsers:[]
        }));
      })
      .catch((error)=>{
        Vue.$toast.error(error);
      })
      .finally(() => {
        if(roomId.length > 0){
          this.currentRoomId = roomId.toString();
        }
        this.loadingRooms = false;
        this.roomsLoaded = true;
      });
      
    },
    async refreshMessages(roomId) {

      await GetListaDetalleConversacion(roomId)
      .then((response) => {
        const data = response.data.messages;

        if(data.length > 0){
          let messagesVoid = _.reverse(data);

          this.messages = messagesVoid?.flatMap((e,index) => {
            
            const baseMessage = {
              _id: e.messageId,
              indexId: index,
              content: e.aiResponse ? e.aiResponse : e.userMessage,
              senderId: e.aiResponse ? this.idIA: this.$root.infoColaboradorActual.correo,
              username: e.aiResponse ? this.idIA : this.$root.infoColaboradorActual.name,
              avatar: e.aiResponse ? this.avatarIA : this.avatar,
              date: new Date(e.timestamp).toLocaleDateString(),
              system: false,
              saved: true,
              distributed: true,
              seen: true,
              disableActions: true,
              disableReactions: true,
            };

            const messages = [baseMessage];

            if (e.references?.length > 0) {
              
              const referenceCount = e.references.length;
              const messageContent = referenceCount === 1 
              ? "El resultado de la búsqueda es el siguiente: " 
              : "Los resultados de búsqueda son los siguientes: ";

              messages.push({
                _id: `${e.messageId}-ref`,
                indexId: `${index}-ref`,
                content: messageContent,
                senderId: this.idIA,
                username: this.idIA,
                avatar: this.avatarIA,
                date: new Date(e.timestamp).toLocaleDateString(),
                system: true,
                saved: true,
                distributed: true,
                seen: true,
                disableActions: true,
                disableReactions: true
              });

              e.references.forEach((ref, refIndex) => {
                const referenceMessage = {
                  _id: `${e.messageId}-ref-${refIndex}`,
                  indexId: `${index}-ref-${refIndex}`,
                  content: `Te comparto el cv de ${ref.name} - ${ref.email}`,
                  senderId: this.idIA,
                  username: this.idIA,
                  avatar: this.avatarIA,
                  date: new Date(e.timestamp).toLocaleDateString(),
                  files: [this.formattedFile(ref)],
                  system: false,
                  saved: true,
                  distributed: true,
                  seen: true,
                  disableActions: true,
                  disableReactions: true
                };
              messages.push(referenceMessage);
          
              });

            }
            return messages;
          });

          this.updateLastMessage(roomId,this.messages[this.messages.length - 1].content)
        }
      })
      .finally((e) => {
        this.messagesLoaded = true;
      })
      .catch((error) =>{
        Vue.$toast.error(error);
      });

    },
    async getPhoto() {
      try {
        
        const {
          data: { fotoColaborador }
        } =  await GetPhoto(this.$root.infoColaboradorActual.correo);

        if(fotoColaborador == null)
          this.avatar = "/img/avatars/currentUser.jpg";
        else
          this.avatar = fotoColaborador?.foto;   

      } catch (error) {
        this.avatar = "/img/avatars/currentUser.jpg";
      }
    },
    formattedFile(file) {

      const formatted =
      {
        name: file.name,
        size: file.size?"0":file.size,
        type: file.extension,
        extension: file.extension,
        url: file.url 
      }

			return formatted;
		},
    openFile({ file }) {
			window.open(file.file.url, '_blank')
		},

  }
};
</script>
  
<style>

/* FIN */
</style>

  