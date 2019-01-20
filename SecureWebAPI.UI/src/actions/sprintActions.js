import axios from 'axios';
import { GET_CURRENT_SPRINT, GET_ERRORS } from './types';

export const getCurrentSprint = (teamId) => (dispatch) => {
    axios.get(`/api/sprint/getcurrentsprint/${teamId}`)
        .then(res => {
            dispatch({
                type: GET_CURRENT_SPRINT,
                payload: res.data
            });
        })
        .catch(err => {
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            })
        });
}
