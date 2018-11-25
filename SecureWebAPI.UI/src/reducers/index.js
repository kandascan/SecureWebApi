import { combineReducers } from 'redux';
import authReducer from './authReducer';
import errorReducer from './errorReducer';
import backlogReducer from './backlogReducer';
import dictionaryReducer from './dictionaryReducer';

export default combineReducers({
    auth: authReducer,
    errors: errorReducer,
    backlog: backlogReducer,
    dics: dictionaryReducer
});