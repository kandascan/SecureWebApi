import { SHOW_CREATE_TASK_MODAL, SHOW_EDIT_TASK_MODAL, GET_ERRORS, SHOW_CREATE_TEAM_MODAL} from './types';

export const toggleCreateTeamModal = () => {
    return {
        type: SHOW_CREATE_TEAM_MODAL
    }
}

export const toggleCreateTaskModal = () => {
    return {
        type: SHOW_CREATE_TASK_MODAL
    }
}

export const toggleEditTaskModal = () => {
    return {
        type: SHOW_EDIT_TASK_MODAL
    }
}

export const clearErrorsModal = () => {
    return {
        type: GET_ERRORS,
        payload: {}
    }
}