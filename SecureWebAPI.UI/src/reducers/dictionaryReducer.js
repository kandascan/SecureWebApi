import { GET_PRIORITIES, GET_EFFORTS } from '../actions/types';

const initialState = {
    priorities: null,
    efforts: null
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
        default:
            return state;
    }
}