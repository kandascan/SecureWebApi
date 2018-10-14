import React from 'react'

 const HOC = (WrappedComponent) => {
    const style = {
        //backgroundColor: "gray",
        padding: "0 30em"
    };

     return (props) => {
        return (
            <div style={style}>
              <WrappedComponent {...props} />
            </div>
          )
     }  
}

export default HOC;
