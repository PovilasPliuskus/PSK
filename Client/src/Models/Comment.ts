export interface Comment {
    id: string;
    subTaskId?: string | null;
    taskId: string;
    createdAt: string;
    updatedAt: string;
    writtenByUserId: string; // Sita paliekam kol dar naudojam userId vietoj userEmail
    writtenByUserEmail: string;
    edited: boolean;
    text: string;
}