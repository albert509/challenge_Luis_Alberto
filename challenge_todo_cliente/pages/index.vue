<template>
    <div>
        <div class="container px-4 mx-auto ">

            <div class="flex justify-center items-center">

                <div class="w-full mt-5 lg:mt-10">

                    <h1 class="text-center mx-auto text-4xl font-bold mb-5 border shadow-md w-fit py-2 px-3 rounded-full bg-white">
                        {{ '<TASKY/>' }}
                    </h1>

                    <div class="card bg-white select-none shadow-lg border-2 max-w-screen-sm mx-auto my-10">
                        <div class="card-body p-2 lg:p-5">

                            <div class="card-title px-3">
                                <input v-model="filterText" type="text" placeholder="Filtrar tareas por titulo" class="input input-bordered w-full" />
                                

                                <div class="dropdown dropdown-end">
                                <label tabindex="0" class="btn m-1">
                                    <Icon name="heroicons:funnel" class="h-5 w-5"/>
                                </label>
                                <ul tabindex="0" class="dropdown-content menu p-2 shadow bg-base-100 rounded-box border w-52 text-sm">

                                    <li class="text-sm hover:bg-transparent text-center py-1 border-b">
                                        Ordenar por
                                    </li>

                                    <li>
                                        <a :class="[
                                           filterBy === 1 ? 'bg-primary text-white' : ''
                                        ]" @click="sortDescBy(1)">
                                           <Icon name="heroicons:calendar"/> Creacion (Desc)
                                        </a>
                                    </li>
                                    <li>
                                        <a :class="[
                                            filterBy === 2 ? 'bg-primary text-white' : ''
                                        ]" @click="sortDescBy(2)">
                                           <Icon name="heroicons:calendar"/> Entrega (Desc)
                                        </a>
                                    </li>
                                    
                                    
                                </ul>
                                </div>

                            </div>

                            <TaskCreator @taskCreated="addNewTask"></TaskCreator>

                            <div class="divider px-3"></div>

                                <TransitionGroup name="taskList" tag="div" class="flex flex-wrap">

                                    <TaskCard v-for="task in filteredTasks" :key="task.id" :task="task" @onDelete="deleteFromList"></TaskCard>

                                </TransitionGroup>

                            </div>
                    </div>

                </div>


            </div>

            
        </div>
    </div>
</template>

<script setup lang="ts">

import { api } from '~/utils/customAxios'

let filterBy = ref(1)
let filterText = ref('')


let tasks = ref<any[]>([])

let filteredTasks = computed(() => {
    let tt = tasks.value
    .filter(t => t.title.toUpperCase().includes(filterText.value.toUpperCase()))

    return [
        ...tt.filter(t => !t.isCompleted),
        ...tt.filter(t => t.isCompleted)
    ]
})

let getAllMyTasks = async () => {

    try {

        let results = await api.get('/Todo/getMyTasks');

        tasks.value = results.data;
        
    } catch (error) {

        console.log("No se encontraron tasks");
        
    }

}

let addNewTask = (newTask: any) => {
    tasks.value.unshift(newTask)
}

let sortDescBy = (arg: number) => {

    if(filterBy.value === arg) return;

    tasks.value = arg === 1 ? 
        tasks.value.sort((a,b) => a.createdAt < b.createdAt ? 1 : -1)
        : tasks.value = tasks.value.sort((a,b) => a.dueDate < b.dueDate ? 1 : -1);

    
    filterBy.value = arg;

}

let deleteFromList = (taskId: number) => {

    tasks.value = tasks.value.filter(t => t.id !== taskId)

}

onMounted( async () => {
    await getAllMyTasks()
})

</script>

<style scoped>

</style>