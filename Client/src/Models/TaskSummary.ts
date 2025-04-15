import { StatusEnum, EstimateEnum, TypeEnum, PriorityEnum } from "./TaskEnums";

export interface TaskSummary {
  name: string;
  id: string;
  fkCreatedByUserId: string; // Sita paliekam kol dar naudojam userId
  createdByUserEmail: string; 
  workspaceId: string;
  assignedToUserId: string | null; // Sita paliekam kol dar naudojam userId
  assignedToUserEmail: string | null;
  dueDate: Date | null;
  status: StatusEnum;
  estimate: EstimateEnum;
  type: TypeEnum;
  priority: PriorityEnum;
};