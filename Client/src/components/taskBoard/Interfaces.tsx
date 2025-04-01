export interface ITask {
    Id: number,
    CreatedAt: string,
    UpdatedAt: string,
    Name: string,
    CreatedByUserId: number,
    WorkspaceId: number,
    AssignedToUserId: number,
    DueDate: string,
    Description: string,
    Status: string,
    Estimate: string,
    Type: string,
    Priority: string
}

export interface BoardProp {
    boardType: string;
    onTaskDrop: (task: ITask, boardType: string) => void;
    taskList: ITask[];
}
