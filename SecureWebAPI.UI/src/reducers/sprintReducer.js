import { GET_CURRENT_SPRINT } from '../actions/types';

const initialState = {
    tasks: null,
    sprintId: 0
};

export default function (state = initialState, action) {
    switch (action.type) {
        case GET_CURRENT_SPRINT:
        return {
            ...state,
            tasks: action.payload.tasks,
            sprintId: action.payload.sprintId
        }
        default:
            return state;
    }
}