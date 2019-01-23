import axios from 'axios';
import { GET_USERS_WITHOUT_ME, GET_TEAM_USERS, GET_USER_ROLES, GET_ERRORS } from './types';

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

export const getUserRoles = () => async dispatch => {
    axios.get("/api/user/getuserroles")
        .then(res => {
            dispatch({
                type: GET_USER_ROLES,
                payload: res.data
            })
        })
        .catch(err => {
            dispatch({
                type: GET_USER_ROLES,
                payload: {}
            })
        });
}

export const addUserToTeam = (user) => async dispatch => {
    axios.post("/api/user/addusertoteam", user)
        .then(res => {
            dispatch(getTeamUsers(res.data.teamId));
            dispatch({
                type: GET_ERRORS,
                payload: {}
            });
        })
        .catch(err => {
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            })
        })
}
