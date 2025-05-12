export interface CreateCommentBody {
    text: string;
    subTaskId?: string;
    taskId?: string;
    version: number;
}