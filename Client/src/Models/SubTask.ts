import { EstimateEnum, PriorityEnum, StatusEnum, TypeEnum } from "./TaskEnums";

export interface SubTask {
    name: string,
    createdAt: string;
    updatedAt: string;
    taskId: string,
    createdByUserId: string, // Sita paliekam kol dar naudojam userId vietoj userEmail
    createdByUserEmail: string,
    assignedToUserId?: string, // Sita paliekam kol dar naudojam userId vietoj userEmail
    assignedToUserEmail?: string,
    dueDate?: Date | null;
    description?: string,
    status: StatusEnum;
    estimate: EstimateEnum;
    type: TypeEnum;
    priority: PriorityEnum;
}