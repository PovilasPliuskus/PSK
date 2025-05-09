import { Dispatch, SetStateAction, useState } from "react"
import { SubTask as SubTaskType } from "../../../Models/SubTask"
import SubTask from "./SubTask"
import { Form } from "react-bootstrap"
import Comment from "./Comment";
import { BsChevronLeft } from "react-icons/bs";
import { axiosInstance } from "../../../utils/axiosInstance";
import { CreateCommentBody } from "../../../Models/RequestBodies/CreateCommentBody";

type subTaskExpandedSectionProps = {
    selectedSubtask: SubTaskType,
    setSelectedSubtask: Dispatch<SetStateAction<SubTaskType | null>>,
    workspaceId: string,
    fetchDetailedTask: () => void
}


const SubTaskExpandedSection: React.FC<subTaskExpandedSectionProps> = ({ selectedSubtask, setSelectedSubtask, workspaceId, fetchDetailedTask }) => {
    const [commentText, setCommentText] = useState("");
    const handleCommentChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setCommentText(e.target.value);
    }

    const handleCommentAdd = () => {
        const createCommentBody: CreateCommentBody = {
            text: commentText,
            subTaskId: selectedSubtask.id,
            version: 0,
        }
        axiosInstance.post(`/comment`, createCommentBody)
            .then(response => {
                // TODO: Kazka cia reikia daryti
                fetchDetailedTask();
            })
            .catch(error => {
                console.error(error);
            })
    }


    return (
        <div className="subtask-expanded-section">
            <div style={{ display: 'flex' }}>
                <BsChevronLeft />
                <div onClick={() => { setSelectedSubtask(null) }}>Back To Task</div>
            </div>

            <div className="subtask-expanded-section-content">
                <SubTask subTask={selectedSubtask} setSelectedSubtask={setSelectedSubtask} fetchDetailedTask={fetchDetailedTask}></SubTask>
                <div className="subtask-comment-section" style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between', marginLeft: '20px' }}>
                    {selectedSubtask.comments && (
                        <div>
                            {selectedSubtask.comments.map((c) => {
                                return (
                                    <Comment comment={c} key={c.id}></Comment>
                                )
                            })}
                        </div>
                    )}

                    <div style={{ display: 'flex' }}>
                        <Form>
                            <Form.Group className="mb-3">
                                <Form.Control
                                    type="text"
                                    placeholder="Enter comment"
                                    name="name"
                                    value={commentText}
                                    onChange={handleCommentChange}
                                />
                            </Form.Group>
                        </Form>
                        <button onClick={handleCommentAdd} className="btn btn-primary">Primary</button>
                    </div>
                </div>
            </div>
        </div>

    )
}

export default SubTaskExpandedSection;