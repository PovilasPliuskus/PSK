import { BaseModel } from "./BaseModel";
import { StatusEnum, EstimateEnum, TypeEnum, PriorityEnum } from "./TaskEnums";

export interface TaskSummary extends BaseModel {
  name: string;
  createdByUserEmail: string; 
  workspaceId: string;
  assignedToUserEmail?: string | null;
  dueDate?: Date | null;
  status: StatusEnum;
  estimate: EstimateEnum;
  type: TypeEnum;
  priority: PriorityEnum;
};