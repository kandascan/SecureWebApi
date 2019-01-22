import axios from 'axios';
import { GET_USERS_WITHOUT_ME, GET_TEAM_USERS } from './types';

export const getUsersWithoutMe = () => async dispatch => {
    axios.get("/api/user/getalluserswithoutme")
        .then(res => {
            dispatch({
                type: GET_USERS_WITHOUT_ME,
                payload: res.data
            })
        })
        .catch(err =>{
            dispatch({
                type: GET_USERS_WITHOUT_ME,
                payload: {}
            })
        });
}

export const getTeamUsers = (teamId) => async dispatch => {
    axios.get(`/api/user/getteamusers/${teamId}`)
        .then(res  => {
            dispatch({
                type: GET_TEAM_USERS,
                payload: res.data
            })
        })
        .catch(err =>{
            dispatch({
                type: GET_TEAM_USERS,
                payload: {}
            })
        });
}
