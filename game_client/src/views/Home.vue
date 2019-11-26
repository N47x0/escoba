<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/Broom_.svg">
    <div v-if="getGameDataLoaded">
      <DeckTable />
    </div>
    <GamesList msg="Welcome to GamesList"/>
  </div>
</template>

<script>
// @ is an alias to /src
import GamesList from '@/components/GamesList.vue'
import DeckTable from '@/components/DeckTable.vue'
import { mapGetters } from 'vuex'

export default {
  name: 'Home',
  components: {
    GamesList,
    DeckTable
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'getDeck'
    ])
  },
  methods: {
  },
  mounted: function () {
  }
}
</script>
