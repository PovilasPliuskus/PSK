import { useDrop } from "react-dnd";
import Task from "./Task";
import { BoardProp, ITask } from "./Interfaces";
import './Board.css';

const board: React.FC<BoardProp> = ({boardType, onTaskDrop , taskList}) => {
    const [{isOver}, drop] = useDrop(() => ({
        accept: "task",
        drop: (item: { task: ITask }, monitor) => {;
            onTaskDrop(item.task, boardType);
            },
        collect: (monitor) => ({
            isOver: !!monitor.isOver(),
        }),
    }))

    return (
        <div style={{display: "flex", alignItems: "center", flexDirection: 'column'}}>
            <h2 >{boardType}</h2>
            <div 
            ref={drop} 
            className="board"
            >
                {taskList.map((task) => {
                    return (
                        <Task key={task.Id} task={task}></Task>
                    )
                })}
            </div>
        </div>
    )
}

export default board;