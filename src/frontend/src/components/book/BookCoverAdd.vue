<script setup lang="ts">
import { Icon } from '@iconify/vue'
import BookCoverBase from './BookCoverBase.vue'
import { useRouter } from 'vue-router'

defineProps<{ bgColor?: string }>()
const router = useRouter()

function create() {
  router.push({ name: 'book_create' })
}

</script>

<template>
  <BookCoverBase class="cursor-pointer" :bgColor="bgColor ?? 'bg-green-500'" @click="create">
    <Icon class="text-5xl text-green-300" icon="octicon:plus-circle-16" />
  </BookCoverBase>
</template>
