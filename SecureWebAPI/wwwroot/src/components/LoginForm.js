import React, { Component } from 'react';
import HOC from './HOC';

class LoginForm extends Component {
  render() {
    return (
      <div className="form-main text-center">
        <form className="form-signin">
            <img className="mb-4" src="dist/bootstrap-solid.svg" alt="" width="72" height="72" />
            <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
            <label htmlFor="inputName" className="sr-only">Name</label>
            <input type="text" id="inputName" className="form-control" placeholder="Name" required autoFocus />
            <label htmlFor="inputPassword" className="sr-only">Password</label>
            <input type="password" id="inputPassword" className="form-control" placeholder="Password" required />
            <div className="checkbox mb-3">
                <label>
                <input type="checkbox" value="remember-me" /> Remember me
                </label>
            </div>
            <button className="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>
            <p className="mt-5 mb-3 text-muted">&copy; 2018-2019</p>
        </form>
      </div>
    )
  }
}
export default HOC(LoginForm);