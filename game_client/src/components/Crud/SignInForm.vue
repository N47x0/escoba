<template>
    <b-div
      class="sign-in-form"
      id="crud-form"
    >
      <v-icon
        id="crud-icon-create"
        name="sign-in-alt"
        scale=3.5
      />
      <span>
        |
      </span>
      <h5>Sign In Form</h5>
      <hr />
      <b-row>
        <b-col md=4>
        </b-col>
        <b-col md=4>
        </b-col>
        <b-col md=4>
        </b-col>
      </b-row>
    </div>
</template>

<script>
// import TableCardsSingle from '@/components/Table/TableCardsSingle'
// import HandCompSingle from '@/components/Hand/HandCompSingle'
// import HandComp from '@/components/Hand/HandComp'
import { mapGetters } from 'vuex'

export default {
  name: 'CrudForm',
  props: {
  },
  components: {
    // TableCardsSingle,
    // HandComp,
    // HandCompSingle
  },
  data () {
    return {
      tableCardsHighlighted: [],
    }
  },
  computed: {
    ...mapGetters([
    ]),
  },
  methods: {
    log: function (input) {
      var comp = this
      if (input) {
        console.log(input)
      } else {
        console.log(comp)
      }
    },
  },
  mounted: function () {
  },
  watch: {
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

/* h3 {
  margin: 40px 0 0;
} */
ul {
  list-style-type: none;
  padding: 0;
}
/* li {
  display: inline-block;
  margin: 0 10px;
} */
a {
  color: #42b983;
}
</style>
