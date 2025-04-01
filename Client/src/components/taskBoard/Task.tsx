import { useDrag } from "react-dnd";
import { TaskProp } from "./Interfaces";
import './Task.css'
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import PersonIcon from '@mui/icons-material/Person';
import WorkHistoryIcon from '@mui/icons-material/WorkHistory';
import ArticleIcon from '@mui/icons-material/Article';

const Task = ({task}: {task: TaskProp}) => {
    const [{isDragging}, drag] = useDrag(() => ({
        type: "task",
        item: {task: task},
        collect: (monitor) => ({
            isDragging: !!monitor.isDragging(),
        }),
    }));


    return (
        <div 
        ref={drag}
        className= { isDragging ? "static-task dragging-task" : "static-task"}
        >
            <div className="top-container">
                <div className="task-name">{task.Name}</div>
                {task.Priority == "Low" && (
                    <div className="task-priority-low">{task.Priority} Priority</div>
                )}
                {task.Priority == "Medium" && (
                    <div className="task-priority-medium">{task.Priority} Priority</div>
                )}
                {task.Priority == "High" && (
                    <div className="task-priority-high">{task.Priority} Priority</div>
                )}
            </div>
            
            <div className="middle-container">
                <PersonIcon></PersonIcon>
                <div className="task-assignedtouserid">{task.AssignedToUserId}</div>
            </div>
            <div className="middle-container">
                <WorkHistoryIcon></WorkHistoryIcon>
                <div className="task-estimate">{task.Estimate}</div>
            </div>
            <div className="middle-container">
                <ArticleIcon></ArticleIcon>
                <div className="task-type">{task.Type}</div>
            </div>
            <div className="bottom-container">
                <AccessTimeIcon></AccessTimeIcon>
                <div className="task-duedate">Due: {task.DueDate}</div>
            </div>
        </div>
    )
}

export default Task;