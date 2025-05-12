import { Card, Col, Container, Form, Row } from "react-bootstrap";
import type { Comment } from "../../../Models/Comment";

type CommentProps = {
    comment: Comment
}

const Comment: React.FC<CommentProps> = ({ comment }) => {
    return (
        <Card>
            <Card.Title className="text-muted" style={{fontSize: '12px'}}>
                {comment.writtenByUserEmail}
            </Card.Title>
            <Card.Subtitle className="text-muted" style={{fontSize: '10px'}}>
                {/* {comment.createdAt} */}
            </Card.Subtitle>
            <Card.Text style={{fontSize: '15px'}}>
                {comment.text}
            </Card.Text>
        </Card>
    )
}

export default Comment;