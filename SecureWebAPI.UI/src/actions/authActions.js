import axios from 'axios';
import setAuthToken from '../utils/setAuthToken';
import jwt_decode from 'jwt-decode';
import { GET_ERRORS, SET_CURRENT_USER, SET_CURRENT_TEAM } from './types';

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