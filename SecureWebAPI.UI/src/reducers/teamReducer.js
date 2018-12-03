import { GET_USER_TEAMS } from '../actions/types';

const initialState = {
    userteams: [],
    areteamsloaded: false
};

export default function (state = initialState, action) {
    switch (action.type) {
        case GET_USER_TEAMS:
            return {
                ...state,
                userteams: action.payload,
                areteamsloaded: action.teamsLoaded
            }
        default:
            return state;
    }
}