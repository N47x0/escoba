<template>
    <div
      class="crud-table"
      id="crud-table"
    >
      <b-row>
        <b-col>
          <b-table
            class="crud-table" 
            striped 
            :items="items" 
            :fields="fields"
          ></b-table>
      </b-col>
      </b-row>
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
  name: 'CrudTable',
  props: {
    items: {
      type: Array,
      required: false
    }
  },
  components: {
  },
  data () {
    return {
      fields: [
        {
          key: 'game',
          sortable: true
        },
        {
          key: 'ruleTitle',
          sortable: true
        },
        {
          key: 'ruleText',
          sortable: false
        }
      ]
    }
  },
  computed: {
    ...mapGetters([
    ]),
    rows () {
      return Array.from(document.querySelectorAll('tr'))
    },
    cells () {
      return Array.from(document.querySelectorAll('td'))
    }
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
    applyRowIds: function () {
      this.rows.map(x => x.id = "table-row")
    },
    watchForRows: function () {
      var comp = this
      document.addEventListener('mouseover', function(e) {
        if (comp.cells.includes(e.target)) {
          var style = 
            "background-color: #42b983;" +
            "color: #1d2833;" 
          e.target.parentNode.style = style
        }
      })
      document.addEventListener('mouseout', function(e) {
        if (comp.cells.includes(e.target)) {
          var style = 
            "background-color: #1d2833;" +
            "color: #42b983;"
          e.target.parentNode.style = style
        }
      })
    }
  },
  mounted: function () {
    this.applyRowIds()
    this.watchForRows()
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

.crud-table {
  background-color: #1d2833;
  color: #42b983;
}
table tbody tr[role="row"]:hover {
  background-color: #42b983 !important;
  color: #1d2833 !important;
}
tr#table-row:hover {
  background-color: #42b983 !important;
  color: #1d2833 !important;
}
.b-table-sort-icon-left {
  color: #42b983
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
