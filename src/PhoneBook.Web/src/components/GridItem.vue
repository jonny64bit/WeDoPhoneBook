<script setup lang="ts">
const props = defineProps({
  name: { type: String, required: true },
  phone: { type: String, required: true },
  id: { type: Number, required: true },
})
</script>

<template>
    <li class="list-group-item" :key="props.id">
        <div class="row">
            <div class="col-10">{{props.name}}</div>
            <div class="col-2">
                <div class="d-flex">
                    <button type="button" class="btn btn-primary btn-sm ms-auto">
                        <font-awesome-icon icon="fa-solid fa-pen" fixed-width />
                    </button>
                    <button type="button" class="btn btn-danger btn-sm ms-1">
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