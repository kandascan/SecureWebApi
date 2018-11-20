import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { getBacklogItems, orderBacklogItems } from '../actions/backlogActions';
import BacklogSortable from './BacklogSortable';

class BacklogComponent extends Component {
    componentDidMount() {
        if (!this.props.auth.isAuthenticated) {
            this.props.history.push('/');            
        }
        this.props.getBacklogItems();
    }

    sortBacklogItems = (sortdItems)=>  {
        this.props.orderBacklogItems(sortdItems);
    }

    render() {
        const { items, loading } = this.props.backlog;
        return (
            <BacklogSortable items={items} loading={loading} sortBacklogItems={this.sortBacklogItems}/>
        )
    }
}

BacklogComponent.propTypes = {
    auth: PropTypes.object.isRequired,
    backlog: PropTypes.object.isRequired,
    getBacklogItems: PropTypes.func.isRequired,
    orderBacklogItems: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    auth: state.auth,
    backlog: state.backlog
});

export default connect(mapStateToProps, {getBacklogItems, orderBacklogItems})(BacklogComponent);
