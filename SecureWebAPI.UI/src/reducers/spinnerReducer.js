import { TOGGLE_SPINNER } from '../actions/types';

const initialState = {
    showSpinner: false
};

export default function (state = initialState, action) {
    switch (action.type) {
        case TOGGLE_SPINNER: 
        return {
            ...state,
            showSpinner: !state.showSpinner
        }
        default:
            return state;
    }
}