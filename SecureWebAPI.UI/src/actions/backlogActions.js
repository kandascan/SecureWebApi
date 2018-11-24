import axios from 'axios';
import { GET_BACKLOG_ITEMS, BACKLOG_LOADING, CLEAR_BACKLOG, GET_ERRORS, TOGGLE_MODAL } from './types';

export const getBacklogItems = () => dispatch => {
    dispatch(setBacklogLoading());
    axios.get("api/task/getbacklogtasks")
        .then(res => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: res.data
            });
        })
        .catch(err =>
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: {}
            })
        );
}

export const createTask = (newTask) => dispatch => {
    dispatch(setBacklogLoading());
    axios.post("api/task", newTask)
        .then(res => {
            dispatch(toggleModal());
            dispatch(getBacklogItems());
        })
        .catch(err =>
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            })
        );
}

export const removeTask = (id) => dispatch => {
    dispatch(setBacklogLoading());
    axios.delete("api/task", {
        params: { id: id }
    })
        .then(res => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: res.data
            });
        })
        .catch(err => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: {}
            })
        });
};

export const orderBacklogItems = (sortedItems) => dispatch => {
    dispatch(setBacklogLoading());
    axios.post("api/task/sortedbacklog", sortedItems)
        .then(res => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: res.data
            });
        })
        .catch(err =>
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: {}
            })
        );
};

export const toggleModal = () => {
    return {
        type: TOGGLE_MODAL
    }
}

export const setBacklogLoading = () => {
    return {
        type: BACKLOG_LOADING
    }
}

export const clearBacklog = () => {
    return {
        type: CLEAR_BACKLOG
    }
}