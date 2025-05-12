import { BaseModel } from "./BaseModel";

export interface Comment extends BaseModel {
    subTaskId?: string | null;
    taskId: string;
    writtenByUserEmail: string;
    edited: boolean;
    text: string;
}