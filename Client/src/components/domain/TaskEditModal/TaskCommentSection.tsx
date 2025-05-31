import { Form } from "react-bootstrap";
import { Comment as CommentType } from "../../../Models/Comment"
import Comment from "./Comment";
import { useState } from "react";
import { CreateCommentBody } from "../../../Models/RequestBodies/CreateCommentBody";
import { axiosInstance } from "../../../utils/axiosInstance";

type taskCommentSection = {
    comments: CommentType[]
    fetchDetailedTask: () => void
    wokspaceId: string
    taskId: string
}

const TaskCommentSection: React.FC<taskCommentSection> = ({ comments, fetchDetailedTask, wokspaceId, taskId }) => {
    const [commentText, setCommentText] = useState("");
    const handleCommentChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setCommentText(e.target.value);
    }

    const handleCommentAdd = () => {
        const createCommentBody : CreateCommentBody = {
            commentText: commentText,
            taskId: taskId,
            version: 0,
        }
        axiosInstance.post(`/comment`, createCommentBody)
        .then(response => {
            fetchDetailedTask();
            setCommentText("");
        })
        .catch(error => {
            console.error(error);
        })
    }

    return (
        <div className="comment-section" style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between', marginLeft: '20px' }}>
            <div>
                
                {comments && comments.map((c) => {
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
                <button onClick={handleCommentAdd} className="btn btn-primary">Comment</button>
            </div>
        </div>
    )
}

export default TaskCommentSection;