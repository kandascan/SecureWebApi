import { GET_BACKLOG_ITEMS, BACKLOG_LOADING, CLEAR_BACKLOG, TOGGLE_MODAL, SORTED_BACKLOG_ITEMS } from '../actions/types';

const initialState = {
    items: {
        tasks: []
    },
    loading: false,
    modal: false
};

export default function (state = initialState, action) {
    switch (action.type) {
        case SORTED_BACKLOG_ITEMS: 
            return {
                ...state,
                items: {
                    tasks: action.payload
                } 
            }
        case BACKLOG_LOADING:
            return {
                ...state,
               
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
        case TOGGLE_MODAL:
            return {
                ...state,
                modal: !state.modal
            }
        default:
            return state;
    }
}