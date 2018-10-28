import React from 'react'

 const HOC = (props) => (WrappedComponent) => {

   return (props) => {
       console.log(props);
        return (
            <WrappedComponent {...props} />          
          )
     }  
}

export default HOC;
