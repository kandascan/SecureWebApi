import { GET_USER_TEAMS, CURRENT_TEAM } from '../actions/types';

const initialState = {
    userteams: [],
    areteamsloaded: false,
    teamid: null
};

export default function (state = initialState, action) {
    switch (action.type) {
        case CURRENT_TEAM:
        return {
            ...state,
            teamid: action.payload
        }
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