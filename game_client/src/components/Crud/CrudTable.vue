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
  },
  components: {
  },
  data () {
    return {
      // Note 'isActive' is left out and will not appear in the rendered table
      fields: [
        {
          key: 'last_name',
          sortable: true
        },
        {
          key: 'first_name',
          sortable: false
        },
        {
          key: 'age',
          label: 'Person age',
          sortable: true,
          // Variant applies to the whole column, including the header and footer
          // variant: 'danger'
        }
      ],
      items: [
        { isActive: true, age: 40, first_name: 'Dickerson', last_name: 'Macdonald' },
        { isActive: false, age: 21, first_name: 'Larsen', last_name: 'Shaw' },
        { isActive: false, age: 89, first_name: 'Geneva', last_name: 'Wilson' },
        { isActive: true, age: 38, first_name: 'Jami', last_name: 'Carney' }
      ]
    }
  },
  computed: {
    ...mapGetters([
    ]),
    loaded () {
      return this.rows.length > 0
    },
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
      console.log('applyrow')
      console.log(this.rows)
      this.rows.map(x => x.id = "table-row")
      console.log(this.rows)
    },
    watchForRows: function () {
      var comp = this
      if (comp.loaded === true) {
        console.log(comp.rows)
        document.addEventListener('mouseover', function(e) {
          if (comp.cells.includes(e.target)) {
            console.log(e)
            var style = 
              "background-color: #42b983;" +
              "color: #1d2833;" 
            e.target.parentNode.style = style
          }
        })
        document.addEventListener('mouseout', function(e) {
          if (comp.cells.includes(e.target)) {
            console.log(e)
            var style = 
              "background-color: #1d2833;" +
              "color: #42b983;"
            e.target.parentNode.style = style
          }
        })
      }
    }
  },
  mounted: function () {
    this.applyRowIds()
    if (this.loaded === true) {
      this.watchForRows()
    }
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
