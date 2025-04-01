import { useDrag } from "react-dnd";
import { TaskProp } from "./Interfaces";

const Task = ({task}: {task: TaskProp}) => {
    const [{isDragging}, drag] = useDrag(() => ({
        type: "task",
        item: {id: task.Id},
        collect: (monitor) => ({
            isDragging: !!monitor.isDragging(),
        }),
    }));
    return (
        <div 
        ref={drag}
        style={{border: isDragging ? "5px solid pink" : "0px"}} 
        >{task.Name}</div>
    )
}

export default Task;