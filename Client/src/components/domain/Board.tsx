import { useDrop } from "react-dnd";
import Task from "./Task";
import { BoardProp, ITask } from "./Interfaces";

const board: React.FC<BoardProp> = ({boardType, onTaskDrop , taskList}) => {
    const [{isOver}, drop] = useDrop(() => ({
        accept: "task",
        drop: (item: { task: ITask }, monitor) => {
            onTaskDrop(item, boardType);
            },
        collect: (monitor) => ({
            isOver: !!monitor.isOver(),
        }),
    }))

    return (
        <div>
            <div 
            ref={drop} 
            className='Board' 
            style={{border: "5px solid black", 
            height: "500px", width: "500px"}}
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