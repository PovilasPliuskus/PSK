import { Button, ButtonGroup, Form } from "react-bootstrap";
import { TaskDetailed } from "../../../Models/TaskDetailed";
import { estimateMapper, priorityMapper, typeMapper } from "../../utility/enumMapper";
import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { UpdateTaskBody } from "../../../Models/RequestBodies/UpdateTaskBody";
import { axiosInstance } from "../../../utils/axiosInstance";
import ScriptResources from "../../../assets/resources/strings";

type taskInfoSectionProps = {
    taskDetailed: TaskDetailed,
    setTaskDetailed: Dispatch<SetStateAction<TaskDetailed>>
    workspaceId: string
    fetchDetailedTask: () => void
}

// TODO html datetime-local format skiriasi nuo dotnet DateTime. Kai turesim endpointus, reiks sutvarkyt.

const TaskInfoSection: React.FC<taskInfoSectionProps> = ({ taskDetailed, setTaskDetailed, workspaceId, fetchDetailedTask }) => {
    const [taskDetailedEdited, setTaskDetailedEdited] = useState<TaskDetailed>(taskDetailed);
    const [isEdited, setIsEdited] = useState<boolean>(false);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setIsEdited(true);
        if (taskDetailedEdited != null) {
            setTaskDetailedEdited((prevDetails) => ({
                ...prevDetails,
                [name]: name === "status" || name === "estimate" || name === "type" || name === "priority"
                    ? parseInt(value, 10)  // Ensure parsing for integer fields
                    : name === "dueDate"
                        ? new Date(value) // Parse dueDate field as a Date object
                        : value,
            }));
        }
    };

    useEffect(() => {
        setTaskDetailedEdited(taskDetailed);
    }, [taskDetailed]);

    const handleTaskInfoChange = () => {
        const updateTaskBody: UpdateTaskBody = {
            name: taskDetailedEdited.name,
            id: taskDetailedEdited.id,
            workspaceId: workspaceId,
            createdByUserEmail: taskDetailedEdited.createdByUserEmail,
            assignedToUserEmail: taskDetailedEdited.assignedToUserEmail,
            dueDate: taskDetailedEdited.dueDate ? new Date(taskDetailedEdited.dueDate) : null,
            description: taskDetailedEdited.description,
            status: taskDetailedEdited.status,
            estimate: taskDetailedEdited.estimate,
            type: taskDetailedEdited.type,
            priority: taskDetailedEdited.priority,
            version: taskDetailedEdited.version,
            force: false,
        }

        axiosInstance.put(`/task`, updateTaskBody)
            .then(response => {
                // TODO: Kazka cia reikia daryti
                fetchDetailedTask();
            })
            .catch(error => {
                if (error.status === 409) {
                    const userChoice = window.confirm(ScriptResources.OptimisticLockingUserChoice);
                    if (userChoice) {
                        updateTaskBody.force = true;
                        axiosInstance.put(`/subtask`, updateTaskBody)
                            .catch(error => {
                                console.error("Error forcing subtask update:", error);
                            })
                    }
                    else {
                        fetchDetailedTask();
                    }
                }
                else {
                    console.error("Error updating task:", error);
                    console.log(error);
                }
            })
    }

    return (
        <Form>
            <Form.Group className="mb-3" controlId="taskName">
                <Form.Label>Name</Form.Label>
                <Form.Control
                    type="text"
                    placeholder="Enter task name"
                    name="name"
                    value={taskDetailedEdited.name}
                    onChange={handleInputChange}
                />
            </Form.Group>
            <Form.Group className="mb-3" controlId="taskDueDate">
                <Form.Label>Due Date</Form.Label>
                <Form.Control
                    type="date"
                    name="dueDate"
                    value={taskDetailedEdited.dueDate ? taskDetailedEdited.dueDate.toISOString().split("T")[0] : ""}
                    onChange={handleInputChange}
                />
            </Form.Group>
            <Form.Group className="mb-3" controlId="taskEstimate">
                <Form.Label>Estimate</Form.Label>
                <Form.Control
                    as="select"
                    name="estimate"
                    value={taskDetailedEdited.estimate}
                    onChange={handleInputChange}
                >
                    <option value={0}>{estimateMapper[0]}</option>
                    <option value={1}>{estimateMapper[1]}</option>
                    <option value={2}>{estimateMapper[2]}</option>
                </Form.Control>
            </Form.Group>
            <Form.Group className="mb-3" controlId="taskType">
                <Form.Label>Type</Form.Label>
                <Form.Control
                    as="select"
                    name="type"
                    value={taskDetailedEdited.type}
                    onChange={handleInputChange}
                >
                    <option value={0}>{typeMapper[0]}</option>
                    <option value={1}>{typeMapper[1]}</option>
                    <option value={2}>{typeMapper[2]}</option>
                </Form.Control>
            </Form.Group>
            <Form.Group className="mb-3" controlId="taskPriority">
                <Form.Label>Priority</Form.Label>
                <Form.Control
                    as="select"
                    name="priority"
                    value={taskDetailedEdited.priority}
                    onChange={handleInputChange}
                >
                    <option value={0}>{priorityMapper[0]}</option>
                    <option value={1}>{priorityMapper[1]}</option>
                    <option value={2}>{priorityMapper[2]}</option>
                </Form.Control>
            </Form.Group>
            {isEdited && (
                <div style={{ display: 'flex', justifyContent: 'center' }}>
                    <ButtonGroup size="sm">
                        <Button variant="success" onClick={() => { handleTaskInfoChange(); setIsEdited(false); setTaskDetailed(taskDetailedEdited) }} >Save</Button>
                        <Button variant="danger" onClick={() => { setIsEdited(false); setTaskDetailedEdited(taskDetailed) }}>Cancel</Button>
                    </ButtonGroup>
                </div>
            )}
        </Form>
    )
}

export default TaskInfoSection;