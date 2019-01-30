import { SET_CURRENT_USER, SET_TEAM_MEMBER } from '../actions/types';
import isEmpty from '../validation/is-Empty';

const initialState = {
    isAuthenticated: false,
    user: {},
    isTeamMember: false
};

export default function(state = initialState, action) {
    switch(action.type){       
        case SET_TEAM_MEMBER: 
            return {
                ...state,
                isTeamMember: !isEmpty(action.payload)
            }
        case SET_CURRENT_USER:
            return {
                ...state,
                isAuthenticated: !isEmpty(action.payload),
                user: action.payload
            } 
        default:
            return state;
    }
}