import axios from 'axios';
import { GET_BACKLOG_ITEMS, BACKLOG_LOADING, CLEAR_BACKLOG } from './types';

export const getBacklogItems = () => dispatch => {
    dispatch(setBacklogLoading());
    axios.get("api/task/backlog")
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