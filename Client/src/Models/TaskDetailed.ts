import { SubTask } from "./SubTask";
import { Comment } from "./Comment";
import { EstimateEnum, PriorityEnum, StatusEnum, TypeEnum } from "./TaskEnums";
import { BaseModel } from "./BaseModel";

export interface TaskDetailed extends BaseModel{
    name: string;
    createdByUserEmail: string; 
    workspaceId: string;
    assignedToUserEmail?: string | null;
    dueDate?: Date | null;
    description?: string,
    status: StatusEnum;
    estimate: EstimateEnum;
    type: TypeEnum;
    priority: PriorityEnum;
    SubTasks: SubTask[];
    Comments: Comment[];
    // TODO dar truksta Attachment, reikes nusprest kaip jis atrodys
}