<template>
  <div class="rule-carousel">
    <div v-if="getRulesLoaded">
      <h5>Rules</h5>
      <b-carousel
        id="carousel-1"
        v-model="slide"
        :interval="0"
        controls
        indicators
        background="#6d31a8"
        img-width="256"
        img-height="30"
        style="text-shadow: 1px 1px 2px #333;"
        @sliding-start="onSlideStart"
        @sliding-end="onSlideEnd"
      >

        <!-- Slide with blank fluid image to maintain slide aspect ratio -->
        <b-carousel-slide
          v-for="(rule, i) in getRules"
          :key="i" 
          :caption="rule.title" 
          img-blank img-alt="Blank image"
        >
          <p>
            {{rule.rule_Text}}
          </p>
        </b-carousel-slide>
      </b-carousel>

      <!-- <p class="mt-4">
        Slide #: {{ slide }}<br>
        Sliding: {{ sliding }}
      </p> -->
    </div>    
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'CardComp',
  props: {
    card: Object,
    isDeck: Boolean,
    isHand: Boolean
  },
  data() {
    return {
      slide: 0,
      sliding: null
    }
  },
  computed: {
    ...mapGetters([
      'getRulesLoaded',
      'getRules'
    ]),
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
    },
    onSlideStart(slide) {
      this.sliding = true
    },
    onSlideEnd(slide) {
      this.sliding = false
    }
  },
  mounted: function() {
    console.log('#### card comp ####')
    //console.log(this)  
    // this.log(this.getDeck)
    // if(this.getGameDataLoaded) {
    //   console.log(this.getDeck)
    // }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.rule-carousel {
  /* height: 1rem;
  width: 10rem; */
}
/* set card-container to absolute for relative relationship to parent #### UNWANTED SIDE EFFECT #### */

/* .card-container {
  position: absolute;
} */

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
