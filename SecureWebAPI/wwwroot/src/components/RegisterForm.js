import React, { Component } from 'react'

class RegisterForm extends Component {
  render() {
    return (
      <div className="form-main text-center">
        <form className="form-signin">
            <img className="mb-4" src="dist/bootstrap-solid.svg" alt="" width="72" height="72" />
            <h1 className="h3 mb-3 font-weight-normal">Please sign up</h1>
            <label htmlFor="inputtName" className="sr-only">Email address</label>
            <input type="text" id="inputName" className="form-control" placeholder="Name" required autoFocus />  
            <label htmlFor="inputEmail" className="sr-only">Email address</label>
            <input type="email" id="inputEmail" className="form-control" placeholder="Email address" required />
            <label htmlFor="inputPassword" className="sr-only">Password</label>
            <input type="password" id="inputPassword" className="form-control" placeholder="Password" required />
            <button className="btn btn-lg btn-primary btn-block" type="submit">Sign up</button>
            <p className="mt-5 mb-3 text-muted">&copy; 2018-2019</p>
        </form>
      </div>
    )
  }
}

export default RegisterForm;