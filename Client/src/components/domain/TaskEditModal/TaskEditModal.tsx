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

    useEffect(() => {
        const fetchDetailedTask = () => {
            setError(null);
            setIsLoading(true);
            // Reikia naujo endpoint, kad gautume pilna task objekta pagal id (Dabartinis endpoint yra skirtas task summary sarasa gauti)
            axiosInstance.get(`/task/detailed/${editingTaskId}`, {
            })
                .then(response => {
                    setTaskDetailed(response.data);
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
        if (keycloak.authenticated) {
            //fetchDetailedTask();
            // TODO kolkas naudojam dummy data
            setTaskDetailed(dummyTasks[0]);
            setIsLoading(false);
            console.log("FETCH ENDPOINT");
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
                            <TaskInfoSection taskDetailed={taskDetailed} setTaskDetailed={setTaskDetailed}/>

                            {!selectedSubtask && (
                                <SubTaskListSection SubTasks={taskDetailed.SubTasks} setSelectedSubtask={setSelectedSubtask}/>
                            )}


                            {selectedSubtask && (
                                <SubTaskExpandedSection selectedSubtask={selectedSubtask} setSelectedSubtask={setSelectedSubtask}/>
                            )}
                        </div>
                        
                        <TaskCommentSection comments={taskDetailed.Comments}/>
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