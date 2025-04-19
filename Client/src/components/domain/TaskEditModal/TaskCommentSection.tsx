import { Form } from "react-bootstrap";
import { Comment as CommentType } from "../../../Models/Comment"
import Comment from "./Comment";
import { useState } from "react";

type taskCommentSection = {
    comments: CommentType[]
}

const TaskCommentSection: React.FC<taskCommentSection> = ({ comments }) => {
    const [commentText, setCommentText] = useState("");
    const handleCommentChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setCommentText(e.target.value);
    }

    const handleCommentAdd = () => {
        // susisiekiam su endpointu
        console.log("ADD TASK COMMENT: " + commentText);
    }

    return (
        <div className="comment-section" style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between', marginLeft: '20px' }}>
            <div>
                {comments.map((c) => {
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
    )
}

export default TaskCommentSection;