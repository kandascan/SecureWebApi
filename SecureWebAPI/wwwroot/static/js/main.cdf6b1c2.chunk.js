(window.webpackJsonp=window.webpackJsonp||[]).push([[0],{38:function(e,t,a){},45:function(e,t,a){e.exports=a(85)},50:function(e,t,a){},85:function(e,t,a){"use strict";a.r(t);var n=a(1),r=a.n(n),o=a(13),l=a.n(o),c=(a(50),a(3)),s=a(4),i=a(6),m=a(5),u=a(7),p=a(86),d=a(79),h=a(9),g=a(11),f=a.n(g),b=function(e){e?f.a.defaults.headers.common.Authorization=e:delete f.a.defaults.headers.common.Authorization},E=a(20),v=a.n(E),y=function(e){return{type:"SET_CURRENT_USER",payload:e}},k=function(){return function(e){localStorage.removeItem("smToken"),b(!1),e(y({}))}},O=function(){return function(e){e(j()),f.a.get("api/task/getbacklogtasks").then(function(t){e({type:"GET_BACKLOG_ITEMS",payload:t.data})}).catch(function(t){return e({type:"GET_BACKLOG_ITEMS",payload:{}})})}},N=function(){return{type:"TOGGLE_MODAL"}},j=function(){return{type:"BACKLOG_LOADING"}},C=function(e){function t(){var e,a;Object(c.a)(this,t);for(var n=arguments.length,r=new Array(n),o=0;o<n;o++)r[o]=arguments[o];return(a=Object(i.a)(this,(e=Object(m.a)(t)).call.apply(e,[this].concat(r)))).onLogoutClick=function(e){e.preventDefault(),a.props.clearBacklog(),a.props.logoutUser(),window.location.href="/"},a}return Object(u.a)(t,e),Object(s.a)(t,[{key:"render",value:function(){var e=this.props.auth,t=e.isAuthenticated,a=e.user,n=r.a.createElement("div",{className:"collapse navbar-collapse",id:"mobile-nav"},r.a.createElement("ul",{className:"navbar-nav mr-auto"},r.a.createElement("li",{className:"nav-item"},r.a.createElement(p.a,{className:"nav-link",to:"/currentSprint"},"Current Sprint"))),r.a.createElement("ul",{className:"navbar-nav ml-auto"},r.a.createElement("li",{className:"nav-item"},r.a.createElement(p.a,{className:"nav-link",to:"/profile"},a.sub)),r.a.createElement("li",{className:"nav-item"},r.a.createElement("a",{href:"",onClick:this.onLogoutClick,className:"nav-link",to:"/login"},r.a.createElement("span",{className:"oi oi-account-logout"})," ","Logout")))),o=r.a.createElement("div",{className:"collapse navbar-collapse",id:"mobile-nav"},r.a.createElement("ul",{className:"navbar-nav ml-auto"},r.a.createElement("li",{className:"nav-item"},r.a.createElement(p.a,{className:"nav-link",to:"/register"},"Sign Up")),r.a.createElement("li",{className:"nav-item"},r.a.createElement(p.a,{className:"nav-link",to:"/login"},"Login"))));return r.a.createElement("nav",{className:"navbar navbar-expand-sm navbar-dark bg-dark mb-4"},r.a.createElement("div",{className:"container"},r.a.createElement(d.a,{className:"navbar-brand",to:"/"},"Scrum Manager"),r.a.createElement("button",{className:"navbar-toggler",type:"button","data-toggle":"collapse","data-target":"#mobile-nav"},r.a.createElement("span",{className:"navbar-toggler-icon"})),t?n:o))}}]),t}(n.Component),w=Object(h.b)(function(e){return{auth:e.auth}},{logoutUser:k,clearBacklog:function(){return{type:"CLEAR_BACKLOG"}}})(C),S=function(){return r.a.createElement("footer",{className:"bg-dark text-white mt-5 p-4 text-center"},"Copyright \xa9 ",(new Date).getFullYear()," Scrum Manager")},T=a(87),I=a(77),_=a(17),x=a(42),A=a(15),G=function(e){return void 0===e||null===e||"object"===typeof e&&0===Object.keys(e).length||"string"===typeof e&&0===e.trim().length},R={isAuthenticated:!1,user:{}},B={},D={items:null,loading:!1,modal:!1},L={priorities:null,efforts:null},M=Object(_.c)({auth:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:R,t=arguments.length>1?arguments[1]:void 0;switch(t.type){case"SET_CURRENT_USER":return Object(A.a)({},e,{isAuthenticated:!G(t.payload),user:t.payload});default:return e}},errors:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:B,t=arguments.length>1?arguments[1]:void 0;switch(t.type){case"GET_ERRORS":return t.payload;default:return e}},backlog:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:D,t=arguments.length>1?arguments[1]:void 0;switch(t.type){case"BACKLOG_LOADING":return Object(A.a)({},e,{items:[],loading:!0});case"GET_BACKLOG_ITEMS":return Object(A.a)({},e,{items:t.payload,loading:!1});case"CLEAR_BACKLOG":return Object(A.a)({},e,{items:null});case"TOGGLE_MODAL":return Object(A.a)({},e,{modal:!e.modal});default:return e}},dics:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:L,t=arguments.length>1?arguments[1]:void 0;switch(t.type){case"GET_PRIORITIES":return Object(A.a)({},e,{priorities:t.payload});case"GET_EFFORTS":return Object(A.a)({},e,{efforts:t.payload});default:return e}}}),U=[x.a],P=Object(_.e)(M,{},Object(_.d)(_.a.apply(void 0,U),window.__REDUX_DEVTOOLS_EXTENSION__&&window.__REDUX_DEVTOOLS_EXTENSION__())),K=a(16),F=a(2),V=a.n(F),W=function(e){var t=e.id,a=e.name,n=e.placeholder,o=e.value,l=e.label,c=e.error,s=e.info,i=e.type,m=e.onChange;e.disabled;return r.a.createElement("div",null,r.a.createElement("label",{htmlFor:t,className:"sr-only"},l),r.a.createElement("input",{type:i,id:t,name:a,onChange:m,value:o,className:V()("form-control",{"is-invalid":c}),placeholder:n}),s&&r.a.createElement("small",{className:"form-text text-muted"},s),c&&r.a.createElement("div",{className:"invalid-feedback"},c))};W.defaultProps={type:"text"};var X=W,z=function(e){function t(e){var a;return Object(c.a)(this,t),(a=Object(i.a)(this,Object(m.a)(t).call(this,e))).componentWillReceiveProps=function(e){e.auth.isAuthenticated&&a.props.history.push("/backlog"),e.errors&&a.setState({errors:e.errors})},a.handleChange=function(e){a.setState(Object(K.a)({},e.target.id,e.target.value))},a.handleSubmit=function(e){e.preventDefault();var t={username:a.state.username,password:a.state.password};a.props.loginUser(t)},a.state={username:"",password:"",errors:{}},a}return Object(u.a)(t,e),Object(s.a)(t,[{key:"componentDidMount",value:function(){this.props.auth.isAuthenticated&&this.props.history.push("/backlog")}},{key:"render",value:function(){var e=this.state.errors;return r.a.createElement("div",{className:"landing landing-background-login"},r.a.createElement("div",{className:"dark-overlay landing-inner text-light"},r.a.createElement("div",{className:"container"},r.a.createElement("div",{className:"form-main text-center"},r.a.createElement("form",{noValidate:!0,className:"form-signin",onSubmit:this.handleSubmit.bind(this)},r.a.createElement("img",{className:"mb-4",src:"dist/bootstrap-solid.svg",alt:"",width:"72",height:"72"}),r.a.createElement("h1",{className:"h3 mb-3 font-weight-normal"},"Please sign in"),r.a.createElement(X,{type:"text",id:"username",onChange:this.handleChange,value:this.state.username,placeholder:"Name",error:e.username}),r.a.createElement(X,{type:"password",id:"password",onChange:this.handleChange,value:this.state.password,placeholder:"Password",error:e.password}),r.a.createElement("button",{className:"btn btn-lg btn-primary btn-block",type:"submit"},"Sign in"))))))}}]),t}(n.Component),J=Object(h.b)(function(e){return{auth:e.auth,errors:e.errors}},{loginUser:function(e){return function(t){f.a.post("api/auth/login",e).then(function(e){var a=e.data.token;localStorage.setItem("smToken","Bearer ".concat(a)),b(a);var n=v()(a);t(y(n))}).catch(function(e){return t({type:"GET_ERRORS",payload:e.response.data})})}}})(z),H=a(88),Y=function(e){function t(){var e;return Object(c.a)(this,t),(e=Object(i.a)(this,Object(m.a)(t).call(this))).componentWillReceiveProps=function(t){t.errors&&e.setState({errors:t.errors})},e.handleChange=function(t){e.setState(Object(K.a)({},t.target.id,t.target.value))},e.handleSubmit=function(t){t.preventDefault();var a={username:e.state.username,email:e.state.email,password:e.state.password};e.props.registerUser(a,e.props.history)},e.state={username:"",email:"",password:"",errors:{}},e}return Object(u.a)(t,e),Object(s.a)(t,[{key:"componentDidMount",value:function(){this.props.auth.isAuthenticated&&this.props.history.push("/backlog")}},{key:"render",value:function(){var e=this.state.errors;return r.a.createElement("div",{className:"landing landing-background-register"},r.a.createElement("div",{className:"dark-overlay landing-inner text-light"},r.a.createElement("div",{className:"container"},r.a.createElement("div",{className:"form-main text-center"},r.a.createElement("form",{noValidate:!0,className:"form-signin",onSubmit:this.handleSubmit},r.a.createElement("img",{className:"mb-4",src:"dist/bootstrap-solid.svg",alt:"",width:"72",height:"72"}),r.a.createElement("h1",{className:"h3 mb-3 font-weight-normal"},"Please sign up"),r.a.createElement(X,{type:"text",id:"username",onChange:this.handleChange,value:this.state.username,placeholder:"Name",error:e.username}),r.a.createElement(X,{type:"email",id:"email",onChange:this.handleChange,value:this.state.email,placeholder:"Email",error:e.email}),r.a.createElement(X,{type:"password",id:"password",onChange:this.handleChange,value:this.state.password,placeholder:"Password",error:e.password}),r.a.createElement("button",{className:"btn btn-lg btn-primary btn-block",type:"submit"},"Sign up"))))))}}]),t}(n.Component),$=Object(h.b)(function(e){return{auth:e.auth,errors:e.errors}},{registerUser:function(e,t){return function(a){f.a.post("api/auth/register",e).then(function(e){t.push("/login")}).catch(function(e){return a({type:"GET_ERRORS",payload:e.response.data})})}}})(Object(H.a)(Y)),q=function(e){function t(){return Object(c.a)(this,t),Object(i.a)(this,Object(m.a)(t).apply(this,arguments))}return Object(u.a)(t,e),Object(s.a)(t,[{key:"componentDidMount",value:function(){this.props.auth.isAuthenticated&&this.props.history.push("/backlog")}},{key:"render",value:function(){return r.a.createElement("div",{className:"landing landing-background-home"},r.a.createElement("div",{className:"dark-overlay landing-inner text-light"},r.a.createElement("div",{className:"container"},r.a.createElement("div",{className:"row"},r.a.createElement("div",{className:"col-md-12 text-center"},r.a.createElement("h1",{className:"display-3 mb-4"},"Scrum Manager"),r.a.createElement("p",{className:"lead"},"Great and free tool for developers to smart manage their work"),r.a.createElement("hr",null),r.a.createElement(d.a,{to:"/register",className:"btn btn-lg btn-info mr-2"},"Sign Up"),r.a.createElement(d.a,{to:"/login",className:"btn btn-lg btn-light"},"Login"))))))}}]),t}(n.Component),Q=Object(h.b)(function(e){return{auth:e.auth}})(q),Z=a(23),ee=function(e){function t(){var e,a;Object(c.a)(this,t);for(var n=arguments.length,r=new Array(n),o=0;o<n;o++)r[o]=arguments[o];return(a=Object(i.a)(this,(e=Object(m.a)(t)).call.apply(e,[this].concat(r)))).onButtonClick=function(e){(0,a.props.onDeleteItem)(parseInt(e))},a}return Object(u.a)(t,e),Object(s.a)(t,[{key:"render",value:function(){var e=this,t=this.props,a=t.index,n=t.value,o=t.id;return r.a.createElement("li",{className:"list-group-item d-flex justify-content-between align-items-center list-group-item-light"},n,r.a.createElement("span",{className:"badge badge-primary badge-pill"},a),r.a.createElement("div",null,r.a.createElement("button",{onClick:function(){return e.onButtonClick(o)},type:"button",className:"btn btn-outline-info btn-sm"},"Delete")))}}]),t}(n.Component),te=(a(38),Object(Z.SortableElement)(function(e){var t=e.index,a=e.value,n=e.onDeleteItem,o=e.id;return r.a.createElement(ee,{index:t,value:a,onDeleteItem:n,id:o})})),ae=Object(Z.SortableContainer)(function(e){var t=e.items,a=e.onDeleteItem;return r.a.createElement("ul",{className:"list-group"},t.map(function(e,t){return r.a.createElement(te,{key:"item-".concat(t),index:t,value:e.taskname,onDeleteItem:a,id:e.id})}))}),ne=function(e){function t(){var e,a;Object(c.a)(this,t);for(var n=arguments.length,r=new Array(n),o=0;o<n;o++)r[o]=arguments[o];return(a=Object(i.a)(this,(e=Object(m.a)(t)).call.apply(e,[this].concat(r)))).onDeleteItem=function(e){a.props.removeBacklogTask(e)},a.onSortEnd=function(e){var t=e.oldIndex,n=e.newIndex,r=a.props.items,o=Object(Z.arrayMove)(r.tasks,t,n);a.props.sortBacklogItems(o)},a}return Object(u.a)(t,e),Object(s.a)(t,[{key:"render",value:function(){var e=this.props.items;return r.a.createElement(ae,{items:e.tasks,onSortEnd:this.onSortEnd,onDeleteItem:this.onDeleteItem})}}]),t}(n.Component),re=a(18),oe=function(e){function t(e){var a;return Object(c.a)(this,t),(a=Object(i.a)(this,Object(m.a)(t).call(this,e))).toggle=function(){a.props.toggleModal();var e=a.props.dics,t=e.priorities,n=e.efforts;null===t&&null===n&&a.props.getEffortsAndPriorities()},a.handleChange=function(e){a.setState(Object(K.a)({},e.target.name,e.target.value))},a.handleSubmit=function(e){e.preventDefault();var t={taskname:a.state.taskname,description:a.state.description,effortId:+a.state.effort,priorityId:+a.state.priority,username:a.state.username};console.log(t),a.props.createTask(t),a.setState({taskname:"",description:"",effort:-1,priority:-1,username:""})},a.state={taskname:"",description:"",effort:-1,priority:-1,username:""},a}return Object(u.a)(t,e),Object(s.a)(t,[{key:"render",value:function(){var e=this.props.backlog.modal,t=this.props.dics,a=t.priorities,n=t.efforts,o=null;null!==a&&(o=a.priorities.map(function(e){return r.a.createElement("option",{key:e.priorityId,value:e.priorityId},e.priorityName)}));var l=null;return null!==n&&(l=n.efforts.map(function(e){return r.a.createElement("option",{key:e.effortId,value:e.effortId},e.effortName)})),r.a.createElement("div",null,r.a.createElement("div",{className:"container"},r.a.createElement(re.a,{color:"success",style:{marginTop:"-70px",float:"right"},onClick:this.toggle},"Create task")),r.a.createElement(re.b,{isOpen:e},r.a.createElement("form",{onSubmit:this.handleSubmit},r.a.createElement(re.e,null,"New task for backlog"),r.a.createElement(re.c,null,r.a.createElement("div",{className:"row"},r.a.createElement("div",{className:"form-group col-md-12"},r.a.createElement("label",null,"Task name:"),r.a.createElement("input",{name:"taskname",type:"text",value:this.state.taskname,onChange:this.handleChange,className:"form-control"}))),r.a.createElement("div",{className:"row"},r.a.createElement("div",{className:"form-group col-md-12"},r.a.createElement("label",null,"Description"),r.a.createElement("input",{name:"description",type:"text",value:this.state.description,onChange:this.handleChange,className:"form-control"}))),r.a.createElement("div",{className:"row"},r.a.createElement("div",{className:"form-group col-md-12"},r.a.createElement("label",null,"Effort:"),r.a.createElement("select",{className:"custom-select mr-sm-2",name:"effort",onChange:this.handleChange,disabled:null===n},r.a.createElement("option",{key:-1,value:-1},"Choose..."),l))),r.a.createElement("div",{className:"row"},r.a.createElement("div",{className:"form-group col-md-12"},r.a.createElement("label",null,"Priority:"),r.a.createElement("select",{className:"custom-select mr-sm-2",name:"priority",onChange:this.handleChange,disabled:null===a},r.a.createElement("option",{key:-1,value:-1},"Choose..."),o))),r.a.createElement("div",{className:"row"},r.a.createElement("div",{className:"form-group col-md-12"},r.a.createElement("label",null,"Username:"),r.a.createElement("input",{name:"username",type:"text",value:this.username,onChange:this.handleChange,className:"form-control"})))),r.a.createElement(re.d,null,r.a.createElement("input",{type:"submit",value:"Submit",color:"primary",className:"btn btn-primary"}),r.a.createElement(re.a,{color:"danger",onClick:this.toggle},"Cancel")))))}}]),t}(r.a.Component),le=Object(h.b)(function(e){return{backlog:e.backlog,dics:e.dics}},{createTask:function(e){return function(t){t(j()),f.a.post("api/task",e).then(function(e){t(N()),t(O())}).catch(function(e){return t({type:"GET_ERRORS",payload:e.response.data})})}},toggleModal:N,getEffortsAndPriorities:function(){return function(e){f.a.get("api/list/getefforts").then(function(t){e({type:"GET_EFFORTS",payload:t.data}),f.a.get("api/list/getpriorities").then(function(t){e({type:"GET_PRIORITIES",payload:t.data})}).catch(function(t){return e({type:"GET_PRIORITIES",payload:{}})})}).catch(function(t){return e({type:"GET_EFFORTS",payload:{}})})}}})(oe),ce=function(e){function t(){var e,a;Object(c.a)(this,t);for(var n=arguments.length,r=new Array(n),o=0;o<n;o++)r[o]=arguments[o];return(a=Object(i.a)(this,(e=Object(m.a)(t)).call.apply(e,[this].concat(r)))).sortBacklogItems=function(e){a.props.orderBacklogItems(e)},a.removeBacklogTask=function(e){a.props.removeTask(e)},a}return Object(u.a)(t,e),Object(s.a)(t,[{key:"componentDidMount",value:function(){this.props.auth.isAuthenticated||this.props.history.push("/"),this.props.getBacklogItems()}},{key:"render",value:function(){var e=this.props.backlog,t=e.items,a=e.loading;return r.a.createElement("div",{className:"landing landing-background-backlog"},r.a.createElement("div",{className:"dark-overlay landing-inner text-light"},r.a.createElement(le,null),r.a.createElement("h1",{className:"centerText",style:{marginTop:"-50px"}},"Main Backlog"),r.a.createElement("div",{className:"container"},null==t||null==t.tasks||a?r.a.createElement("div",{className:"loader"}):r.a.createElement("div",null,t.tasks.length>0?r.a.createElement(ne,{items:t,sortBacklogItems:this.sortBacklogItems,removeBacklogTask:this.removeBacklogTask}):r.a.createElement("div",null,r.a.createElement("br",null),r.a.createElement("h5",{className:"centerText"},"There is no items on the backlog yet, create first Task!"))))))}}]),t}(n.Component),se=Object(h.b)(function(e){return{auth:e.auth,backlog:e.backlog}},{getBacklogItems:O,orderBacklogItems:function(e){return function(t){t(j()),f.a.post("api/task/sortedbacklog",e).then(function(e){t({type:"GET_BACKLOG_ITEMS",payload:e.data})}).catch(function(e){return t({type:"GET_BACKLOG_ITEMS",payload:{}})})}},removeTask:function(e){return function(t){t(j()),f.a.delete("api/task",{params:{id:e}}).then(function(e){t({type:"GET_BACKLOG_ITEMS",payload:e.data})}).catch(function(e){t({type:"GET_BACKLOG_ITEMS",payload:{}})})}}})(ce),ie=function(e){function t(){return Object(c.a)(this,t),Object(i.a)(this,Object(m.a)(t).apply(this,arguments))}return Object(u.a)(t,e),Object(s.a)(t,[{key:"componentDidMount",value:function(){this.props.auth.isAuthenticated||this.props.history.push("/")}},{key:"render",value:function(){return r.a.createElement("div",{className:"landing landing-background-currentSprint"},r.a.createElement("div",{className:"dark-overlay landing-inner text-light"},r.a.createElement("div",{className:"container"},r.a.createElement("div",{className:"row"},r.a.createElement("div",{className:"col-md-12 text-center"},r.a.createElement("h1",null,"Here will be board with current task for users"))))))}}]),t}(n.Component),me=Object(h.b)(function(e){return{auth:e.auth}})(ie),ue=function(e){function t(){return Object(c.a)(this,t),Object(i.a)(this,Object(m.a)(t).apply(this,arguments))}return Object(u.a)(t,e),Object(s.a)(t,[{key:"componentDidMount",value:function(){this.props.auth.isAuthenticated||this.props.history.push("/")}},{key:"render",value:function(){var e=this.props.auth.user;return r.a.createElement("div",{className:"landing landing-background-profile"},r.a.createElement("div",{className:"dark-overlay landing-inner text-light"},r.a.createElement("div",{className:"container"},r.a.createElement("div",{className:"row"},r.a.createElement("div",{className:"col-md-12 text-center"},r.a.createElement("h1",null,"User info"),r.a.createElement("h3",null,e.sub))))))}}]),t}(n.Component),pe=Object(h.b)(function(e){return{auth:e.auth}})(ue);if(localStorage.smToken){b(localStorage.smToken);var de=v()(localStorage.smToken);P.dispatch(y(de));var he=Date.now()/1e3;de.exp<he&&(P.dispatch(k()),window.location.href="/login")}var ge=function(e){function t(){return Object(c.a)(this,t),Object(i.a)(this,Object(m.a)(t).apply(this,arguments))}return Object(u.a)(t,e),Object(s.a)(t,[{key:"render",value:function(){return r.a.createElement(h.a,{store:P},r.a.createElement(T.a,null,r.a.createElement("div",null,r.a.createElement(w,null),r.a.createElement(I.a,{exact:!0,path:"/",component:Q}),r.a.createElement(I.a,{exact:!0,path:"/register",component:$}),r.a.createElement(I.a,{exact:!0,path:"/login",component:J}),r.a.createElement(I.a,{exact:!0,path:"/backlog",component:se}),r.a.createElement(I.a,{exact:!0,path:"/currentSprint",component:me}),r.a.createElement(I.a,{exact:!0,path:"/profile",component:pe}),r.a.createElement(S,null))))}}]),t}(n.Component);Boolean("localhost"===window.location.hostname||"[::1]"===window.location.hostname||window.location.hostname.match(/^127(?:\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$/));l.a.render(r.a.createElement(ge,null),document.getElementById("root")),"serviceWorker"in navigator&&navigator.serviceWorker.ready.then(function(e){e.unregister()})}},[[45,2,1]]]);
//# sourceMappingURL=main.cdf6b1c2.chunk.js.map