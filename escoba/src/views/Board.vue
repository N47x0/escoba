<template>
  <div class="board">
    <div v-if="getGameDataLoaded">
      <GameControls />
      <hr />
      <b-container>
        <b-row>
          <b-col md="4">
            <PlayerComp player=1 />
          </b-col>
          <b-col md="4">
            <TableCards />
          </b-col>
          <b-col md="4">
            <PlayerComp player=2 />
          </b-col>
        </b-row>
      </b-container>
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
import TableCards from '@/components/TableCards'
import { mapGetters } from 'vuex'

export default {
  name: 'Board',
  components: {
    DeckComp,
    PlayerComp,
    GameControls,
    TableCards
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
    console.log(this)
  }
}
</script>
