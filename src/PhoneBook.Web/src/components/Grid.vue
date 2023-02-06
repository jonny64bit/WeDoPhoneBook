<script setup lang="ts">
import GridItem from './GridItem.vue'
import { onMounted, ref } from 'vue'
import type { GridRow } from '@/models';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

const search = ref("");
const records = ref<GridRow[]>([]);
const firstName = ref("");
const lastName = ref("");
const phoneNumber = ref("");
const editingId = ref(0);
const mode = ref<"grid" | "editing" | "adding">("grid");

const loadPage = async () => {
    fetch("home/read?search=" + search.value)
        .then((response) => response.json())
        .then((data) => {
            records.value.splice(0);
            data.records.forEach((x: GridRow) => records.value.push(x));
            mode.value = 'grid';
        });
};
onMounted(loadPage);

const reload = () => loadPage();

const setupEdit = (id: number) => {
    editingId.value = id;
    const record = records.value.find(x => x.id == id);
    if (record == undefined) return;
    firstName.value = record.firstName;
    lastName.value = record.lastName;
    phoneNumber.value = record?.phoneNumber;
    mode.value = 'editing';
};

const setupAdd = () => {
    firstName.value = "";
    lastName.value = "";
    phoneNumber.value = "";
    mode.value = 'adding';
}

const process = () => {
    if (mode.value == 'adding') {
        postData("home/add", {
            "FirstName" : firstName.value,
            "LastName" : lastName.value,
            "PhoneNumber" : phoneNumber.value
        }).then((_) => {
            loadPage();
        });
    }
    else if (mode.value == 'editing') {
        postData("home/edit", {
            "FirstName" : firstName.value,
            "LastName" : lastName.value,
            "PhoneNumber" : phoneNumber.value,
            "id" : editingId.value
        }).then((_) => {
            loadPage();
        });
    }
}

async function postData(url: string, value: any) {
    const response = await fetch(url, {
        method: "POST",
        cache: "no-cache",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(value),
    });
    return response.json();
}
</script>

<template>
    <div class="row">
        <div class="col">
            <div class="row" v-if="mode == 'grid'">
                <div class="col">
                    <h2>Contacts</h2>
                </div>
                <div class="col">
                    <div class="d-flex">
                        <button type="button" class="btn btn-primary ms-auto" @click="setupAdd">
                            <font-awesome-icon icon="fa-solid fa-plus" />
                            Add Contact
                        </button>
                    </div>
                </div>
            </div>
            <div class="row" v-if="mode == 'grid'">
                <div class="col">
                    <input type="text" v-model="search" class="form-control mt-2"
                        placeholder="Search for contact by last name..." @keyup="reload" />
                </div>
            </div>
            <div class="row" v-if="mode == 'grid'">
                <div class="col">
                    <ul class="list-group mt-3">
                        <GridItem v-for="record in records" :key="record.id" :first-name="record.firstName"
                            :last-name="record.lastName" :phone="record.phoneNumber" :id="record.id"
                            @edit="setupEdit" />
                    </ul>
                </div>
            </div>
            <div class="row" v-if="mode == 'adding'">
                <div class="col">
                    <h2>Add Contact</h2>
                </div>
            </div>
            <div class="row" v-if="mode == 'editing'">
                <div class="col">
                    <h2>Edit Contact</h2>
                </div>
            </div>
            <div class="row" v-if="mode == 'adding' || mode == 'editing'">
                <div class="col">
                    <div class="mb-3">
                        <label for="first-name" class="form-label">First Name</label>
                        <input type="text" class="form-control" id="first-name" v-model="firstName">
                    </div>
                    <div class="mb-3">
                        <label for="last-name" class="form-label">Last Name</label>
                        <input type="text" class="form-control" id="last-name" v-model="lastName">
                    </div>
                    <div class="mb-3">
                        <label for="phone-number" class="form-label">Phone Number</label>
                        <input type="text" class="form-control" id="phone-number" v-model="phoneNumber">
                    </div>
                    <button type="submit" class="btn btn-primary" @click="process">
                        <font-awesome-icon icon="fa-solid fa-check" />
                        Submit
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>

</style>