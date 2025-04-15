export interface Comment {
    taskId: string,
    createdAt: string;
    updatedAt: string;
    subTaskId: string,
    writtenByUserId: string, // Sita paliekam kol dar naudojam userId vietoj userEmail
    writtenByUserEmail: string,
    edited: boolean,
    text: string
}