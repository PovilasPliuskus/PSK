import { EstimateEnum, PriorityEnum, StatusEnum, TypeEnum } from "../TaskEnums";

export type UpdateTaskBody = {
    id: string;
    workspaceId: string;
    name: string;
    dueDate?: Date | null;
    createdByUserEmail: string;
    assignedToUserEmail?: string | null;
    description?: string | null;
    status: StatusEnum;
    estimate: EstimateEnum;
    type: TypeEnum;
    priority: PriorityEnum;
    version: number;  
    force: boolean | null;
}