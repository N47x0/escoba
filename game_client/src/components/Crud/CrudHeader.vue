<template>
    <div
      class="crud-header"
      id="crud-header"
    >
      <div class="crud-icon-group">
        <v-icon
          id="crud-icon-create"
          name="plus-square"
          scale=3.5
        />
        <span class="crud-icon-spacer">
          |
        </span>
        <v-icon
          id="crud-icon-read"
          name="brands/readme"
          scale=3.5
        />
        <span class="crud-icon-spacer">
          |
        </span>
        <v-icon
          id="crud-form-icon"
          name="pen"
          scale=3.5
        />
        <span class="crud-icon-spacer">
          |
        </span>
        <v-icon
          id="crud-form-icon"
          name="trash-alt"
          scale=3.5
        />
      </div>
      <h5
        class="crud-header-text"
      >
        Crud Form
      </h5>
    </div>
</template>

<script>
// import TableCardsSingle from '@/components/Table/TableCardsSingle'
// import HandCompSingle from '@/components/Hand/HandCompSingle'
// import HandComp from '@/components/Hand/HandComp'
import { mapGetters } from 'vuex'

export default {
  name: 'CrudHeader',
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

.crud-icon-group {
  padding: 0rem 0rem 1.5rem 0rem;
}

.crud-icon-spacer {
  padding: 0rem .5rem 0rem .5rem;
}

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
