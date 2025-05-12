import { Button, ButtonGroup, Form } from "react-bootstrap";
import type { SubTask } from "../../../Models/SubTask";
import { useState, Dispatch, version, useEffect } from "react";
import { estimateMapper, priorityMapper, statusMapper, typeMapper } from "../../utility/enumMapper";
import { axiosInstance } from "../../../utils/axiosInstance";
import ScriptResources from "../../../assets/resources/strings";
import { UpdateTaskBody } from "../../../Models/RequestBodies/UpdateTaskBody";
import { UpdateSubTaskBody } from "../../../Models/RequestBodies/UpdateSubTaskBody";

type SubTaskProps = {
    subTask: SubTask,
    setSelectedSubtask: Dispatch<React.SetStateAction<SubTask | null>>
    fetchDetailedTask: () => void
}

const Comment: React.FC<SubTaskProps> = ({ subTask, fetchDetailedTask }) => {
    const [subtaskEdited, setSubtaskEdited] = useState<SubTask>(subTask);
    const [isEdited, setIsEdited] = useState<boolean>(false);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setIsEdited(true);

        if (subtaskEdited != null) {
            setSubtaskEdited((prevDetails) => ({
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
        setSubtaskEdited(subTask);
    }, [subTask]);
    
    const handleSubTaskInfoChange = () => {
        const updateSubTaskBody: UpdateSubTaskBody = {
            name: subtaskEdited.name,
            id: subtaskEdited.id,
            taskId: subtaskEdited.taskId,
            createdByUserEmail: subtaskEdited.createdByUserEmail,
            assignedToUserEmail: subtaskEdited.assignedToUserEmail,
            dueDate: subtaskEdited.dueDate ? new Date(subtaskEdited.dueDate) : null,
            description: subtaskEdited.description,
            status: subtaskEdited.status,
            estimate: subtaskEdited.estimate,
            type: subtaskEdited.type,
            priority: subtaskEdited.priority,
            version: subtaskEdited.version,
            force: false,
        }

        axiosInstance.put(`/subtask`, updateSubTaskBody)
            .then(response => {
                // TODO: Kazka cia reikia daryti
                fetchDetailedTask();
            })
            .catch(error => {
                if (error.status === 409) {
                    const userChoice = window.confirm(ScriptResources.OptimisticLockingUserChoice);
                    if (userChoice) {
                        updateSubTaskBody.force = true;
                        axiosInstance.put(`/subtask`, updateSubTaskBody)
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
        <div className="subtask">
            <Form.Control
                type="text"
                placeholder="Name"
                name="name"
                value={subtaskEdited.name}
                onChange={handleInputChange}
            />
            <div style={{ display: 'flex', justifyContent: "space-between" }}>
                <Form.Control
                    style={{ margin: '5px' }}
                    size="sm"
                    as="select"
                    name="status"
                    value={subtaskEdited.status}
                    onChange={handleInputChange}
                >
                    <option value={0}>{statusMapper[0]}</option>
                    <option value={1}>{statusMapper[1]}</option>
                    <option value={2}>{statusMapper[2]}</option>
                </Form.Control>
                <Form.Control
                    style={{ margin: '5px' }}
                    size="sm"
                    as="select"
                    name="estimate"
                    value={subtaskEdited.estimate}
                    onChange={handleInputChange}
                >
                    <option value={0}>{estimateMapper[0]}</option>
                    <option value={1}>{estimateMapper[1]}</option>
                    <option value={2}>{estimateMapper[2]}</option>
                </Form.Control>
                <Form.Control
                    style={{ margin: '5px' }}
                    size="sm"
                    as="select"
                    name="type"
                    value={subtaskEdited.type}
                    onChange={handleInputChange}
                >
                    <option value={0}>{typeMapper[0]}</option>
                    <option value={1}>{typeMapper[1]}</option>
                    <option value={2}>{typeMapper[2]}</option>
                </Form.Control>
                <Form.Control
                    style={{ margin: '5px' }}
                    size="sm"
                    as="select"
                    name="priority"
                    value={subtaskEdited.priority}
                    onChange={handleInputChange}
                >
                    <option value={0}>{priorityMapper[0]}</option>
                    <option value={1}>{priorityMapper[1]}</option>
                    <option value={2}>{priorityMapper[2]}</option>
                </Form.Control>
            </div>

            <Form.Control
                as="textarea"
                rows={3}
                type="text"
                placeholder="Description"
                name="description"
                value={subtaskEdited.description}
                onChange={handleInputChange}
            />
            <Form.Group className="mb-3" controlId="subtaskDueDate">
                <Form.Label>Due Date</Form.Label>
                <Form.Control
                    type="date"
                    name="dueDate"
                    value={subtaskEdited.dueDate ? subtaskEdited.dueDate.toISOString().split("T")[0] : ""}
                    onChange={handleInputChange}
                />
            </Form.Group>
            <Form.Group className="mb-3" controlId="subtaskAssignedToUserEmail">
                <Form.Label>Assigned to</Form.Label>
                <Form.Control
                    as="select"
                    name="assignedToUserEmail"
                    value={subtaskEdited.assignedToUserEmail}
                    onChange={handleInputChange}
                />
            </Form.Group>
            {isEdited && (
                <div style={{ display: 'flex', justifyContent: 'center' }}>
                    <ButtonGroup size="sm">
                        <Button variant="success" onClick={() => { handleSubTaskInfoChange(); setIsEdited(false) }} >Save</Button>
                        <Button variant="danger" onClick={() => { setIsEdited(false); setSubtaskEdited(subTask) }}>Cancel</Button>
                    </ButtonGroup>
                </div>
            )}
        </div>
    )
}

export default Comment;