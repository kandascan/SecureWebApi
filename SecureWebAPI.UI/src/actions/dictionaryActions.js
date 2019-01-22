import axios from 'axios';
import { GET_PRIORITIES, GET_EFFORTS, GET_SPRINTS } from './types';

export const getSprintsList = (teamId) => async dispatch => {
    axios.get(`/api/list/getsprintslist/${teamId}`)
        .then(res => {
            dispatch({
                type: GET_SPRINTS,
                payload: res.data
            });
        })
        .catch(err => {
            dispatch({
                type: GET_SPRINTS,
                payload: {}
            });
        });
}

export const getEfforts = () => async dispatch => {
    axios.get("/api/list/getefforts")
        .then(res => {
            dispatch({
                type: GET_EFFORTS,
                payload: res.data
            });
        })
        .catch(err =>
            dispatch({
                type: GET_EFFORTS,
                payload: {}
            })
        );
}
export const getPriorities = () => async dispatch => {
    axios.get("/api/list/getpriorities")
        .then(res => {
            dispatch({
                type: GET_PRIORITIES,
                payload: res.data
            })
        })
        .catch(err => {
            dispatch({
                type: GET_PRIORITIES,
                payload: {}
            })
        }
        );
};

