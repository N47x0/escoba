<template>
  <div class="hand-comp-cards">
    <b-row>
      <CardCollection 
        :collection="getHand"
        owner="player"
        :highlighted="highlighted"
        @new-selected="onNewSelected"
      />
    </b-row>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import CardCollection from '@/components/CardCollection'

export default {
  name: 'HandCompCards',
  data: function () {
    return {
      selected: []
    }
  },
  props: {
    player: String,
    highlighted: {
      type: Array
    }
  },
  components: {
    CardCollection
  },
  computed: {
    ...mapGetters([
      'getPlayer1',
      'getPlayer2'
    ]),
    getPlayer: function () {
      return this[`getPlayer${this.player}`]
    },
    getHand: function () {
      return this.getPlayer.hand
    },
    validPayload () {
      // return "test-payload"
      return JSON.stringify({
        sessionId: this.getClientSessionId
      })
    }
  },
  methods: {
    ...mapActions([
    ]),
    log: function (input) {
      var comp = this
      if (input) {
        console.log(input)
      } else {
        console.log(comp)
      }
    },
    onNewSelected (payload) {
      console.log(payload)
      console.log('on new selected from hand comp cards')
      this.$emit('new-selected', payload)
    }
  },
  watch: {
    highlighted: function(val, oldVal) {
      if(val !== oldVal) {
        console.log('change in highlighted watch')
      }
    }
  },
  mounted: function () {
    console.log('#### hand comp cards ####')
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.valid-plays-toolbar {
  /* width: 100%; */
}

/* set hand comp top padding smaller to separate from icon */

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
