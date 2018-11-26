import axios from 'axios';
import { GET_BACKLOG_ITEMS, BACKLOG_LOADING, CLEAR_BACKLOG, GET_ERRORS, TOGGLE_MODAL, SORTED_BACKLOG_ITEMS, TOGGLE_SPINNER } from './types';

export const getBacklogItems = () => dispatch => {
    dispatch(toggleSpinner());
    axios.get("api/task/getbacklogtasks")
        .then(res => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: res.data
            });
            dispatch(toggleSpinner());
        })
        .catch(err => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: {}
            })
            dispatch(toggleSpinner());
        }  
        );
}

export const createTask = (newTask) => dispatch => {
    dispatch(toggleSpinner());
    axios.post("api/task", newTask)
        .then(res => {
            dispatch(toggleModal());
            dispatch(toggleSpinner());
            dispatch(getBacklogItems());
        })
        .catch(err => {
            dispatch(toggleSpinner());
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            })
        }            
        );
}

export const removeTask = (id) => dispatch => {
    dispatch(toggleSpinner());
    axios.delete("api/task", {
        params: { id: id }
    })
        .then(res => {
            dispatch(toggleSpinner());
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: res.data
            });
        })
        .catch(err => {
            dispatch(toggleSpinner());
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
    dispatch(toggleSpinner());

    axios.post("api/task/sortedbacklog", sortedItems)
        .then(res => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: res.data
            });
            dispatch(toggleSpinner());
        })
        .catch(err => {
            dispatch({
                type: GET_BACKLOG_ITEMS,
                payload: {}
            })
            dispatch(toggleSpinner());
        }
        );
};

export const toggleSpinner = () => {
    return {
        type: TOGGLE_SPINNER
    }
}

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