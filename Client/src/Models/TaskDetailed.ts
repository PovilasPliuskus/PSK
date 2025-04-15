import { SubTask } from "./SubTask";
import { EstimateEnum, PriorityEnum, StatusEnum, TypeEnum } from "./TaskEnums";

export interface TaskDetailed {
    name: string;
    id: string;
    createdAt: string;
    updatedAt: string;
    fkCreatedByUserId: string; // Sita paliekam kol dar naudojam userId vietoj userEmail
    createdByUserEmail: string; 
    workspaceId: string;
    assignedToUserId?: string | null; // Sita paliekam kol dar naudojam userId vietoj userEmail
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