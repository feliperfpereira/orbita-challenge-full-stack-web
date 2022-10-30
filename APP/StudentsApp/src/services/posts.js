import useApi from 'src/composables/UseApi'

export default function postsService () {
  const { list, post, update, remove, getById,searchByValue } = useApi('students')

  return {
    list,
    post,
    update,
    remove,
    getById,
    searchByValue
  }
}
