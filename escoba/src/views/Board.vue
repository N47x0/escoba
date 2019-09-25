<template>
  <div class="board">
    <div v-if="getGameDataLoaded">
        <DeckComp />
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import DeckComp from '@/components/DeckComp.vue'
import { mapGetters } from 'vuex'

export default {
  name: 'Board',
  components: {
    DeckComp
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
