import { GET_BACKLOG_ITEMS, BACKLOG_LOADING, CLEAR_BACKLOG } from '../actions/types';

const initialState = {
    items: null,
    loading: false
};

export default function(state = initialState, action) {
    switch(action.type){   
        case BACKLOG_LOADING:
            return {
                ...state,
                items: [],
                loading: true
            }    
        case GET_BACKLOG_ITEMS: 
            return {
                ...state,
                items: action.payload,
                loading: false
            };
        case CLEAR_BACKLOG:
            return {
                ...state,
                items: null
            }
        default:
            return state;
    }
}