import axios from 'axios';
import { GET_BACKLOG_ITEMS, BACKLOG_LOADING, CLEAR_BACKLOG } from './types';

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

export const orderBacklogItems = (sortedItems) => dispatch => {
    console.log(sortedItems);
    axios.post("api/task/sortedbacklog", sortedItems)
    .then(res => {
        console.log(res)
        // dispatch({
        //     type: ORDER_BACKLOG,
        //     payload: err.response.data
        // })
    })
    .catch(err =>{
    }
        
    );
};

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