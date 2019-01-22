import { GET_USERS_WITHOUT_ME, GET_TEAM_USERS } from '../actions/types';

const initialState = {
    users: null,
    teamUsers: null
};

export default function (state = initialState, action) {
    switch (action.type) {   
        case GET_TEAM_USERS:
            return {
                ...state,
                teamUsers: action.payload
            }
        case  GET_USERS_WITHOUT_ME:
            return {
                ...state,
                users: action.payload
            }       
        default:
            return state;
    }
}