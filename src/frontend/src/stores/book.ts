import { ref } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios'
import { sleep } from '@/utils/sleep'

export const useBookStore = defineStore('book', () => {
  const books = ref<Book[]>([])
  const isLoading = ref(false)

  return {
    books,
    isLoading,

    fetch: async () => {
      const { data } = await axios.get<Book[]>('/book')
      await sleep(500) // Peut être utilisé pour tester le "chargement".
      books.value = data
    },
    create: async (book: Book) => {
      const { data: id } = await axios.post('/book', book)
      books.value.push(book)
      return id
    },
    update: async (book: Book) => {
      await axios.put(`/book/${book.id}`, book)
      const index = books.value.findIndex((b) => b.id === book.id)
      if (index !== -1) {
        books.value[index] = book
      }
    },
    remove: async (id: number) => {
      await axios.delete(`/book/${id}`)
      const index = books.value.findIndex((b) => b.id === id)
      if (index !== -1) {
        books.value.splice(index, 1)
      }
    }
  }
})

export interface Book {
  value(arg0: string, value: any): any
  id: number
  title: string
  author: string
  isAvailable: boolean
}
