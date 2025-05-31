export interface CreateCommentBody {
    commentText: string;
    subTaskId?: string;
    taskId?: string;
    version: number;
}