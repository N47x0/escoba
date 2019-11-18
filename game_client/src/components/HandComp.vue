<template>
  <div class="hand-comp">
    <div v-if="getGameDataLoaded">
      <!-- <b-alert
        :show="tooManySelections"
        dismissible
        variant="warning"
        @dismissed="clearSelections"
      ></b-alert> -->
      <b-card
        :class="'hand-' +getPlayer.name"
        :id="'hand-' +getPlayer.name"
      >
        <b-row>
          <b-col md=4></b-col>
          <b-col md=4>{{getPlayer.name}}'s Hand</b-col>
          <b-col md=4></b-col>
        </b-row>
        <b-row>
          <b-col md=4>
            <!-- <b-button
              @click="loadValidPlays(validPayload)"
            > -->
            <b-button
              @click="onGetValidPlays"
            >
              Get Valid Plays
            </b-button>
          </b-col>
          <b-col md=4>
            <v-icon
              id="hand-icon"
              name="hand-spock"
              scale=3.5
            ></v-icon>
          </b-col>
          <b-col md=4>
            <b-button
              @click="getBestPlay()"
            >
              Get Best Play
            </b-button>
          </b-col>
        </b-row>
        <hr />
        <b-row 
          v-if="showValidPlays"
          align-h="center"
        >
          <b-col cols=6>
            <b-button-toolbar 
              aria-label="Toolbar with buttons to control which valid play is displayed and selected"
              justify
              class="valid-plays-toolbar"
            >
              <b-button-group 
                class="mx-1"
                
              >
                <b-button
                  @click="previousValidPlay"
                >            
                  <v-icon
                    id="backward-icon"
                    name="backward"
                  ></v-icon>
                </b-button>
                <b-button>{{ playerValidPlaysIds }} | {{ tableValidPlaysIds }}</b-button>
                <b-button
                  @click="nextValidPlay"
                >            
                  <v-icon
                    id="forward-icon"
                    name="forward"
                  ></v-icon>
                </b-button>
              </b-button-group>
            </b-button-toolbar>
          </b-col>
        </b-row>
        <hr />
        <b-row>
          <CardCollection 
            :collection="getHand"
            owner="player"
            :highlighted="playerValidPlays"
          />
        </b-row>
      </b-card>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import CardCollection from '@/components/CardCollection'

export default {
  name: 'HandComp',
  data: function () {
    return {
      cardsSelected: Object,
      showValidPlays: false,
      currentValidPlayIndex: 0
    }
  },
  props: {
    // cards: Object,
    player: String
  },
  components: {
    CardCollection
  },
  computed: {
    ...mapGetters([
      'getGameDataLoaded',
      'getPlayer1',
      'getPlayer2',
      'getDeck',
      'getTableCards',
      'getClientSessionId',
      'getValidPlays'
    ]),
    getPlayer: function () {
      return this[`getPlayer${this.player}`]
    },
    getHand: function () {
      return this.getPlayer.hand
    },
    cards: function () {
      console.log(this.getHand)
      return this.getDeck.filter(x => this.getHand.includes(x.card))
    },
    validPlays() {
      return this.getValidPlays[`Player ${this.player}`]     
    },
    playerValidPlays() {
      var pvp = []
      if (this.showValidPlays) {
        pvp = this.validPlays[this.currentValidPlayIndex].filter(x => x.owner === this.getPlayer.name)
      }
      return pvp
    },
    tableValidPlays() {
      var tvp = []
      if (this.showValidPlays) {
        tvp = this.validPlays[this.currentValidPlayIndex].filter(x => x.owner === 'table')
      }
      return tvp
    },
    playerValidPlaysIds() {
      var text = ''
      var ids = this.playerValidPlays.map(x => x.id)
      if (ids.length > 1) {
        text = ids.join(', ')
      } else {
        text = ids[0]
      }
      return text
    },
    tableValidPlaysIds() {
      var text = ''
      var ids = this.tableValidPlays.map(x => x.id)
      if (ids.length > 1) {
        text = ids.join(', ')
      } else {
        text = ids[0]
      }
      return text
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
      'getBestPlay',
      'loadValidPlays'
    ]),
    log: function (input) {
      var comp = this
      if (input) {
        console.log(input)
      } else {
        console.log(comp)
      }
    },
    onGetValidPlays() {
      this.showValidPlays = !this.showValidPlays
      this.currentValidPlayIndex = 0
    },
    previousValidPlay() {
      console.log(this.currentValidPlayIndex)
      this.currentValidPlayIndex -= 1
    },
    nextValidPlay() {
      console.log(this.currentValidPlayIndex)
      this.currentValidPlayIndex += 1
    },
  },
  watch: {
    tableValidPlays: function(val, oldVal) {
      if(val !== oldVal) {
        this.$emit('new-highlighted', this.tableValidPlays)
      }
    }
  },
  mounted: function () {
    console.log('#### hand comp ####')
    console.log(this.tableValidPlays)
    this.$emit('new-highlighted', this.tableValidPlays)
    // console.log(this)
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
