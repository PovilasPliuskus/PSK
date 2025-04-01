import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';
import { useState } from 'react';
import Board from './Board';
import { ITask } from './Interfaces';

const TaskBoardViewPage = () => {
    return (
        <DndProvider backend={HTML5Backend}>
            <TaskBoardView></TaskBoardView>
        </DndProvider>
    )
}


const taskList = [
    {
        Id: 1,
        CreatedAt: "03/30/2020 14:45:00",
        UpdatedAt: "03/30/2020 14:45:00",
        Name: "task1",
        CreatedByUserId: 1,
        WorkspaceId: 1,
        AssignedToUserId: 1,
        DueDate: "03/30/2025 14:45:00",
        Description: "task 1 description",
        Status: "todo",
        Estimate: "idk",
        Type: "idk",
        Priority: "Medium"
    },
    {
        Id: 2,
        CreatedAt: "03/30/2020 14:45:00",
        UpdatedAt: "03/30/2020 14:45:00",
        Name: "task2",
        CreatedByUserId: 2,
        WorkspaceId: 1,
        AssignedToUserId: 2,
        DueDate: "03/20/2026 4:45:00",
        Description: "task 2 description",
        Status: "inprogress",
        Estimate: "idk",
        Type: "idk",
        Priority: "Medium"
    }
]

const TaskBoardView = () => {
    const [tasks, setTasks] = useState<ITask[]>(taskList);

    const handleTaskDrop  = (newTask : ITask, boardType : string) => {
        setTasks(prevTasks => 
            prevTasks.map(task => 
                task.Id === newTask.Id ? {...task, Status: boardType} : task
            )
        );
    }

    return (
        <div style={{ display: "flex" }}>
            <Board
                boardType={"todo"}
                onTaskDrop={handleTaskDrop}
                taskList={tasks.filter((task) => task.Status === "todo" ? true : false)}></Board>
            <Board
                boardType={"inprogress"}
                onTaskDrop={handleTaskDrop}
                taskList={tasks.filter((task) => task.Status === "inprogress" ? true : false)}></Board>
            <Board
                boardType={"done"}
                onTaskDrop={handleTaskDrop}
                taskList={tasks.filter((task) => task.Status === "done" ? true : false)}></Board>

        </div>

    )
}

export default TaskBoardViewPage;