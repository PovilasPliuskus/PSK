import { Card, Col, Container, Form, Row } from "react-bootstrap";
import type { Comment } from "../../../Models/Comment";

type CommentProps = {
    comment: Comment
}

const formatDate = (isoDate: string) => {
    const date = new Date(isoDate);
    const yyyy = date.getFullYear();
    const mm = String(date.getMonth() + 1).padStart(2, '0');
    const dd = String(date.getDate()).padStart(2, '0');
    const hh = String(date.getHours()).padStart(2, '0');
    const min = String(date.getMinutes()).padStart(2, '0');
    return `${yyyy}-${mm}-${dd} ${hh}:${min}`;
};

const Comment: React.FC<CommentProps> = ({ comment }) => {
    return (
        <Card>
            <Card.Title className="text-muted" style={{fontSize: '12px'}}>
                {comment.createdByUserEmail}
            </Card.Title>
            <Card.Subtitle className="text-muted" style={{fontSize: '10px'}}>
                {formatDate(comment.createdAt)}
            </Card.Subtitle>
            <Card.Text style={{fontSize: '15px'}}>
                {comment.text}
            </Card.Text>
        </Card>
    )
}

export default Comment;