<template>
  <div class="player-comp">
    <div v-if="getGameDataLoaded">
      <b-card
        class="player"
        :id="'player-' + getPlayer.name"
        style="width: 20rem; height: 25rem"
      >
        <b-row class="player-info">
          <b-col>{{ getPlayer.name }}</b-col>
          <b-col>Score: {{ getPlayer.score }}</b-col> 
          <b-col></b-col>
        </b-row>
        <hr />
        <b-row>
          <b-col>              
            <v-icon
              :id="'-player-icon' + getPlayer.name"
              :class="'-player-icon' + getPlayer.name"
              name="poo"
              scale=3.5
            ></v-icon>
          </b-col>
          <b-col>
            <span
              v-for="(c, i) in cards" 
              :key="i"
            >{{ c.card }} | </span>
          </b-col>
          <b-col></b-col>
        </b-row>
      </b-card>
      <!-- TODO add dynamic icon according to player rank -->
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import HandComp from '@/components/HandComp'

export default {
  name: 'PlayerComp',
  props: {
    player: String
  },
  components: {
    HandComp
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
      return this.getDeck.filter(x => this.getHand.includes(x.card))
    }
  },
  methods: {
    log: function(input) {
      var comp = this
      if(input) {
        console.log(input)
      }
      else {
        console.log(comp)
      }
    }
  },
  mounted: function() {
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

/* make player cards relative to parent */

.player-card {
  position: relative;
  /* height: .5em;
  width: .5em; */
}

/* set all cards to center of div and position: relative for absolute positioning of child icons */

[class*=play-card-] {
  transform: translate(500,0);
  display: inline-block;
  position: relative;
}

/* card value 1 */

.play-card-1 #icon-1 {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%)
}

/* card value 2 */

.play-card-2 #icon-1 {
  position: absolute;
  top: 25%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-2 #icon-2 {
  position: absolute;
  top: 75%;
  left: 50%;
  transform: translate(-50%, -50%)
}

/* card value 3 */

.play-card-3 #icon-1 {
  position: absolute;
  top: 25%;
  left: 75%;
  transform: translate(-50%, -50%)
}
.play-card-3 #icon-2 {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-3 #icon-3 {
  position: absolute;
  top: 75%;
  left: 25%;
  transform: translate(-50%, -50%)
}

/* card value 4 */

.play-card-4 #icon-1 {
  position: absolute;
  top: 28%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-4 #icon-2 {
  position: absolute;
  top: 28%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-4 #icon-3 {
  position: absolute;
  top: 72%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-4 #icon-4 {
  position: absolute;
  top: 72%;
  left: 72%;
  transform: translate(-50%, -50%)
}

/* card value 5 */

.play-card-5 #icon-1 {
  position: absolute;
  top: 20%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-5 #icon-2 {
  position: absolute;
  top: 20%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-5 #icon-3 {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-5 #icon-4 {
  position: absolute;
  top: 80%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-5 #icon-5 {
  position: absolute;
  top: 80%;
  left: 72%;
  transform: translate(-50%, -50%)
}

/* card value 6 */

.play-card-6 #icon-1 {
  position: absolute;
  top: 20%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-6 #icon-2 {
  position: absolute;
  top: 20%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-6 #icon-3 {
  position: absolute;
  top: 50%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-6 #icon-4 {
  position: absolute;
  top: 50%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-6 #icon-5 {
  position: absolute;
  top: 80%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-6 #icon-6 {
  position: absolute;
  top: 80%;
  left: 72%;
  transform: translate(-50%, -50%)
}

/* card value 7 */

.play-card-7 #icon-1 {
  position: absolute;
  top: 15%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-7 #icon-2 {
  position: absolute;
  top: 15%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-7 #icon-3 {
  position: absolute;
  top: 35%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-7 #icon-4 {
  position: absolute;
  top: 55%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-7 #icon-5 {
  position: absolute;
  top: 55%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-7 #icon-6 {
  position: absolute;
  top: 80%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-7 #icon-7 {
  position: absolute;
  top: 80%;
  left: 72%;
  transform: translate(-50%, -50%)
}

/* card value 8 */

.play-card-8 #icon-1 {
  position: absolute;
  top: 12%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-8 #icon-2 {
  position: absolute;
  top: 12%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-8 #icon-3 {
  position: absolute;
  top: 31%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-8 #icon-4 {
  position: absolute;
  top: 50%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-8 #icon-5 {
  position: absolute;
  top: 50%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-8 #icon-6 {
  position: absolute;
  top: 69%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-8 #icon-7 {
  position: absolute;
  top: 88%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-8 #icon-8 {
  position: absolute;
  top: 88%;
  left: 72%;
  transform: translate(-50%, -50%)
}

/* card value 9 */

.play-card-9 #icon-1 {
  position: absolute;
  top: 12%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-9 #icon-2 {
  position: absolute;
  top: 12%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-9 #icon-3 {
  position: absolute;
  top: 36%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-9 #icon-4 {
  position: absolute;
  top: 36%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-9 #icon-5 {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-9 #icon-6 {
  position: absolute;
  top: 64%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-9 #icon-7 {
  position: absolute;
  top: 64%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-9 #icon-8 {
  position: absolute;
  top: 88%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-9 #icon-9 {
  position: absolute;
  top: 88%;
  left: 72%;
  transform: translate(-50%, -50%)
}

/* card value 10  14% */

.play-card-10 #icon-1 {
  position: absolute;
  top: 12%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-2 {
  position: absolute;
  top: 12%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-3 {
  position: absolute;
  top: 26%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-4 {
  position: absolute;
  top: 40%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-5 {
  position: absolute;
  top: 40%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-6 {
  position: absolute;
  top: 60%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-7 {
  position: absolute;
  top: 60%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-8 {
  position: absolute;
  top: 74%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-9 {
  position: absolute;
  top: 88%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-10 {
  position: absolute;
  top: 88%;
  left: 72%;
  transform: translate(-50%, -50%)
}

/* 15%

.play-card-10 #icon-1 {
  position: absolute;
  top: 12%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-2 {
  position: absolute;
  top: 12%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-3 {
  position: absolute;
  top: 27%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-4 {
  position: absolute;
  top: 42%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-5 {
  position: absolute;
  top: 42%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-6 {
  position: absolute;
  top: 58%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-7 {
  position: absolute;
  top: 58%;
  left: 72%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-8 {
  position: absolute;
  top: 73%;
  left: 50%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-9 {
  position: absolute;
  top: 88%;
  left: 28%;
  transform: translate(-50%, -50%)
}
.play-card-10 #icon-10 {
  position: absolute;
  top: 88%;
  left: 72%;
  transform: translate(-50%, -50%)
} */



h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
