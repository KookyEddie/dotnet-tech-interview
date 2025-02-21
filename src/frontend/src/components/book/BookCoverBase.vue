<template>
  <div
    class="w-[150px] h-[200px] px-2 text-white rounded flex flex-row hover:shadow-xl transition-all"
    :class="bgColor"
  >
    <div class="h-full bg-black/10 w-0.5"></div>
    <div class="flex flex-col items-center justify-center w-full p-2 text-center">
      <slot />
    </div>
  </div>
</template>

<script setup lang="ts">
defineProps<{ bgColor: string }>()
</script>
