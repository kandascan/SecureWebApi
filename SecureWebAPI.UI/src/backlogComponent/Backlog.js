import React, { Component } from 'react'
import { SortableContainer, SortableElement, arrayMove } from 'react-sortable-hoc';
import ItemComponent from './ItemComponent';
import '.././App.css'
const SortableItem = SortableElement(({ index, value, onDeleteItem }) => <ItemComponent index={index} value={value} onDeleteItem={onDeleteItem} />);

const SortableList = SortableContainer(({ items, onDeleteItem }) => {
    return (
        <div className="landing landing-background-backlog">
            <div className="dark-overlay landing-inner text-light">
                <div className="container">
                    <ul className="list-group">
                        {items.map((value, index) => (
                            <SortableItem key={`item-${index}`} index={index} value={value} onDeleteItem={onDeleteItem} />
                        ))}
                    </ul>
                </div>
            </div>
        </div>
    );
});
class BacklogComponent extends Component {
    state = {
        items: ['Item 1', 'Item 2', 'Item 3', 'Item 4', 'Item 5', 'Item 6'],
    };

    onDeleteItem = (id) => {
        console.log(`Delete: ${id}`);
        const newItems = this.state.items.filter(item => {
            return item !== id
        });
        this.setState({
            items: newItems
        });
    }

    onSortEnd = ({ oldIndex, newIndex }) => {
        this.setState({
            items: arrayMove(this.state.items, oldIndex, newIndex),
        });
    };

    render() {
        return <SortableList items={this.state.items} onSortEnd={this.onSortEnd} onDeleteItem={this.onDeleteItem} />;
    }

}

export default BacklogComponent;