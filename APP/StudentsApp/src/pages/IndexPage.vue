<template>
  <q-page padding>
    <div style="margin-bottom: 15px;">
      <div class="pull-left">
        <div style="display:flex ;">
          <q-input outlined v-model="textSearch" label="Pesquisa" style="width:400px ;" />
        <q-btn square color="primary" icon="search" style="margin-left: 5px;" @click="handleSearch(textSearch)" />
        <q-btn square color="red" icon="search_off" style="margin-left: 5px;" @click="handleSearch()" />

        </div>
      </div>

      <div class="pull-right">
        <q-btn color="primary" class="pull-right" style="height:56px ;" label="Cadastrar Aluno" :to="{ name: 'formStudent' }" />
      </div>


    </div>
    <q-table :rows="students" :columns="columns" row-key="name">
      <!-- <template v-slot:top>
        <span class="text-h5">Alunos</span>
        <q-space />
      </template> -->
      <template v-slot:body-cell-actions="props">
        <q-td :props="props" class="q-gutter-sm">
          <q-btn icon="edit" color="info" dense size="sm" @click="handleEditStudent(props.row.id)" />
          <q-btn icon="delete" color="negative" dense size="sm" @click="handleDeleteStudent(props.row.id)" />
        </q-td>
      </template>
    </q-table>
  </q-page>
</template>

<script>
import { defineComponent, ref, onMounted } from 'vue'
import studentsService from 'src/services/students'
import { useQuasar } from 'quasar'
import { useRouter } from 'vue-router'

export default defineComponent({
  name: 'IndexPage',
  setup() {
    const students = ref([])
    const { list, remove, searchByValue } = studentsService()
    const columns = [
      { name: 'ra', field: 'ra', label: 'RA', sortable: true, align: 'left' },
      { name: 'nome', field: 'nome', label: 'Nome', sortable: true, align: 'left' },
      { name: 'cpf', field: 'cpf', label: 'CPF', sortable: true, align: 'left' },
      { name: 'actions', field: 'actions', label: 'Ações', align: 'right' }
    ]
    const $q = useQuasar()
    const router = useRouter()
    let textSearch;

    onMounted(() => {
      getStudents()
    })

    const getStudents = async () => {
      try {
        const data = await list()
        students.value = data
      } catch (error) {
        console.error(error)
      }
    }

    const handleSearch = async (value) => {
      try {
        let data;
        if (value)
          data = await searchByValue(value)
        else {
          
          data = await list()
        }

        students.value = data
      } catch (error) {
        console.error(error)
      }
    }


    const handleDeleteStudent = async (id) => {
      try {
        $q.dialog({
          title: 'Deletar',
          message: 'Deseja deletar este Aluno ?',
          cancel: true,
          persistent: true
        }).onOk(async () => {
          await remove(id)
          await getStudents()
        })
      } catch (error) {
      }
    }

    const handleEditStudent = (id) => {
      router.push({ name: 'formStudent', params: { id } })
    }

    return {
      students,
      columns,
      handleDeleteStudent,
      handleEditStudent,
      handleSearch,
      textSearch
    }
  }
})
</script>

<style>
.pull-left {
  display: inline-block;
}

.pull-right {
  float: right;
}
</style>