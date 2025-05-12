import { StatusEnum } from "../TaskEnums";

export interface CreateSubTaskBody {
    name: string;
    taskId: string;
    workspaceId: string;
    version: number;
}