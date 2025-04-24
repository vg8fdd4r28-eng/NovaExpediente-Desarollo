<template>
    <!-- <div class="comment">
        <div class="avatar">
            <img src="http://via.placeholder.com/100x100/a74848" alt="">
        </div>
        <div class="text">
            <a class="username" href="#">@{{ comment.responsable }}</a> <span>{{ comment.comentario }}</span>
        </div>
    </div> -->
    <div class="d-flex justify-content-center row">
        <div class="col-md-8">
            <div class="d-flex flex-column comment-section">
                <div class="bg-white p-2">
                    <div class="d-flex flex-row user-info">
                        <avatar :size="36" color="white" :username="comment.responsable"></avatar>
                        <!-- <img  src="https://cdn3.iconfinder.com/data/icons/business-office-2/512/memory_card_pencil-512.png" width="40"> -->
                        <div class="d-flex flex-column justify-content-start ml-2">
                            <span class="d-block font-weight-bold name" >{{ comment.responsable }}</span>
                            <span class="date text-black-50">Acción: {{ comment.accion }} - {{ comment.fecha }}</span>
                        </div>
                    </div>
                    <div class="mt-2">
                        <p class="comment-text">
                            {{ comment.comentario }}
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Avatar from 'vue-avatar'
    export default {
        name: 'singleComment',
        props: ['comment'],
         components: {
                Avatar
            },
    }
</script>

<style>
/* Single-comment component */
/* .comment {
    display: flex;
    padding: 5px;
    margin-bottom: 10px;
    align-items: center;
    color: #333;
    background-color: #F2F2F2;
    border-radius: 30px;
    box-shadow: 1px 1px 3px rgba(0, 0, 0, 0.2);
}
.comment .avatar {
    align-self: flex-start;
}
.comment .avatar > img {
    width: 32px;
    height: 32px;
    border-radius: 100%;
    align-self: start;
}
.comment .text {
    text-align: left;
    margin-left: 5px;
}
.comment .text span {
    margin-left: 5px;
}
.comment .text .username {
    font-weight: bold;
    color: #333;
} */
.date {
    font-size: 11px
}

.comment-text {
    font-size: 12px;
    color: #212529;
}

.fs-12 {
    font-size: 12px
}

.shadow-none {
    box-shadow: none
}

.name {
    color: #007bff
}

.cursor:hover {
    color: blue
}

.cursor {
    cursor: pointer
}

.textarea {
    resize: none
}
</style>