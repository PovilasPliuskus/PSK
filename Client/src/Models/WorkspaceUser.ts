import { BaseModel } from "./BaseModel";

export interface WorkspaceUser extends BaseModel {
    userEmail: string;
    isOwner: boolean;
}