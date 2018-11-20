import { combineReducers } from 'redux';
import authReducer from './authReducer';
import errorReducer from './errorReducer';
import backlogReducer from './backlogReducer';

export default combineReducers({
    auth: authReducer,
    errors: errorReducer,
    backlog: backlogReducer
});