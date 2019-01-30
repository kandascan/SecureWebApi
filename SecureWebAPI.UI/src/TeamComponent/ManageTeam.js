import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { getUsersWithoutUsersInTeam, getTeamUsers, getUserRoles, addUserToTeam, deleteUserFromTeam } from '../actions/userActions';
import { getTeamById, currentTeam } from '../actions/teamActions';
import { isTeamMember } from '../actions/authActions';
import { withRouter } from 'react-router-dom';
import classnames from 'classnames';
import isEmpty from '../../src/validation/is-Empty'

class ManageTeam extends Component {
    constructor(props) {
        super(props);
        this.state = { userId: -1, roleId: -1 };
    }
    
    componentWillReceiveProps = (nextProps) => {
        const { errors } = nextProps;
        if (isEmpty(errors))
            this.setState({ userId: -1, roleId: -1 });
    }

    componentDidMount = () => {
        if (!this.props.auth.isAuthenticated) {
            this.props.history.push('/');
        }
        if (this.props.match.params.teamid){
            this.props.isTeamMember(this.props.match.params.teamid, this.props.history);
            this.props.getUsersWithoutUsersInTeam(this.props.match.params.teamid);
            this.props.getTeamUsers(this.props.match.params.teamid)
            this.props.getUserRoles();
            this.props.getTeamById(this.props.match.params.teamid);
           // this.props.currentTeam(this.props.match.params.teamid);
        }
    }

    handleChange = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    handleClick = () => {
        const newUser = {
            Id: this.state.userId,
            TeamId: parseInt(this.props.match.params.teamid),
            RoleId: this.state.roleId
        }
        this.props.addUserToTeam(newUser);
    }

    handleDeleteUserFromTeam = (userId) => {
        const xrefUserTeam = {
            UserId: userId,
            TeamId: parseInt(this.props.match.params.teamid)
        };
        this.props.deleteUserFromTeam(xrefUserTeam);
    }

    render() {
        const { errors, team } = this.props;
        const { users, teamUsers, roles } = this.props.user;
        let ddlUsers, ddlRoles, teamUserList = null;
        if (users != null) {
            ddlUsers = users.userList.map((user) =>
                <option key={user.userId} value={user.userId}>{user.userName}</option>
            );
        }

        if (roles != null) {
            ddlRoles = roles.roles.map((role) =>
                <option key={role.roleId} value={role.roleId}>{role.roleName}</option>
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
                    {!teamUser.me ? (<i className="fas fa-trash" onClick={() =>this.handleDeleteUserFromTeam(teamUser.userId)}></i>) : (null)}
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
                                    <h1 className="display-4">Manage team: <small>{team.team.teamName}</small></h1>
                                </div>
                            </div>
                            <br /><br /><br /><br />
                            <form className="col-md-5">
                                <div className="form-group">
                                    <label htmlFor="inputUser">Users</label>
                                    <select
                                        className={classnames("custom-select mr-sm-2", {
                                            'is-invalid': errors.userId
                                        })}
                                        name="userId"
                                        onChange={this.handleChange}
                                        value={this.state.userId} >
                                        <option key={-1} value={-1}>Choose user for your team</option>
                                        {ddlUsers}
                                    </select>
                                    {errors.userId && (<div className="invalid-feedback">{errors.userId}</div>)}
                                </div>
                                <div className="form-group">
                                    <label htmlFor="inputUser">Role</label>
                                    <select
                                        className={classnames("custom-select mr-sm-2", {
                                            'is-invalid': errors.roleId
                                        })}
                                        name="roleId"
                                        onChange={this.handleChange}
                                        value={this.state.roleId} >
                                        <option key={-1} value={-1}>Choose role for user</option>
                                        {ddlRoles}
                                    </select>
                                    {errors.roleId && (<div className="invalid-feedback">{errors.roleId}</div>)}
                                </div>
                                <button onClick={this.handleClick} type="button" className="btn btn-warning">Add User</button>
                            </form>
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
    auth: PropTypes.object.isRequired,
    user: PropTypes.object.isRequired,
    errors: PropTypes.object.isRequired,
    getUsersWithoutUsersInTeam: PropTypes.func.isRequired,
    getTeamUsers: PropTypes.func.isRequired,
    getUserRoles: PropTypes.func.isRequired,
    addUserToTeam: PropTypes.func.isRequired,
    deleteUserFromTeam: PropTypes.func.isRequired,
    getTeamById: PropTypes.func.isRequired,
    currentTeam: PropTypes.func.isRequired,
    isTeamMember: PropTypes.func.isRequired
}

const mapStateToProps = (state) => ({
    auth: state.auth,
    user: state.user,
    errors: state.errors,
    team: state.team
});

export default connect(mapStateToProps, { getUsersWithoutUsersInTeam, getTeamUsers, getUserRoles, addUserToTeam, deleteUserFromTeam, getTeamById, currentTeam, isTeamMember })(withRouter(ManageTeam));