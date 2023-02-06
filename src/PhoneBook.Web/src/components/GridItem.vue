<script setup lang="ts">
import { ref } from 'vue';

const props = defineProps({
  firstName: { type: String, required: true },
  lastName: { type: String, required: true },
  phone: { type: String, required: true },
  id: { type: Number, required: true },
})

const emit = defineEmits(['edit']);

const deleted = ref(false);

const remove = () =>
    fetch("home/delete?id=" + props.id)
        .then((response) => response.json())
        .then((_) => {
            deleted.value = true;
        });

const edit = () => emit("edit", props.id);
</script>

<template>
    <li class="list-group-item" :key="props.id" v-if="!deleted">
        <div class="row">
            <div class="col-10">{{props.firstName}} {{props.lastName}}</div>
            <div class="col-2">
                <div class="d-flex">
                    <button type="button" class="btn btn-primary btn-sm ms-auto" @click="edit">
                        <font-awesome-icon icon="fa-solid fa-pen" fixed-width />
                    </button>
                    <button type="button" class="btn btn-danger btn-sm ms-1" @click="remove">
                        <font-awesome-icon icon="fa-solid fa-xmark" fixed-width />
                    </button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <p class="fw-light">{{props.phone}}</p>
            </div>
        </div>
    </li>
</template>

<style scoped>
</style>