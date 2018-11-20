import React, { Component } from 'react'
import { SortableContainer, SortableElement, arrayMove } from 'react-sortable-hoc';
import ItemComponent from './ItemComponent';
import '.././App.css'

const SortableItem = SortableElement(({ index, value, onDeleteItem, id }) => <ItemComponent index={index} value={value} onDeleteItem={onDeleteItem} id={id} />);
const SortableList = SortableContainer(({ items, onDeleteItem }) => {
    //console.log('render1')

    let backlogItems;
    if(items != null){
        backlogItems = items.map((value, index) => (
            <SortableItem key={`item-${index}`} index={index} value={value.name} onDeleteItem={onDeleteItem} id={value.id} />
        ));
    }
    
    return (
        <div className="landing landing-background-backlog">
            <div className="dark-overlay landing-inner text-light">
            <h1 style={{textAlign: 'center'}}>Main Backlog</h1>
                <div className="container">
                    <ul className="list-group">
                        {backlogItems}
                    </ul>
                </div>
            </div>
        </div>
    );
});

class BacklogSortable extends Component {
    state = {
        tasks: []
    };

    onDeleteItem = (id) => {
        this.props.removeBacklogTask(id);
    }

    onSortEnd = ({ oldIndex, newIndex }) => {
        const { items } = this.props;
        const sortedItems = arrayMove(items.tasks, oldIndex, newIndex);
        this.props.sortBacklogItems(sortedItems);
    };

    render() {
        const { items, loading } = this.props;
        if(loading || items == null){
            return <h2>Loading...</h2>    
        }
        else {
            return <SortableList 
            items={items.tasks} 
            onSortEnd={this.onSortEnd} 
            onDeleteItem={this.onDeleteItem} />;
        }        
    }
}

export default BacklogSortable;
