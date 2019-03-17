import axios from 'axios';
import { GET_CURRENT_SPRINT, GET_ERRORS, TOGGLE_SPINNER } from './types';

export const onSortSprintTasks = (sprintTasks) => dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.post("/api/sprint/onsortsprinttasks", sprintTasks)
        .then(res => {
            dispatch(getCurrentSprint(sprintTasks.TeamId))
            dispatch({
                type: TOGGLE_SPINNER
            });
        })
        .catch(err => {
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
        });
}

export const getCurrentSprint = (teamId) => async dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.get(`/api/sprint/getcurrentsprint/${teamId}`)
        .then(res => {
            dispatch({
                type: GET_CURRENT_SPRINT,
                payload: res.data
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
        })
        .catch(err => {
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
        });
}

export const createSprint = (newSprint) => (dispatch) => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.post("/api/sprint/", newSprint)
        .then(res => {
            dispatch({
                type: TOGGLE_SPINNER
            });
            dispatch(getCurrentSprint(res.data.teamId));
        })
        .catch(err => {
            dispatch({ 
                type: GET_ERRORS,
                payload: err.response.data
            })
            dispatch({
                type: TOGGLE_SPINNER
            });
        });            
}
