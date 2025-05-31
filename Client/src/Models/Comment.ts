import { BaseModel } from "./BaseModel";

export interface Comment extends BaseModel {
    subTaskId?: string | null;
    taskId: string;
    createdByUserEmail: string;
    edited: boolean;
    text: string;
}