import React, { Component } from 'react'
import { SortableContainer, SortableElement, arrayMove } from 'react-sortable-hoc';
import ItemComponent from './ItemComponent';
import '.././App.css'

const SortableItem = SortableElement(({ index, value, onDeleteItem, id }) => <ItemComponent index={index} value={value} onDeleteItem={onDeleteItem} id={id} />);
const SortableList = SortableContainer(({ items, onDeleteItem }) => {
    return (

        <ul className="list-group">
            {items.map((value, index) => (
                <SortableItem key={`item-${index}`} index={index} value={value.taskname} onDeleteItem={onDeleteItem} id={value.id} />
            ))}
        </ul>

    );
});

class BacklogSortable extends Component {
    onDeleteItem = (id) => {
        this.props.removeBacklogTask(id);
    }

    onSortEnd = ({ oldIndex, newIndex }) => {
        const { items } = this.props;
        const sortedItems = arrayMove(items.tasks, oldIndex, newIndex);
        this.props.sortBacklogItems(sortedItems);
    };

    render() {
        const { items } = this.props;
        return <SortableList
            items={items.tasks}
            onSortEnd={this.onSortEnd}
            onDeleteItem={this.onDeleteItem} />;

    }
}

export default BacklogSortable;
