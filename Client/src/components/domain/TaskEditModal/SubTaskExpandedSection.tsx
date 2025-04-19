import { Dispatch, SetStateAction, useState } from "react"
import { SubTask as SubTaskType } from "../../../Models/SubTask"
import SubTask from "./SubTask"
import { Form } from "react-bootstrap"
import Comment from "./Comment";
import { BsChevronLeft } from "react-icons/bs";
type subTaskExpandedSectionProps = {
    selectedSubtask: SubTaskType,
    setSelectedSubtask: Dispatch<SetStateAction<SubTaskType | null>>,
}


const SubTaskExpandedSection: React.FC<subTaskExpandedSectionProps> = ({ selectedSubtask, setSelectedSubtask }) => {
    const [commentText, setCommentText] = useState("");
    const handleCommentChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setCommentText(e.target.value);
    }

    const handleCommentAdd = () => {
        // susisiekiam su endpointu
        console.log("ADD SUBTASK COMMENT: " + commentText);
    }


    return (
        <div className="subtask-expanded-section">
            <div style={{ display: 'flex' }}>
                <BsChevronLeft />
                <div onClick={() => { setSelectedSubtask(null) }}>Back To Task</div>
            </div>

            <div className="subtask-expanded-section-content">
                <SubTask subTask={selectedSubtask} setSelectedSubtask={setSelectedSubtask}></SubTask>
                <div className="subtask-comment-section" style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between', marginLeft: '20px' }}>
                    <div>
                        {selectedSubtask.comments.map((c) => {
                            return (
                                <Comment comment={c} key={c.id}></Comment>
                            )
                        })}
                    </div>
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