import { TaskDetailed } from "../../Models/TaskDetailed";
// Assuming Enums are still defined/imported if type checking is strict,
// but we will use 0 directly as requested.
// import { StatusEnum, EstimateEnum, TypeEnum, PriorityEnum } from "../../Models/TaskEnums";
import { SubTask } from "../../Models/SubTask";
import { Comment } from "../../Models/Comment";


const generateId = (): string => Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
const generateTimestamp = (): string => new Date().toISOString();

// Generate stable IDs for parent tasks to reference them consistently
const task1Id = generateId();
const task2Id = generateId();
const task3Id = generateId();

// IDs for specific subtasks we'll add comments to
const subtask1_1_Id = generateId();
const subtask1_2_Id = generateId();
const subtask2_1_Id = generateId();
// ID for the other subtask
const subtask2_2_Id = generateId();


export const dummyTasks: TaskDetailed[] = [
    {
        name: "Implement User Authentication",
        id: task1Id,
        createdAt: generateTimestamp(),
        updatedAt: generateTimestamp(),
        fkCreatedByUserId: "user-123",
        createdByUserEmail: "john.doe@example.com",
        workspaceId: "workspace-abc",
        assignedToUserId: "user-456",
        assignedToUserEmail: "jane.smith@example.com",
        dueDate: new Date("2025-04-22T17:00:00.000Z"),
        description: "Implement user authentication using JWT.",
        status: 0,
        estimate: 0,
        type: 0,
        priority: 0,
        SubTasks: [
            {
                id: subtask1_1_Id, // Use generated ID
                name: "Design Login API endpoint",
                createdAt: generateTimestamp(),
                updatedAt: generateTimestamp(),
                taskId: task1Id,
                createdByUserId: "user-123",
                createdByUserEmail: "john.doe@example.com",
                assignedToUserId: "user-456",
                assignedToUserEmail: "jane.smith@example.com",
                dueDate: new Date("2025-04-18T17:00:00.000Z"),
                description: "Define the request and response structure for the login endpoint.",
                status: 0,
                estimate: 0,
                type: 0,
                priority: 0,
                comments: [ // Comments for this subtask
                    {
                        id: generateId(),
                        taskId: task1Id, // Belongs to the parent task
                        subTaskId: subtask1_1_Id, // Belongs to this specific subtask
                        createdAt: generateTimestamp(),
                        updatedAt: generateTimestamp(),
                        writtenByUserId: "user-456", // Assigned user
                        writtenByUserEmail: "jane.smith@example.com",
                        edited: false,
                        text: "Initial API design proposal is pushed to the 'feature/auth-api' branch."
                    },
                    {
                        id: generateId(),
                        taskId: task1Id,
                        subTaskId: subtask1_1_Id,
                        createdAt: generateTimestamp(),
                        updatedAt: generateTimestamp(),
                        writtenByUserId: "user-123", // Creator user
                        writtenByUserEmail: "john.doe@example.com",
                        edited: false,
                        text: "Looks good, please add error response examples."
                    }
                ],
            },
            {
                id: subtask1_2_Id, // Use generated ID
                name: "Implement JWT generation",
                createdAt: generateTimestamp(),
                updatedAt: generateTimestamp(),
                taskId: task1Id,
                createdByUserId: "user-123",
                createdByUserEmail: "john.doe@example.com",
                assignedToUserId: "user-456",
                assignedToUserEmail: "jane.smith@example.com",
                dueDate: new Date("2025-04-20T17:00:00.000Z"),
                description: "Implement the logic to generate JWT tokens upon successful authentication.",
                status: 0,
                estimate: 0,
                type: 0,
                priority: 0,
                comments: [ // Comments for this subtask
                    {
                        id: generateId(),
                        taskId: task1Id,
                        subTaskId: subtask1_2_Id, // Belongs to this specific subtask
                        createdAt: generateTimestamp(),
                        updatedAt: generateTimestamp(),
                        writtenByUserId: "user-456", // Assigned user
                        writtenByUserEmail: "jane.smith@example.com",
                        edited: false,
                        text: "Which library should we use for JWT? `jsonwebtoken` or `jose`?"
                    }
                ],
            },
        ],
        Comments: [ // Task-level comments remain
            {
                id: generateId(),
                taskId: task1Id,
                subTaskId: null, // Still null for task-level comments
                createdAt: generateTimestamp(),
                updatedAt: generateTimestamp(),
                writtenByUserId: "user-789",
                writtenByUserEmail: "peter.jones@example.com",
                edited: false,
                text: "Let's ensure we handle edge cases for login failures.",
            },
            {
                id: generateId(),
                taskId: task1Id,
                subTaskId: null,
                createdAt: generateTimestamp(),
                updatedAt: generateTimestamp(),
                writtenByUserId: "user-456",
                writtenByUserEmail: "jane.smith@example.com",
                edited: false,
                text: "Working on the JWT implementation now.",
            },
        ],
    },
    {
        name: "Fix UI Bug on Homepage",
        id: task2Id,
        createdAt: generateTimestamp(),
        updatedAt: generateTimestamp(),
        fkCreatedByUserId: "user-001",
        createdByUserEmail: "alice.brown@example.com",
        workspaceId: "workspace-xyz",
        assignedToUserId: "user-222",
        assignedToUserEmail: "bob.white@example.com",
        dueDate: new Date("2025-04-19T17:00:00.000Z"),
        description: "The logo on the homepage is misaligned on mobile devices.",
        status: 0,
        estimate: 0,
        type: 0,
        priority: 0,
        SubTasks: [
            {
                id: subtask2_1_Id, // Use generated ID
                name: "Inspect CSS on mobile",
                createdAt: generateTimestamp(),
                updatedAt: generateTimestamp(),
                taskId: task2Id,
                createdByUserId: "user-001",
                createdByUserEmail: "alice.brown@example.com",
                assignedToUserId: "user-222",
                assignedToUserEmail: "bob.white@example.com",
                dueDate: new Date("2025-04-18T17:00:00.000Z"),
                description: "Use browser developer tools to inspect the CSS causing the misalignment.",
                status: 0,
                estimate: 0,
                type: 0,
                priority: 0,
                comments: [ // Comments for this subtask
                     {
                        id: generateId(),
                        taskId: task2Id,
                        subTaskId: subtask2_1_Id, // Belongs to this specific subtask
                        createdAt: generateTimestamp(),
                        updatedAt: generateTimestamp(),
                        writtenByUserId: "user-222", // Assigned user
                        writtenByUserEmail: "bob.white@example.com",
                        edited: false,
                        text: "Found the conflicting CSS rule in `layout.css`. It seems to be a flexbox issue."
                    }
                ],
            },
            {
                id: subtask2_2_Id, // Use generated ID
                name: "Apply CSS fix",
                createdAt: generateTimestamp(),
                updatedAt: generateTimestamp(),
                taskId: task2Id,
                createdByUserId: "user-001",
                createdByUserEmail: "alice.brown@example.com",
                assignedToUserId: "user-222",
                assignedToUserEmail: "bob.white@example.com",
                dueDate: new Date("2025-04-19T17:00:00.000Z"),
                description: "Implement the necessary CSS changes to fix the logo alignment.",
                status: 0,
                estimate: 0,
                type: 0,
                priority: 0,
                comments: [], // No comments added to this one yet
            },
        ],
        Comments: [ // Task-level comments remain
            {
                id: generateId(),
                taskId: task2Id,
                subTaskId: null,
                createdAt: generateTimestamp(),
                updatedAt: generateTimestamp(),
                writtenByUserId: "user-222",
                writtenByUserEmail: "bob.white@example.com",
                edited: false,
                text: "Investigating the CSS now.",
            },
        ],
    },
    { // Task 3 remains unchanged as it has no subtasks
        name: "Refactor Payment Service",
        id: task3Id,
        createdAt: generateTimestamp(),
        updatedAt: generateTimestamp(),
        fkCreatedByUserId: "user-333",
        createdByUserEmail: "charlie.green@example.com",
        workspaceId: "workspace-def",
        assignedToUserId: null,
        assignedToUserEmail: null,
        dueDate: new Date("2025-04-26T17:00:00.000Z"),
        description: "Refactor the payment service to improve performance and maintainability.",
        status: 0,
        estimate: 0,
        type: 0,
        priority: 0,
        SubTasks: [],
        Comments: [
            {
                id: generateId(),
                taskId: task3Id,
                subTaskId: null,
                createdAt: generateTimestamp(),
                updatedAt: generateTimestamp(),
                writtenByUserId: "user-333",
                writtenByUserEmail: "charlie.green@example.com",
                edited: false,
                text: "Planning the refactoring steps.",
            },
        ],
    },
];