import axios from 'axios';
import setAuthToken from '../utils/setAuthToken';
import jwt_decode from 'jwt-decode';
import { GET_ERRORS, SET_CURRENT_USER, SET_CURRENT_TEAM, SET_TEAM_MEMBER } from './types';
import store from '../store';

export const isTeamMember = (teamId, history) => dispatch => {
    const teamIds = store.getState().auth.user.UserTeams.split(',')
    const isTeamMember = teamIds.includes(teamId);
    if(!isTeamMember){
        history.push('/');
        localStorage.removeItem('teamid');
    }else{
        localStorage.setItem('teamid', `${teamId}`);        
    }
    dispatch({
        type: SET_TEAM_MEMBER,
        payload: isTeamMember
    })
}

export const registerUser = (userData, history) => dispatch => {
    axios.post("api/auth/register", userData)
        .then(res => {
            history.push('/login');
        })
        .catch(err =>
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            })
        );
};

export const loginUser = (userData) => dispatch => {
    axios.post("api/auth/login", userData)
        .then(res => {
            const { token } = res.data;
            localStorage.setItem('smToken', `Bearer ${token}`);
            setAuthToken(`Bearer ${token}`);
            const decoded = jwt_decode(token);
            dispatch(setCurrentUser(decoded));
        })
        .catch(err =>
            dispatch({
                type: GET_ERRORS,
                payload: err.response.data
            })
        );
};

export const setCurrentUser = (decoded) => {
    return {
        type: SET_CURRENT_USER,
        payload: decoded
    }
}

export const logoutUser = () => dispatch => {
    localStorage.removeItem('smToken');
    localStorage.removeItem('teamid');
    dispatch({
        type: SET_CURRENT_TEAM,
        payload: false
    })
    setAuthToken(false);
    dispatch(setCurrentUser({}));
}