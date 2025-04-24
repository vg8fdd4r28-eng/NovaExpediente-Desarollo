<template>
  <CSidebar 
    fixed 
    :minimize="minimize"
    :show="show"
    @update:show="(value) => $store.commit('set', ['sidebarShow', value])"
  >
    <CSidebarBrand class="d-md-down-none" to="/">
      <!-- <CIcon 
        class="c-sidebar-brand-full" 
        name="cibCcMastercard" 
        size="custom-size" 
        :height="35" 
        viewBox="0 0 556 134"
      />
      <CIcon 
        class="c-sidebar-brand-minimized" 
        name="cilBasket" 
        size="custom-size" 
        :height="35" 
        viewBox="0 0 110 134"
      /> -->
      <img src="@/assets/Logo-blanco.png" class="c-sidebar-brand-full" style="height:70%"/>
      <img src="@/assets/isotipo-blanco.png" class="c-sidebar-brand-minimized" style="height:70%"/>
    </CSidebarBrand>

    <!-- <CRenderFunction flat :content-to-render="$options.nav"/> -->
    <CRenderFunction flat :content-to-render="nav2"/>
    <CSidebarMinimizer
      class="d-md-down-none"
      @click.native="$store.commit('set', ['sidebarMinimize', !minimize])"
    />
  </CSidebar>
</template>

<script>
import nav from './_nav'

export default {
  name: 'TheSidebar',
  nav,
  methods: {
    getNavChildren(){
      
      return nav[0]._children.filter(menuItem => {
        if(menuItem.permission)
          return this.$root.userPermissions.some(p => p == menuItem.permission);
        else
          return true;
      }).map(menuItem => {        
        if(menuItem._name == "CSidebarNavItem")
        {
          return {
            _name: menuItem._name,
            name: menuItem.name,
            to: menuItem.to,
            icon: menuItem.icon,
            badge: menuItem.badge
          }
        }
        else if(menuItem._name == "CSidebarNavTitle")
        {
          return {
            _name: menuItem._name,
            _children: menuItem._children
          }
        }
        else if(menuItem._name == "CSidebarNavDropdown")        
        {
          return {
            _name: menuItem._name,
            name: menuItem.name,
            route: menuItem.route,
            icon: menuItem.icon,
            items: menuItem.items
          }
        }
      })
    }
  },
  computed: {
    show () {
      return this.$store.state.sidebarShow 
    },
    minimize () {
      return this.$store.state.sidebarMinimize 
    },
    nav2(){
      return [
        {
          _name: 'CSidebarNav',
          _children: this.getNavChildren()
        }
      ];
    },
    nav3(){
      return nav;

      // return [
      //   {
      //     _name: 'CSidebarNav',
      //     _children: []
      //   }
      // ];
    }
  },
  created(){
    //this.nav2 = nav;
    /* [
        {
          _name: 'CSidebarNav',
          _children: []
        }
      ];*/

    // nav = [
    //     {
    //       _name: 'CSidebarNav',
    //       _children: []
    //     }
    //   ];
  }
}
</script>
<style>
  .c-active {
    background-color: #29ABE2 !important;
  }
  .c-sidebar {
    background-color: #15335E !important;
  }
  .c-sidebar-brand {
    height: 134px !important;
    justify-content: start !important;
    border-bottom: white 1px solid;
  }
  .c-sidebar-nav-dropdown.c-show > .c-sidebar-nav-dropdown-items {    
      background-color: #10578E;
  }
  
@media (hover: hover), (-ms-high-contrast: none) {
  /* Estilo del HOVER en el menú lateral */
  .c-sidebar .c-sidebar-nav-link:hover, .c-sidebar .c-sidebar-nav-dropdown-toggle:hover {
          color: #fff;
          font-weight: 500;
          background: #0B233F !important;
  }
}

/* Estilos para el menu dropdown lateral */
  .c-sidebar .c-sidebar-nav-dropdown.c-show {
      background: #10578E;
  }
  .c-sidebar .c-sidebar-nav-dropdown.c-show .c-sidebar-nav-dropdown-toggle{
    border-bottom: white 1px solid;
  }
/* .c-sidebar .c-sidebar-nav-dropdown-toggle .c-sidebar-nav-icon, .c-sidebar .c-sidebar-nav-link .c-sidebar-nav-icon{
  height: 32px;
} */
  
</style>
