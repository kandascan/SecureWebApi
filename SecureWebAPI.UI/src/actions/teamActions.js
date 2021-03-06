import axios from 'axios';
import { TOGGLE_SPINNER, SHOW_CREATE_TEAM_MODAL, GET_ERRORS, GET_USER_TEAMS, SET_CURRENT_TEAM, GET_TEAM_BY_ID } from './types';
import { updateToken } from './authActions';
import isEmpty from '../validation/is-Empty';

export const getTeamById = (teamid) => dispacht => {
    axios.get(`/api/team/getteambyid/${teamid}`)
        .then(res => {
            dispacht({
                type: GET_TEAM_BY_ID,
                payload: res.data.team
            })
        })
        .catch(err => {
            dispacht({
                type: GET_TEAM_BY_ID,
                payload: {}
            })
        }
        );
}

export const currentTeam = (teamid) => dispatch => {
    localStorage.setItem('teamid', `${teamid}`);
    dispatch({
        type: SET_CURRENT_TEAM,
        payload: true
    })
}

export const getUserTeams = () => dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.get("api/team/getuserteams")
        .then(res => {
            dispatch({
                type: GET_USER_TEAMS,
                payload: res.data.userTeams,
                teamsLoaded: true
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
        })
        .catch(err => {
            dispatch({
                type: GET_USER_TEAMS,
                payload: [],
                teamsLoaded: true

            })
            dispatch({
                type: TOGGLE_SPINNER
            });
        }
        );

        if(localStorage.teamid){
            dispatch({
                type: SET_CURRENT_TEAM,
                payload: true
            })
        }
}

export const createTeam = (newTeam) => dispatch => {
    dispatch({
        type: TOGGLE_SPINNER
    });
    axios.post("api/team", newTeam)
        .then(res => {
            if(!isEmpty(res.data.errors)){
                dispatch({
                    type: GET_ERRORS,
                    payload: res.data.errors
                })
                dispatch({
                    type: TOGGLE_SPINNER
                });
                return;
            }
            dispatch({
                type: SHOW_CREATE_TEAM_MODAL
            });
            dispatch({
                type: TOGGLE_SPINNER
            });
            dispatch(getUserTeams());
            dispatch({
                type: GET_ERRORS,
                payload: {}
            })
            dispatch(updateToken(res.data));
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