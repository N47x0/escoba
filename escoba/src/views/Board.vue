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
            <TableCards 
              :table-cards="getTableCards"
            />
          </b-col>
          <b-col md="4">
            <PlayerComp player=2 />
          </b-col>
        </b-row>
        <b-row>
          <b-col></b-col>
          <b-col>
            <PlayArea />
          </b-col>
          <b-col></b-col>
        </b-row>
      </b-container>
      <hr />
      <!-- <DeckComp /> -->
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import DeckComp from '@/components/DeckComp.vue'
import PlayerComp from '@/components/PlayerComp.vue'
import GameControls from '@/components/GameControls'
import TableCards from '@/components/TableCards'
import PlayArea from '@/components/PlayArea'
import { mapGetters } from 'vuex'

export default {
  name: 'Board',
  components: {
    DeckComp,
    PlayerComp,
    GameControls,
    TableCards,
    PlayArea
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'getDeck',
      'getTableCards'
    ])
  },
  methods: {
  },
  mounted: function () {
    console.log(this)
  }
}
</script>
