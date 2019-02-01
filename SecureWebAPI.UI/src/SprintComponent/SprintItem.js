import React from 'react'

export default function SprintItem({ task }) {
    return (
        <div className="col-sm ">
            <div className="alert alert-primary" role="alert">
                {task.columnName}:
                        </div>
        </div>
    )
}
