import { EstimateEnum, PriorityEnum, StatusEnum, TypeEnum } from "../TaskEnums";

export interface UpdateSubTaskBody {
    id: string;
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
    version: number;  
    force: boolean | null;
}