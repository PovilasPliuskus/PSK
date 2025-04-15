import { useState } from "react";
import { TaskSummary } from "../../../Models/TaskSummary";
import { motion } from "framer-motion";
import { Modal, Button, Form } from "react-bootstrap";
import { BsHammer } from "react-icons/bs";
import { axiosInstance } from "../../../utils/axiosInstance";
import { estimateMapper, typeMapper, priorityMapper } from "../../utility/enumMapper";
import { TaskDetailed } from "../../../Models/TaskDetailed";
import DropIndicator from "./DropIndicator";
import './TaskBoardViewPage.css';
import keycloak from "../../../keycloak";
type CardProps = {
    task: TaskSummary
    handleDragStart: Function;
};

const Card: React.FC<CardProps> = ({
    task,
    handleDragStart
}) => {
    const [isHoveredOver, setIsHoveredOver] = useState<boolean>(false);
    const [show, setShow] = useState(false);
    const [taskDetailed, setTaskDetailed] = useState<TaskDetailed | null>(null);
    const [taskSummary, setTaskSummary] = useState<TaskSummary>(task);

    const [error, setError] = useState(null);
    const [isLoading, setIsLoading] = useState<boolean>(true);


    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const handleEditClick = () => {
        handleShow();
        const fetchDetailedTask = () => {
            setError(null);
            setIsLoading(true);
            // Reikia naujo endpoint, kad gautume pilna task objekta pagal id (Dabartinis endpoint yra skirtas task summary sarasa gauti)
            axiosInstance.get(`/task/detailed/${taskSummary.id}`, {
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
            fetchDetailedTask();
        }
    }

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;

        if(taskDetailed != null){
            setTaskDetailed((prevDetails) => ({
            ...prevDetails,
            [name]: name === "status" || name === "estimate" || name === "type" || name === "priority"
                ? parseInt(value, 10)
                : value,
        }));
        }
    };

    const handleSave = () => {
        console.log("Saving changes:", taskDetailed);

        const cardPatchRequest = (taskDetailed : TaskDetailed) => {
            axiosInstance.patch(`/task/${taskDetailed.id}`, taskDetailed)
                .then(response => {
                    console.log(response);
                    const returnedCard = response.data;
                    // TODO update cards once response is received.
                })
                .catch(error => {
                    console.log(error);
                })
        }
        if(taskDetailed != null){
            cardPatchRequest(taskDetailed);
        }
        setShow(false);
    };

    return (
        <>
            <DropIndicator beforeId={taskSummary.id} column={taskSummary.status} />
            <motion.div
                layout
                layoutId={taskSummary.id}
                draggable="true"
                onDragStart={(e) => handleDragStart(e, taskSummary)}
                onHoverStart={() => setIsHoveredOver(true)}
                onHoverEnd={() => setIsHoveredOver(false)}
                className="card"
            >
                <p className="card-title">{taskSummary.name}</p>
                {isHoveredOver && (
                    <span onClick={handleEditClick} className="edit-button-wrapper">
                        <BsHammer style={{ pointerEvents: 'none' }} />
                    </span>
                )}
            </motion.div>

            
                <Modal show={show} onHide={handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>Edit Task</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        {taskDetailed && (
                            <Form>
                            <Form.Group className="mb-3" controlId="taskName">
                                <Form.Label>Name</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Enter task name"
                                    name="name"
                                    value={taskDetailed.name}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group className="mb-3" controlId="taskDueDate">
                                <Form.Label>Due Date</Form.Label>
                                <Form.Control
                                    type="date"
                                    name="dueDate"
                                    value={taskDetailed.dueDate}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group className="mb-3" controlId="taskEstimate">
                                <Form.Label>Estimate</Form.Label>
                                <Form.Control
                                    as="select"
                                    name="estimate"
                                    value={taskDetailed.estimate}
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
                                    value={taskDetailed.type}
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
                                    value={taskDetailed.priority}
                                    onChange={handleInputChange}
                                >
                                    <option value={0}>{priorityMapper[0]}</option>
                                    <option value={1}>{priorityMapper[1]}</option>
                                    <option value={2}>{priorityMapper[2]}</option>
                                </Form.Control>
                            </Form.Group>

                        </Form>
                        )}
                        {error && (
                            <div>Error</div>
                        )}
                        {isLoading && (
                            <div>Loading...</div>
                        )}
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleClose}>
                            Close
                        </Button>
                        <Button variant="primary" onClick={handleSave}>
                            Save changes
                        </Button>
                    </Modal.Footer>
                </Modal>
        </>
    );
};

export default Card;