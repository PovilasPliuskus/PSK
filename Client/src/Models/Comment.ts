import { BaseModel } from "./BaseModel";

export interface Comment extends BaseModel {
    subTaskId?: string | null;
    taskId: string;
    writtenByUserId: string; // Sita paliekam kol dar naudojam userId vietoj userEmail
    writtenByUserEmail: string;
    edited: boolean;
    text: string;
}