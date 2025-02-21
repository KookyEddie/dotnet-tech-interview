<script setup lang="ts">
import { useRouter } from 'vue-router'
import { useBookStore } from '@/stores/book'
import { ref, onMounted, computed } from 'vue'
import { Icon } from '@iconify/vue'
import BookCover from '@/components/book/BookCover.vue'
import BookCoverAdd from '@/components/book/BookCoverAdd.vue'

const router = useRouter()
const bookStore = useBookStore()
const isLoading = ref(true)
const searchQuery = ref('')
const sortOrder = ref('asc')

onMounted(async () => {
  bookStore.isLoading = true 
  await bookStore.fetch()
  bookStore.isLoading = false 
})

const deleteBook = async (id: number) => {
  if (confirm("Voulez-vous vraiment supprimer ce livre ?")) {
    await bookStore.remove(id)
  }
}

const filteredBooks = computed(() => {
  return bookStore.books.filter(book =>
    book.title.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
    book.author.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
})

const sortedBooks = computed(() => {
  return [...filteredBooks.value].sort((a, b) => {
    if (sortOrder.value === 'asc') {
      return a.title.localeCompare(b.title)
    } else {
      return b.title.localeCompare(a.title)
    }
  })
})

</script>

<template>
  <div v-if="!bookStore.isLoading" class="flex flex-col gap-4 p-4">
    <div class="flex flex-row gap-4">
      <input
        v-model="searchQuery"
        type="text"
        placeholder="Rechercher un livre..."
        class="p-2 border rounded w-full"
      />

      <select v-model="sortOrder" class="p-2 border rounded">
        <option value="asc">A-Z</option>
        <option value="desc">Z-A</option>
      </select>
    </div>
    <div class="flex flex-row flex-wrap gap-4">
      <div v-for="book in sortedBooks" :key="book.id" class="relative">
        <BookCover
          class="cursor-pointer"
          :bgColor="book.isAvailable === true ? 'bg-green-500' : 'bg-red-500'"
          :title="book.title"
          :author="book.author"
          @click="router.push({ name: 'book_update', params: { id: book.id } })"
        />
        <button
          class="absolute top-2 right-2 text-white p-1 rounded"
          @click="deleteBook(book.id)"
        >
        <Icon icon="material-symbols:delete-forever" class="text-xl" />
        </button>
      </div>
      <BookCoverAdd />
    </div>
  </div>
  <div v-else>Chargement...</div>
</template>
