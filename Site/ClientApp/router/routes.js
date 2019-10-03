import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import BaseTesetPage from 'components/base-test-page'
import UserTestPage from 'components/user-test-page'

export const routes = [
    { name: 'Base', path: '/', component: BaseTesetPage, display: 'Base Test  ', icon: 'info' },
    { name: 'User', path: '/user', component: UserTestPage, display: 'User Test', icon: 'info' },
    //{ name: 'counter', path: '/counter', component: CounterExample, display: 'Counter', icon: 'graduation-cap' },
    //{ name: 'fetch-data', path: '/fetch-data', component: FetchData, display: 'Data', icon: 'list' }
]
