import { Button, ButtonGroup, Form } from "react-bootstrap";
import { TaskDetailed } from "../../../Models/TaskDetailed";
import { estimateMapper, priorityMapper, typeMapper } from "../../utility/enumMapper";
import { Dispatch, SetStateAction, useState } from "react";

type taskInfoSectionProps = {
    taskDetailed: TaskDetailed,
    setTaskDetailed: Dispatch<SetStateAction<TaskDetailed>>
}

// TODO html datetime-local format skiriasi nuo dotnet DateTime. Kai turesim endpointus, reiks sutvarkyt.

const TaskInfoSection: React.FC<taskInfoSectionProps> = ({ taskDetailed, setTaskDetailed }) => {
    const [taskDetailedEdited, setTaskDetailedEdited] = useState<TaskDetailed>(taskDetailed);
    const [isEdited, setIsEdited] = useState<boolean>(false);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setIsEdited(true);
        if (taskDetailedEdited != null) {
            setTaskDetailedEdited((prevDetails) => ({
                ...prevDetails,
                [name]: name === "status" || name === "estimate" || name === "type" || name === "priority"
                    ? parseInt(value, 10)
                    : value,
            }));
        }
    };

    const handleTaskInfoChange = () => {
        // susisiekiam su endpointu
        console.log("TASK INFO CHANGE: ");
        console.log(taskDetailedEdited);
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
                    value={taskDetailedEdited.dueDate}
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