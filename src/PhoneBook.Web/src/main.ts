import { createApp } from 'vue'
import App from './App.vue'

import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faContactBook } from '@fortawesome/free-regular-svg-icons'
import { faPlus, faPen, faXmark } from '@fortawesome/free-solid-svg-icons'
library.add(faContactBook, faPlus, faPen, faXmark)

import './styles.scss'

createApp(App)
.component('font-awesome-icon', FontAwesomeIcon)
.mount('#app')
