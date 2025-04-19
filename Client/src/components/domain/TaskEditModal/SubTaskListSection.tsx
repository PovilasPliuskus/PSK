import { Dispatch, useState } from "react"
import { SubTask } from "../../../Models/SubTask"
import { Form, Card as BootstrapCard } from "react-bootstrap"

type SubTaskListSectionProps = {
    SubTasks: SubTask[],
    setSelectedSubtask: Dispatch<React.SetStateAction<SubTask | null>>
}

const SubTaskListSection: React.FC<SubTaskListSectionProps> = ({ SubTasks, setSelectedSubtask }) => {
    const [newSubTaskName, setNewSubTaskName] = useState<string>("New SubTask")

    const handleNewSubTaskNameChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setNewSubTaskName(e.target.value);
    }

    const handleAddNewSubtask = () => {
        // susisiekiam su endpointu
        console.log("ADD SUBTASK: " + newSubTaskName);
    }

    return (
        <div className="subtask-list-section">
            <div style={{ display: 'flex', flexDirection: 'column' }}>
                {SubTasks.map((s) => {
                    return (
                        <BootstrapCard
                            style={{ width: '100%' }}
                            onClick={() => { setSelectedSubtask(s) }}
                            key={s.id}
                        >
                            <BootstrapCard.Title>{s.name}</BootstrapCard.Title>
                            <BootstrapCard.Subtitle className="mb-2 text-muted">Assigned: {s.createdByUserEmail}</BootstrapCard.Subtitle>
                        </BootstrapCard>
                    )
                })}
            </div>
            <div style={{ display: 'flex' }}>
                <BootstrapCard
                    style={{ width: '100%' }}>
                    <Form.Control
                        type="text"
                        placeholder="Enter task name"
                        name="name"
                        value={newSubTaskName}
                        onChange={handleNewSubTaskNameChange}
                    />
                    <BootstrapCard.Title onClick={handleAddNewSubtask} style={{ display: 'flex', justifyContent: 'center' }}>+</BootstrapCard.Title>
                </BootstrapCard>
            </div>
        </div>
    )
}

export default SubTaskListSection;