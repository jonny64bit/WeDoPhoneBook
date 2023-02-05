<script setup lang="ts">
import GridItem from './GridItem.vue'
import { onMounted, ref } from 'vue'
import type { GridRow } from '@/models';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

const search = ref("");
const records = ref<GridRow[]>([]);

const loadPage = async () => {
    fetch("home/read?search=" + search.value)
        .then((response) => response.json())
        .then((data) => {
            records.value.splice(0);
            data.records.forEach((x: GridRow) => records.value.push(x));
        });
};
onMounted(loadPage);

function reload(event: Event) {
    loadPage();
}
</script>

<template>
    <div class="row">
        <div class="col">
            <div class="row">
                <div class="col">
                    <h2>Contacts</h2>
                </div>
                <div class="col">
                    <div class="d-flex">
                        <button type="button" class="btn btn-primary ms-auto">
                            <font-awesome-icon icon="fa-solid fa-plus" />
                            Add Contact
                        </button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <input type="text" v-model="search" class="form-control mt-2"
                        placeholder="Search for contact by last name..." @keyup="reload"/>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <ul class="list-group mt-3">
                        <GridItem v-for="record in records" :key="record.id" :name="record.name"
                            :phone="record.phoneNumber" :id="record.id" />
                    </ul>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>

</style>