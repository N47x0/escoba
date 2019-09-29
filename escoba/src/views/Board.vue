<template>
  <div class="board">
    <div v-if="getGameDataLoaded">
      <GameControls />
      <hr />
      <PlayerComp />
      <hr />
      <DeckComp />
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import DeckComp from '@/components/DeckComp.vue'
import PlayerComp from '@/components/PlayerComp.vue'
import GameControls from '@/components/GameControls'
import { mapGetters } from 'vuex'

export default {
  name: 'Board',
  components: {
    DeckComp,
    PlayerComp,
    GameControls
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
