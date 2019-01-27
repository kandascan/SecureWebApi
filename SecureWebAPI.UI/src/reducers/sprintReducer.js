import { GET_CURRENT_SPRINT } from '../actions/types';

const initialState = {
    tasks: null,
    sprintId: 0,
    teamId: 0
};

export default function (state = initialState, action) {
    switch (action.type) {
        case GET_CURRENT_SPRINT:
        return {
            ...state,
            tasks: action.payload.sprintBoardTasks,
            sprintId: action.payload.sprintId,
            teamId: action.payload.teamId,
        }
        default:
            return state;
    }
}