import { Button, ButtonGroup, Form } from "react-bootstrap";
import type { SubTask } from "../../../Models/SubTask";
import { useState, Dispatch } from "react";
import { estimateMapper, priorityMapper, statusMapper, typeMapper } from "../../utility/enumMapper";

type SubTaskProps = {
    subTask: SubTask,
    setSelectedSubtask: Dispatch<React.SetStateAction<SubTask | null>>
}

// TODO html datetime-local format skiriasi nuo dotnet DateTime. Kai turesim endpointus, reiks sutvarkyt.

const Comment: React.FC<SubTaskProps> = ({ subTask }) => {
    const [subtask, setSubtask] = useState<SubTask>(subTask);
    const [subtaskEdited, setSubtaskEdited] = useState<SubTask>(subTask);
    const [isEdited, setIsEdited] = useState<boolean>(false);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setIsEdited(true);

        if (subtaskEdited != null) {
            setSubtaskEdited((prevDetails) => ({
                ...prevDetails,
                [name]: name === "status" || name === "estimate" || name === "type" || name === "priority"
                    ? parseInt(value, 10)
                    : value,
            }));
        }
    };

    const handleSubTaskInfoChange = () => {
        // susisiekiam su endpointu
        console.log("EDITED SUBTASK: ");
        console.log(subtaskEdited);
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
                    type="datetime-local"
                    name="dueDate"
                    value={subtaskEdited.dueDate}
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
                        <Button variant="success" onClick={() => { handleSubTaskInfoChange(); setIsEdited(false); setSubtask(subtaskEdited) }} >Save</Button>
                        <Button variant="danger" onClick={() => { setIsEdited(false); setSubtaskEdited(subtask) }}>Cancel</Button>
                    </ButtonGroup>
                </div>
            )}
        </div>
    )
}

export default Comment;