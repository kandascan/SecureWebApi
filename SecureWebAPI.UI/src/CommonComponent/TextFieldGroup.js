import React from 'react';
import classnames from 'classnames';
import PropTypes from 'prop-types';

const TextFieldGroup = ({
    id,
    name,
    placeholder,
    value,
    label,
    error,
    info,
    type,
    onChange,
    disabled
    
}) => {
    return (
        <div>
             <label 
                htmlFor={id} 
                className="sr-only">{label}
            </label>
            <input 
                type={type} 
                id={id} 
                name={name}
                onChange={onChange} 
                value={value} 
                className={classnames("form-control", {
                    'is-invalid': error
                })} 
                placeholder={placeholder} 
            />
            {info && <small className="form-text text-muted">{info}</small>}
            {error && (<div className="invalid-feedback">{error}</div>)}
        </div>
    );
};

TextFieldGroup.propTypes = {
    id: PropTypes.string,
    name: PropTypes.string,
    placeholder: PropTypes.string,
    value: PropTypes.string.isRequired,
    info: PropTypes.string,
    error: PropTypes.string,
    type: PropTypes.string.isRequired,
    onChange: PropTypes.func.isRequired,
    disabled: PropTypes.string
} 

TextFieldGroup.defaultProps = {
    type: 'text',
}
    
export default TextFieldGroup;