import React, { Component } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

class ModaComponent extends Component {
  render() {
    return (
        <Modal isOpen={this.props.show}>
            <ModalHeader>{this.props.header}</ModalHeader>
            <ModalBody>
                {this.props.content}
            </ModalBody>
            <ModalFooter>
                <Button onClick={this.props.onCancelClick} color="danger">Cancel</Button>
                <Button onClick={this.props.onSubmitClick} color="primary">Submit</Button>
            </ModalFooter>
    </Modal>
    )
  }
}

export default ModaComponent;
