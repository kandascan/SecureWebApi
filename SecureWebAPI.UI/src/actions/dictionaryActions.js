import axios from 'axios';
import { GET_PRIORITIES, GET_EFFORTS } from './types';

export const getEffortsAndPriorities = () => dispatch => {
    axios.get("api/list/getefforts")
        .then(res => {
            dispatch({
                type: GET_EFFORTS,
                payload: res.data
            });
            axios.get("api/list/getpriorities")
                .then(res => {
                    dispatch({
                        type: GET_PRIORITIES,
                        payload: res.data
                    })
                })
                .catch(err =>
                    dispatch({
                        type: GET_PRIORITIES,
                        payload: {}
                    })
                );
        })
        .catch(err =>
            dispatch({
                type: GET_EFFORTS,
                payload: {}
            })
        );
}
