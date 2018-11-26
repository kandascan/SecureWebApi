import { combineReducers } from 'redux';
import authReducer from './authReducer';
import errorReducer from './errorReducer';
import backlogReducer from './backlogReducer';
import dictionaryReducer from './dictionaryReducer';
import spinnerReducer from './spinnerReducer';

export default combineReducers({
    auth: authReducer,
    errors: errorReducer,
    backlog: backlogReducer,
    dics: dictionaryReducer,
    spinner: spinnerReducer
});