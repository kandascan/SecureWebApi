import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { getUsersWithoutMe, getTeamUsers } from '../actions/userActions';
import classnames from 'classnames';

class ManageTeam extends Component {
    constructor(props) {
        super(props);
        this.state = { userId: -1, roleId: -1 };
    }
    componentDidMount = () => {
        const { users, teamUsers } = this.props.user;
        if (users === null)
            this.props.getUsersWithoutMe();
        if (teamUsers === null)
            this.props.getTeamUsers(this.props.match.params.teamid)
    }
    handleChange = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    render() {
        const { users, teamUsers } = this.props.user;
        let ddlUsers, ddlRoles, teamUserList = null;
        if (users != null) {
            ddlUsers = users.userList.map((user) =>
                <option key={user.userId} value={user.userId}>{user.userName}</option>
            );
        }

        if (teamUsers != null) {
            teamUserList = teamUsers.teamUsers.map((teamUser) =>
                <li key={teamUser.userId}
                    className={classnames("list-group-item list-group-item-light d-flex justify-content-between align-items-center", {
                        'active': teamUser.me
                    })}
                >
                    {teamUser.userName} - {teamUser.userRole}
                    <span
                        className={classnames("badge  badge-pill", {
                            'badge-info': !teamUser.me,
                            'badge-dark': teamUser.me
                        })}>14</span>
                    {teamUser.me ? (null) : (<button className="btn btn-danger"><i className="fas fa-trash"></i></button>)}
                </li>
            );
        }
        return (
            <div className="landing landing-background-home">
                <div className="dark-overlay text-light">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-12 text-center">
                                <div>
                                    <h1 className="display-4">Team manager </h1>
                                </div>
                            </div>
                            <br /><br /><br /><br />
                            <div className="form-group col-md-5">
                                <label htmlFor="inputUser">Users</label>
                                <select className="custom-select mr-sm-2" name="userId" onChange={this.handleChange} value={this.state.userId} >
                                    <option key={-1} value={-1}>Choose user for your team</option>
                                    {ddlUsers}
                                </select>
                                <label htmlFor="inputUser">Role</label>
                                <select className="custom-select mr-sm-2" name="roleId" onChange={this.handleChange} value={this.state.roleId} >
                                    <option key={-1} value={-1}>Choose role for user</option>
                                    {ddlRoles}
                                </select>
                                <br /><br />
                                <button type="button" className="btn btn-warning">Add User</button>
                            </div>
                            <div className="col-md-2">
                            </div>
                            <div className="col-md-5">
                                <ul className="list-group">
                                    {teamUserList}
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

ManageTeam.propTypes = {
    user: PropTypes.object.isRequired,
    getUsersWithoutMe: PropTypes.func.isRequired,
    getTeamUsers: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    user: state.user
});

export default connect(mapStateToProps, { getUsersWithoutMe, getTeamUsers })(ManageTeam);