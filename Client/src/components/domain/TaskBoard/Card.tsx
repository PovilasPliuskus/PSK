import { useState } from "react";
import { TaskSummary } from "../../../Models/TaskSummary";
import { motion } from "framer-motion";
import { BsHammer } from "react-icons/bs";
import DropIndicator from "./DropIndicator";
import './TaskBoardViewPage.css';
import TaskEditModal from "../TaskEditModal/TaskEditModal";

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
    const [taskSummary, setTaskSummary] = useState<TaskSummary>(task);
    const [showEditModal, setShowEditModal] = useState<boolean>(false);


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
                    <span onClick={() => { setShowEditModal(true) }} className="edit-button-wrapper">
                        <BsHammer style={{ pointerEvents: 'none' }} />
                    </span>
                )}
            </motion.div>

            {showEditModal && (
                <TaskEditModal editingTaskId={taskSummary.id} setShowEditModal={setShowEditModal} showEditModal={showEditModal} />
            )}

        </>
    );
};

export default Card;