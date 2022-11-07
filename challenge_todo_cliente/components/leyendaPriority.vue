<template>
<div class="dropdown dropdown-right">
  <label tabindex="0" class="">
    <Icon name="heroicons:exclamation-circle" class="mb-1 cursor-help" size="20"/>
  </label>
  <div tabindex="0" class="dropdown-content card card-compact bg-white w-60 p-2 shadow-md border">
    <div class="card-body">
      <h3 class="card-title text-sm">Leyenda de prioridades</h3>

      <ul>
        <li>
            <Icon name="heroicons:stop-20-solid" class="text-success"/> Baja
        </li>
        <li>
            <Icon name="heroicons:stop-20-solid" class="text-info"/> Media
        </li>
        <li>
            <Icon name="heroicons:stop-20-solid" class="text-warning"/> Alta
        </li>
        <li>
            <Icon name="heroicons:stop-20-solid" class="text-error"/> Urgente
        </li>
      </ul>
      
    </div>
  </div>
</div>
</template>