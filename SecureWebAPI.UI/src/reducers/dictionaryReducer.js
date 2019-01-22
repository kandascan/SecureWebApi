import { GET_PRIORITIES, GET_EFFORTS, GET_SPRINTS } from '../actions/types';

const initialState = {
    priorities: null,
    efforts: null,
    sprints: null
};

export default function (state = initialState, action) {
    switch (action.type) {    
        case  GET_PRIORITIES:
            return {
                ...state,
                priorities: action.payload
            }
        case  GET_EFFORTS:
            return {
                ...state,
                efforts: action.payload
            }
        case  GET_SPRINTS:
            return {
                ...state,
                sprints: action.payload
            }
        default:
            return state;
    }
}