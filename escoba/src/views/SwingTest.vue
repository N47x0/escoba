<template>
  <div id="app">
    <button @click="add">Add card</button>
    <button @click="remove">Remove card</button>
    <button @click="swing">Swing card</button>
    <vue-swing
      @throwout="onThrowout"
      :config="config"
      ref="vueswing"
    >
      <div
        v-for="(card, i) in cards"
        :key="i"
        class="card"
      >
        <!-- <span>{{ card }}</span> -->
      </div>
    </vue-swing>
  </div>
</template>

<script>
import VueSwing from 'vue-swing'
import { mapGetters, mapActions } from 'vuex'
import axios from 'axios'
import CardComp from '@/components/CardComp'


export default {
  name: 'app',
  components: { VueSwing },
  data () {
    return {
      config: {
        allowedDirections: [
          VueSwing.Direction.UP,
          VueSwing.Direction.DOWN,
          VueSwing.Direction.LEFT,
          VueSwing.Direction.RIGHT
        ],
        minThrowOutDistance: 250,
        maxThrowOutDistance: 300
      },
      //cards: ['A', '2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K']
    }
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'getPlayer1',
      'getPlayer2',
      'getDeck'
    ]),
    getPlayer: function () {
      return this[`getPlayer${this.player}`]
    },
    getHand: function () {
      // console.log(this.getPlayer)
      return this.getPlayer.hand
    },
    cards: function () {
      return this.getDeck
      //return this.getDeck.filter(x => this.getHand.includes(x.card))
    }
  },
  methods: {
    add () {
      this.cards.push(`${this.cards.length + 1}`)
    },
    remove () {
      this.swing()
      setTimeout(() => {
        this.cards.pop()
      }, 100)
    },
    swing () {
      const cards = this.$refs.vueswing.cards
      cards[cards.length - 1].throwOut(
        Math.random() * 100 - 50,
        Math.random() * 100 - 50
      )
    },
    onThrowout ({ target, throwDirection }) {
      console.log(`Threw out ${target.textContent}!`)
      console.log(target)
      console.log(throwDirection)
    }
  }
}
</script>

<style scoped>
body {
  background-color: #444;
  margin: 0;
}
.card {
  align-items: center;
  background-color: #fff;
  border-radius: 20px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  display: flex;
  font-size: 72px;
  height: 200px;
  justify-content: center;
  left: calc(50% - 100px);
  position: absolute;
  top: calc(50% - 100px);
  width: 200px;
}
</style>