import { BaseModel } from "./BaseModel";
import { StatusEnum, EstimateEnum, TypeEnum, PriorityEnum } from "./TaskEnums";

export interface TaskSummary extends BaseModel {
  name: string;
  fkCreatedByUserId: string; // Sita paliekam kol dar naudojam userId vietoj userEmail
  createdByUserEmail: string; 
  workspaceId: string;
  assignedToUserId?: string | null; // Sita paliekam kol dar naudojam userId vietoj userEmail
  assignedToUserEmail?: string | null;
  dueDate?: Date | null;
  status: StatusEnum;
  estimate: EstimateEnum;
  type: TypeEnum;
  priority: PriorityEnum;
};