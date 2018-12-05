import React, { Component } from 'react'
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import ModalComponent from '../CommonComponent/ModaComponent';
import { toggleCreateTeamModal, clearErrorsModal } from '../actions/modalActions';
import { createTeam, getUserTeams } from '../actions/teamActions';
import TextFieldGroup from '../CommonComponent/TextFieldGroup';
import Spinner from '../CommonComponent/Spinner';
import classnames from 'classnames';

class Team extends Component {
    constructor(props) {
        super(props);
        this.state = { teamname: '', errors: {} };
    }

    componentDidMount = () => {
        this.props.getUserTeams();
    }

    toggle = () => {
        this.setState({ teamname: '', errors: {} });
        this.props.toggleCreateTeamModal();
    }

    handleChange = (e) => {
        this.setState({ [e.target.name]: e.target.value, errors: {} });
        this.props.clearErrorsModal();
    }

    handleCancel = () => {
        this.props.toggleCreateTeamModal();
        this.props.clearErrorsModal();
    }

    handleSubmit = (e) => {
        e.preventDefault();
        let newTeam = {
            teamname: this.state.teamname
        }
        this.props.createTeam(newTeam);
    }

    render() {
        const { showCreateTeamModal } = this.props.modal;
        const { userteams, areteamsloaded } = this.props.team;
        const { errors } = this.props;
        const { showSpinner } = this.props.spinner;

        const userTeams = userteams.length === 0 ? (
            <div>{ areteamsloaded ?  (<div className="jumbotron homeColor">
            <h4>You have not assigned to any teams, or you haven't any own teams, create first team now!</h4>
            </div>)       : (null)      
            }</div>
        ) : (userteams.map((team) => (
            <Link
                to={`backlog/${team.teamId}`}
                key={team.teamId}
                className={classnames("list-group-item list-group-item-action list-group-item-light", {
                    'active': team.scrumMasterUser
                })}>
                {team.teamName}
            </Link>
        ))) ;

        const modalContent = (
            <form onSubmit={this.handleSubmit}>
                <div className="row">
                    <div className="form-group col-md-12">
                        <label>Team name:</label>
                        <TextFieldGroup
                            type="text"
                            name="teamname"
                            onChange={this.handleChange}
                            value={this.state.teamname}
                            placeholder=""
                            error={errors.teamname}
                        />
                    </div>
                </div>
            </form>
        );

        return (
            <div className="row">
            
                <div className="col-md-6">
                              <br />

                    <button type="button" className="btn btn-primary btn-lg" onClick={this.toggle}>Creat team</button>
                </div>
                <div className="col-md-6">
                    <div className="list-group">
                        {userTeams}
                    </div>
                </div>
                <ModalComponent
                    show={showCreateTeamModal}
                    header="New Team"
                    content={modalContent}
                    onCancelClick={this.handleCancel}
                    onSubmitClick={this.handleSubmit}
                />
                <div>{showSpinner ? (<Spinner />) : (null)}</div>
            </div>
        )
    }
}

Team.propTypes = {
    toggleCreateTeamModal: PropTypes.func.isRequired,
    createTeam: PropTypes.func.isRequired,
    getUserTeams: PropTypes.func.isRequired,
    clearErrorsModal: PropTypes.func.isRequired,
    team: PropTypes.object.isRequired,
    errors: PropTypes.object.isRequired,
    spinner: PropTypes.object.isRequired,
}

const mapStateToProps = (state) => ({
    modal: state.modal,
    team: state.team,
    errors: state.errors,
    spinner: state.spinner
});

export default connect(mapStateToProps, { toggleCreateTeamModal, clearErrorsModal, createTeam, getUserTeams })(Team);