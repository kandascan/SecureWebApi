import React, { Component } from 'react';
import { sortableContainer, sortableElement, arrayMove, DragLayer } from '../react-sortable-multiple-hoc';
//import '../react-sortable-multiple-hoc/SortableComponent.css';

const dragLayer = new DragLayer();

const SortableItem = sortableElement((props) => {
    return (<li className="list-group-item list-group-item-light" onClick={props.onSelect}>
            {/* {props.item.ind}  */}
            {props.item.val}
        </li>);
});

const SortableListItems = sortableContainer(({ items }) =>
    <ul className="list-group">
        {items.map((value, index) => (
          <SortableItem
              key={index}
              index={index}
              item={value}
          />
        ))}
    </ul>
);

const SortablePart = sortableElement(props =>
    <div className="col-sm">
        {/* <div><span>{props.item.name}</span></div> */}
        <SortableListItems
            {...props}
            items={props.item.items}
            dragLayer={dragLayer}
            distance={3}
            helperClass={'selected'}
            isMultiple={true}
            helperCollision={{ top: 0, bottom: 0 }}
        />
    </div>
);

const SortableListParts = sortableContainer(({ items, onSortItemsEnd }) =>
    <div className="row">
        {items.map((value, index) => (
            <SortablePart
                key={index}
                index={index}
                item={value}
                id={index}
                onMultipleSortEnd={onSortItemsEnd}
            />
        ))}
    </div>
);

const getParts = (countParts, countLessons) => {
    const parts = [];

    for (let i = 0; i < countParts; i++) {
        const lessons = [];

        for (let j = 0; j < countLessons; j++) {
            lessons.push('Lesson-' + (i + 1) + '-' + (j + 1));
        }
        parts.push({
            name: 'Part',
            items: lessons,
        });
    }

    return parts;
};

export default class SortableComponent extends Component {
    constructor(props) {
        super(props);
        this.state = {
            parts: getParts(3, 5),
        };
    }
    onSortEnd = ({ oldIndex, newIndex }) => {
        this.setState({
            parts: arrayMove(this.state.parts, oldIndex, newIndex),
        });
    }
    onSortItemsEnd = ({ newListIndex, newIndex, items }) => {
        const parts = this.state.parts.slice();
        const itemsValue = [];

        items.forEach(item => {
            itemsValue.push(parts[item.listId].items[item.id]);
        });
        for (let i = items.length - 1; i >= 0; i--) {
            const item = items[i];

            parts[item.listId].items.splice(item.id, 1);
        }
        parts[newListIndex].items.splice(newIndex, 0, ...itemsValue);
        this.setState({
            parts: parts,
        });
    }
    render() {
        const parts = this.state.parts.map((value, index) => {
            return {
                name: value.name,
                items: value.items.map((val, ind) => {
                    return {
                        val,
                        ind: (index + 1) + '.' + (ind + 1),
                    };
                }),
            };
        });

        return (<div className="container">
        <SortableListParts
            items={parts}
            onSortEnd={this.onSortEnd}
            onSortItemsEnd={this.onSortItemsEnd}
            helperClass={'selected'}/>
    </div>);
    }
}