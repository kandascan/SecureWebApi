import { GET_USERS_WITHOUT_ME, GET_TEAM_USERS, GET_USER_ROLES } from '../actions/types';

const initialState = {
    users: null,
    teamUsers: null,
    roles: null
};

export default function (state = initialState, action) {
    switch (action.type) {   
        case GET_USER_ROLES:
            return {
                ...state,
                roles: action.payload
            }
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