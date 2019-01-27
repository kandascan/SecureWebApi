import axios from 'axios';
import { GET_USERS_WITHOUT_USERS_IN_TEAM, GET_TEAM_USERS, GET_USER_ROLES, GET_ERRORS } from './types';

export const getUsersWithoutUsersInTeam = (teamId) => async dispatch => {
    axios.get(`/api/user/getalluserswitusersinteam/${teamId}`)
        .then(res => {
            dispatch({
                type: GET_USERS_WITHOUT_USERS_IN_TEAM,
                payload: res.data
            })
        })
        .catch(err =>{
            dispatch({
                type: GET_USERS_WITHOUT_USERS_IN_TEAM,
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
            dispatch(getUsersWithoutUsersInTeam(res.data.teamId));
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

export const deleteUserFromTeam = (xrefUserTeam) => async dispatch => {
    axios.delete("/api/user/deleteuserfromteam",  {data: xrefUserTeam} )
        .then(res => {
            dispatch(getTeamUsers(res.data.teamId));
            dispatch(getUsersWithoutUsersInTeam(res.data.teamId));
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
