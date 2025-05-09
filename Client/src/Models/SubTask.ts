import { EstimateEnum, PriorityEnum, StatusEnum, TypeEnum } from "./TaskEnums";
import { Comment } from "./Comment";
import { BaseModel } from "./BaseModel";

export interface SubTask extends BaseModel{
    name: string;
    taskId: string;
    createdByUserEmail: string;
    assignedToUserEmail?: string;
    dueDate?: Date | null;
    description?: string;
    status: StatusEnum;
    estimate: EstimateEnum;
    type: TypeEnum;
    priority: PriorityEnum;
    comments: Comment[];
}