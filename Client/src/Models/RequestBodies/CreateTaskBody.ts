import { StatusEnum } from "../TaskEnums";

export interface CreateTaskBody {
    name: string;
    workspaceId: string;
    status: StatusEnum;
    version: number;
}