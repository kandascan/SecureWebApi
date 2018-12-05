import { GET_USER_TEAMS, SET_CURRENT_TEAM } from '../actions/types';

const initialState = {
    userteams: [],
    areteamsloaded: false,
    isSaveCurrentTeamToLocalStorage: false
};

export default function (state = initialState, action) {
    switch (action.type) {
        case SET_CURRENT_TEAM:
        return {
            ...state,
            isSaveCurrentTeamToLocalStorage: action.payload
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