import React, { Component } from 'react'
import axios from 'axios';

class RegisterForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
      email: '',
      password: ''
    };
  }  

  handleChange(e){
    this.setState({
      [e.target.id]: e.target.value
    });
  }

  handleSubmit(e){
    e.preventDefault();    
    axios.post("http://localhost:50754/api/auth/register", this.state).then(res => { // api url wyrzucic do osobnego pliku
      const { token, success, errors } = res.data;
      if(success){
        this.setState({
          username: '',
          email: '',
          password: ''
        });        
        console.log(token);//tokena zapisac w local storage
        this.props.history.push("/login");
      }else{
        console.log(errors);//dorobic errory
      }
    });  
  }

  render() {
    return (
      <div className="form-main text-center">
        <form className="form-signin" onSubmit={this.handleSubmit.bind(this)}>
            <img className="mb-4" src="dist/bootstrap-solid.svg" alt="" width="72" height="72" />
            <h1 className="h3 mb-3 font-weight-normal">Please sign up</h1>
            <label htmlFor="username" className="sr-only">Email address</label> {/*utworzyc nowy komponent dla labela i inputa */}
            <input type="text" id="username" onChange={this.handleChange.bind(this)} value={this.state.username} className="form-control" placeholder="Name" required autoFocus/>  
            <label htmlFor="email" className="sr-only">Email address</label>
            <input type="email" id="email" onChange={this.handleChange.bind(this)} value={this.state.email} className="form-control" placeholder="Email address" required />
            <label htmlFor="password" className="sr-only">Password</label>
            <input type="password" id="password" onChange={this.handleChange.bind(this)} value={this.state.password} className="form-control" placeholder="Password" required />
            <button className="btn btn-lg btn-primary btn-block" type="submit">Sign up</button>
            <p className="mt-5 mb-3 text-muted">&copy; 2018-2019</p>
        </form>
      </div>
    )
  }
}

export default RegisterForm;