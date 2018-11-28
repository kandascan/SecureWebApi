import { GET_BACKLOG_ITEMS, CLEAR_BACKLOG, SORTED_BACKLOG_ITEMS, GET_TASK_BY_ID } from '../actions/types';

const initialState = {
    items: {
        tasks: []
    },
    task: {
        task: {
            taskname: '', description: '', effort: -1, priority: -1, username: ''
        }
    }
};

export default function (state = initialState, action) {
    switch (action.type) {
        case GET_TASK_BY_ID:
            return {
                ...state,
                task: action.payload
            }
        case SORTED_BACKLOG_ITEMS:
            return {
                ...state,
                items: {
                    tasks: action.payload
                }
            }
        case GET_BACKLOG_ITEMS:
            return {
                ...state,
                items: action.payload
            };
        case CLEAR_BACKLOG:
            return {
                ...state,
                items: null
            }
        default:
            return state;
    }
}