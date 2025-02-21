<script setup lang="ts">
import { useBookStore, type Book } from '@/stores/book'
import BookForm from '@/components/book/BookForm.vue'
import Button, { ButtonStylingMode } from '@/components/Button.vue'
import { computed, ref, watch } from 'vue'
import { useRouter } from 'vue-router'

const bookStore = useBookStore()
bookStore.fetch()

const router = useRouter()

const book = ref<Book>({
  id: 1,
  title: '',
  author: ''
})

// Ne pas retirer.
watch(book, () => console.log(`Une modification est survenue!`, book.value), {deep : true})

const isValid = computed(
  () =>
    book.value.title.trim().length > 0 &&
    book.value.author.trim().length > 0 &&
    book.value.id > 0 &&
    bookStore.books.every((b) => b.id !== book.value.id)
)

async function handleCreate() {
  if (!isValid.value) return

  const id = await bookStore.create(book.value)
  router.push({ name: 'book_update', params: { id } })
}
</script>

<template>
  <div class="flex flex-col items-center p-4">
    <div class="flex flex-col w-full max-w-lg gap-4 p-4 bg-white rounded shadow-xl">
      <span class="text-xl font-bold">Nouveau livre</span>
      <div class="flex flex-row gap-4">
        <BookForm v-model="book" :is-id-change-enabled="true" />
      </div>
      <Button :styling-mode="ButtonStylingMode.Primary" :disabled="!isValid" @click="handleCreate()"
        >Créer</Button
      >
    </div>
  </div>
</template>
