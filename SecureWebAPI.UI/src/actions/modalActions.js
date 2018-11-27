import { SHOW_CREATE_TASK_MODAL, SHOW_EDIT_TASK_MODAL} from './types';

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