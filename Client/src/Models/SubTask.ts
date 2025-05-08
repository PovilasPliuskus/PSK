import { EstimateEnum, PriorityEnum, StatusEnum, TypeEnum } from "./TaskEnums";
import { Comment } from "./Comment";
import { BaseModel } from "./BaseModel";

export interface SubTask extends BaseModel{
    name: string;
    taskId: string;
    createdByUserId: string; // Sita paliekam kol dar naudojam userId vietoj userEmail
    createdByUserEmail: string;
    assignedToUserId?: string; // Sita paliekam kol dar naudojam userId vietoj userEmail
    assignedToUserEmail?: string;
    dueDate?: Date | null;
    description?: string;
    status: StatusEnum;
    estimate: EstimateEnum;
    type: TypeEnum;
    priority: PriorityEnum;
    comments: Comment[];
}