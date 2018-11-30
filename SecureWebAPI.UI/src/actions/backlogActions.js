import axios from 'axios';
import { GET_BACKLOG_ITEMS, CLEAR_BACKLOG, GET_ERRORS, SORTED_BACKLOG_ITEMS, TOGGLE_SPINNER, SHOW_CREATE_TASK_MODAL, SHOW_EDIT_TASK_MODAL, GET_TASK_BY_ID } from './types';

export const getTaskById = (id) => dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.get(`api/task/gettaskbyid/${id}`)
        .then(res => {
            dispatch({
                type: GET_TASK_BY_ID,
                payload: res.data
            });
            dispatch({
                type: SHOW_EDIT_TASK_MODAL
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
        })
        .catch(err => {
            dispatch({
                type: GET_TASK_BY_ID,
                payload: {}
            })
            dispatch({
                type: TOGGLE_SPINNER
            });
        }
        );
} 

export const getBacklogItems = () => dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.get("api/task/getbacklogtasks")
        .then(res => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: res.data
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
        })
        .catch(err => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: {}
            })
            dispatch({
                type: TOGGLE_SPINNER
            });
        }
        );
}

export const updateTask = (task) => dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.put("api/task", task)
        .then(res => {
            dispatch({
                type: SHOW_EDIT_TASK_MODAL
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
            dispatch(getBacklogItems());
        })
        .catch(err => {
            dispatch({
                type: TOGGLE_SPINNER
            });
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            })
        }
        );
}

export const createTask = (newTask) => dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.post("api/task", newTask)
        .then(res => {
            dispatch({
                type: SHOW_CREATE_TASK_MODAL
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
            dispatch(getBacklogItems());
            dispatch({
                type: GET_ERRORS,
                payload: {}
            })
        })
        .catch(err => {
            dispatch({
                type: TOGGLE_SPINNER
            });
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            })
        }
        );
}

export const removeTask = (id) => dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.delete("api/task", {
        params: { id: id }
    })
        .then(res => {
            dispatch({
                type: TOGGLE_SPINNER
            });
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: res.data
            });
        })
        .catch(err => {
            dispatch({
                type: TOGGLE_SPINNER
            });
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: {}
            })
        });
};

export const orderBacklogItems = (sortedItems) => dispatch => {
    dispatch({
        type: SORTED_BACKLOG_ITEMS,
        payload: sortedItems
    });
    dispatch({
        type: TOGGLE_SPINNER
    });

    axios.post("api/task/sortedbacklog", sortedItems)
        .then(res => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: res.data
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
        })
        .catch(err => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: {}
            })
            dispatch({
                type: TOGGLE_SPINNER
            });
        }
        );
};

export const clearBacklog = () => {
    return {
        type: CLEAR_BACKLOG
    }
}