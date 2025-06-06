import { Button, Modal } from "react-bootstrap"
import { TaskDetailed } from "../../../Models/TaskDetailed"
import { Dispatch, useEffect, useState } from "react"
import keycloak from "../../../keycloak";
import { dummyTasks } from "../dummyData";
import { axiosInstance } from "../../../utils/axiosInstance";
import type { SubTask as SubTaskType } from "../../../Models/SubTask";
import './TaskEditModal.css';
import TaskInfoSection from "./TaskInfoSection";
import SubTaskListSection from "./SubTaskListSection";
import SubTaskExpandedSection from "./SubTaskExpandedSection";
import TaskCommentSection from "./TaskCommentSection";

type TaskEditModalProps= {
    editingTaskId: string,
    setShowEditModal: Dispatch<React.SetStateAction<boolean>>,
    showEditModal: boolean
}

const TaskEditModal: React.FC<TaskEditModalProps> = ({editingTaskId, setShowEditModal, showEditModal}) => {
    const [taskDetailed, setTaskDetailed] = useState<TaskDetailed | null>(null);
    const [selectedSubtask, setSelectedSubtask] = useState<SubTaskType | null>(null);
    const [error, setError] = useState(null);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [comments, setComments] = useState<Comment[]>([]);

    const fetchDetailedTask = () => {
        setError(null);
        setIsLoading(true);

        axiosInstance.get(`/task/detailed/${editingTaskId}`, {
        })
            .then(response => {
                const taskData = response.data;
                if (taskData && taskData.dueDate) {
                    taskData.dueDate = new Date(taskData.dueDate);
                }
                if (taskData && taskData.subTasks) {
                    taskData.subTasks = taskData.subTasks.map(st => {
                        
                        if (st.dueDate){
                            st.dueDate = new Date(st.dueDate);
                            return st;
                        }
                        return st;
                    })
                }
                setTaskDetailed(taskData);
                setComments(taskData.comments);
                if(selectedSubtask){
                    setSelectedSubtask(taskData.subTasks.filter(st => st.id === selectedSubtask.id)[0]);
                }
            })
            .catch(error => {
                setTaskDetailed(null);
                setError(error);
                console.log(error);
            })
            .finally(() => {
                setIsLoading(false);
            })
    }

    useEffect(() => {
        if (keycloak.authenticated) {
            fetchDetailedTask();
            setIsLoading(false);
        }
    }, [])

    return (
        <Modal show={showEditModal} onHide={() => {setShowEditModal(false)}} size="xl">
            <Modal.Header closeButton>
                <Modal.Title>Edit Task</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {taskDetailed && (
                    <div style={{ display: 'flex', flexDirection: 'column' }}>
                        <div style={{ display: 'flex', flexDirection: 'row' }}>
                            <TaskInfoSection taskDetailed={taskDetailed} setTaskDetailed={setTaskDetailed} workspaceId={taskDetailed.workspaceId} fetchDetailedTask={fetchDetailedTask}/>
                            {!selectedSubtask && (
                                <SubTaskListSection SubTasks={taskDetailed.subTasks} setSelectedSubtask={setSelectedSubtask} workspaceId={taskDetailed.workspaceId} fetchDetailedTask={fetchDetailedTask} taskId={taskDetailed.id}/>
                            )}
                            {selectedSubtask && (
                                <SubTaskExpandedSection selectedSubtask={selectedSubtask} setSelectedSubtask={setSelectedSubtask} workspaceId={taskDetailed.workspaceId} fetchDetailedTask={fetchDetailedTask}/>
                            )}
                        </div>
                        <TaskCommentSection comments={taskDetailed.comments} fetchDetailedTask={fetchDetailedTask} workspaceId={taskDetailed.workspaceId} taskId={taskDetailed.id}/>
                    </div>
                )}
                {error && (
                    <div>Error</div>
                )}
                {isLoading && (
                    <div>Loading...</div>
                )}

            </Modal.Body>
            <Modal.Footer>
            </Modal.Footer>
        </Modal>
    )
}

export default TaskEditModal;