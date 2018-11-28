import React, { Component } from 'react'
import { SortableContainer, SortableElement, arrayMove } from 'react-sortable-hoc';
import ItemComponent from './ItemComponent';
import '.././App.css'

const SortableItem = SortableElement(({ index, value, onDeleteItem, onEditTask,  id }) => <ItemComponent index={index} value={value} onDeleteItem={onDeleteItem} onEditTask={onEditTask} id={id} />);
const SortableList = SortableContainer(({ items, onDeleteItem, onEditTask }) => {
    return (        
        <ul className="list-group">
            {items.map((value, index) => (
                <SortableItem key={`item-${index}`} index={index} value={value.taskname} onDeleteItem={onDeleteItem} onEditTask={onEditTask} id={value.id} />
            ))}
        </ul>
    );
});

class BacklogSortable extends Component {
    onEditTask = (id) => {
        this.props.getTaskById(id);
    }

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
            onDeleteItem={this.onDeleteItem}
            onEditTask={this.onEditTask
            } />;
    }
}

export default BacklogSortable;
