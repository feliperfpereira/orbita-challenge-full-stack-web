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
        <q-btn color="primary" class="pull-right" style="height:56px ;" label="Cadastrar Aluno" :to="{ name: 'formPost' }" />
      </div>


    </div>
    <q-table :rows="posts" :columns="columns" row-key="name">
      <!-- <template v-slot:top>
        <span class="text-h5">Alunos</span>
        <q-space />
      </template> -->
      <template v-slot:body-cell-actions="props">
        <q-td :props="props" class="q-gutter-sm">
          <q-btn icon="edit" color="info" dense size="sm" @click="handleEditPost(props.row.id)" />
          <q-btn icon="delete" color="negative" dense size="sm" @click="handleDeletePost(props.row.id)" />
        </q-td>
      </template>
    </q-table>
  </q-page>
</template>

<script>
import { defineComponent, ref, onMounted } from 'vue'
import postsService from 'src/services/posts'
import { useQuasar } from 'quasar'
import { useRouter } from 'vue-router'

export default defineComponent({
  name: 'IndexPage',
  setup() {
    const posts = ref([])
    const { list, remove, searchByValue } = postsService()
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
      getPosts()
    })

    const getPosts = async () => {
      try {
        const data = await list()
        posts.value = data
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
          debugger
          data = await list()
        }

        posts.value = data
      } catch (error) {
        console.error(error)
      }
    }


    const handleDeletePost = async (id) => {
      try {
        $q.dialog({
          title: 'Deletar',
          message: 'Deseja deletar este post ?',
          cancel: true,
          persistent: true
        }).onOk(async () => {
          await remove(id)
          $q.notify({ message: 'Apagado com sucesso', icon: 'check', color: 'positive' })
          await getPosts()
        })
      } catch (error) {
        $q.notify({ message: 'Erro ao apagar post', icon: 'times', color: 'negative' })
      }
    }

    const handleEditPost = (id) => {
      router.push({ name: 'formPost', params: { id } })
    }

    return {
      posts,
      columns,
      handleDeletePost,
      handleEditPost,
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