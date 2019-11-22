<template>
  <div class="board">
    <div v-if="getGameDataLoaded">
      <b-container>
        <GameControls />
        <RuleCarousel />
        <PlayArea />
        <!-- <b-row>
          <b-col md="4">
            <PlayerComp player=1 />
          </b-col>
          <b-col md="4">
          </b-col>
          <b-col md="4">
            <PlayerComp player=2 />
          </b-col>
        </b-row> -->
      </b-container>
      <!-- <DeckComp /> -->
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
// import DeckComp from '@/components/DeckComp.vue'
import PlayerComp from '@/components/PlayerComp.vue'
import GameControls from '@/components/GameControls'
import RuleCarousel from '@/components/RuleCarousel'
import PlayArea from '@/components/PlayArea'
import { mapGetters } from 'vuex'

export default {
  name: 'Board',
  components: {
    //    DeckComp,
    PlayerComp,
    GameControls,
    RuleCarousel,
    PlayArea
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded'
    ])
  },
  methods: {
  },
  mounted: function () {
    console.log(this)
  }
}
</script>

<style scoped>
  .board {
    padding: 1rem 0rem 0rem 0rem;
    background-color: #2c3e50;  
  }
</style>