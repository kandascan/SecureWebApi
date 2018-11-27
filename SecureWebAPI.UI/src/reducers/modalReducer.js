import { SHOW_CREATE_TASK_MODAL, SHOW_EDIT_TASK_MODAL } from '../actions/types';

const initialState = {
    showCreateTaskModal: false,
    showEditTaskModal: false
};

export default function (state = initialState, action) {
    switch (action.type) {
        case SHOW_CREATE_TASK_MODAL:
            return {
                ...state,
                showCreateTaskModal: !state.showCreateTaskModal
            }
        case SHOW_EDIT_TASK_MODAL:
            return {
                ...state,
                showEditTaskModal: !state.showEditTaskModal
            }
        default:
            return state;
    }
}