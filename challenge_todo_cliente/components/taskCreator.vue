<template>
    <div>

        <div class="px-3">
            <label for="taskCreator" class="btn btn-block btn-outline btn-sm">
                Agregar tarea
                <Icon name="heroicons:plus" class="h-5 w-5 ml-3" />
            </label>
        </div>



        <input v-model="popup" type="checkbox" id="taskCreator" class="modal-toggle" />

        <div class="modal">
            <div :class="[
                'modal-box w-11/12 lg:w-5/12 max-w-5xl border-4'
            ]">

                <div class="flex flex-wrap">
                    
                    <div class="w-full p-3">
                        <h3 class="font-bold text-lg mb-5 w-full">
                            Agregando tarea
                        </h3>
                        <input v-model="newTask.title" type="text" placeholder="Ej: iniciar el curso de threejs" class="input input-bordered w-full" />
                        
                        <div class="w-full text-right mt-1" :class="{'text-error' : !canSave}">
                            <small class="mr-3 text-xs">{{ newTask.title.length }}/50</small>
                        </div>
                        
                    </div>

                </div>

                <div class="flex flex-wrap">
                    
                    <div class="w-full lg:w-6/12 p-3">

                        <div class="form-control w-full">

                            <label class="ml-1" for="">
                                Prioridad <LeyendaPriority></LeyendaPriority>
                            </label>

                            <select v-model="newTask.priorityId" :class="[
                                'select select-bordered w-full',
                                newTask.priorityId == 1 ? 'select-success' : '',
                                newTask.priorityId == 2 ? 'select-info' : '',
                                newTask.priorityId == 3 ? 'select-warning' : '',
                                newTask.priorityId == 4 ? 'select-error' : '',
                            ]">

                                <option disabled selected>Prioridad de la tarea</option>

                                <option :value="p.id" v-for="p in priorities" :key="p.id">
                                    {{ p.name }}
                                </option>

                            </select>
                        </div>
                    </div>

                    <div class="w-full lg:w-6/12 p-3">

                        <div class="form-control w-full">
                            <label class="ml-1" for="">Fecha limite</label>
                            <input v-model="newTask.dueDate" type="date" placeholder="Type here" class="input input-bordered w-full" />
                        </div>

                    </div>

                </div>
                
                <div class="modal-action">

                    <div class="w-full lg:w-6/12 p-3">
                        <label for="taskCreator" class="btn btn-block">Cerrar</label>
                    </div>
                    <div class="w-full lg:w-6/12 p-3">
                        <button :disabled="!canSave || saving" @click="saveTask" :class="['btn btn-block btn-primary', saving ? 'loading' : '']">
                            <span v-if="!saving">
                                Guardar tarea
                            </span>
                        </button>
                    </div>

                </div>
                
            </div>
        </div>

    </div>
</template>

<script setup lang="ts">

import { api } from '~/utils/customAxios'

import { toast } from '~/utils/customSwal'

let popup = ref(false)

let getBlankTask = () => ({
  title: "",
  dueDate: new Date().toISOString().split('T')[0],
  priorityId: 1,
  isCompleted: false
})

let newTask = ref(getBlankTask())

let saving = ref(false)

let canSave = computed(() => newTask.value.title && newTask.value.title.length <= 50 ? true : false)

let emits = defineEmits(['taskCreated'])

let priorities = ref<any[]>([]);

let getPriorities = async () => {

    try {

        let result = await api.get('/Priority/getPriorities');

        priorities.value = result.data;
        
    } catch (error) {

        console.log("No se pudo obtener los priorities: ", error);
        
    }

}

onMounted(async () => {
    await getPriorities();
})


let saveTask = async () => {

    try {

        saving.value = true;

        let results = await api.post('/Todo/addTask', newTask.value)

        let taskA = results.data;

        emits('taskCreated', taskA)

        toast.fire({
            icon: 'success',
            text: 'Se ha agregado una tarea nueva!'
        })

        newTask.value = getBlankTask()

        saving.value = false;

        popup.value = false
        
    } catch (error) {

        console.log("Un error tratando crear el task")

        toast.fire({
            icon: 'error',
            text: 'Error tratando de agragar una nueva tarea.'
        })

        saving.value = false;
        
    }

}

</script>

<style scoped>

</style>