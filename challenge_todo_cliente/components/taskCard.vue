<template>
    <div class="w-full p-3 relative group">
        <div :class="[
            'flex-1 card border-2 shadow hover:shadow-md transition-all duration-300  cursor-pointer',
            task.priorityId === 1 ? 'bg-success/60 hover:bg-success border-success' : '',
            task.priorityId === 2 ? 'bg-info/60 hover:bg-info border-info' : '',
            task.priorityId === 3 ? 'bg-warning/60 hover:bg-warning border-warning' : '',
            task.priorityId === 4 ? 'bg-error/60 hover:bg-error border-error' : '',
        ]" @click="markComplete(task.id)">
            <div class="card-body p-3">

                <div class="flex ">
                    <input v-model="task.isCompleted" type="checkbox" class="checkbox mr-3 bg-white" />
                    <span :class="[
                        'mb-1',
                        task.isCompleted ? 'line-through' : 'font-bold'
                    ]">
                        {{ task.title }}
                    </span>
                </div>

                <div class="text-xs ml-auto">
                    {{ formatDate(task.createdAt) }}
                    <Icon name="heroicons:arrow-right" class="mx-2" />
                    <span class="font-bold">
                        {{ formatDate(task.dueDate) }}
                    </span>
                </div>
            </div>
        </div>

        <button :disabled="deleting === task.id" @click="deleteTask(task.id)" :class="[
            'btn btn-error btn-sm btn-square absolute top-5 shadow-md border border-white right-5 text-white opacity-0 group-hover:opacity-100 transition-all duration-300',
            deleting === task.id ? 'loading opacity-100' : ''
        ]">
            <Icon v-if="deleting !== task.id" name="heroicons:trash" size="25" />
        </button>

        <div v-if="completando === task.id" class="absolute inset-0 bg-white/30 px-20 pt-10">
            <progress class="progress"></progress>
        </div>

    </div>
</template>

<script setup lang="ts">

import { api } from '~/utils/customAxios'
import { toast, swal } from '~/utils/customSwal'

let props = defineProps(['task'])

let emit = defineEmits(['onDelete', 'onUpdate'])

let formatDate = (value: string) => {
    return new Date(value).toLocaleDateString()
}

let deleting = ref(0)

let deleteTask = async (taskId: number) => {

    try {

        let sealResponse = await swal.fire({
            text: 'Esta a punto de eliminar esta tarea, desea continuar?',
            showConfirmButton: true,
            showDenyButton: true,
            confirmButtonText: 'Si, borrar',
            denyButtonText: 'Cancelar'
        })

        if (!sealResponse.isConfirmed) return;

        deleting.value = taskId;

        let response = await api.delete(`/Todo/deleteTask?taskId=${taskId}`);

        let deletedTask = response.data;

        // tasks.value = tasks.value.filter(t => t.id !== taskId)

        emit('onDelete', taskId)

        toast.fire({
            icon: 'info',
            text: 'Tarea eliminada'
        })

        deleting.value = 0;

    } catch (error) {

        toast.fire({
            icon: 'info',
            text: 'Error tratando de eliminar la tarea.'
        })

        deleting.value = 0;

    }

}

let completando = ref(0);

let markComplete = async (taskId: number) => {

    try {

        let tarea = props.task//tasks.value.find(t => t.id === taskId)

        if (tarea.isCompleted) {

            let sealResponse = await swal.fire({
                text: 'Esta tarea ya ha sido completada, la quiere marcar como no completada?',
                showConfirmButton: true,
                showDenyButton: true,
                confirmButtonText: 'Si!',
                denyButtonText: 'Cancelar'
            })

            if (!sealResponse.isConfirmed) return;

        }

        completando.value = taskId;

        let response = await api.put(`/Todo/markCompleted?taskId=${taskId}`);

        let updatedTask = response.data;

        tarea.isCompleted = updatedTask.isCompleted;

        emit('onUpdate', tarea)

        toast.fire({
            icon: 'success',
            text: 'La tarea actualizada.'
        })

        completando.value = 0

    } catch (error) {

        toast.fire({
            icon: 'error',
            text: 'error completando la tarea.'
        })

        completando.value = 0

    }

}

</script>

<style scoped>

</style>