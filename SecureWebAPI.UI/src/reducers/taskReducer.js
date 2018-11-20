import { GET_BACKLOG_ITEMS, BACKLOG_LOADING, CLEAR_BACKLOG } from '../actions/types';

const initialState = {
    tasks: null,
    loading: false
};

export default function(state = initialState, action) {
    switch(action.type){   
        case BACKLOG_LOADING:
            return {
                ...state,
                loading: true
            }    
        case GET_BACKLOG_ITEMS: 
            return {
                ...state,
                tasks: action.payload,
                loading: false
            };
        case CLEAR_BACKLOG:
            return {
                ...state,
                tasks: null
            }
        default:
            return state;
    };
};
