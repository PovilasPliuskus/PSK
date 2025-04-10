export type StatusEnum = 0 | 1 | 2 | 3;
export type EstimateEnum = 0 | 1 | 2;
export type TypeEnum = 0 | 1 | 2;
export type PriorityEnum = 0 | 1 | 2;

export type CardType = {
  name: string;
  id: string;
  fkCreatedByUserId: string; // Guid as string
  fkWorkspaceId: string; // Guid as string
  fkAssignedToUserId: string | null; // Guid as string or null
  dueDate: Date | null;
  description?: string | null;
  status: StatusEnum;
  estimate: EstimateEnum;
  type: TypeEnum;
  priority: PriorityEnum;
};