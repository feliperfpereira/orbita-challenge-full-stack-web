import { api } from 'boot/axios'
import { useQuasar } from 'quasar'

export default function useApi (url) {
  const $q = useQuasar()

  const list = async () => {
    try {
      const { data } = await api.get(url)
      return data
    } catch (error) {
      throw new Error(error)
    }
  }

  const getById = async (id) => {
    try {
      const { data } = await api.get(`${url}/${id}`)
      return data
    } catch (error) {
      throw new Error(error)
    }
  }

  const searchByValue = async (value) => {
    try {
      const { data } = await api.get(`${url}/search/${value}`)
      return data
    } catch (error) {
      throw new Error(error)
    }
  }

  const post = async (form) => {
    try {
      const { data } = await api.post(url, form)
      return data
    } catch (error) {
      debugger
      $q.notify({ message: 'Erro Ao inserir', icon: 'error', color: 'negative' })
      throw new Error(error)
    }
  }

  const update = async (form) => {
    try {
      const { data } = await api.put(`${url}/${form.id}`, form)
      return data
    } catch (error) {
      throw new Error(error)
    }
  }

  const remove = async (id) => {
    try {
      const { data } = await api.delete(`${url}/${id}`)
      return data
    } catch (error) {
      throw new Error(error)
    }
  }

  return {
    list,
    post,
    update,
    remove,
    getById,
    searchByValue
  }
}
