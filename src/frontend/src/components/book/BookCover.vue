<script setup lang="ts">
import BookCoverBase from './BookCoverBase.vue'

defineProps<{
  title: string
  author: string
  bgColor: string
}>()
</script>

<template>
  <BookCoverBase :bgColor="bgColor">
    <span class="font-bold text-ellipsis line-clamp-3" :title="title">{{ title }}</span>
    <span class="text-sm text-white/50 line-clamp-1" :title="author">{{ author }}</span>
  </BookCoverBase>
</template>
