import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { getBacklogItems } from '../actions/taskActions';
import BacklogSortable from './BacklogSortable';

class BacklogComponent extends Component {
    componentDidMount() {
        if (!this.props.auth.isAuthenticated) {
            this.props.history.push('/');            
        }
        this.props.getBacklogItems();
    }

    // componentWillReceiveProps = (nextProps) => {
    //     if (nextProps.auth.isAuthenticated) {
    //       this.props.history.push('/backlog');
    //     }
    //   }

    render() {
        const { tasks, loading } = this.props.backlog;
        // console.log(tasks);
        // console.log(loading);



        return (
            <BacklogSortable tasks={tasks} loading={loading}/>
        )
    }
}

BacklogComponent.propTypes = {
    auth: PropTypes.object.isRequired,
    backlog: PropTypes.object.isRequired,
    getBacklogItems: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    auth: state.auth,
    backlog: state.backlog
});

export default connect(mapStateToProps, {getBacklogItems})(BacklogComponent);
