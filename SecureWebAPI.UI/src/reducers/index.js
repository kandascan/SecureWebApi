import { combineReducers } from 'redux';
import authReducer from './authReducer';
import errorReducer from './errorReducer';
import backlogReducer from './backlogReducer';
import dictionaryReducer from './dictionaryReducer';
import spinnerReducer from './spinnerReducer';
import modalReducer from './modalReducer';
import teamReducer from './teamReducer';
import sprintReducer from './sprintReducer';
import userReducer from './userReducer';

export default combineReducers({
    auth: authReducer,
    errors: errorReducer,
    backlog: backlogReducer,
    dics: dictionaryReducer,
    spinner: spinnerReducer,
    modal: modalReducer,
    team: teamReducer,
    sprint: sprintReducer,
    user: userReducer
});