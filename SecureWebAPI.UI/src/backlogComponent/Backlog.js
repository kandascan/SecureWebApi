import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { getBacklogItems, orderBacklogItems, removeTask } from '../actions/backlogActions';
import BacklogSortable from './BacklogSortable';
import CreateTask from './CreateTask';

class BacklogComponent extends Component {
    componentDidMount() {
        if (!this.props.auth.isAuthenticated) {
            this.props.history.push('/');
        }
        this.props.getBacklogItems();
    }

    sortBacklogItems = (sortdItems) => {
        this.props.orderBacklogItems(sortdItems);
    }

    removeBacklogTask = (id) => {
        this.props.removeTask(id);
    }

    render() {
        const { items, loading } = this.props.backlog;
        return (
            <div className="landing landing-background-backlog">
                <div className="dark-overlay landing-inner text-light">
                    <CreateTask />
                    <h1 className="centerText" style={{ marginTop: "-50px" }}>Main Backlog</h1>
                    <div className="container">{items != null && items.tasks != null && !loading ?
                        (
                            <div>{items.tasks.length > 0 ? (
                                <BacklogSortable
                                    items={items}
                                    sortBacklogItems={this.sortBacklogItems}
                                    removeBacklogTask={this.removeBacklogTask}
                                />
                            ) : (
                                    <div>
                                        <br />
                                        <h5 className="centerText">There is no items on the backlog yet, create first Task!</h5>
                                    </div>
                                )}
                            </div>
                        ) : (
                            <div className="loader"></div>
                        )}</div>
                </div>
            </div>
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
    backlog: state.backlog,
});

export default connect(mapStateToProps, { getBacklogItems, orderBacklogItems, removeTask })(BacklogComponent);
