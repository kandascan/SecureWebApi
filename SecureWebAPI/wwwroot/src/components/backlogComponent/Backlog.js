import React, { Component } from 'react'
import { SortableContainer, SortableElement, arrayMove } from 'react-sortable-hoc';
import BacklogTask from './BacklogTask';

const SortableItem = SortableElement(({ value, onDeleteItem }) => <BacklogTask value={value} onDeleteItem={onDeleteItem} />);

const SortableList = SortableContainer(({ items, onDeleteItem }) => {
    return (
        <ul className="list-group container">
            {items.map((value, index) => (
                <SortableItem key={`item-${index}`} index={index} value={value} onDeleteItem={onDeleteItem} />
            ))}
        </ul>
    );
});

class Backlog extends Component {
    constructor(props) {
        super(props);
        this.state = {
            items: ['Item 1', 'Item 2', 'Item 3', 'Item 4', 'Item 5', 'Item 6'],
        };
      }  
    
    onDeleteItem(id) {
        console.log(this.state);
        console.log(`Delete: ${id}`);
        const newItems = this.state.items.filter(item => {
            return item !== id
        });
        this.setState({
            items: newItems
        });
    }

    onSortEnd({ oldIndex, newIndex }) {
        this.setState({
            items: arrayMove(this.state.items, oldIndex, newIndex),
        });
    };
    render() {
        return (
            <div className="landing landing-background-backlog">
                <div className="dark-overlay landing-inner text-light">
                    <div className="container">
                        <SortableList items={this.state.items} onSortEnd={this.onSortEnd} onDeleteItem={this.onDeleteItem} />
                    </div>
                </div>
            </div>
        )
    }
}

export default Backlog;
