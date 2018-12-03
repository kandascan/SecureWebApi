import axios from 'axios';
import { TOGGLE_SPINNER, SHOW_CREATE_TEAM_MODAL, GET_ERRORS } from './types';

export const createTeam = (newTeam) => dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.post("api/team", newTeam)
        .then(res => {
            dispatch({
                type: SHOW_CREATE_TEAM_MODAL
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
            //dispatch(getBacklogItems());
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