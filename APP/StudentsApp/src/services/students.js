import useApi from 'src/composables/UseApi'

export default function studentsService () {
  const { list, student, update, remove, getById,searchByValue } = useApi('students')

  return {
    list,
    student,
    update,
    remove,
    getById,
    searchByValue
  }
}
