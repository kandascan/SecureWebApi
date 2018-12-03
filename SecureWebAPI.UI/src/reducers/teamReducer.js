import { GET_USER_TEAMS } from '../actions/types';

const initialState = {
    teams: [
        {
            id: 1,
            teamname: "Team 1",
            scrumMasterUser: true
        },
        {
            id: 2,
            teamname: "Team 2",
            scrumMasterUser: false
        },
        {
            id: 3,
            teamname: "Team 3",
            scrumMasterUser: true
        },
        {
            id: 4,
            teamname: "Team 4",
            scrumMasterUser: false
        },
        {
            id: 5,
            teamname: "Team 5",
            scrumMasterUser: false
        },
        {
            id: 6,
            teamname: "Team 6",
            scrumMasterUser: false
        }
    ]
};

export default function (state = initialState, action) {
    switch (action.type) {
        case GET_USER_TEAMS:
            return {
                ...state,
                teams: action.payload
            }
        default:
            return state;
    }
}