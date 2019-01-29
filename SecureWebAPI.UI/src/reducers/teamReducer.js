import { GET_USER_TEAMS, SET_CURRENT_TEAM, GET_TEAM_BY_ID } from '../actions/types';

const initialState = {
    userteams: [],
    areteamsloaded: false,
    isSaveCurrentTeamToLocalStorage: false,
    team: {}
};

export default function (state = initialState, action) {
    switch (action.type) {
        case GET_TEAM_BY_ID: 
            return {
                ...state,
                team: action.payload
            }
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